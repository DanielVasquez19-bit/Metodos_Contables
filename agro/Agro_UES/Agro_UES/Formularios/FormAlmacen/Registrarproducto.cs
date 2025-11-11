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
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace Agro_UES.Formularios.FormAlmacen
{
    public partial class Registrarproducto : Form
    {
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;
        private string rutaImagenSeleccionada = null;
        private Timer reloj;



        public Registrarproducto(int idUsuario, string nombreUsuario, string rolUsuario)
        {
            InitializeComponent();
            idUsuarioActual = idUsuario;
            nombreUsuarioActual = nombreUsuario;
            rolUsuarioActual = rolUsuario;

            CargarCategorias();

            //Panel superior
            lblNombreUsuario.Text = nombreUsuarioActual;
            lblRolUsuario.Text = rolUsuarioActual;

            reloj = new Timer();
            reloj.Interval = 1000;
            reloj.Tick += (s, e) => { lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt"); };
            reloj.Start();

            dtpVencimiento.MinDate = DateTime.Today;

        }

        // Registra  accion en historial_acciones
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

        //Representa una categoria como item del ComboBox
        public class ComboBoxItem
        {
            public string Nombre { get; set; }
            public int ID { get; set; }

            public ComboBoxItem(string nombre, int id)
            {
                Nombre = nombre;
                ID = id;
            }

            public override string ToString()
            {
                return Nombre; // Muestra el nombre en el ComboBox
            }
        }



        // Carga categorias activas desde la base de datos
        private void CargarCategorias()
        {
            cmbcategorias.Items.Clear();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                string sql = "SELECT id_categoria, nombre_categoria FROM categorias WHERE estado = 'Activa'";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        string nombre = rdr.GetString("nombre_categoria");
                        int id = rdr.GetInt32("id_categoria");
                        cmbcategorias.Items.Add(new ComboBoxItem(nombre, id));
                    }
                }
            }

            if (cmbcategorias.Items.Count > 0)
                cmbcategorias.SelectedIndex = 0;
            else
                MessageBox.Show("No hay categorias activas. Registre una desde el formulario de categorias.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Valida ingreso de precios: numeros y decimales(máx 2)

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (char.IsControl(e.KeyChar)) return;

            if (e.KeyChar == '.' && !txt.Text.Contains('.')) return;

            if (!char.IsDigit(e.KeyChar)) { e.Handled = true; return; }

            int pos = txt.SelectionStart;
            string textoFinal = txt.Text.Insert(pos, e.KeyChar.ToString());

            if (textoFinal.Contains('.') && textoFinal.Split('.')[1].Length > 2)
                e.Handled = true;

        }


        //Valida ingreso de stock: solo enteros positivos

        private void txtstock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


        //registro o solicitud

        private void btnregistro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtproducto.Text) ||
       string.IsNullOrWhiteSpace(txtdescripcion.Text) ||
       cmbcategorias.SelectedItem == null ||
       string.IsNullOrWhiteSpace(txtprecio.Text) ||
       string.IsNullOrWhiteSpace(txtstock.Text))
            {
                MessageBox.Show("Complete todos los campos requeridos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Extraer datos
            string nombre = txtproducto.Text.Trim();
            string descripcion = txtdescripcion.Text.Trim();
            var itemSeleccionado = cmbcategorias.SelectedItem as ComboBoxItem;
            int categoriaId = itemSeleccionado.ID;
            decimal precio = decimal.Parse(txtprecio.Text);
            int stock = int.Parse(txtstock.Text);
            DateTime fechaVencimiento = dtpVencimiento.Value;

            // Procesar imagen
            string rutaImagenRelativa = null;
            if (!string.IsNullOrWhiteSpace(rutaImagenSeleccionada))
            {
                string nombreArchivo = Path.GetFileName(rutaImagenSeleccionada);
                string carpetaDestino = Path.Combine(Application.StartupPath, "imagenes");
                Directory.CreateDirectory(carpetaDestino);
                string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);
                File.Copy(rutaImagenSeleccionada, rutaDestino, true);
                rutaImagenRelativa = Path.Combine("imagenes", nombreArchivo);
            }

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();

                    string sql = @"INSERT INTO productos 
                            (nombre, descripcion, categoria_id, precio, stock, fecha_vencimiento, 
                             ruta_imagen, estado)
                           VALUES 
                            (@nombre, @desc, @cat, @precio, @stock, @venc, @img, 'Disponible')";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@desc", descripcion);
                        cmd.Parameters.AddWithValue("@cat", categoriaId);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@venc", fechaVencimiento);
                        cmd.Parameters.AddWithValue("@img", rutaImagenRelativa ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }

                    RegistrarAccion("Registró producto: " + nombre, conn);
                    MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //Boton para seleccionar una imagen del sistema

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialogo.ShowDialog() == DialogResult.OK)
                {
                    rutaImagenSeleccionada = dialogo.FileName;
                    pictureBoxProducto.Image = Image.FromFile(rutaImagenSeleccionada);
                }
            }


        }
    }
}