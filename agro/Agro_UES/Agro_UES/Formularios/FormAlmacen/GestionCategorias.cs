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

namespace Agro_UES.Formularios.FormAlmacen
{
    
    public partial class GestionCategorias: Form
    {
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;

        public GestionCategorias(int idUsuario, string nombreUsuario, string rolUsuario)
        {
            InitializeComponent();
            idUsuarioActual = idUsuario;
            nombreUsuarioActual = nombreUsuario;
            rolUsuarioActual = rolUsuario;

        }



        private void RegistrarAccion(string descripcion)
        {
            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"INSERT INTO historial_acciones  
                           (usuario_id, nombre_usuario, accion, fecha_hora)
                           VALUES (@id, @nombre, @accion, NOW())";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idUsuarioActual);
                        cmd.Parameters.AddWithValue("@nombre", $"{nombreUsuarioActual} ({rolUsuarioActual})");
                        cmd.Parameters.AddWithValue("@accion", descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            
            }
        }

        private void CargarCategorias()
        {
            dgvCategorias.Rows.Clear();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                string sql = @"SELECT id_categoria, nombre_categoria, estado FROM categorias ORDER BY nombre_categoria";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32("id_categoria");
                        string nombre = rdr.GetString("nombre_categoria");
                        string estado = rdr.GetString("estado");

                        int idx = dgvCategorias.Rows.Add();
                        var fila = dgvCategorias.Rows[idx];
                        fila.Cells["colID"].Value = id;
                        fila.Cells["colNombre"].Value = nombre;
                        fila.Cells["colEstado"].Value = estado;
                    }
                }
            }


        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnsolicitar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre valido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"INSERT INTO categorias (nombre_categoria, estado)
                                   VALUES (@nombre, 'Activa')";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.ExecuteNonQuery();
                    }
                }

                RegistrarAccion($"Agrego categoria: {nombre}");
                MessageBox.Show("Categoría agregada correctamente.", "exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombre.Clear();
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void GestionCategorias_Load_1(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtcategoria_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una categoria primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvCategorias.SelectedRows[0].Cells["colID"].Value);
            string nuevoNombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre))
            {
                MessageBox.Show("El nombre no puede estar vacio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"UPDATE categorias SET nombre_categoria = @nombre WHERE id_categoria = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nuevoNombre);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                RegistrarAccion($"Modifico categoria ID {id} → {nuevoNombre}");
                MessageBox.Show("Categoria modificada.", "exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombre.Clear();
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una categoría.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvCategorias.SelectedRows[0].Cells["colID"].Value);
            string estado = dgvCategorias.SelectedRows[0].Cells["colEstado"].Value.ToString();
            string nuevoEstado = estado == "Activa" ? "Inactiva" : "Activa";

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"UPDATE categorias SET estado = @estado WHERE id_categoria = @id";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                RegistrarAccion($"{(nuevoEstado == "Activa" ? "Activo" : "Inactivo")} categoría ID {id}");
                MessageBox.Show($"Categoria actualizada: {nuevoEstado}.", "exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
     

        private void dgvCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCategorias.Rows[e.RowIndex].Cells["colNombre"].Value != null)
            {
                txtNombre.Text = dgvCategorias.Rows[e.RowIndex].Cells["colNombre"].Value.ToString();
            }



        }
    }
}
