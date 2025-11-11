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
using System.Windows.Forms.DataVisualization.Charting;
using Agro_UES.Formularios.FormEditarUsuario;
using Agro_UES.Formularios.FormCrearUsuario;
using System.Diagnostics;
using System.IO;
using Agro_UES.Formularios.FormReportes;
using Agro_UES.Formularios.FormCajero;
using Agro_UES.Formularios.FormLogin;

namespace Agro_UES
{
    public partial class FormSuperAdmin: Form
    {
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;

        public FormSuperAdmin(int id, string nombre, string rol)
        {
            InitializeComponent();
            idUsuarioActual = id;
            nombreUsuarioActual = nombre;
            rolUsuarioActual = rol;


            relojHora.Start();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");

            cmbFiltroVentas.SelectedIndex = 0;
            CargarGraficaVentasMensual();
            CargarGraficaTopCajeros();
            CargarResumen();

        }
        private void OcultarTodosLosPaneles()
        {
            panelResumenGeneral.Visible = false;
            panelUsuarios.Visible = false;
            panelHistorial.Visible = false;
            panelModulo.Visible = false;

        }
        /***************Seccion panel superior la barra de arriba ******************/

        private void AgregarNotificacion(string texto, Color color)
        {
            Label etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.ForeColor = color;
            etiqueta.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            etiqueta.AutoSize = true;
            etiqueta.Margin = new Padding(5, 2, 5, 2);
            panelNotificaciones.Controls.Add(etiqueta);
        }
        /**cargar notificaciones**/ 
        private void CargarNotificaciones()
        {
            panelNotificaciones.Controls.Clear();

            MySqlConnection conexion = ConexionDB.Conexion();
            conexion.Open();

            // 1. Productos con stock crítico
            string consultaStock = "SELECT nombre FROM productos WHERE stock < 10 OR alerta_bajo_stock = 1";
            MySqlCommand comandoStock = new MySqlCommand(consultaStock, conexion);
            MySqlDataReader lectorStock = comandoStock.ExecuteReader();
            while (lectorStock.Read())
            {
                string nombre = lectorStock["nombre"].ToString();
                AgregarNotificacion($"-{nombre} con stock critico", Color.Red);
            }
            lectorStock.Close();

            // 2. Productos vencidos
            string consultaVencidos = "SELECT nombre FROM productos WHERE fecha_vencimiento <= CURDATE() AND fecha_vencimiento IS NOT NULL";
            MySqlCommand comandoVencidos = new MySqlCommand(consultaVencidos, conexion);
            MySqlDataReader lectorVencidos = comandoVencidos.ExecuteReader();
            while (lectorVencidos.Read())
            {
                string nombre = lectorVencidos["nombre"].ToString();
                AgregarNotificacion($"- {nombre} vencido", Color.Orange);
            }
            lectorVencidos.Close();

            // 3. Nuevos usuarios registrados hoy
            string consultaNuevos = "SELECT COUNT(*) FROM usuarios WHERE DATE(fecha_registro) = CURDATE()";
            MySqlCommand comandoNuevos = new MySqlCommand(consultaNuevos, conexion);
            int nuevos = Convert.ToInt32(comandoNuevos.ExecuteScalar());
            if (nuevos > 0)
            {
                AgregarNotificacion($"{nuevos} nuevos usuarios registrados hoy", Color.Blue);
            }

            conexion.Close();
        }
        /*** timer para mostrar la hora***/
        private void relojHora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");

        }
        /***Boton para mostrar las notificaciones ***/
        private void btnNotificaciones_Click_1(object sender, EventArgs e)
        {
            CargarNotificaciones();
            panelNotificaciones.Visible = !panelNotificaciones.Visible;

        }








        /******************************************************************************/

        /****************** Metodos para el panel Resumen General ******************/


