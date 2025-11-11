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
using MySql.Data.MySqlClient;

namespace Agro_UES.Formularios.FormCrearUsuario
{
    public partial class FormNuevoUsuario: Form
    {
        // Variables para el usuario que opera
        private int idOperador;
        private string nombreOperador;

        public FormNuevoUsuario(int idOperador, string nombreOperador)
        {
            InitializeComponent();
            this.idOperador = idOperador;
            this.nombreOperador = nombreOperador;
            CargarRoles();

        }
        // Metodo para cargar los roles en el combo de roles
        private void CargarRoles()
        {
            try
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                string consulta = "SELECT id_rol, nombre_rol FROM roles";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                cmbRol.DataSource = tabla;
                cmbRol.DisplayMember = "nombre_rol";
                cmbRol.ValueMember = "id_rol";
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cargar roles: " + error.Message);
            }
        }

        // Metodo para encriptar la contraseña
        private string EncriptarContraseña(string contrasena)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        // Metodo para registrar una accion en historial_acciones
        private void RegistrarAccion(string descripcion)
        {
            try
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                string consulta = "INSERT INTO historial_acciones (usuario_id, nombre_usuario, accion) VALUES (@id, @nombre, @accion)";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@id", idOperador);
                comando.Parameters.AddWithValue("@nombre", nombreOperador);
                comando.Parameters.AddWithValue("@accion", descripcion);
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al registrar accion: " + error.Message);
            }
        }

        // Evento click del boton Guardar para crear el usuario
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, llenar todos los campos");
                return;
            }

            int rolId = Convert.ToInt32(cmbRol.SelectedValue);
            string contraseñaEncriptada = EncriptarContraseña(contraseña);

            try
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                // Se inserta el usuario con estado 'activo'
                string consulta = "INSERT INTO usuarios (nombre, correo, contraseña_hash, rol_id, estado, fecha_registro) VALUES (@nombre, @correo, @contraseña, @rol, 'activo', NOW())";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@correo", correo);
                comando.Parameters.AddWithValue("@contraseña", contraseñaEncriptada);
                comando.Parameters.AddWithValue("@rol", rolId);
                comando.ExecuteNonQuery();
                conexion.Close();

                // Se registra la accion en historial
                RegistrarAccion("Registro nuevo usuario: " + nombre);
                MessageBox.Show("Usuario creado exitosamente");
                // Limpiar campos después del guardado
                txtNombre.Clear();
                txtCorreo.Clear();
                txtContraseña.Clear();
                cmbRol.SelectedIndex = 0;
                txtNombre.Focus();

            }
            catch (Exception error)
            {
                MessageBox.Show("Error al crear usuario: " + error.Message);
            }

        }

     
       

        // Al cerrar este form
        private void FormNuevoUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
  
        }
        // Evento click del boton Regresar - cierra este form
        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
