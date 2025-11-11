using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agro_UES;
using Agro_UES.Formularios.Formgerente;

using MySql.Data.MySqlClient;




namespace Agro_UES.Formularios.Formgerente
{
    public partial class FormSolicitudes : Form
    {
        
        private void dgvSolicitudesPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvHistorialSolicitudes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private int idAprobador;
        private string nombreAprobador;
        private string rolAprobador;


        public FormSolicitudes(int idUsuario, string nombreUsuario, string rol)
        {
            InitializeComponent();
            this.idAprobador = idUsuario;
            this.nombreAprobador = nombreUsuario;
            this.rolAprobador = rol;


            Load += FormSolicitudes_Load;
        }

        private void FormSolicitudes_Load(object sender, EventArgs e)
        {
            CargarPendientes();
            CargarHistorial();
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
                        cmd.Parameters.AddWithValue("@id", idAprobador);
                        cmd.Parameters.AddWithValue("@nombre", nombreAprobador);
                        cmd.Parameters.AddWithValue("@accion", descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                
            }

        }




        /**************Mostra detalless***********/

       




        private void CargarHistorial()
        {
            dgvHistorialSolicitudes.Rows.Clear();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                const string sql = @"
                    SELECT
                      id_aprobacion,
                      id_producto,
                      descripcion,
                      precio,
                      stock,
                      fecha_vencimiento,
                      estado,
                      nombre_solicita,
                      fecha_solicita,
                      nombre_responde,
                      fecha_respuesta
                    FROM aprobaciones_almacen
                    WHERE estado IN ('Aprobada','Rechazada')
                    ORDER BY fecha_respuesta DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    int oProd = rdr.GetOrdinal("id_producto");
                    int oDesc = rdr.GetOrdinal("descripcion");
                    int oPrecio = rdr.GetOrdinal("precio");
                    int oStock = rdr.GetOrdinal("stock");
                    int oFechaVenc = rdr.GetOrdinal("fecha_vencimiento");
                    int oEstado = rdr.GetOrdinal("estado");
                    int oSolicita = rdr.GetOrdinal("nombre_solicita");
                    int oFechaSol = rdr.GetOrdinal("fecha_solicita");
                    int oRespNombre = rdr.GetOrdinal("nombre_responde");
                    int oFechaResp = rdr.GetOrdinal("fecha_respuesta");

                    while (rdr.Read())
                    {
                        int idProducto = rdr.GetInt32(oProd);
                        string descripcion = rdr.GetString(oDesc);
                        decimal precio = rdr.GetDecimal(oPrecio);
                        int stock = rdr.GetInt32(oStock);
                        string vencimiento = rdr.IsDBNull(oFechaVenc)
                            ? "—"
                            : rdr.GetDateTime(oFechaVenc).ToShortDateString();
                        string estado = rdr.GetString(oEstado);
                        string solicita = rdr.GetString(oSolicita);
                        string fechaSolicita = rdr.GetDateTime(oFechaSol).ToString("g");
                        string aprobadoPor = rdr.IsDBNull(oRespNombre)
                            ? "—"
                            : rdr.GetString(oRespNombre);
                        string fechaRespuesta = rdr.IsDBNull(oFechaResp)
                            ? "—"
                            : rdr.GetDateTime(oFechaResp).ToString("g");

                        int idx = dgvHistorialSolicitudes.Rows.Add();
                        var fila = dgvHistorialSolicitudes.Rows[idx];
                        fila.Cells["dgvHistIDProd"].Value = idProducto;
                        fila.Cells["dgvHistDescripcion"].Value = descripcion;
                        fila.Cells["dgvHistPrecio"].Value = precio;
                        fila.Cells["dgvHistStock"].Value = stock;
                        fila.Cells["dgvHistVencimiento"].Value = vencimiento;
                        fila.Cells["dgvHistEstado"].Value = estado;
                        fila.Cells["dgvHistSolicita"].Value = solicita;
                        fila.Cells["dgvHistFechaSolicita"].Value = fechaSolicita;
                        fila.Cells["dgvHistAprobador"].Value = aprobadoPor;
                        fila.Cells["dgvHistFechaRespuesta"].Value = fechaRespuesta;
                    }
                }
            }

        }


  



        //**************Cargar solicitudes pendientes***********/
        private void CargarPendientes()
        {
            dgvSolicitudesPendientes.Rows.Clear();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                const string sql = @"
                    SELECT
                      id_aprobacion,
                      id_producto,
                      descripcion,
                      precio,
                      stock,
                      fecha_vencimiento,
                      nombre_solicita,
                      fecha_solicita
                    FROM aprobaciones_almacen
                    WHERE estado = 'Pendiente'
                    ORDER BY fecha_solicita DESC";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    int oIdAprob = rdr.GetOrdinal("id_aprobacion");
                    int oIdProd = rdr.GetOrdinal("id_producto");
                    int oDesc = rdr.GetOrdinal("descripcion");
                    int oPrecio = rdr.GetOrdinal("precio");
                    int oStock = rdr.GetOrdinal("stock");
                    int oFechaVenc = rdr.GetOrdinal("fecha_vencimiento");
                    int oSolicita = rdr.GetOrdinal("nombre_solicita");
                    int oFechaSol = rdr.GetOrdinal("fecha_solicita");

                    while (rdr.Read())
                    {
                        int idAprobacion = rdr.GetInt32(oIdAprob);
                        int idProducto = rdr.GetInt32(oIdProd);
                        string descripcion = rdr.GetString(oDesc);
                        decimal precio = rdr.GetDecimal(oPrecio);
                        int stock = rdr.GetInt32(oStock);
                        string fechaVencimiento = rdr.IsDBNull(oFechaVenc)
                            ? "—"
                            : rdr.GetDateTime(oFechaVenc).ToShortDateString();
                        string solicita = rdr.GetString(oSolicita);
                        string fechaSol = rdr.GetDateTime(oFechaSol).ToString("g");

                        int idx = dgvSolicitudesPendientes.Rows.Add();
                        var fila = dgvSolicitudesPendientes.Rows[idx];
                        fila.Cells["dgvIDProducto"].Value = idProducto;
                        fila.Cells["dgvDescripcion"].Value = descripcion;
                        fila.Cells["dgvPrecio"].Value = precio;
                        fila.Cells["dgvStock"].Value = stock;
                        fila.Cells["dgvFechaVencimiento"].Value = fechaVencimiento;
                        fila.Cells["dgvSolicita"].Value = solicita;
                        fila.Cells["dgvFechaSolicitud"].Value = fechaSol;
                        fila.Tag = idAprobacion;
                    }
                }
            }

        }



     

        private void dgvSolicitudesPendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void dgvHistorialSolicitudes_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvHistorialSolicitudes_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void AplicarAjusteProductoDesdeDescripcion(string descripcion, MySqlConnection conn)
        {
            try
            {
                int idProducto = int.Parse(descripcion.Split(new[] { "ID " }, StringSplitOptions.None)[1].Split(':')[0]);
                int nuevoStock = int.Parse(
                    descripcion.ToLower().Contains("stock:")
                        ? descripcion.Split(new[] { "Stock:" }, StringSplitOptions.None)[1].Split(',')[0].Trim()
                        : "0"
                );

                string sql = @"UPDATE productos 
                       SET stock = @stock, estado = 'Aprobado'
                       WHERE id_producto = @id";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idProducto);
                    cmd.Parameters.AddWithValue("@stock", nuevoStock);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar el ajuste de stock:\n{ex.Message}", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnAprobarProceso_Click(object sender, EventArgs e)
        {
            if (dgvSolicitudesPendientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una solicitud para aprobar.");
                return;
            }

            int idAprobacion = Convert.ToInt32(dgvSolicitudesPendientes
                .SelectedRows[0].Tag);
            int idProducto;
            string descripcion;
            decimal precio;
            int stock;
            DateTime? vencimiento;
            string rutaImagen;

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();

                // 1. Leer solicitud completa
                const string selectSql = @"
            SELECT id_producto, descripcion, precio, stock, fecha_vencimiento, ruta_imagen
              FROM aprobaciones_almacen
             WHERE id_aprobacion = @id";
                using (var cmd = new MySqlCommand(selectSql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idAprobacion);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (!rdr.Read())
                        {
                            MessageBox.Show("Solicitud no encontrada.");
                            return;
                        }

                        int oIdProd = rdr.GetOrdinal("id_producto");
                        int oDesc = rdr.GetOrdinal("descripcion");
                        int oPrecio = rdr.GetOrdinal("precio");
                        int oStock = rdr.GetOrdinal("stock");
                        int oVenc = rdr.GetOrdinal("fecha_vencimiento");
                        int oImg = rdr.GetOrdinal("ruta_imagen");

                        idProducto = rdr.GetInt32(oIdProd);
                        descripcion = rdr.GetString(oDesc);
                        precio = rdr.GetDecimal(oPrecio);
                        stock = rdr.GetInt32(oStock);

                        if (rdr.IsDBNull(oVenc))
                            vencimiento = null;
                        else
                            vencimiento = rdr.GetDateTime(oVenc);

                        rutaImagen = rdr.IsDBNull(oImg)
                            ? null
                            : rdr.GetString(oImg);
                    }
                }

                // 2. Actualizar tabla productos
                const string updateProdSql = @"
            UPDATE productos
               SET descripcion      = @desc,
                   precio           = @precio,
                   stock            = @stock,
                   fecha_vencimiento= @venc,
                   ruta_imagen      = @img
             WHERE id_producto     = @idProd";
                using (var cmd = new MySqlCommand(updateProdSql, conn))
                {
                    cmd.Parameters.AddWithValue("@desc", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue(
                        "@venc",
                        vencimiento.HasValue ? (object)vencimiento.Value : DBNull.Value
                    );
                    cmd.Parameters.AddWithValue(
                        "@img",
                        rutaImagen ?? (object)DBNull.Value
                    );
                    cmd.Parameters.AddWithValue("@idProd", idProducto);
                    cmd.ExecuteNonQuery();
                }

                // 3. Marcar solicitud como Aprobada
                const string updateAprSql = @"
            UPDATE aprobaciones_almacen
               SET estado          = 'Aprobada',
                   usuario_responde = @uid,
                   nombre_responde  = @nombre,
                   fecha_respuesta  = NOW()
             WHERE id_aprobacion  = @idAprob";
                using (var cmd = new MySqlCommand(updateAprSql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", idAprobador);
                    cmd.Parameters.AddWithValue("@nombre", nombreAprobador);
                    cmd.Parameters.AddWithValue("@idAprob", idAprobacion);
                    cmd.ExecuteNonQuery();
                }
            }

            // 4. Registrar en historial
            RegistrarAccion(
                $"✔️ Aprobó modificación del producto ID {idProducto} (solicitud #{idAprobacion})"
            );

            MessageBox.Show(
                "✅ Solicitud aprobada correctamente.",
                "Aprobación", MessageBoxButtons.OK, MessageBoxIcon.Information
            );

            CargarPendientes();
            CargarHistorial();


        }



        private void btnRechazarSolicitud_Click(object sender, EventArgs e)
        {

            if (dgvSolicitudesPendientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una solicitud para rechazar.");
                return;
            }

            int idAprobacion = Convert.ToInt32(dgvSolicitudesPendientes
                .SelectedRows[0].Tag);
            string motivo = Microsoft.VisualBasic.Interaction.InputBox(
                "Indica la razón del rechazo:", "Rechazar solicitud", "No especificado"
            );

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Se requiere una observacion para rechazar.");
                return;
            }

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();

                const string sql = @"
            UPDATE aprobaciones_almacen
               SET estado          = 'Rechazada',
                   observacion     = @obs,
                   usuario_responde= @uid,
                   nombre_responde = @nombre,
                   fecha_respuesta = NOW()
             WHERE id_aprobacion  = @idAprob";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@obs", motivo);
                    cmd.Parameters.AddWithValue("@uid", idAprobador);
                    cmd.Parameters.AddWithValue("@nombre", nombreAprobador);
                    cmd.Parameters.AddWithValue("@idAprob", idAprobacion);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show($"ID a rechazar: {idAprobacion}");
            RegistrarAccion($"Rechazo solicitud #{idAprobacion} – Motivo: {motivo}");

            MessageBox.Show(
                "Solicitud rechazada correctamente.",
                "Rechazo", MessageBoxButtons.OK, MessageBoxIcon.Warning
            );

            CargarPendientes();
            CargarHistorial();




        }
    }
}