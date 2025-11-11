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
using static Agro_UES.FormLogin;

namespace Agro_UES
{
    public partial class Menú: Form
    {


        public Menú()
        {
            InitializeComponent();
        }

        private void btnGestionProductos_Click(object sender, EventArgs e)
        {
            this.Hide();
            GestionProductos frm = new GestionProductos();
            frm.FormClosed += (s, args) => this.Close();
            frm.Show();
        }

        private void btnGestionUsuarios_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registrousuarios frm = new Registrousuarios();
            frm.FormClosed += (s, args) => this.Close();
            frm.Show();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {

        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {

        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportesGenerales frm = new ReportesGenerales();
            frm.FormClosed += (s, args) => this.Close();
            frm.Show();
        }

        private void btnAprobacionesGerenciales_Click(object sender, EventArgs e)
        {

        }
        private void Menú_FormClosing(object sender, FormClosingEventArgs e)
        {
            SesionUsuario.NotificarFormularioCerrado();
        }

        private void Menú_Load(object sender, EventArgs e)
        {
            SesionUsuario.NotificarFormularioAbierto();
        }
    }
}
