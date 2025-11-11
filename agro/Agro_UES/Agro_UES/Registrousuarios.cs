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

namespace Agro_UES
{
    public partial class Registrousuarios: Form
    {
        public Registrousuarios()
        {
            InitializeComponent();
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtnombre.Text.Trim();
            string correo = txtcorreo.Text.Trim();
            string contrasena = txtcontraseña.Text; 

            // Obtener el rol seleccionado del ComboBox
            if (cmbrol.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un rol.");
                return;
            }

            int rolId;
            if (!int.TryParse(cmbrol.SelectedItem.ToString(), out rolId))
            {
                MessageBox.Show("Rol inválido.");
                return;
            }

            DateTime fechaRegistro = DateTime.Now;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Completa todos los campos.");
                return;
            }

            using (var conexion = new ConexionDB().Conectar())
            {
                if (conexion == null)
                {
                    MessageBox.Show("No se pudo conectar a la base de datos.");
                    return;
                }

                string query = "INSERT INTO usuarios (nombre, correo, contraseña, rol_id, fecha_registro) VALUES (@nombre, @correo, @contrasena, @rol_id, @fecha)";
                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@rol_id", rolId);
                    cmd.Parameters.AddWithValue("@fecha", fechaRegistro);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Usuario registrado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar usuario: " + ex.Message);
                    }
                }
            }
        }

        private void cmbrol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtcorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcontraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Registrousuarios_Load(object sender, EventArgs e)
        {
            SesionUsuario.NotificarFormularioAbierto();
        }

        private void Registrousuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            SesionUsuario.NotificarFormularioCerrado();
        }
    }
}