        /***Cargar la grafica de ventas mensual***/
        private void CargarGraficaVentasMensual()
        {
            chartVentas.Series.Clear();
            chartVentas.Series.Add("Ventas");
            chartVentas.Series["Ventas"].ChartType = SeriesChartType.Column;

            MySqlConnection conexion = ConexionDB.Conexion();
            conexion.Open();

            string consulta = @"
                SELECT MONTH(fecha_venta) AS mes, SUM(total) AS total
                FROM ventas
                WHERE YEAR(fecha_venta) = YEAR(CURDATE())
                GROUP BY MONTH(fecha_venta)";

            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataReader lector = comando.ExecuteReader();

            string[] nombresMeses = { "", "Ene", "Feb", "Mar", "Abr", "May", "Jun",
                                      "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };

            while (lector.Read())
            {
                int mes = Convert.ToInt32(lector["mes"]);
                decimal total = Convert.ToDecimal(lector["total"]);
                chartVentas.Series["Ventas"].Points.AddXY(nombresMeses[mes], total);
            }

            lector.Close();
            conexion.Close();
        }


        /*****Cargar la grafica de ventas diarias******/

        private void CargarGraficaVentasDiaria()
        {
            chartVentas.Series.Clear();
            chartVentas.Series.Add("Ventas");
            chartVentas.Series["Ventas"].ChartType = SeriesChartType.Column;

            MySqlConnection conexion = ConexionDB.Conexion();
            conexion.Open();

            string consulta = @"
                SELECT DATE(fecha_venta) AS dia, SUM(total) AS total
                FROM ventas
                WHERE fecha_venta >= DATE_SUB(CURDATE(), INTERVAL 15 DAY)
                GROUP BY DATE(fecha_venta)
                ORDER BY dia ASC";

            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                string dia = Convert.ToDateTime(lector["dia"]).ToString("dd/MM");
                decimal total = Convert.ToDecimal(lector["total"]);
                chartVentas.Series["Ventas"].Points.AddXY(dia, total);
            }

            lector.Close();
            conexion.Close();
        }


        /***Cargar la grafica de los cajeros con mas ventas***/
        private void CargarGraficaTopCajeros()
        {
            chartUsuarios.Series.Clear();
            chartUsuarios.Series.Add("Cajeros");
            chartUsuarios.Series["Cajeros"].ChartType = SeriesChartType.Column;

            MySqlConnection conexion = ConexionDB.Conexion();
            conexion.Open();

            string consulta = @"
                SELECT u.nombre, COUNT(*) AS totalVentas
                FROM ventas v
                JOIN usuarios u ON v.usuario_id = u.id_usuario
                WHERE u.rol_id = 3
                GROUP BY u.nombre
                ORDER BY totalVentas DESC
                LIMIT 5";

            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                string nombre = lector["nombre"].ToString();
                int total = Convert.ToInt32(lector["totalVentas"]);
                chartUsuarios.Series["Cajeros"].Points.AddXY(nombre, total);
            }

