using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Agro_UES.Formularios.FormAlmacen
{
    public partial class ActualizarProductos: Form
    {
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;
        private int idProductoSeleccionado = -1;
        private string imagenActual = null;
        private string nuevaRutaImagen = null;
        private Timer reloj;

        public ActualizarProductos(int idUsuario, string nombreUsuario, string rolUsuario)
        {
            InitializeComponent();
            idUsuarioActual = idUsuario;
            nombreUsuarioActual = nombreUsuario;
            rolUsuarioActual = rolUsuario;

            dtpVencimiento.MinDate = DateTime.Today;

            //Panel superior
            lblNombreUsuario.Text = nombreUsuarioActual;
            lblRolUsuario.Text = rolUsuarioActual;

            reloj = new Timer();
            reloj.Interval = 1000;
            reloj.Tick += (s, e) => { lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt"); };
            reloj.Start();

        }

        private void ActualizarProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        //registra la accion del usuario
        private void RegistrarAccion(string accion, MySqlConnection conn)
        {
            string sql = @"INSERT INTO historial_acciones 
                           (usuario_id, nombre_usuario, accion, fecha_hora)
                           VALUES (@uid, @nombre, @accion, NOW())";
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@uid", idUsuarioActual);
                cmd.Parameters.AddWithValue("@nombre", nombreUsuarioActual);
                cmd.Parameters.AddWithValue("@accion", accion);
                cmd.ExecuteNonQuery();
            }
        }


        // Carga los productos activos en el DataGridView
        private void CargarProductos()
        {
            dgvProductos.Rows.Clear();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                string sql = @"SELECT id_producto, nombre, descripcion, precio, stock, fecha_vencimiento, ruta_imagen 
               FROM productos 
               WHERE estado IN ('Activo', 'Disponible')";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id_producto");
                        string nombre = reader.GetString("nombre");
                        string descripcion = reader.GetString("descripcion");
                        decimal precio = reader.GetDecimal("precio");
                        int stock = reader.GetInt32("stock");

                        DateTime? fechaVenc = null;
                        int ordFecha = reader.GetOrdinal("fecha_vencimiento");
                        if (!reader.IsDBNull(ordFecha))
                            fechaVenc = reader.GetDateTime(ordFecha);

                        string rutaImg = reader.IsDBNull(reader.GetOrdinal("ruta_imagen")) ? null : reader.GetString("ruta_imagen");

                        dgvProductos.Rows.Add(id, nombre, descripcion, precio, stock, fechaVenc?.ToString("yyyy-MM-dd") ?? "", rutaImg);
                    }
                }
            }
        }

        // Maneja el evento de clic en el boton y el product 
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }





        

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == -1)
            {
                MessageBox.Show("Selecciona un producto primero.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text) ||
                (rolUsuarioActual == "Super Administrador" && string.IsNullOrWhiteSpace(txtNombre.Text)))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            // Solo se toma el nombre si es Super Administrador
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            decimal precio = decimal.Parse(txtPrecio.Text);
            int stock = int.Parse(txtStock.Text);
            DateTime vencimiento = dtpVencimiento.Value;

            string rutaRelativa = imagenActual;
            if (!string.IsNullOrWhiteSpace(nuevaRutaImagen))
            {
                string nombreArchivo = Path.GetFileName(nuevaRutaImagen);
                string carpetaDestino = Path.Combine(Application.StartupPath, "imagenes");
                Directory.CreateDirectory(carpetaDestino);
                string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);
                File.Copy(nuevaRutaImagen, rutaDestino, true);
                rutaRelativa = Path.Combine("imagenes", nombreArchivo);
            }

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();

                    if (rolUsuarioActual == "Super Administrador")
                    {
                        string sql = @"UPDATE productos SET
                                nombre = @nombre,
                                descripcion = @descripcion,
                                precio = @precio,
                                stock = @stock,
                                fecha_vencimiento = @vencimiento,
                                ruta_imagen = @img
                               WHERE id_producto = @id";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", idProductoSeleccionado);
                            cmd.Parameters.AddWithValue("@nombre", nombre);
                            cmd.Parameters.AddWithValue("@descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@precio", precio);
                            cmd.Parameters.AddWithValue("@stock", stock);
                            cmd.Parameters.AddWithValue("@vencimiento", vencimiento);
                            cmd.Parameters.AddWithValue("@img", rutaRelativa ?? (object)DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }

                        RegistrarAccion($"Actualizó producto ID {idProductoSeleccionado} directamente", conn);
                        MessageBox.Show("Producto actualizado correctamente.");
                    }
                    else
                    {
                        string sql = @"INSERT INTO aprobaciones_almacen 
                            (id_producto, descripcion, precio, stock, fecha_vencimiento, 
                             usuario_solicita, nombre_solicita, ruta_imagen)
                            VALUES 
                            (@id, @descripcion, @precio, @stock, @venc, @uid, @uname, @img)";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", idProductoSeleccionado);
                            cmd.Parameters.AddWithValue("@descripcion", descripcion);
                            cmd.Parameters.AddWithValue("@precio", precio);
                            cmd.Parameters.AddWithValue("@stock", stock);
                            cmd.Parameters.AddWithValue("@venc", vencimiento);
                            cmd.Parameters.AddWithValue("@uid", idUsuarioActual);
                            cmd.Parameters.AddWithValue("@uname", nombreUsuarioActual);
                            cmd.Parameters.AddWithValue("@img", rutaRelativa ?? (object)DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }

                        RegistrarAccion($"Solicitó modificación del producto ID {idProductoSeleccionado}", conn);
                        MessageBox.Show("Solicitud enviada al gerente para revisión.");
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }




        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    nuevaRutaImagen = dialogo.FileName;
                    pictureBoxProducto.Image = Image.FromFile(nuevaRutaImagen);
                }
            }


        }

        private void dgvProductos_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProductos.Rows.Count)
            {
                var row = dgvProductos.Rows[e.RowIndex];
                idProductoSeleccionado = Convert.ToInt32(row.Cells[0].Value);
                txtNombre.Text = row.Cells[1].Value.ToString();
                txtDescripcion.Text = row.Cells[2].Value.ToString();
                txtPrecio.Text = row.Cells[3].Value.ToString();
                txtStock.Text = row.Cells[4].Value.ToString();
                dtpVencimiento.Value = DateTime.TryParse(row.Cells[5].Value.ToString(), out DateTime fecha)
                                        ? fecha
                                        : DateTime.Today;

                imagenActual = row.Cells[6].Value?.ToString();
                string rutaCompleta = Path.Combine(Application.StartupPath, imagenActual ?? "");
                if (File.Exists(rutaCompleta))
                    pictureBoxProducto.Image = Image.FromFile(rutaCompleta);
                else
                    pictureBoxProducto.Image = null;

                nuevaRutaImagen = null; // Reseteamos selección de nueva imagen
            }

        }
    }
}
