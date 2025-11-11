namespace Agro_UES.Formularios.Formgerente
{
    partial class FormSolicitudes
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRechazarSolicitud = new System.Windows.Forms.Button();
            this.dgvSolicitudesPendientes = new System.Windows.Forms.DataGridView();
            this.dgvIDProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFechaVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSolicita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFechaSolicitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAprobarProceso = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvHistorialSolicitudes = new System.Windows.Forms.DataGridView();
            this.dgvHistIDProd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistSolicita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistFechaSolicita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistAprobador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvHistFechaRespuesta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudesPendientes)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialSolicitudes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Honeydew;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnRechazarSolicitud);
            this.panel2.Controls.Add(this.dgvSolicitudesPendientes);
            this.panel2.Controls.Add(this.btnAprobarProceso);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1254, 382);
            this.panel2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pendiente de aprobar/rechazar";
            // 
            // btnRechazarSolicitud
            // 
            this.btnRechazarSolicitud.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnRechazarSolicitud.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRechazarSolicitud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRechazarSolicitud.ForeColor = System.Drawing.Color.White;
            this.btnRechazarSolicitud.Image = global::Agro_UES.Properties.Resources.rechazo;
            this.btnRechazarSolicitud.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRechazarSolicitud.Location = new System.Drawing.Point(639, 30);
            this.btnRechazarSolicitud.Name = "btnRechazarSolicitud";
            this.btnRechazarSolicitud.Size = new System.Drawing.Size(130, 40);
            this.btnRechazarSolicitud.TabIndex = 14;
            this.btnRechazarSolicitud.Text = "Rechazar";
            this.btnRechazarSolicitud.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRechazarSolicitud.UseVisualStyleBackColor = false;
            this.btnRechazarSolicitud.Click += new System.EventHandler(this.btnRechazarSolicitud_Click);
            // 
            // dgvSolicitudesPendientes
            // 
            this.dgvSolicitudesPendientes.AllowUserToAddRows = false;
            this.dgvSolicitudesPendientes.AllowUserToDeleteRows = false;
            this.dgvSolicitudesPendientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSolicitudesPendientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSolicitudesPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolicitudesPendientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvIDProducto,
            this.dgvDescripcion,
            this.dgvPrecio,
            this.dgvStock,
            this.dgvFechaVencimiento,
            this.dgvSolicita,
            this.dgvFechaSolicitud});
            this.dgvSolicitudesPendientes.Location = new System.Drawing.Point(16, 81);
            this.dgvSolicitudesPendientes.Name = "dgvSolicitudesPendientes";
            this.dgvSolicitudesPendientes.ReadOnly = true;
            this.dgvSolicitudesPendientes.RowHeadersWidth = 51;
            this.dgvSolicitudesPendientes.RowTemplate.Height = 24;
            this.dgvSolicitudesPendientes.Size = new System.Drawing.Size(1208, 271);
            this.dgvSolicitudesPendientes.TabIndex = 3;
            this.dgvSolicitudesPendientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSolicitudesPendientes_CellClick);
            this.dgvSolicitudesPendientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSolicitudesPendientes_CellContentClick);
            // 
            // dgvIDProducto
            // 
            this.dgvIDProducto.HeaderText = "ID";
            this.dgvIDProducto.MinimumWidth = 6;
            this.dgvIDProducto.Name = "dgvIDProducto";
            this.dgvIDProducto.ReadOnly = true;
            // 
            // dgvDescripcion
            // 
            this.dgvDescripcion.HeaderText = "Descripcion";
            this.dgvDescripcion.MinimumWidth = 6;
            this.dgvDescripcion.Name = "dgvDescripcion";
            this.dgvDescripcion.ReadOnly = true;
            // 
            // dgvPrecio
            // 
            this.dgvPrecio.HeaderText = "Precio";
            this.dgvPrecio.MinimumWidth = 6;
            this.dgvPrecio.Name = "dgvPrecio";
            this.dgvPrecio.ReadOnly = true;
            // 
            // dgvStock
            // 
            this.dgvStock.HeaderText = "Stock";
            this.dgvStock.MinimumWidth = 6;
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.ReadOnly = true;
            // 
            // dgvFechaVencimiento
            // 
            this.dgvFechaVencimiento.HeaderText = "Fecha Vencimiento";
            this.dgvFechaVencimiento.MinimumWidth = 6;
            this.dgvFechaVencimiento.Name = "dgvFechaVencimiento";
            this.dgvFechaVencimiento.ReadOnly = true;
            // 
            // dgvSolicita
            // 
            this.dgvSolicita.HeaderText = "Solicitado por";
            this.dgvSolicita.MinimumWidth = 6;
            this.dgvSolicita.Name = "dgvSolicita";
            this.dgvSolicita.ReadOnly = true;
            // 
            // dgvFechaSolicitud
            // 
            this.dgvFechaSolicitud.HeaderText = "Fecha";
            this.dgvFechaSolicitud.MinimumWidth = 6;
            this.dgvFechaSolicitud.Name = "dgvFechaSolicitud";
            this.dgvFechaSolicitud.ReadOnly = true;
            // 
            // btnAprobarProceso
            // 
            this.btnAprobarProceso.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnAprobarProceso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAprobarProceso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAprobarProceso.ForeColor = System.Drawing.Color.White;
            this.btnAprobarProceso.Image = global::Agro_UES.Properties.Resources.aprobar;
            this.btnAprobarProceso.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAprobarProceso.Location = new System.Drawing.Point(842, 30);
            this.btnAprobarProceso.Name = "btnAprobarProceso";
            this.btnAprobarProceso.Size = new System.Drawing.Size(130, 40);
            this.btnAprobarProceso.TabIndex = 14;
            this.btnAprobarProceso.Text = "Aprobar";
            this.btnAprobarProceso.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAprobarProceso.UseVisualStyleBackColor = false;
            this.btnAprobarProceso.Click += new System.EventHandler(this.btnAprobarProceso_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Honeydew;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.dgvHistorialSolicitudes);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 382);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1254, 402);
            this.panel3.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label8.Location = new System.Drawing.Point(3, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(355, 20);
            this.label8.TabIndex = 4;
            this.label8.Text = "Historial de solicitudes aprobadas/rechazadas";
            // 
            // dgvHistorialSolicitudes
            // 
            this.dgvHistorialSolicitudes.AllowUserToAddRows = false;
            this.dgvHistorialSolicitudes.AllowUserToDeleteRows = false;
            this.dgvHistorialSolicitudes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistorialSolicitudes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorialSolicitudes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorialSolicitudes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvHistIDProd,
            this.dgvHistDescripcion,
            this.dgvHistPrecio,
            this.dgvHistStock,
            this.dgvHistVencimiento,
            this.dgvHistEstado,
            this.dgvHistSolicita,
            this.dgvHistFechaSolicita,
            this.dgvHistAprobador,
            this.dgvHistFechaRespuesta});
            this.dgvHistorialSolicitudes.Location = new System.Drawing.Point(16, 109);
            this.dgvHistorialSolicitudes.Name = "dgvHistorialSolicitudes";
            this.dgvHistorialSolicitudes.ReadOnly = true;
            this.dgvHistorialSolicitudes.RowHeadersWidth = 51;
            this.dgvHistorialSolicitudes.RowTemplate.Height = 24;
            this.dgvHistorialSolicitudes.Size = new System.Drawing.Size(1208, 262);
            this.dgvHistorialSolicitudes.TabIndex = 3;
            this.dgvHistorialSolicitudes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistorialSolicitudes_CellClick);
            this.dgvHistorialSolicitudes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistorialSolicitudes_CellContentClick);
            // 
            // dgvHistIDProd
            // 
            this.dgvHistIDProd.HeaderText = "ID";
            this.dgvHistIDProd.MinimumWidth = 6;
            this.dgvHistIDProd.Name = "dgvHistIDProd";
            this.dgvHistIDProd.ReadOnly = true;
            // 
            // dgvHistDescripcion
            // 
            this.dgvHistDescripcion.HeaderText = "Descripcion";
            this.dgvHistDescripcion.MinimumWidth = 6;
            this.dgvHistDescripcion.Name = "dgvHistDescripcion";
            this.dgvHistDescripcion.ReadOnly = true;
            // 
            // dgvHistPrecio
            // 
            this.dgvHistPrecio.HeaderText = "Precio";
            this.dgvHistPrecio.MinimumWidth = 6;
            this.dgvHistPrecio.Name = "dgvHistPrecio";
            this.dgvHistPrecio.ReadOnly = true;
            // 
            // dgvHistStock
            // 
            this.dgvHistStock.HeaderText = "Stock";
            this.dgvHistStock.MinimumWidth = 6;
            this.dgvHistStock.Name = "dgvHistStock";
            this.dgvHistStock.ReadOnly = true;
            // 
            // dgvHistVencimiento
            // 
            this.dgvHistVencimiento.HeaderText = "Fecha Vencimineto";
            this.dgvHistVencimiento.MinimumWidth = 6;
            this.dgvHistVencimiento.Name = "dgvHistVencimiento";
            this.dgvHistVencimiento.ReadOnly = true;
            // 
            // dgvHistEstado
            // 
            this.dgvHistEstado.HeaderText = "Estado";
            this.dgvHistEstado.MinimumWidth = 6;
            this.dgvHistEstado.Name = "dgvHistEstado";
            this.dgvHistEstado.ReadOnly = true;
            // 
            // dgvHistSolicita
            // 
            this.dgvHistSolicita.HeaderText = "Solicitado por";
            this.dgvHistSolicita.MinimumWidth = 6;
            this.dgvHistSolicita.Name = "dgvHistSolicita";
            this.dgvHistSolicita.ReadOnly = true;
            // 
            // dgvHistFechaSolicita
            // 
            this.dgvHistFechaSolicita.HeaderText = "Fecha solicitud";
            this.dgvHistFechaSolicita.MinimumWidth = 6;
            this.dgvHistFechaSolicita.Name = "dgvHistFechaSolicita";
            this.dgvHistFechaSolicita.ReadOnly = true;
            // 
            // dgvHistAprobador
            // 
            this.dgvHistAprobador.HeaderText = "Accion por";
            this.dgvHistAprobador.MinimumWidth = 6;
            this.dgvHistAprobador.Name = "dgvHistAprobador";
            this.dgvHistAprobador.ReadOnly = true;
            // 
            // dgvHistFechaRespuesta
            // 
            this.dgvHistFechaRespuesta.HeaderText = "Fecha Respuesta";
            this.dgvHistFechaRespuesta.MinimumWidth = 6;
            this.dgvHistFechaRespuesta.Name = "dgvHistFechaRespuesta";
            this.dgvHistFechaRespuesta.ReadOnly = true;
            // 
            // FormSolicitudes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 784);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "FormSolicitudes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSolicitudes";
            this.Load += new System.EventHandler(this.FormSolicitudes_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudesPendientes)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialSolicitudes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSolicitudesPendientes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvHistorialSolicitudes;
        private System.Windows.Forms.Button btnAprobarProceso;
        private System.Windows.Forms.Button btnRechazarSolicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIDProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFechaVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSolicita;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFechaSolicitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistIDProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistSolicita;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistFechaSolicita;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistAprobador;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvHistFechaRespuesta;
    }
}