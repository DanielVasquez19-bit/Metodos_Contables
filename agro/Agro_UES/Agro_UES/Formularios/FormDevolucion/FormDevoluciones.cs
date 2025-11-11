using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Agro_UES.Formularios.FormDevolucion
{
    public partial class FormDevoluciones: Form
    {
        private int idUsuario;
        private string nombreUsuario;
        private string rolUsuario;


        public FormDevoluciones(int idUsuario, string nombreUsuario, string rolUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
            this.rolUsuario = rolUsuario;

            cmbVentas.SelectedIndexChanged += cmbVentas_SelectedIndexChanged;


            CargarVentas();

        }

        private void CargarVentas()
        {
            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = "SELECT id_venta, fecha_venta FROM ventas ORDER BY fecha_venta DESC LIMIT 50";
                    using (var da = new MySqlDataAdapter(sql, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cmbVentas.DataSource = dt;
                        cmbVentas.DisplayMember = "id_venta";
                        cmbVentas.ValueMember = "id_venta";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ventas: " + ex.Message);
            }
        }

        private void cmbVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVentas.SelectedValue is int idVenta)
            {
                CargarProductosVenta(idVenta);
            }
        }

        private void CargarProductosVenta(int idVenta)
        {
            dgvDevoluciones.Rows.Clear();

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"
                        SELECT d.producto_id, d.nombre_producto, d.cantidad, d.precio_unitario
                          FROM detalle_ventas d
                         WHERE d.venta_id = @vid";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@vid", idVenta);
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                int idP = rdr.GetInt32("producto_id");
                                string nombre = rdr.GetString("nombre_producto");
                                int cantidadVendida = rdr.GetInt32("cantidad");
                                decimal precio = rdr.GetDecimal("precio_unitario");

                                dgvDevoluciones.Rows.Add(false, idP, nombre, cantidadVendida, 0, precio, "");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private void btnEnviarSolicitud_Click(object sender, EventArgs e)
        {
            string motivo = txtMotivo.Text.Trim();
            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Por favor ingrese un motivo de devolución.");
                return;
            }

            int idVenta = Convert.ToInt32(cmbVentas.SelectedValue);
            int solicitudesRegistradas = 0;

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();

                foreach (DataGridViewRow fila in dgvDevoluciones.Rows)
                {
                    bool seleccionado = Convert.ToBoolean(fila.Cells["Seleccionar"].Value);
                    int cantidad = Convert.ToInt32(fila.Cells["CantidadDevolver"].Value);

                    if (!seleccionado || cantidad <= 0)
                        continue;

                    int idProducto = Convert.ToInt32(fila.Cells["IDProducto"].Value);

                    string sql = @"INSERT INTO solicitudes_devoluciones 
                           (id_venta, id_producto, cantidad_devuelta, motivo,
                            usuario_solicita, nombre_solicita, fecha_solicita)
                           VALUES 
                           (@idv, @idp, @cant, @mot, @uid, @nom, NOW())";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@idv", idVenta);
                        cmd.Parameters.AddWithValue("@idp", idProducto);
                        cmd.Parameters.AddWithValue("@cant", cantidad);
                        cmd.Parameters.AddWithValue("@mot", motivo);
                        cmd.Parameters.AddWithValue("@uid", idUsuario);
                        cmd.Parameters.AddWithValue("@nom", nombreUsuario);
                        cmd.ExecuteNonQuery();
                        solicitudesRegistradas++;
                    }
                }
            }

            MessageBox.Show(solicitudesRegistradas > 0
                ? "Solicitud enviada correctamente."
                : "No se seleccionó ningún producto válido.");

            this.Close();



        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
