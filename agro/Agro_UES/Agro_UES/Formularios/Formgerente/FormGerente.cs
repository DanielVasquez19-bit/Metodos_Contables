using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Agro_UES.Formularios.FormReportes;
using Agro_UES.Formularios.Formgerente;
using Agro_UES.Formularios.FormLogin;

namespace Agro_UES
{
    public partial class FormGerente: Form
    {
        private int idUsuarioActual;
        private string nombreUsuarioActual;
        private string rolUsuarioActual;
        public FormGerente(int id, string nombre, string rol)
        {
            InitializeComponent();
            idUsuarioActual = id;
            nombreUsuarioActual = nombre;
            rolUsuarioActual = rol;
            lblBienvenida.Text = $"Bienvenido, {nombreUsuarioActual}";


        }

        private void picSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void abrirFormularios(Object formHija)
        {
            if (this.panelPrincipalGerente.Controls.Count > 0)
                this.panelPrincipalGerente.Controls.RemoveAt(0);

            Form fh = formHija as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None; // Esto elimina el marco
            fh.Dock = DockStyle.Fill;
            this.panelPrincipalGerente.Controls.Add(fh);
            this.panelPrincipalGerente.Tag = fh;
            fh.Show();
        }



        private void btnReportes_Click(object sender, EventArgs e)
        {
            abrirFormularios(new FormReportes(idUsuarioActual, nombreUsuarioActual));
        }

        private void btnProcesos_Click(object sender, EventArgs e)
        {
            abrirFormularios(new FormSolicitudes(idUsuarioActual, nombreUsuarioActual, rolUsuarioActual));

        }

        private void btnEstadísticasDesempeño_Click(object sender, EventArgs e)
        {
            abrirFormularios(new EstadísticasUsuarios());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy – HH:mm:ss");
        }

        private void FormGerente_FormClosing(object sender, FormClosingEventArgs e)
        {
            SesionHelper.MarcarSesionInactiva(idUsuarioActual);
        }
    }
}
