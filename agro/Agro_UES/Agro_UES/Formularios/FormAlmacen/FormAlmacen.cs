using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agro_UES.Formularios.FormAlmacen;
using Agro_UES.Formularios.FormLogin;
using MySql.Data.MySqlClient;

namespace Agro_UES
{
    public partial class FormAlmacen : Form
    {
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;

        public FormAlmacen(int id, string nombre, string rol)
        {
            InitializeComponent();
            idUsuarioActual = id;
            nombreUsuarioActual = nombre;
            rolUsuarioActual = rol;



            lblUsuario.Text = nombreUsuarioActual;
            lblRol.Text = rolUsuarioActual;
            btnRegresar.Visible = rolUsuarioActual == "Super Administrador";

            CargarAlertasPanelContenido();
            RegistrarAccion("Ingreso al modulo de almacen");
            


        }

        // Actualiza hora en tiempo real
        private void relojHora_Tick_1(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");

        }


        //esto e para cagar todas las alertas en los datagrid
        private void CargarAlertasPanelContenido()
        {
            CargarStockBajo();
            CargarVencimientos();
            CargarSolicitudes();
        }

        // Stock bajo: rojo (<5), amarillo (5–10), verde (>10)
        private void CargarStockBajo()
        {
            dgvStocBajo.Rows.Clear();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                string sql = @"SELECT p.nombre, p.stock, c.nombre_categoria
                       FROM productos p
                       JOIN categorias c ON p.categoria_id = c.id_categoria
                       WHERE p.estado IN ('Activo', 'Disponible')";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader.GetString("nombre");
                        int stock = reader.GetInt32("stock");
                        string categoria = reader.GetString("nombre_categoria");

                        int fila = dgvStocBajo.Rows.Add(nombre, stock, categoria);
                        var row = dgvStocBajo.Rows[fila];

                        if (stock < 5)
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        else if (stock <= 10)
                            row.DefaultCellStyle.BackColor = Color.Gold;
                        else
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                }
            }

        }

        // Vencimientos: rojo (vencido), dorado (próximo)
        private void CargarVencimientos()
        {
            dgvVecimientos.Rows.Clear();
            DateTime hoy = DateTime.Today;

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                string sql = @"SELECT nombre, fecha_vencimiento
                       FROM productos
                       WHERE fecha_vencimiento IS NOT NULL 
                         AND estado IN ('Activo', 'Disponible')";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader.GetString("nombre");
                        DateTime? fechaVencimiento = null;

                        int ordFecha = reader.GetOrdinal("fecha_vencimiento");
                        if (!reader.IsDBNull(ordFecha))
                            fechaVencimiento = reader.GetDateTime(ordFecha);

                        if (fechaVencimiento.HasValue)
                        {
                            int fila = dgvVecimientos.Rows.Add(nombre, fechaVencimiento.Value.ToShortDateString());
                            var row = dgvVecimientos.Rows[fila];

                            if (fechaVencimiento.Value < hoy)
                                row.DefaultCellStyle.BackColor = Color.Tomato;
                            else if (fechaVencimiento.Value <= hoy.AddDays(10))
                                row.DefaultCellStyle.BackColor = Color.Gold;
                        }
                    }
                }
            }

        }
        //  Solicitudes: amarillo (pendiente), azul (aprobado)
        private void CargarSolicitudes()
        {
            dgvSolicitudes.Rows.Clear();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                // Traigo sólo las columnas que existen en aprobaciones_almacen
                string sql = @"
SELECT 
    id_aprobacion,
    id_producto,
    descripcion,
    estado,
    fecha_solicita
FROM aprobaciones_almacen
ORDER BY fecha_solicita DESC;";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Leer campos
                        int id = reader.GetInt32("id_aprobacion");
                        int idProducto = reader.GetInt32("id_producto");
                        string desc = reader.GetString("descripcion");
                        string estado = reader.GetString("estado");

                        // Campo datetime que puede venir null
                        DateTime? fecha = null;
                        int idxFecha = reader.GetOrdinal("fecha_solicita");
                        if (!reader.IsDBNull(idxFecha))
                            fecha = reader.GetDateTime(idxFecha);

                        // Agrego la fila al grid (5 columnas)
                        int fila = dgvSolicitudes.Rows.Add(
                            id,
                            idProducto,
                            desc,
                            estado,
                            fecha?.ToString("g") ?? "—"
                        );

                        // Coloreo según el estado
                        var row = dgvSolicitudes.Rows[fila];
                        switch (estado.ToLower())
                        {
                            case "pendiente":
                                row.DefaultCellStyle.BackColor = Color.Khaki;
                                break;
                            case "aprobada":
                                row.DefaultCellStyle.BackColor = Color.LightBlue;
                                break;
                            case "rechazada":
                                row.DefaultCellStyle.BackColor = Color.LightGray;
                                row.DefaultCellStyle.ForeColor = Color.DimGray;
                                break;
                        }
                    }
                }
            }
        }
        private void RegistrarAccion(string descripcion)
        {
            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"INSERT INTO historial_acciones 
                                   (usuario_id, nombre_usuario, rol_usuario, accion, fecha_hora)
                                   VALUES (@id, @nombre, @rol, @accion, NOW())";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idUsuarioActual);
                        cmd.Parameters.AddWithValue("@nombre", nombreUsuarioActual);
                        cmd.Parameters.AddWithValue("@rol", rolUsuarioActual);
                        cmd.Parameters.AddWithValue("@accion", descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Silenciar error de historial (opcional)
            }
              }



        private void btnregistro_Click(object sender, EventArgs e)
        {
            var frm = new Registrarproducto(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual);
            frm.ShowDialog();
        }

        private void btnactualizarinv_Click(object sender, EventArgs e)
        {
            var frm = new ActualizarProductos(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual);
            frm.ShowDialog();


        }

        private void btncategorias_Click(object sender, EventArgs e)
        {
            var ventana = new GestionCategorias(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual);
            ventana.Show();


        }

        private void btnalertas_Click(object sender, EventArgs e)
        {
            
           
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FormAlmacen_FormClosing(object sender, FormClosingEventArgs e)
        {
            SesionHelper.MarcarSesionInactiva(idUsuarioActual);
        }
    }
}
