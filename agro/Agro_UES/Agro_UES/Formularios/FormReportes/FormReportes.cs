using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Agro_UES.Formularios.FormReportes
{
    public partial class FormReportes: Form
    {
        private int idUsuario;
        private string nombreUsuario;
        private string rutaDestino;



        public FormReportes(int idUsuario, string nombreUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
            rutaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Agro_UES", "Reportes");

            cmbTipoReporte.SelectedIndexChanged += cmbTipoReporte_SelectedIndexChanged;

            CargarTiposDeReporte();
            ConfigurarControles();
            CargarDatosReporte();
        }
        private void CargarTiposDeReporte()
        {
            cmbTipoReporte.Items.AddRange(new string[] {
                "Detalle de Ventas",
                "Usuarios",
                "Productos",
                "Aprobaciones de Almacen",
                "Historial de Acciones"

            });
            cmbTipoReporte.SelectedIndex = 0;
        }

        private void ConfigurarControles()
        {
            lblUsuarioActual.Text = nombreUsuario;
            lblFechaActual.Text = DateTime.Now.ToString("dd/MM/yyyy");
           

            if (!Directory.Exists(rutaDestino))
                Directory.CreateDirectory(rutaDestino);

            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;
        }

        private void cmbTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosReporte();
        }
        private void CargarDatosReporte()
        {
            string tipo = cmbTipoReporte.SelectedItem.ToString().Trim();
            string consulta = "";
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            dgvVistaReporte.DataSource = null;

            try
            {
                using (MySqlConnection conexion = ConexionDB.Conexion())
                {
                    conexion.Open();
                    MySqlCommand comando;

                    switch (tipo)
                    {
                        case "Detalle de Ventas":
                            consulta = @"SELECT d.id_detalle, v.id_venta, d.nombre_producto, 
                            d.cantidad, d.precio_unitario, d.subtotal, v.fecha_venta
                     FROM detalle_ventas d
                     JOIN ventas v ON d.venta_id = v.id_venta
                     WHERE v.fecha_venta BETWEEN @desde AND @hasta
                     ORDER BY v.fecha_venta DESC";
                            break;

                        case "Usuarios":
                            consulta = @"SELECT u.id_usuario, u.nombre, u.correo, r.nombre_rol, u.estado
                     FROM usuarios u
                     JOIN roles r ON r.id_rol = u.rol_id
                     ORDER BY u.id_usuario";
                            break;

                        case "Productos":
                            consulta = @"SELECT p.id_producto, p.nombre, c.nombre_categoria, 
                            p.descripcion, p.precio, p.stock, p.fecha_vencimiento
                     FROM productos p
                     JOIN categorias c ON c.id_categoria = p.categoria_id
                     ORDER BY p.nombre";
                            break;

                        case "Solicitudes de Devolucion":
                            consulta = @"SELECT s.id_solicitud, s.id_venta, p.nombre AS producto,
                            s.cantidad_devuelta, s.motivo, 
                            s.nombre_solicita, s.estado, s.fecha_solicita
                     FROM solicitudes_devoluciones s
                     JOIN productos p ON p.id_producto = s.id_producto
                     WHERE s.fecha_solicita BETWEEN @desde AND @hasta
                     ORDER BY s.fecha_solicita DESC";
                            break;

                        case "Aprobaciones de Almacen":
                            consulta = @"SELECT a.id_aprobacion, p.nombre AS producto, a.stock, a.precio, 
                            a.estado, a.nombre_solicita, a.fecha_solicita
                     FROM aprobaciones_almacen a
                     JOIN productos p ON p.id_producto = a.id_producto
                     WHERE a.fecha_solicita BETWEEN @desde AND @hasta
                     ORDER BY a.fecha_solicita DESC";
                            break;

                        case "Historial de Acciones":
                            consulta = @"SELECT h.id_historial, h.nombre_usuario, h.accion, h.fecha_hora
                     FROM historial_acciones h
                     WHERE h.fecha_hora BETWEEN @desde AND @hasta
                     ORDER BY h.fecha_hora DESC";
                            break;

                        default:
                            MessageBox.Show("Tipo de reporte no reconocido.");
                            return;
                    }

                    comando = new MySqlCommand(consulta, conexion);

                    // Parametros solo si el reporte usa fechas
                    if (consulta.Contains("@desde") && consulta.Contains("@hasta"))
                    {
                        comando.Parameters.AddWithValue("@desde", desde);
                        comando.Parameters.AddWithValue("@hasta", hasta);
                    }

                    MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvVistaReporte.DataSource = tabla;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }
        }



        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (dgvVistaReporte.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.");
                return;
            }

            string tipo = cmbTipoReporte.SelectedItem.ToString();
            string nombreArchivo = $"Reporte_{tipo.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            // Raiz del ejecutable + subcarpeta "Reportes"
            string carpetaRaiz = AppDomain.CurrentDomain.BaseDirectory;
            string rutaCarpeta = Path.Combine(carpetaRaiz, "Reportes");

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

            try
            {
                var fuenteTitulo = FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD);
                var fuenteInfo = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL);
                var fuenteTabla = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL);

                Document doc = new Document(PageSize.A4.Rotate(), 10f, 10f, 20f, 20f);
                PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
                doc.Open();

                Paragraph titulo = new Paragraph($"REPORTE: {tipo.ToUpper()}", fuenteTitulo)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(titulo);

                Paragraph info = new Paragraph(
                    $"Generado por: {nombreUsuario}     Fecha: {DateTime.Now:dd/MM/yyyy}\n\n", fuenteInfo);
                doc.Add(info);

                PdfPTable tablaPDF = new PdfPTable(dgvVistaReporte.Columns.Count)
                {
                    WidthPercentage = 100
                };

                foreach (DataGridViewColumn col in dgvVistaReporte.Columns)
                {
                    tablaPDF.AddCell(new Phrase(col.HeaderText, fuenteTabla));
                }

                foreach (DataGridViewRow row in dgvVistaReporte.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        tablaPDF.AddCell(new Phrase(cell.Value?.ToString() ?? "", fuenteTabla));
                    }
                }

                doc.Add(tablaPDF);
                doc.Close();

                RegistrarAccion($"Generó reporte: {tipo} ({dtpDesde.Value:dd/MM/yyyy} - {dtpHasta.Value:dd/MM/yyyy})");

                MessageBox.Show("PDF generado correctamente:\n" + rutaArchivo);

                // automaticamente el PDF generado
                if (File.Exists(rutaArchivo))
                    System.Diagnostics.Process.Start(rutaArchivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message);
            }




        }
        private void RegistrarAccion(string descripcion)
        {
            try
            {
                using (MySqlConnection conexion = ConexionDB.Conexion())
                {
                    conexion.Open();
                    string sql = @"INSERT INTO historial_acciones (usuario_id, nombre_usuario, accion) 
                                   VALUES (@id, @nombre, @accion)";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conexion))
                    {
                        cmd.Parameters.AddWithValue("@id", idUsuario);
                        cmd.Parameters.AddWithValue("@nombre", nombreUsuario);
                        cmd.Parameters.AddWithValue("@accion", descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {




            }
        }
    }
}

