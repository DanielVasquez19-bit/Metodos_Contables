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
using Agro_UES.Formularios.FormDevolucion;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace Agro_UES.Formularios.FormCajero
{
    public partial class FormCajero0 : Form
    {
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;

        // Constructor corregido, ahora recibe los parametros necesarios
        public FormCajero0(int idUsuario, string nombreUsuario, string rolUsuario)
        {
            InitializeComponent();
            idUsuarioActual = idUsuario;
            nombreUsuarioActual = nombreUsuario;
            rolUsuarioActual = rolUsuario;

            button1.Visible = rolUsuarioActual == "Super Admin";


            lblNombreUsuario.Text = nombreUsuarioActual;
            lblRol.Text = rolUsuarioActual;
            timerCajero.Tick += timerCajero_Tick;
            timerCajero.Start();
            //datos del datafrid
            dgvCarrito.AllowUserToAddRows = false;
            dgvCarrito.CellContentClick += dgvCarrito_CellContentClick;

            btnEfectuarVenta.Click += btnEfectuarVenta_Click;
            btnCancelar.Click += btnCancelar_Click;
            btnDevolucion.Click += btnDevolucion_Click_1;
            btnCerrar.Click += btnCerrar_Click;

            CargarCategoriasEnPanel();
            _ = CargarProductosAsync();


            _ = CargarProductosAsync();
        }

        // Metodo para cargar los productos en el panel dinamico
        private async System.Threading.Tasks.Task CargarProductosAsync()
        {
            flpProductos.Controls.Clear();
            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    await conn.OpenAsync();
                    string sql = @"SELECT id_producto, nombre, precio, stock, IFNULL(ruta_imagen, '') AS ruta_imagen
                                   FROM productos
                                   WHERE stock > 0 ";
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Correccion: Usar GetOrdinal para evitar errores
                            int id = reader.GetInt32(reader.GetOrdinal("id_producto"));
                            string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                            decimal precio = reader.GetDecimal(reader.GetOrdinal("precio"));
                            int stock = reader.GetInt32(reader.GetOrdinal("stock"));
                            string imgPath = reader.IsDBNull(reader.GetOrdinal("ruta_imagen"))
                                             ? null
                                             : reader.GetString(reader.GetOrdinal("ruta_imagen"));

                            var card = CrearCardProducto(id, nombre, precio, stock, imgPath);
                            flpProductos.Controls.Add(card);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        // Metodo para crear una tarjeta de producto en el panel dinamico
        private Panel CrearCardProducto(int id, string nombre, decimal precio, int stock, string imgPath)
        {
            var panel = new Panel
            {
                Width = 120,
                Height = 160,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Tag = new { id, nombre, precio, stock } // Aqui se asigna el Tag correctamente
            };

            var pic = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Bounds = new Rectangle(10, 5, 100, 80),
                Image = !string.IsNullOrEmpty(imgPath) && File.Exists(imgPath)
                        ? Image.FromFile(imgPath)
                        : Properties.Resources.placeholder // Si la imagen es null, usa un placeholder
            };
            panel.Controls.Add(pic);

            var lblN = new Label
            {
                Text = nombre ?? "Producto sin nombre", // Manejo seguro de null
                TextAlign = ContentAlignment.MiddleCenter,
                Bounds = new Rectangle(0, 90, 120, 30),
                AutoEllipsis = true
            };
            panel.Controls.Add(lblN);

            var lblP = new Label
            {
                Text = precio.ToString("C2"),
                ForeColor = Color.Green,
                TextAlign = ContentAlignment.MiddleCenter,
                Bounds = new Rectangle(0, 120, 120, 20)
            };
            panel.Controls.Add(lblP);

            // Asignar el mismo Tag a los controles internos
            pic.Tag = panel.Tag;
            lblN.Tag = panel.Tag;
            lblP.Tag = panel.Tag;

            // Asignar eventos de clic
            panel.Click += Card_Click;
            pic.Click += Card_Click;
            lblN.Click += Card_Click;
            lblP.Click += Card_Click;

            return panel;
        }

        private void Card_Click(object sender, EventArgs e)
        {
            dynamic info = ((Control)sender).Tag;

            if (info == null)
            {
                MessageBox.Show("Error: No se pudo obtener la informacion del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = info.id;
            string nombre = info.nombre ?? "Producto desconocido";
            decimal precio = info.precio;
            int stock = info.stock;

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Stock disponible: {stock}\nCantidad para \"{nombre}\"?",
                "Cantidad", "1"
            );

            if (!int.TryParse(input, out int cantidad) || cantidad <= 0) return;
            if (cantidad > stock)
            {
                MessageBox.Show("No hay suficiente stock.", "Error");
                return;
            }

            // Agregar el ID del producto junto con los demás datos
            dgvCarrito.Rows.Add(id, nombre, cantidad, precio.ToString("N2"));

            // Calcular los valores y mostrarlos en los TextBox
            decimal subtotal = cantidad * precio;
            decimal iva = subtotal * 0.13m;
            decimal total = subtotal + iva;

            txtSubtotal.Text = subtotal.ToString("N2");
            txtIVA.Text = iva.ToString("N2");
            txtTotalPagar.Text = total.ToString("N2");

            CalcularTotalesGenerales();


        }



        // Metodo para calcular los totales generales del carrito
        private void CalcularTotalesGenerales()
        {
            decimal sub = 0, iva = 0, tot = 0;

            // Recorrer todas las filas del carrito y calcular los valores
            foreach (DataGridViewRow fila in dgvCarrito.Rows)
            {
                if (fila.Cells["Cantidad"].Value == null || fila.Cells["PrecioUnitario"].Value == null)
                    continue; // Evitar errores si hay celdas vacias

                int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                decimal precio = Convert.ToDecimal(fila.Cells["PrecioUnitario"].Value);

                decimal subtotal = cantidad * precio;
                decimal ivaCalculado = subtotal * 0.13m;
                decimal totalCalculado = subtotal + ivaCalculado;

                sub += subtotal;
                iva += ivaCalculado;
                tot += totalCalculado;
            }

            // Mostrar los valores en los TextBox
            txtSubtotal.Text = sub.ToString("N2");
            txtIVA.Text = iva.ToString("N2");
            txtTotalPagar.Text = tot.ToString("N2");

        }

        // Metodo para eliminar un producto del carrito
        private void dgvCarrito_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCarrito.Columns[e.ColumnIndex].Name == "dgvEliminarProducto"
                && e.RowIndex >= 0)
            {
                dgvCarrito.Rows.RemoveAt(e.RowIndex);
                CalcularTotalesGenerales();
            }
        }

        // Metodo para efectuar la venta
        private void btnEfectuarVenta_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.Rows.Count == 0)
            {
                MessageBox.Show("Carrito vacio.");
                return;
            }

            // Validar que txtEfectivo tenga un valor numerico correcto
            if (!decimal.TryParse(txtEfectivo.Text.Replace(",", "").Replace("$", "").Trim(), out decimal efectivo) || efectivo <= 0)
            {
                MessageBox.Show("Efectivo invalido. Ingrese un monto numerico valido.");
                return;
            }

            // Validar que txtTotalPagar tenga un valor numerico correcto
            if (!decimal.TryParse(txtTotalPagar.Text.Replace(",", "").Replace("$", "").Trim(), out decimal totalPagar))
            {
                MessageBox.Show("Total invalido. Verifique los valores.");
                return;
            }

            if (efectivo < totalPagar)
            {
                MessageBox.Show("Efectivo insuficiente.");
                return;
            }

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        var cmdV = new MySqlCommand(
                          "INSERT INTO ventas (usuario_id, total) VALUES (@uid,@tot)",
                          conn, trans);
                        cmdV.Parameters.AddWithValue("@uid", idUsuarioActual);
                        cmdV.Parameters.AddWithValue("@tot", totalPagar);
                        cmdV.ExecuteNonQuery();

                        int idVenta = Convert.ToInt32(
                            new MySqlCommand("SELECT LAST_INSERT_ID()", conn, trans)
                                .ExecuteScalar()
                        );

                        foreach (DataGridViewRow fila in dgvCarrito.Rows)
                        {
                            if (fila.Cells["Cantidad"].Value == null || fila.Cells["PrecioUnitario"].Value == null)
                                continue; // Evitar errores si hay celdas vacias

                            int idP = Convert.ToInt32(fila.Cells["ID"].Value);
                            int cant = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                            decimal pr = Convert.ToDecimal(fila.Cells["PrecioUnitario"].Value);

                            var d = new MySqlCommand(@"
                        INSERT INTO detalle_ventas
                           (venta_id, producto_id, nombre_producto, cantidad, precio_unitario)
                         VALUES
                           (@vid,@pid,@nom,@cant,@prec)",
                                conn, trans);
                            d.Parameters.AddWithValue("@vid", idVenta);
                            d.Parameters.AddWithValue("@pid", idP);
                            d.Parameters.AddWithValue("@nom", fila.Cells["Producto"].Value);
                            d.Parameters.AddWithValue("@cant", cant);
                            d.Parameters.AddWithValue("@prec", pr);
                            d.ExecuteNonQuery();

                            var u = new MySqlCommand(
                              "UPDATE productos SET stock = stock - @cant WHERE id_producto = @pid",
                              conn, trans);
                            u.Parameters.AddWithValue("@cant", cant);
                            u.Parameters.AddWithValue("@pid", idP);
                            u.ExecuteNonQuery();
                        }

                        trans.Commit();
                    }
                }

                RegistrarAccion($"Venta realizada. Total: {txtTotalPagar.Text}");
                MessageBox.Show("Venta realizada con exito.");
                dgvCarrito.Rows.Clear();
                CalcularTotalesGenerales();
                txtCambio.Text = (efectivo - totalPagar).ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar venta:\n" + ex.Message);
            }

        }

        // Metodo para registrar acciones en el historial
        private void RegistrarAccion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion)) return; // Evitar registros vacios

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"INSERT INTO historial_acciones (usuario_id, nombre_usuario, accion, fecha_hora)
                                   VALUES (@uid, @nombre, @accion, NOW())";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", idUsuarioActual);
                        cmd.Parameters.AddWithValue("@nombre", nombreUsuarioActual);
                        cmd.Parameters.AddWithValue("@accion", descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la accion: " + ex.Message);
            }
        }

        // Metodo para abrir el formulario de devoluciones
        private void btnDevolucion_Click_1(object sender, EventArgs e)
        {
            // Pasar el rol del usuario para validar si puede hacer devoluciones
            FormDevoluciones formDevolucion = new FormDevoluciones(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual);
            formDevolucion.ShowDialog();
        }

        // Metodo para cancelar la venta y limpiar el carrito
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dgvCarrito.Rows.Clear();
            CalcularTotalesGenerales();
            txtEfectivo.Text = string.Empty;
            txtCambio.Text = "0.00";
            MessageBox.Show("Venta cancelada.");
        }

        // Metodo para cerrar la aplicacion
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Metodo para actualizar la fecha y hora en tiempo real
        private void timerCajero_Tick(object sender, EventArgs e)
        {
            lblFechaYHora.Text = DateTime.Now.ToString("dddd dd/MM/yyyy - hh:mm:ss tt");
        }


        //metodo para filtrar productos por nombre
        private void button12_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                _ = CargarProductosAsync(); // Sin filtro, carga todo
            }
            else
            {
                _ = CargarProductosPorNombreAsync(filtro);
            }

        }


        private async Task CargarProductosPorNombreAsync(string filtro)
        {
            flpProductos.Controls.Clear();

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    await conn.OpenAsync();
                    string sql = @"
                SELECT id_producto, nombre, precio, stock, IFNULL(ruta_imagen,'') AS ruta_imagen
                  FROM productos
                 WHERE stock > 0
                   AND estado = 'Disponible'
                   AND nombre LIKE @filtro
                 ORDER BY nombre";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@filtro", $"%{filtro}%");
                        using (var rdr = await cmd.ExecuteReaderAsync())
                        {
                            while (await rdr.ReadAsync())
                            {
                                int id = rdr.GetInt32(rdr.GetOrdinal("id_producto"));
                                string nombre = rdr.GetString(rdr.GetOrdinal("nombre"));
                                decimal precio = rdr.GetDecimal(rdr.GetOrdinal("precio"));
                                int stock = rdr.GetInt32(rdr.GetOrdinal("stock"));
                                string imgPath = rdr.IsDBNull(rdr.GetOrdinal("ruta_imagen"))
                                    ? null
                                    : rdr.GetString(rdr.GetOrdinal("ruta_imagen"));

                                var card = CrearCardProducto(id, nombre, precio, stock, imgPath);
                                flpProductos.Controls.Add(card);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos:\n" + ex.Message);
            }
        }



        private void CargarCategoriasEnPanel()
        {
            flpCategorias.Controls.Clear();

            // 🔹 Botón "Todos" primero
            var btnTodos = new Button
            {
                Text = "Todos",
                Tag = null, // Tag nulo indica que no hay filtro
                Width = flpCategorias.Width - 20,
                Height = 36,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.DarkOliveGreen,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9),
                TextAlign = ContentAlignment.MiddleLeft
            };
            btnTodos.FlatAppearance.BorderSize = 0;
            btnTodos.Click += Categoria_Click;
            flpCategorias.Controls.Add(btnTodos);

            // 🔹 Resto de categorías activas
            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    conn.Open();
                    string sql = @"SELECT id_categoria, nombre_categoria FROM categorias WHERE estado = 'Activa' ORDER BY nombre_categoria";

                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int idCat = rdr.GetInt32("id_categoria");
                            string nombre = rdr.GetString("nombre_categoria");

                            var btn = new Button
                            {
                                Text = nombre,
                                Tag = idCat, // usaremos esto para filtrar
                                Width = flpCategorias.Width - 20,
                                Height = 36,
                                FlatStyle = FlatStyle.Flat,
                                BackColor = Color.DarkOliveGreen,
                                ForeColor = Color.White,
                                Font = new Font("Segoe UI Semibold", 9),
                                TextAlign = ContentAlignment.MiddleLeft
                            };
                            btn.FlatAppearance.BorderSize = 0;
                            btn.Click += Categoria_Click;
                            flpCategorias.Controls.Add(btn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message);
            }

        }
        private void Categoria_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;

            if (btn.Tag == null)
            {
                // Mostrar todos los productos
                _ = CargarProductosAsync();
            }
            else
            {
                int idCategoria = (int)btn.Tag;
                _ = CargarProductosPorCategoriaAsync(idCategoria);
            }

        }

        private async Task CargarProductosPorCategoriaAsync(int idCategoria)
        {
            flpProductos.Controls.Clear();

            try
            {
                using (var conn = ConexionDB.Conexion())
                {
                    await conn.OpenAsync();
                    string sql = @"
                SELECT id_producto, nombre, precio, stock, IFNULL(ruta_imagen, '') AS ruta_imagen
                  FROM productos
                 WHERE stock > 0 AND estado = 'Disponible' AND categoria_id = @cat
                 ORDER BY nombre";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@cat", idCategoria);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id_producto"));
                                string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                                decimal precio = reader.GetDecimal(reader.GetOrdinal("precio"));
                                int stock = reader.GetInt32(reader.GetOrdinal("stock"));
                                string imgPath = reader.IsDBNull(reader.GetOrdinal("ruta_imagen"))
                                                 ? null
                                                 : reader.GetString(reader.GetOrdinal("ruta_imagen"));

                                var card = CrearCardProducto(id, nombre, precio, stock, imgPath);
                                flpProductos.Controls.Add(card);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos por categoría:\n" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            

        }

        private void btnCambiarPIN_Click(object sender, EventArgs e)
        {
            FormRecuperar frmRecup = new FormRecuperar();
            frmRecup.Show();
            // Ocultar este form
            this.Hide();
        }
    }
}

                        

    
                
