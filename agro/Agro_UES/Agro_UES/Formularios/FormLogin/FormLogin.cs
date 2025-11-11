using Agro_UES.Formularios.FormCajero;
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
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;

        public FormLogin(int id, string nombre, string rol)
        {
            InitializeComponent();
            idUsuarioActual = id;
            nombreUsuarioActual = nombre;
            rolUsuarioActual = rol;
        }
        public FormLogin() : this(0, "", "")
        {
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

        //mantener centrado al mover la vnetana
        private void FormLogin_Escalar(object sender, EventArgs e)
        {
            panelLogin.Left = (this.ClientSize.Width - panelLogin.Width) / 2;
            panelLogin.Top = (this.ClientSize.Height - panelLogin.Height) / 2;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            // 1. Recoger y validar campos
            string identificador = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            if (string.IsNullOrWhiteSpace(identificador) ||
                string.IsNullOrWhiteSpace(contraseña))
            {
                MessageBox.Show("Debe completar usuario y contraseña.", "Datos incompletos");
                return;
            }

            // 2. Encriptar la contraseña ingresada
            string contraEncriptada = EncriptarPin(contraseña);

            try
            {
                using (var conectar = ConexionDB.Conexion())
                {
                    conectar.Open();

                    // 3. Traer datos del usuario (hash y estado)
                    string sql = @"
SELECT 
    u.id_usuario,
    u.nombre,
    r.nombre_rol,
    u.contraseña_hash,
    u.estado
FROM usuarios u
INNER JOIN roles r 
    ON u.rol_id = r.id_rol
WHERE u.correo = @id 
   OR u.nombre = @id;";

                    using (var cmd = new MySqlCommand(sql, conectar))
                    {
                        cmd.Parameters.AddWithValue("@id", identificador);

                        using (var lector = cmd.ExecuteReader())
                        {
                            if (!lector.Read())
                            {
                                MessageBox.Show("Usuario no encontrado.", "Login fallido");
                                return;
                            }

                            int idUsuario = Convert.ToInt32(lector["id_usuario"]);
                            string nombreUsuario = lector["nombre"].ToString();
                            string rol = lector["nombre_rol"].ToString();
                            string hashAlmacenado = lector["contraseña_hash"].ToString();
                            string estado = lector["estado"].ToString();

                            lector.Close();

                            if (!estado.Equals("activo", StringComparison.OrdinalIgnoreCase))
                            {
                                MessageBox.Show(
                                    "La cuenta está inactiva. Contacta al administrador.",
                                    "Acceso denegado");
                                return;
                            }

                            if (!hashAlmacenado.Equals(contraEncriptada, StringComparison.Ordinal))
                            {
                                MessageBox.Show("Contraseña incorrecta.", "Login fallido");
                                return;
                            }

                            string sqlAct = @"
                                UPDATE usuarios
                                   SET sesion = 'activa'
                                 WHERE id_usuario = @uid;
                            ";
                            using (var cmdAct = new MySqlCommand(sqlAct, conectar))
                            {
                                cmdAct.Parameters.AddWithValue("@uid", idUsuario);
                                cmdAct.ExecuteNonQuery();
                            }

                            // 4. Registrar en historial
                            string sqlHist = @"
                                INSERT INTO historial_acciones
                                    (usuario_id, nombre_usuario, accion)
                                VALUES
                                    (@uid,        @nom,            'Inicio de sesión en el sistema');";

                            using (var cmdHist = new MySqlCommand(sqlHist, conectar))
                            {
                                cmdHist.Parameters.AddWithValue("@uid", idUsuario);
                                cmdHist.Parameters.AddWithValue("@nom", nombreUsuario);
                                cmdHist.ExecuteNonQuery();
                            }
                            // 5. Redirigir según rol usando if/else
                            Form siguiente = null;

                            if (rol == "Cajero")
                                siguiente = new FormCajero0(idUsuario, nombreUsuario, rol);
                            else if (rol == "Gerente")
                                siguiente = new FormGerente(idUsuario, nombreUsuario, rol);
                            else if (rol == "Super Administrador")
                                siguiente = new FormSuperAdmin(idUsuario, nombreUsuario, rol);
                            else if (rol == "Encargado de Almacen")
                                siguiente = new FormAlmacen(idUsuario, nombreUsuario, rol);

                            if (siguiente != null)
                            {
                                MessageBox.Show(
                                    $"Bienvenido {nombreUsuario} ({rol})",
                                    "Login exitoso",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                                siguiente.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Rol no reconocido.", "Error");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Ocurrió un error al intentar iniciar sesión. Verifica tu conexión.",
                    "Excepción");
            }
        }


        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.Resize += FormLogin_Escalar;
            FormLogin_Escalar(null, null);
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        /*Logica del link "Olvidaste tu contraseña?"*/
        private void label4_Click(object sender, EventArgs e)
        {  
           
         // Abrir el form pa recuperar la contraseñaa
         FormRecuperar frmRecup = new FormRecuperar();
         frmRecup.Show();
            this.Hide();
       
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
