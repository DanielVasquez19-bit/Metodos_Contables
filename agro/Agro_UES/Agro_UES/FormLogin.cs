using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agro_UES
{
    public partial class FormLogin: Form
    {


        public FormLogin()
        {
            InitializeComponent();
        }

        private bool contraseñaVisible = false;

        private string textoCaptcha;
        private void GenerarNuevoCaptcha()
        {
            textoCaptcha = CaptchaGenerator.GenerarTextoCaptcha();
            picCaptcha.Image = CaptchaGenerator.GenerarImagenCaptcha(textoCaptcha);
        }

        private string EncriptarPin(string pin)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pin);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static class UsuarioSesion
        {
            public static int IdUsuarioActual { get; set; }
        }

        private void ActualizarEstadoSesion(int idUsuario, bool estado)
        {
            string consulta = "UPDATE usuarios SET sesion_activa = @estado WHERE id_usuario = @idUsuario";

            using (MySqlConnection conexion = new ConexionDB().Conectar())
            {
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@estado", estado);
                    comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                    comando.ExecuteNonQuery();
                }
            }
        }

        private bool SesionActiva(MySqlConnection conexion, string nombreUsuario)
        {
            string consulta = "SELECT sesion_activa FROM usuarios WHERE nombre = @nombre";

            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombreUsuario);
                object resultado = comando.ExecuteScalar();

                return resultado != null && Convert.ToBoolean(resultado);
            }
        }


        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtCaptcha.Text.Trim() != textoCaptcha)
            {
                MessageBox.Show("CAPTCHA incorrecto. Intenta nuevamente.");
                GenerarNuevoCaptcha();
                txtCaptcha.Clear();
                return;
            }

            string user = txtUsuario.Text;
            string pinIngresado = txtContraseña.Text;

            ConexionDB conexionDB = new ConexionDB();
            MySqlConnection conexion = conexionDB.Conectar();

            if (conexion == null)
            {
                MessageBox.Show("Error: No se pudo conectar a la base de datos.");
                return;
            }

            try
            {
                if (SesionActiva(conexion, user))
                {
                    MessageBox.Show("⚠ Ya tienes una sesión activa en otro dispositivo.");
                    return;
                }

                string consulta = "SELECT id_usuario, contraseña FROM usuarios WHERE nombre = @nombres";
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@nombres", user);

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            MessageBox.Show("Usuario no encontrado.");
                            return;
                        }

                        int idUsuario = reader.GetInt32(0);
                        string hashAlmacenado = reader.GetString(1);
                        string hashIngresado = EncriptarPin(pinIngresado);

                        if (hashIngresado == hashAlmacenado)
                        {
                            MessageBox.Show("Bienvenido");
                            UsuarioSesion.IdUsuarioActual = idUsuario;
                            ActualizarEstadoSesion(idUsuario, true);

                            this.Hide();
                            Menú frm = new Menú();
                            frm.FormClosed += (s, args) => this.Close();
                            frm.Show();
                        }
                        else
                        {
                            MessageBox.Show("PIN incorrecto.");
                            txtContraseña.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            finally
            {
                conexionDB.CerrarConexion(conexion);
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            GenerarNuevoCaptcha();
            //this.WindowState = FormWindowState.Maximized;
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnVerClave_Click(object sender, EventArgs e)
        {
            if (contraseñaVisible)
            {
                // Ocultar contraseña con símbolo personalizado
                txtContraseña.PasswordChar = '●'; // o el que prefieras, como '•', '*', etc.
                contraseñaVisible = false;
                btnVerClave.Text = "👁️"; // Opcional: cambia icono
            }
            else
            {
                // Mostrar contraseña (sin caracteres de ocultamiento)
                txtContraseña.PasswordChar = '\0'; // Esto muestra el texto real
                contraseñaVisible = true;
                btnVerClave.Text = "🙈"; // Opcional: cambia icono
            }
        }
    }
}