            lector.Close();
            conexion.Close();
        }


        /**xombobox para el friltro de la greafica**/
        private void cmbFiltroVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltroVentas.SelectedItem.ToString() == "Mensual")
                CargarGraficaVentasMensual();
            else
                CargarGraficaVentasDiaria();

        }



        /*****Para cargar el resumen******/
        private void CargarResumen()
        {
            try
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                // Historial reciente
                string consultaHistorial = @"
                       SELECT nombre_usuario AS 'Usuario',
                       accion AS 'Acción',
                       DATE_FORMAT(fecha_hora, '%d/%m/%Y %H:%i') AS 'Fecha y Hora'
                       FROM historial_acciones
                       ORDER BY fecha_hora DESC
                       LIMIT 5";

                MySqlDataAdapter adaptador = new MySqlDataAdapter(consultaHistorial, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvHistorialReciente.DataSource = tabla;


                // Usuarios activos
                string consultaUsuarios = "SELECT COUNT(*) FROM usuarios WHERE estado = 'activo'";
                MySqlCommand comandoUsuarios = new MySqlCommand(consultaUsuarios, conexion);
                int totalUsuarios = Convert.ToInt32(comandoUsuarios.ExecuteScalar());
                lblTotalUsuarios.Text = totalUsuarios.ToString();

                // Productos registrados
                string consultaProductos = "SELECT COUNT(*) FROM productos";
                MySqlCommand comandoProductos = new MySqlCommand(consultaProductos, conexion);
                int totalProductos = Convert.ToInt32(comandoProductos.ExecuteScalar());
                lblTotalProductos.Text = totalProductos.ToString();

                // Ventas totales
                string consultaVentas = "SELECT COUNT(*) FROM ventas";
                MySqlCommand comandoVentas = new MySqlCommand(consultaVentas, conexion);
                int totalVentas = Convert.ToInt32(comandoVentas.ExecuteScalar());
                lblTotalVentas.Text = totalVentas.ToString();

                // Ganancia mensual
                string consultaGanancia = @"
                    SELECT SUM(total) FROM ventas
                    WHERE MONTH(fecha_venta) = MONTH(CURDATE())
                    AND YEAR(fecha_venta) = YEAR(CURDATE())";

                MySqlCommand comandoGanancia = new MySqlCommand(consultaGanancia, conexion);
                object resultado = comandoGanancia.ExecuteScalar();
                decimal ganancia = resultado != DBNull.Value ? Convert.ToDecimal(resultado) : 0;
                lblGananciaMensual.Text = "$" + ganancia.ToString("N2");

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cargar datos del resumen: " + error.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /***************************************************************/










        /****************************************************************/
        /**Panel Gestion Usuaios**/

        /***Registrar acciones del panel***/
        private void RegistrarAccion(string descripcion)
        {
            try
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();

                string consulta = "INSERT INTO historial_acciones (usuario_id, nombre_usuario, accion) VALUES (@id, @nombre, @accion)";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@id", idUsuarioActual);
                comando.Parameters.AddWithValue("@nombre", nombreUsuarioActual);
                comando.Parameters.AddWithValue("@accion", descripcion);
                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("No se pudo registrar la accion: " + error.Message);
            }
        }

        /*** Cargar los usuarios en la tabla de usuarios del panel**/
        private void CargarUsuariosEnTabla()
        {
            string consulta = @"
        SELECT u.id_usuario, u.nombre, u.correo, r.nombre_rol AS rol, u.estado, 
               DATE_FORMAT(u.fecha_registro, '%d/%m/%Y %H:%i') AS fecha
        FROM usuarios u
        JOIN roles r ON u.rol_id = r.id_rol";

            MySqlConnection conexion = ConexionDB.Conexion();
            DataTable tabla = new DataTable();

            try
            {
                conexion.Open();
                MySqlDataAdapter adaptador = new MySqlDataAdapter(consulta, conexion);
                adaptador.Fill(tabla);
                dgvUsuarios.DataSource = tabla;
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al cargar usuarios: " + error.Message);
            }
        }
        /** buscar usuarios por rol para mostrarlos en las terjetas**/
        private void ContarUsuariosPorRol()
        {
            string consulta = @"
        SELECT r.nombre_rol, COUNT(*) AS cantidad
        FROM usuarios u
        JOIN roles r ON u.rol_id = r.id_rol
        GROUP BY r.nombre_rol";

            MySqlConnection conexion = ConexionDB.Conexion();
            conexion.Open();

            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            MySqlDataReader lector = comando.ExecuteReader();

            // Reiniciamos por si ya había valores
            lblCajeros.Text = "0";
            lblAlmacen.Text = "0";
            lblGerentes.Text = "0";

            while (lector.Read())
            {
                string rol = lector["nombre_rol"].ToString();
                string cantidad = lector["cantidad"].ToString();

                if (rol == "Cajero")
                    lblCajeros.Text = cantidad;
                else if (rol == "Encargado de Almacen")
                    lblAlmacen.Text = cantidad;
                else if (rol == "Gerente")
                    lblGerentes.Text = cantidad;
            }

            lector.Close();
            conexion.Close();
        }

        /*******Metodo par buscar usuarios******/
        private void BuscarUsuarios(string texto)
        {
            string consulta = @"
        SELECT u.id_usuario, u.nombre, u.correo, r.nombre_rol AS rol, u.estado, 
               DATE_FORMAT(u.fecha_registro, '%d/%m/%Y %H:%i') AS fecha
        FROM usuarios u
        JOIN roles r ON u.rol_id = r.id_rol
        WHERE u.nombre LIKE @texto OR u.correo LIKE @texto";

            MySqlConnection conexion = ConexionDB.Conexion();
            DataTable tabla = new DataTable();

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@texto", "%" + texto + "%");
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                adaptador.Fill(tabla);
                dgvUsuarios.DataSource = tabla;
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al buscar: " + error.Message);
            }
        }
        /*********Boton para buscar usuarios**********/
        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarUsuario.Text.Trim();
            if (!string.IsNullOrEmpty(texto))
                BuscarUsuarios(texto);
            else
                CargarUsuariosEnTabla(); // recarga todos


        }




        /***************************************************************/

        /********************* PANEL HISTORIAL ********************/

        // Mostrar panelHistorial desde el menu lateral
        private void btnHistorial_Click_1(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            panelHistorial.Visible = true;
            dtpDesde.Value = DateTime.Today.AddDays(-7);  // Por defecto, ultimos 7 dias
            dtpHasta.Value = DateTime.Today;
            CargarHistorial(); // Carga inicial

        }

        // Cargar historial sin filtros (o con los actuales)
        private void CargarHistorial(string texto = "", DateTime? desde = null, DateTime? hasta = null)
        {
            try
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                // Consulta SQL para obtener el historial de acciones
                string consulta = @"SELECT nombre_usuario AS 'Usuario', 
                                   accion AS 'Accion realizada',
                                   DATE_FORMAT(fecha_hora, '%d/%m/%Y %H:%i') AS 'Fecha y Hora'
                            FROM historial_acciones
                            WHERE 1 ";
                
                if (!string.IsNullOrEmpty(texto))
                {
                    consulta += "AND (nombre_usuario LIKE @texto OR accion LIKE @texto) ";
                }

                if (desde != null && hasta != null)
                {
                    consulta += "AND DATE(fecha_hora) BETWEEN @desde AND @hasta ";
                }

                consulta += "ORDER BY fecha_hora DESC";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);

                if (!string.IsNullOrEmpty(texto))
                    comando.Parameters.AddWithValue("@texto", "%" + texto + "%");

                if (desde != null && hasta != null)
                {
                    comando.Parameters.AddWithValue("@desde", desde.Value.ToString("yyyy-MM-dd"));
                    comando.Parameters.AddWithValue("@hasta", hasta.Value.ToString("yyyy-MM-dd"));
                }

                MySqlDataAdapter adaptador = new MySqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dgvHistorialAcciones.DataSource = tabla;

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        // Boton BUSCAR historial
        private void btnBuscarHistorial_Click_1(object sender, EventArgs e)
        {
            string texto = txtBuscarHistorial.Text.Trim();
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;
            CargarHistorial(texto, desde, hasta);

        }

        // Boton LIMPIAR FILTROS
        private void btnLimpiarFiltros_Click_1(object sender, EventArgs e)
        {
            txtBuscarHistorial.Clear();
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;
            CargarHistorial();

        }





        /*********panel de la barra laterea con los botones************/
        private void btnResumenGeneral_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            panelResumenGeneral.Visible = true;
        }

        
        
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            panelUsuarios.Visible = true;
            CargarUsuariosEnTabla();
            ContarUsuariosPorRol();

        }

        private void FormSuperAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            FormNuevoUsuario ventana = new FormNuevoUsuario(idUsuarioActual, nombreUsuarioActual);
            if (ventana.ShowDialog() == DialogResult.OK)
            {
                CargarUsuariosEnTabla();
                ContarUsuariosPorRol();
            }

        }
        /// Botón para editar usuario seleccionado
        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario para editar");
                return;
            }

            int idEditar = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["id_usuario"].Value);

            // Abrir el formulario de edición y pasar datos del operador y el usuario a editar
            FormEditarUsuario ventanaEditar = new FormEditarUsuario(idUsuarioActual, nombreUsuarioActual, idEditar);

            // Al cerrarse, si fue editado, se recarga la tabla
            ventanaEditar.FormClosed += (s, args) =>
            {
                CargarUsuariosEnTabla();
                ContarUsuariosPorRol();
            };

            ventanaEditar.Show();


        }
        /// Botón para desactivar usuario seleccionado

        private void btnDesactivarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario");
                return;
            }

            string nombre = dgvUsuarios.SelectedRows[0].Cells["nombre"].Value.ToString();
            int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["id_usuario"].Value);
            string rol = dgvUsuarios.SelectedRows[0].Cells["rol"].Value.ToString();

            // Evitar desactivacion de Super Administrador
            if (rol == "Super Administrador")
            {
                MessageBox.Show("No se puede desactivar a un usuario con rol de Super Administrador.", "Accion no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            DialogResult respuesta = MessageBox.Show($"¿Seguro que queres desactivar a {nombre}?",
                "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Yes)
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                string consulta = "UPDATE usuarios SET estado = 'inactivo' WHERE id_usuario = @id";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
                conexion.Close();

                CargarUsuariosEnTabla();
                ContarUsuariosPorRol();
                RegistrarAccion("Desactivo al usuario: " + nombre);
            }
            


        }

        

        private void btnActivarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario para activar.");
                return;
            }

            int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells["id_usuario"].Value);
            string nombre = dgvUsuarios.SelectedRows[0].Cells["nombre"].Value.ToString();
            string estado = dgvUsuarios.SelectedRows[0].Cells["estado"].Value.ToString();

            if (estado == "activo")
            {
                MessageBox.Show("Este usuario ya esta activo.");
                return;
            }

            DialogResult confirmacion = MessageBox.Show(
                $"¿Estas seguro que queres activar a {nombre}?",
                "Confirmacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                MySqlConnection conexion = ConexionDB.Conexion();
                conexion.Open();
                string consulta = "UPDATE usuarios SET estado = 'activo' WHERE id_usuario = @id";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@id", id);
                comando.ExecuteNonQuery();
                conexion.Close();

                CargarUsuariosEnTabla();
                ContarUsuariosPorRol();
                RegistrarAccion("Reactivo al usuario: " + nombre);

            }

        }

        private void btnRespaldar_Click(object sender, EventArgs e)
        {
            string rutaMysqldump = @"C:\xampp\mysql\bin\mysqldump.exe";

            // Carpeta "Respaldos" dentro de la ruta del ejecutable
            string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
            string rutaRespaldo = Path.Combine(rutaBase, "Respaldos");

            if (!Directory.Exists(rutaRespaldo))
            {
                Directory.CreateDirectory(rutaRespaldo);
            }

            string nombreArchivo = "Respaldo_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".sql";
            string archivoCompleto = Path.Combine(rutaRespaldo, nombreArchivo);

            if (!File.Exists(rutaMysqldump))
            {
                MessageBox.Show("No se encontro mysqldump en la ruta especificada.");
                return;
            }

            string argumentos = $"--user=root --password='' --host=localhost agro_ues_db --result-file=\"{archivoCompleto}\" --routines --events --single-transaction";

            try
            {
                Process proceso = new Process();
                proceso.StartInfo.FileName = rutaMysqldump;
                proceso.StartInfo.Arguments = argumentos;
                proceso.StartInfo.RedirectStandardOutput = false;
                proceso.StartInfo.UseShellExecute = false;
                proceso.StartInfo.CreateNoWindow = true;
                proceso.Start();
                proceso.WaitForExit();

                if (File.Exists(archivoCompleto))
                {
                    RegistrarAccion("Genero respaldo de la base de datos");
                    MessageBox.Show("Respaldo generado correctamente:\n" + archivoCompleto);
                }
                else
                {
                    MessageBox.Show("No se pudo generar el respaldo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el respaldo:\n" + ex.Message);
            }


        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FormReportes ventana = new FormReportes(idUsuarioActual, nombreUsuarioActual);
            ventana.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OcultarTodosLosPaneles();
            panelModulo.Visible = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void btnCambiarPIN_Click(object sender, EventArgs e)
        {
            FormRecuperar frmRecup = new FormRecuperar();
            frmRecup.Show();
            // Ocultar este form
            this.Hide();
        }

        private void panelHistorial_Paint(object sender, PaintEventArgs e)
        {

        }

        

        

        private void panelResumenGeneral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGerencia_Click(object sender, EventArgs e)
        {
            FormGerente ventana = new FormGerente(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual);
            ventana.Show();
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            FormCajero0 ventana1 = new FormCajero0(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual);
            ventana1.Show();
        }

        private void btnAlmacen_Click(object sender, EventArgs e)
        {
            FormAlmacen ventana2 = new FormAlmacen(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual);
            ventana2.Show();
        }

        private void FormSuperAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            SesionHelper.MarcarSesionInactiva(idUsuarioActual);
        }

        private void panelSuperior_Paint(object sender, PaintEventArgs e)
        {

        }

        /**********Estilo para que se adapten los paneles***************/

    }
}

