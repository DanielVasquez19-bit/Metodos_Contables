using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Agro_UES.FormLogin;

namespace Agro_UES
{
    public partial class GestionProductos : Form
    {
        // Variable global para guardar el ID del producto seleccionado.
        private int productoActualId = 0;

        public GestionProductos()
        {
            InitializeComponent();
        }
        
        private bool ValidarCampos()
        {
            // Validación del TextBox de nombre (no puede estar vacío)
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor, ingresa el nombre del producto.");
                txtNombre.Focus();
                return false;
            }

            // Validación del TextBox de precio: debe ser numérico y mayor que cero
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio) || precio <= 0)
            {
                MessageBox.Show("Por favor, ingresa un precio válido (mayor a 0).");
                txtPrecio.Focus();
                return false;
            }

            // Validación del TextBox de cantidad: debe ser un entero y mayor o igual a 0
            int cantidad;
            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad < 0)
            {
                MessageBox.Show("Por favor, ingresa una cantidad válida (mayor o igual a 0).");
                txtCantidad.Focus();
                return false;
            }

            // Validación del ComboBox de categoría: debe tener un elemento seleccionado
            if (cmbCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecciona una categoría.");
                cmbCategoria.Focus();
                return false;
            }

            // Validación del ComboBox de IVA: debe tener un valor seleccionado
            if (cmbIVA.SelectedIndex < 0)
            {
                MessageBox.Show("Por favor, selecciona el valor de IVA.");
                cmbIVA.Focus();
                return false;
            }

            // Validación del MaskedTextBox de fecha de vencimiento:
            // Primero se comprueba si se ha completado la máscara
            if (!mtbFechaVencimiento.MaskCompleted)
            {
                MessageBox.Show("Por favor, ingresa la fecha de vencimiento completa (dd/MM/yyyy).");
                mtbFechaVencimiento.Focus();
                return false;
            }

            // Luego se verifica que la fecha tenga el formato correcto (por ejemplo "dd/MM/yyyy")
            DateTime fechaVencimiento;
            if (!DateTime.TryParseExact(
                    mtbFechaVencimiento.Text,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out fechaVencimiento))
            {
                MessageBox.Show("La fecha de vencimiento no está en el formato correcto (dd/MM/yyyy).");
                mtbFechaVencimiento.Focus();
                return false;
            }

            // Si se requieren otras validaciones (como límites máximos, etc.), se agregan aquí.

            // Si se pasan todas las validaciones se retorna true.
            return true;
        }

    // Cargar los productos en el DataGridView
    private void CargarProductos()
        {
            dataGridView1.Rows.Clear();
            string consulta = @"
                SELECT 
                p.id_producto, 
                p.nombre, 
                p.precio, 
                p.stock, 
                p.descripcion, 
                p.iva,
                c.nombre_categoria,
                p.fecha_vencimiento
                FROM productos p
                JOIN categorias c ON p.categoria_id = c.id_categoria;
                ";

            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            using (MySqlDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    dataGridView1.Rows.Add(
                        lector["id_producto"],
                        lector["nombre"],
                        lector["stock"],
                        lector["precio"],
                        lector["descripcion"],
                        lector["iva"],               // Valor IVA
                        lector["nombre_categoria"],
                        lector["fecha_vencimiento"]
                    );
                }
            }
        }





        // Al hacer clic en una fila del DataGridView, completar los controles con los datos correspondientes.
        // Recuerda que las columnas del DataGridView tienen los nombres: 
        // idProducto, dgvNombre, dgvPrecio, dgvCantidad, dgvDescripción, dgvIva y dgvCategoria.
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haya hecho click en una fila (no en el encabezado)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];

                // Guardamos el ID del producto
                productoActualId = Convert.ToInt32(fila.Cells["idProducto"].Value);

                txtNombre.Text = fila.Cells["dgvNombreProducto"].Value.ToString();
                txtPrecio.Text = fila.Cells["dgvPrecio"].Value.ToString();
                txtCantidad.Text = fila.Cells["dgvCantidad"].Value.ToString();
                txtDescripcion.Text = fila.Cells["dgvDescripción"].Value.ToString();

                // Procesar la fecha de vencimiento
                object fechaObj = fila.Cells["dgvFechaVencimiento"].Value;
                if (fechaObj != null && fechaObj != DBNull.Value)
                {
                    DateTime fechaVencimiento;
                    if (DateTime.TryParse(fechaObj.ToString(), out fechaVencimiento))
                    {
                        // Se formatea la fecha para que tenga ceros a la izquierda (por ejemplo, "09/05/2023")
                        mtbFechaVencimiento.Text = fechaVencimiento.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        mtbFechaVencimiento.Text = "";
                    }
                }
                else
                {
                    mtbFechaVencimiento.Text = "";
                }

                // Para IVA, actualizar el ComboBox
                object ivaObj = fila.Cells["dgvIva"].Value;
                string ivaTexto = (ivaObj == null || ivaObj == DBNull.Value) ? "0" : ivaObj.ToString();
                cmbIVA.SelectedItem = ivaTexto;

                cmbCategoria.SelectedItem = fila.Cells["dgvCategoria"].Value.ToString();
            }
        }

        // Método auxiliar para obtener el ID de la categoría a partir del nombre
        private int ObtenerIdCategoria(string nombreCategoria)
        {
            string consulta = "SELECT id_categoria FROM categorias WHERE nombre_categoria = @nombre LIMIT 1";
            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", nombreCategoria);
                object resultado = comando.ExecuteScalar();
                return resultado != null ? Convert.ToInt32(resultado) : 0;
            }
        }

        // Evento Load del formulario: aquí se configuran los controles.
        

        // Método para llenar el ComboBox de categorías
        private void LlenarComboCategorias()
        {
            cmbCategoria.Items.Clear();
            string consulta = "SELECT nombre_categoria FROM categorias";
            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            using (MySqlDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    cmbCategoria.Items.Add(lector["nombre_categoria"].ToString());
                }
            }
            if (cmbCategoria.Items.Count > 0)
                cmbCategoria.SelectedIndex = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GestionProductos_Load(object sender, EventArgs e)
        {
            // Llenar el DataGridView con los productos existentes
            CargarProductos();

            // Configurar las columnas del DataGridView manualmente (si aún no existen)
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("idProducto", "ID Producto");
                dataGridView1.Columns.Add("dgvNombreProducto", "Nombre");
                dataGridView1.Columns.Add("dgvPrecio", "Precio");
                dataGridView1.Columns.Add("dgvCantidad", "Cantidad");
                dataGridView1.Columns.Add("dgvDescripción", "Descripción");
                dataGridView1.Columns.Add("dgvIva", "IVA");
                dataGridView1.Columns.Add("dgvCategoria", "Categoría");
            }

            // Configurar el ComboBox de categorías (ejemplo: llenar con nombres de categorías)
            LlenarComboCategorias();

            // Configurar el ComboBox de IVA (por ejemplo, unas opciones predefinidas)
            cmbIVA.Items.Clear();
            cmbIVA.Items.AddRange(new object[] { "0", "12", "15" });
            cmbIVA.SelectedIndex = 0;


        }

        private void GestionProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            CerrarSesionUsuario();
        }

        private void CerrarSesionUsuario()
        {
            string cerrarSesion = "UPDATE usuarios SET sesion_activa = FALSE WHERE id_usuario = @idUsuario";

            using (MySqlConnection conexion = new ConexionDB().Conectar())
            {
                using (MySqlCommand comando = new MySqlCommand(cerrarSesion, conexion))
                {
                    comando.Parameters.AddWithValue("@idUsuario", UsuarioSesion.IdUsuarioActual);
                    comando.ExecuteNonQuery();
                }
            }
            Console.WriteLine("🔒 Sesión cerrada correctamente.");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (productoActualId == 0)
            {
                MessageBox.Show("Selecciona un producto para actualizar.");
                return;
            }

            string consulta = "UPDATE productos SET nombre=@nombre, descripcion=@desc, categoria_id=@cat, precio=@precio, stock=@stock, iva=@iva WHERE id_producto=@id";
            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@desc", txtDescripcion.Text);
                comando.Parameters.AddWithValue("@cat", ObtenerIdCategoria(cmbCategoria.Text));
                comando.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                comando.Parameters.AddWithValue("@stock", Convert.ToInt32(txtCantidad.Text));
                decimal ivaValor = Convert.ToDecimal(cmbIVA.SelectedItem);
                comando.Parameters.AddWithValue("@iva", ivaValor);
                comando.Parameters.AddWithValue("@id", productoActualId);

                MessageBox.Show("Actualizando producto..."); // Mensaje de progreso
                comando.ExecuteNonQuery();
                MessageBox.Show("✏️ Producto actualizado.");
                CargarProductos();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (productoActualId == 0)
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }
            string consulta = "DELETE FROM productos WHERE id_producto = @id";
            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@id", productoActualId);
                comando.ExecuteNonQuery();
                MessageBox.Show("🗑️ Producto eliminado.");
                CargarProductos();
            }

        }

        

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return; // Si alguna validación falla, se aborta el proceso.
            }


            // Primero validamos que se haya completado la máscara
            //if (!mtbFechaVencimiento.MaskCompleted)
            //{
            //    MessageBox.Show("Por favor, ingresa una fecha de vencimiento completa.");
            //    return;
            //}

            // Convertir la cadena a DateTime usando el formato establecido en el MaskedTextBox ("dd/MM/yyyy")
            DateTime fechaVencimiento;
            if (!DateTime.TryParseExact(mtbFechaVencimiento.Text, "dd/MM/yyyy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaVencimiento))
            {
                MessageBox.Show("La fecha de vencimiento no está en el formato correcto (dd/MM/yyyy).");
                return;
            }

            // Ahora, construimos la consulta incluyendo la columna de fecha
            string consulta = "INSERT INTO productos (nombre, descripcion, categoria_id, precio, stock, iva, fecha_vencimiento) " +
                          "VALUES (@nombre, @desc, @cat, @precio, @stock, @iva, @fecha_vencimiento)";
            using (MySqlConnection conexion = new ConexionDB().Conectar())
            using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@desc", txtDescripcion.Text);
                comando.Parameters.AddWithValue("@cat", ObtenerIdCategoria(cmbCategoria.Text));
                comando.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                comando.Parameters.AddWithValue("@stock", Convert.ToInt32(txtCantidad.Text));

                // Leer el IVA desde el ComboBox (asegúrate de que SelectedItem es un número en formato correcto)
                decimal ivaValor = Convert.ToDecimal(cmbIVA.SelectedItem);
                comando.Parameters.AddWithValue("@iva", ivaValor);

                // Agregar el parámetro fecha_vencimiento como DateTime
                comando.Parameters.AddWithValue("@fecha_vencimiento", fechaVencimiento);

                comando.ExecuteNonQuery();
                MessageBox.Show("✅ Producto agregado correctamente.");
                CargarProductos();
            }
        }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verifica si es la columna de fecha de vencimiento
            if (dataGridView1.Columns[e.ColumnIndex].Name == "dgvFechaVencimiento")
            {
                if (e.Value != null)
                {
                    // Intenta convertir el valor a DateTime
                    if (DateTime.TryParse(e.Value.ToString(), out DateTime fechaVencimiento))
                    {
                        if (fechaVencimiento < DateTime.Today)
                        {
                            // Si la fecha de vencimiento es anterior a hoy, cambia el color del texto a rojo
                            e.CellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            // Opcional: puedes establecer otro color si no está vencido
                            e.CellStyle.ForeColor = Color.Black;
                        }
                    }
                }
            }

        }
    }
}