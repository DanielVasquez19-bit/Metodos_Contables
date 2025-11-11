using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Agro_UES.Formularios.Formgerente
{
    public partial class EstadísticasUsuarios: Form
    {
        public EstadísticasUsuarios()
        {
            InitializeComponent();
        }

        // 1) Clase auxiliar para el resumen de cada cajero
        public class CajeroResumen
        {
            public int IdUsuario { get; set; }
            public string Nombre { get; set; }
            public int TotalVentas { get; set; }
            public decimal MontoTotal { get; set; }
            public decimal Promedio { get; set; }
        }

        // 1) Resumen general y Top 3
        private void CargarResumenCajeros()
        {
            dgvCajeros.Rows.Clear();
            dgvTopCajeros.Rows.Clear();

            List<CajeroResumen> lista = new List<CajeroResumen>();

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                string sql = @"
            SELECT u.id_usuario,
                   u.nombre       AS cajero,
                   COUNT(v.id_venta)  AS total_ventas,
                   SUM(v.total)       AS monto_total,
                   ROUND(AVG(v.total), 2) AS promedio_venta
              FROM ventas v
              JOIN usuarios u ON v.usuario_id = u.id_usuario
             WHERE v.fecha_venta >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH)
             GROUP BY u.id_usuario, u.nombre
             ORDER BY monto_total DESC;";

                using (var cmd = new MySqlCommand(sql, conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        lista.Add(new CajeroResumen
                        {
                            IdUsuario = rdr.GetInt32("id_usuario"),
                            Nombre = rdr.GetString("cajero"),
                            TotalVentas = rdr.GetInt32("total_ventas"),
                            MontoTotal = rdr.GetDecimal("monto_total"),
                            Promedio = rdr.GetDecimal("promedio_venta")
                        });
                    }
                }
            }

            // Llenar dgvCajeros
            foreach (var x in lista)
                dgvCajeros.Rows.Add(x.IdUsuario, x.Nombre, x.TotalVentas, x.MontoTotal, x.Promedio);

            // Llenar dgvTopCajeros (top 3)
            for (int i = 0; i < Math.Min(3, lista.Count); i++)
            {
                var top = lista[i];
                dgvTopCajeros.Rows.Add(top.IdUsuario, top.Nombre, top.TotalVentas, top.MontoTotal);
            }
        }

        // 2) Al hacer clic en dgvCajeros, cargas el gráfico
        

        // 3) Gráfico de líneas por cajero usando fecha_venta
        private void CargarGraficoPorCajero(int idUsuario, string nombreUsuario)
        {
            chartVentas.Series.Clear();
            var serie = new Series($"Ventas diarias – {nombreUsuario}")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 6
            };

            using (var conn = ConexionDB.Conexion())
            {
                conn.Open();
                string sql = @"
            SELECT DATE(v.fecha_venta) AS dia,
                   SUM(v.total)         AS total_dia
              FROM ventas v
             WHERE v.usuario_id = @id
               AND v.fecha_venta >= DATE_SUB(CURDATE(), INTERVAL 1 MONTH)
             GROUP BY DATE(v.fecha_venta)
             ORDER BY dia;";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            DateTime dia = rdr.GetDateTime("dia");
                            decimal total = rdr.GetDecimal("total_dia");
                            serie.Points.AddXY(dia.ToString("MMM dd"), total);
                        }
                    }
                }
            }

            chartVentas.Series.Add(serie);
            chartVentas.ChartAreas[0].RecalculateAxesScale();
        }

        private void EstadísticasUsuarios_Load(object sender, EventArgs e)
        {
            CargarResumenCajeros();
            dgvCajeros.CellClick += dgvCajeros_CellClick;
        }

        private void dgvCajeros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int idUsuario = Convert.ToInt32(dgvCajeros
                .Rows[e.RowIndex]
                .Cells["id_usuario"].Value);
            string nombre = dgvCajeros
                .Rows[e.RowIndex]
                .Cells["cajero"].Value.ToString();

            CargarGraficoPorCajero(idUsuario, nombre);
        }

        // 5) En el Load de tu formulario de estadísticas

    }
}
