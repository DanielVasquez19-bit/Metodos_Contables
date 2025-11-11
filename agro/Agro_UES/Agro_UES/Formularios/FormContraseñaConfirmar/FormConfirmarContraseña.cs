using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agro_UES.FormContraseñaConfirmar
{
    public partial class FormConfirmarContraseña: Form
    {
        // Propiedad para almacenar la contraseña ingresada*/
        public string ContraseñaIngresada { get; private set; } = "";

        public FormConfirmarContraseña()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContraseña.Text))
            {
                MessageBox.Show("Por favor escribi tu contraseña");
                return;
            }

            ContraseñaIngresada = txtContraseña.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /*
         * Evento que se ejecuta al cargar el formulario se usa para establecer el foco en el campo de contraseña.
         */
        private void FormConfirmarContrasena_Load(object sender, EventArgs e)
        {
            txtContraseña.Focus();
        }

    }
}
