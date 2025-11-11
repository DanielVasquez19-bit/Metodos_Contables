using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Agro_UES
{
    public partial class FormRecuperar: Form

    {
        // Variable para almacenar el id del usuario y su nombre
        private int idUsuario;
        private string nombreUsuario;
        

        public FormRecuperar()
        {
            InitializeComponent();
            // Al inicio se muestra solo el panel de envío y se oculta el de verificación
            panelEnvio.Visible = true;
            panelVerificacion.Visible = false;


        }
        private void FormRecuperar_Escalar(object sender, EventArgs e)
        {
            panelRecuperar.Left = (this.ClientSize.Width - panelRecuperar.Width) / 2;
            panelRecuperar.Top = (this.ClientSize.Height - panelRecuperar.Height) / 2;
        }

        /*Metodo pa encriptar la contrasena*/
        private string EncriptarPin(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashString;
            }
        }

        /* Logica del boton Enviar Código*/
        private void btnEnviarCodigo_Click(object sender, EventArgs e)
        {
            // Obtener el correo ingresado
            string correo = txtCorreo.Text.Trim();
            if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Ingrese un correo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Consulta pa buscar el usuario por correo en la tabla usuarios
                string consulta = "SELECT id_usuario, nombre FROM usuarios WHERE correo = @correo AND estado = 'activo'";
                using (MySqlConnection conectar = ConexionDB.Conexion())
                {
                    conectar.Open();
                    using (MySqlCommand comando = new MySqlCommand(consulta, conectar))
                    {
                        comando.Parameters.AddWithValue("@correo", correo);
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                idUsuario = Convert.ToInt32(lector["id_usuario"]);
                                nombreUsuario = lector["nombre"].ToString();

                                // Generar codigo de 6 digitos
                                Random rnd = new Random();
                                string codigo = rnd.Next(100000, 1000000).ToString();

                                lector.Close(); // Cerrar el lector pa continuar

                                // Insertar el codigo en la tabla recuperacion_password
                                string consultaInsert = "INSERT INTO recuperacion_password (usuario_id, codigo, fecha_solicitud, usado) " +
                                                        "VALUES (@usuario_id, @codigo, NOW(), 0)";
                                using (MySqlCommand comandoInsert = new MySqlCommand(consultaInsert, conectar))
                                {
                                    comandoInsert.Parameters.AddWithValue("@usuario_id", idUsuario);
                                    comandoInsert.Parameters.AddWithValue("@codigo", codigo);
                                    comandoInsert.ExecuteNonQuery();
                                }

                                // Enviar correo con el codigo de verificacion
                                EnviarCorreoCodigo(correo, codigo);
                                MessageBox.Show("El codigo fue enviado a tu correo", "Exito");

                                // Mostrar el panel de verificacion y ocultar el de envio
                                panelEnvio.Visible = false;
                                panelVerificacion.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("Correo no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción");
            }
        }

        /*Logica del boton Verificar y Cambiar Contraseña*/
        private void btnVerificar_Click(object sender, EventArgs e)
        {
            // Obtener el codigo ingresado y la nueva contraseña
            string codigoIngresado = txtCodigo.Text.Trim();
            string nuevaContrasena = txtNuevaContraseña.Text.Trim();

            if (string.IsNullOrEmpty(codigoIngresado) || string.IsNullOrEmpty(nuevaContrasena))
            {
                MessageBox.Show("Ingrese el codigo y la nueva contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (MySqlConnection conectar = ConexionDB.Conexion())
                {
                    conectar.Open();
                    // Consulta pa verificar el codigo en recuperacion_password
                    string consulta = "SELECT id FROM recuperacion_password " +
                                      "WHERE usuario_id = @idUsuario AND codigo = @codigo AND usado = 0 " +
                                      "ORDER BY fecha_solicitud DESC LIMIT 1";
                    using (MySqlCommand comando = new MySqlCommand(consulta, conectar))
                    {
                        comando.Parameters.AddWithValue("@idUsuario", idUsuario);
                        comando.Parameters.AddWithValue("@codigo", codigoIngresado);
                        object resultado = comando.ExecuteScalar();
                        if (resultado != null)
                        {
                            int idCodigoRecuperacion = Convert.ToInt32(resultado);

                            // Encriptar la nueva contrasena
                            string contraEncriptada = EncriptarPin(nuevaContrasena);

                            // Actualizar la contrasena en la tabla usuarios
                            string actualizarUsuario = "UPDATE usuarios SET contraseña_hash = @nuevaContra WHERE id_usuario = @idUsuario";
                            using (MySqlCommand comandoActualizar = new MySqlCommand(actualizarUsuario, conectar))
                            {
                                comandoActualizar.Parameters.AddWithValue("@nuevaContra", contraEncriptada);
                                comandoActualizar.Parameters.AddWithValue("@idUsuario", idUsuario);
                                comandoActualizar.ExecuteNonQuery();
                            }

                            // Marcar el registro de recuperacion como usado
                            string actualizarRecuperacion = "UPDATE recuperacion_password SET usado = 1 WHERE id = @idRecuperacion";
                            using (MySqlCommand comandoRec = new MySqlCommand(actualizarRecuperacion, conectar))
                            {
                                comandoRec.Parameters.AddWithValue("@idRecuperacion", idCodigoRecuperacion);
                                comandoRec.ExecuteNonQuery();
                            }

                            // Insertar en historial_acciones la accion de cambio de contrasena
                            string consultaHistorial = "INSERT INTO historial_acciones (usuario_id, nombre_usuario, accion) " +
                                                       "VALUES (@usuario_id, @nombre_usuario, @accion)";
                            using (MySqlCommand comandoHist = new MySqlCommand(consultaHistorial, conectar))
                            {
                                comandoHist.Parameters.AddWithValue("@usuario_id", idUsuario);
                                comandoHist.Parameters.AddWithValue("@nombre_usuario", nombreUsuario);
                                comandoHist.Parameters.AddWithValue("@accion", "Cambio de contraseña");
                                comandoHist.ExecuteNonQuery();
                            }

                            MessageBox.Show("Contraseña actualizada correctamente", "Exito");
                            // Abrir el formulario de login y cerrar este form
                            FormLogin frmLogin = new FormLogin();
                            frmLogin.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Codigo de verificacion incorrecto o ya usado", "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Excepción");
            }
        }

        /*Metodo pa enviar correo con el codigo*/
        private void EnviarCorreoCodigo(string destino, string codigo)
        {
            try
            {
                // Se usa el correo uesagro@gmail.com 
                MailMessage mensaje = new MailMessage("uesagro@gmail.com", destino);
                mensaje.Subject = "Recuperacion de contraseña Agro_UES";
                mensaje.Body = "Tu codigo de verificacion es: " + codigo;

                // Configurar SmtpClient para Gmail
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("uesagro@gmail.com", "wjjw wniq ocmi oxyx");
                smtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo: " + ex.Message, "Excepción");
            }
        }

        private void FormRecuperar_Load_1(object sender, EventArgs e)
        {
            this.Resize += FormRecuperar_Escalar;
            FormRecuperar_Escalar(null, null);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            FormLogin frmLogin = new FormLogin();
            frmLogin.Show();
            this.Close();
        }
    }
}
