namespace Agro_UES
{
    partial class FormGerente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelEncabezadoGerente = new System.Windows.Forms.Panel();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblNombreSistema = new System.Windows.Forms.Label();
            this.panelOpcionesGerente = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEstadísticasDesempeño = new System.Windows.Forms.Button();
            this.btnProcesos = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panelPrincipalGerente = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panelEncabezadoGerente.SuspendLayout();
            this.panelOpcionesGerente.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEncabezadoGerente
            // 
            this.panelEncabezadoGerente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(242)))), ((int)(((byte)(234)))));
            this.panelEncabezadoGerente.Controls.Add(this.pictureBox2);
            this.panelEncabezadoGerente.Controls.Add(this.lblHora);
            this.panelEncabezadoGerente.Controls.Add(this.lblBienvenida);
            this.panelEncabezadoGerente.Controls.Add(this.lblNombreSistema);
            this.panelEncabezadoGerente.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezadoGerente.Location = new System.Drawing.Point(0, 0);
            this.panelEncabezadoGerente.Name = "panelEncabezadoGerente";
            this.panelEncabezadoGerente.Size = new System.Drawing.Size(1254, 95);
            this.panelEncabezadoGerente.TabIndex = 1;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(626, 52);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(44, 16);
            this.lblHora.TabIndex = 2;
            this.lblHora.Text = "label1";
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Location = new System.Drawing.Point(398, 52);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(44, 16);
            this.lblBienvenida.TabIndex = 2;
            this.lblBienvenida.Text = "label1";
            // 
            // lblNombreSistema
            // 
            this.lblNombreSistema.AutoSize = true;
            this.lblNombreSistema.Location = new System.Drawing.Point(160, 52);
            this.lblNombreSistema.Name = "lblNombreSistema";
            this.lblNombreSistema.Size = new System.Drawing.Size(67, 16);
            this.lblNombreSistema.TabIndex = 1;
            this.lblNombreSistema.Text = "Agro UES";
            // 
            // panelOpcionesGerente
            // 
            this.panelOpcionesGerente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(227)))), ((int)(((byte)(196)))));
            this.panelOpcionesGerente.Controls.Add(this.flowLayoutPanel1);
            this.panelOpcionesGerente.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelOpcionesGerente.Location = new System.Drawing.Point(0, 95);
            this.panelOpcionesGerente.Name = "panelOpcionesGerente";
            this.panelOpcionesGerente.Size = new System.Drawing.Size(227, 689);
            this.panelOpcionesGerente.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(227)))), ((int)(((byte)(196)))));
            this.flowLayoutPanel1.Controls.Add(this.btnEstadísticasDesempeño);
            this.flowLayoutPanel1.Controls.Add(this.btnProcesos);
            this.flowLayoutPanel1.Controls.Add(this.btnReportes);
            this.flowLayoutPanel1.Controls.Add(this.btnSalir);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(224, 656);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnEstadísticasDesempeño
            // 
            this.btnEstadísticasDesempeño.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnEstadísticasDesempeño.FlatAppearance.BorderSize = 0;
            this.btnEstadísticasDesempeño.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEstadísticasDesempeño.FlatAppearance.MouseOverBackColor = System.Drawing.Color.YellowGreen;
            this.btnEstadísticasDesempeño.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadísticasDesempeño.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstadísticasDesempeño.ForeColor = System.Drawing.Color.White;
            this.btnEstadísticasDesempeño.Image = global::Agro_UES.Properties.Resources.icons8_yield_68;
            this.btnEstadísticasDesempeño.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstadísticasDesempeño.Location = new System.Drawing.Point(3, 20);
            this.btnEstadísticasDesempeño.Margin = new System.Windows.Forms.Padding(3, 20, 3, 20);
            this.btnEstadísticasDesempeño.Name = "btnEstadísticasDesempeño";
            this.btnEstadísticasDesempeño.Size = new System.Drawing.Size(219, 68);
            this.btnEstadísticasDesempeño.TabIndex = 0;
            this.btnEstadísticasDesempeño.Text = "Estadísticas \r\nde desempeño\r\n";
            this.btnEstadísticasDesempeño.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEstadísticasDesempeño.UseVisualStyleBackColor = false;
            this.btnEstadísticasDesempeño.Click += new System.EventHandler(this.btnEstadísticasDesempeño_Click);
            // 
            // btnProcesos
            // 
            this.btnProcesos.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnProcesos.FlatAppearance.BorderSize = 0;
            this.btnProcesos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnProcesos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.YellowGreen;
            this.btnProcesos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesos.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnProcesos.ForeColor = System.Drawing.Color.White;
            this.btnProcesos.Image = global::Agro_UES.Properties.Resources.icons8_requisito_50;
            this.btnProcesos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProcesos.Location = new System.Drawing.Point(3, 128);
            this.btnProcesos.Margin = new System.Windows.Forms.Padding(3, 20, 3, 20);
            this.btnProcesos.Name = "btnProcesos";
            this.btnProcesos.Size = new System.Drawing.Size(219, 68);
            this.btnProcesos.TabIndex = 7;
            this.btnProcesos.Text = "Solicitudes de \r\naprobación";
            this.btnProcesos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcesos.UseVisualStyleBackColor = false;
            this.btnProcesos.Click += new System.EventHandler(this.btnProcesos_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnReportes.FlatAppearance.BorderSize = 0;
            this.btnReportes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReportes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.YellowGreen;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Image = global::Agro_UES.Properties.Resources.icons8_estadísticas_321;
            this.btnReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportes.Location = new System.Drawing.Point(3, 236);
            this.btnReportes.Margin = new System.Windows.Forms.Padding(3, 20, 3, 20);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(219, 68);
            this.btnReportes.TabIndex = 9;
            this.btnReportes.Text = "Reportes ";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Firebrick;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(3, 327);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(5, 5, 10, 5);
            this.btnSalir.Size = new System.Drawing.Size(219, 68);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "Cerrar Sesion";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panelPrincipalGerente
            // 
            this.panelPrincipalGerente.BackColor = System.Drawing.Color.Honeydew;
            this.panelPrincipalGerente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipalGerente.Location = new System.Drawing.Point(227, 95);
            this.panelPrincipalGerente.Name = "panelPrincipalGerente";
            this.panelPrincipalGerente.Size = new System.Drawing.Size(1027, 689);
            this.panelPrincipalGerente.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
            this.pictureBox2.Image = global::Agro_UES.Properties.Resources.logo2;
            this.pictureBox2.Location = new System.Drawing.Point(0, -2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(93, 94);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // FormGerente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 784);
            this.Controls.Add(this.panelPrincipalGerente);
            this.Controls.Add(this.panelOpcionesGerente);
            this.Controls.Add(this.panelEncabezadoGerente);
            this.Name = "FormGerente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGerente";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGerente_FormClosing);
            this.panelEncabezadoGerente.ResumeLayout(false);
            this.panelEncabezadoGerente.PerformLayout();
            this.panelOpcionesGerente.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEncabezadoGerente;
        private System.Windows.Forms.Label lblNombreSistema;
        private System.Windows.Forms.Panel panelOpcionesGerente;
        private System.Windows.Forms.Button btnEstadísticasDesempeño;
        private System.Windows.Forms.Button btnProcesos;
        private System.Windows.Forms.Panel panelPrincipalGerente;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}