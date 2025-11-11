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
using Agro_UES.FormContraseñaConfirmar;
using MySql.Data.MySqlClient;

namespace Agro_UES.Formularios.FormEditarUsuario
{
    public partial class FormEditarUsuario: Form
    {
        // Variables del operador y del usuario a editar
        private int idOperador;
        private string nombreOperador;
        private int idUsuarioEditar;

        // Constructor que recibe el id del operador, su nombre y el id del usuario a editar*/
        public FormEditarUsuario(int idOperador, string nombreOperador, int idUsuarioEditar)
        {
            InitializeComponent();
            this.idOperador = idOperador;
            this.nombreOperador = nombreOperador;
            this.idUsuarioEditar = idUsuarioEditar;
            CargarRoles();
            CargarDatosUsuario();


        }
        // Método para cargar los roles en el combo de roles*/
        private void CargarRoles()
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
        /* Método para cargar los datos del usuario a editar en los campos del formulario */

        private void CargarDatosUsuario()
        {
            MySqlConnection conexion = ConexionDB.Conexion();
            conexion.Open();
            string consulta = "SELECT nombre, correo, rol_id FROM usuarios WHERE id_usuario = @id";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@id", idUsuarioEditar);
            MySqlDataReader lector = comando.ExecuteReader();
            if (lector.Read())
            {
                txtNombre.Text = lector["nombre"].ToString();
                txtCorreo.Text = lector["correo"].ToString();
                cmbRol.SelectedValue = Convert.ToInt32(lector["rol_id"]);
            }
            lector.Close();
            conexion.Close();
        }
        // Método para encriptar la contraseña del operador usando */

        private string EncriptarContraseña(string textoPlano)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(textoPlano);
                byte[] hash = sha.ComputeHash(bytes);
                return string.Concat(hash.Select(b => b.ToString("x2")));
            }
        }
        // Método para verificar la contraseña del operador*/
        private bool VerificarContraseñaOperador(string contraseñaPlano)
        {
            string contraIngresado = EncriptarContraseña(contraseñaPlano);
            string contraGuardado = "";

            MySqlConnection conexion = ConexionDB.Conexion();
            conexion.Open();
            string consulta = "SELECT contraseña_hash FROM usuarios WHERE id_usuario = @id";
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@id", idOperador);
            contraGuardado = comando.ExecuteScalar()?.ToString() ?? "";
            conexion.Close();

            return contraIngresado == contraGuardado;
        }
        // Método para registrar una acción en el historial de acciones*/
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
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo registrar la acción: " + ex.Message);
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Llena todos los campos");
                return;
            }

            // Mostrar el formulario de confirmación de contraseña
            FormConfirmarContraseña confirmForm = new FormConfirmarContraseña();
            if (confirmForm.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Operacion cancelada");
                return;
            }
            string contraseña = confirmForm.ContraseñaIngresada;

            // Verificar si la contraseña está vacía o es nula
            if (string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Operacion cancelada");
                return;
            }

            if (!VerificarContraseñaOperador(contraseña))
            {
                MessageBox.Show("Contraseña incorrecta. No se guardaron los cambios.");
                return;
            }

            int rolId = Convert.ToInt32(cmbRol.SelectedValue);
            // ActualizarUsuario(nombre, correo, rolId);
            try
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                string consulta = "UPDATE usuarios SET nombre = @nombre, correo = @correo, rol_id = @rol WHERE id_usuario = @id";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@correo", correo);
                comando.Parameters.AddWithValue("@rol", rolId);
                comando.Parameters.AddWithValue("@id", idUsuarioEditar);
                comando.ExecuteNonQuery();
                conexion.Close();

                RegistrarAccion("Edito al usuario: " + nombre);
                MessageBox.Show("Usuario actualizado con exito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar usuario: " + ex.Message);
            }




        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();


        }
        private void FormEditarUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Se cierra solo este formulario
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
