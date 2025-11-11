namespace Agro_UES.Formularios.Formgerente
{
    partial class EstadísticasUsuarios
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.dgvCajeros = new System.Windows.Forms.DataGridView();
            this.id_usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_ventas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.promedio_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartVentas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTopCajeros = new System.Windows.Forms.DataGridView();
            this.usuario_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cajero3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_ventas3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto_total3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.promedio_venta3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajeros)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVentas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCajeros)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCajeros
            // 
            this.dgvCajeros.AllowUserToAddRows = false;
            this.dgvCajeros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCajeros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCajeros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_usuario,
            this.cajero,
            this.total_ventas,
            this.monto_total,
            this.promedio_venta});
            this.dgvCajeros.Location = new System.Drawing.Point(12, 54);
            this.dgvCajeros.Name = "dgvCajeros";
            this.dgvCajeros.RowHeadersWidth = 51;
            this.dgvCajeros.RowTemplate.Height = 24;
            this.dgvCajeros.Size = new System.Drawing.Size(870, 292);
            this.dgvCajeros.TabIndex = 0;
            this.dgvCajeros.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCajeros_CellClick);
            // 
            // id_usuario
            // 
            this.id_usuario.HeaderText = "ID";
            this.id_usuario.MinimumWidth = 6;
            this.id_usuario.Name = "id_usuario";
            this.id_usuario.ReadOnly = true;
            this.id_usuario.Visible = false;
            // 
            // cajero
            // 
            this.cajero.HeaderText = "Cajero";
            this.cajero.MinimumWidth = 6;
            this.cajero.Name = "cajero";
            this.cajero.ReadOnly = true;
            // 
            // total_ventas
            // 
            this.total_ventas.HeaderText = "Total ventas";
            this.total_ventas.MinimumWidth = 6;
            this.total_ventas.Name = "total_ventas";
            this.total_ventas.ReadOnly = true;
            // 
            // monto_total
            // 
            this.monto_total.HeaderText = "Monto total";
            this.monto_total.MinimumWidth = 6;
            this.monto_total.Name = "monto_total";
            this.monto_total.ReadOnly = true;
            // 
            // promedio_venta
            // 
            this.promedio_venta.HeaderText = "Promedio de venta";
            this.promedio_venta.MinimumWidth = 6;
            this.promedio_venta.Name = "promedio_venta";
            this.promedio_venta.ReadOnly = true;
            // 
            // chartVentas
            // 
            chartArea1.Name = "MainArea";
            this.chartVentas.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartVentas.Legends.Add(legend1);
            this.chartVentas.Location = new System.Drawing.Point(445, 365);
            this.chartVentas.Name = "chartVentas";
            this.chartVentas.Size = new System.Drawing.Size(453, 212);
            this.chartVentas.TabIndex = 2;
            this.chartVentas.Text = "chart2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(172, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // dgvTopCajeros
            // 
            this.dgvTopCajeros.AllowUserToAddRows = false;
            this.dgvTopCajeros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopCajeros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopCajeros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usuario_id,
            this.cajero3,
            this.total_ventas3,
            this.monto_total3,
            this.promedio_venta3});
            this.dgvTopCajeros.Location = new System.Drawing.Point(12, 365);
            this.dgvTopCajeros.Name = "dgvTopCajeros";
            this.dgvTopCajeros.RowHeadersWidth = 51;
            this.dgvTopCajeros.RowTemplate.Height = 24;
            this.dgvTopCajeros.Size = new System.Drawing.Size(416, 212);
            this.dgvTopCajeros.TabIndex = 0;
            // 
            // usuario_id
            // 
            this.usuario_id.HeaderText = "ID";
            this.usuario_id.MinimumWidth = 6;
            this.usuario_id.Name = "usuario_id";
            this.usuario_id.ReadOnly = true;
            this.usuario_id.Visible = false;
            // 
            // cajero3
            // 
            this.cajero3.HeaderText = "Cajero";
            this.cajero3.MinimumWidth = 6;
            this.cajero3.Name = "cajero3";
            this.cajero3.ReadOnly = true;
            // 
            // total_ventas3
            // 
            this.total_ventas3.HeaderText = "Total ventas";
            this.total_ventas3.MinimumWidth = 6;
            this.total_ventas3.Name = "total_ventas3";
            this.total_ventas3.ReadOnly = true;
            // 
            // monto_total3
            // 
            this.monto_total3.HeaderText = "Monto total";
            this.monto_total3.MinimumWidth = 6;
            this.monto_total3.Name = "monto_total3";
            this.monto_total3.ReadOnly = true;
            // 
            // promedio_venta3
            // 
            this.promedio_venta3.HeaderText = "Promedio de venta";
            this.promedio_venta3.MinimumWidth = 6;
            this.promedio_venta3.Name = "promedio_venta3";
            this.promedio_venta3.ReadOnly = true;
            // 
            // EstadísticasUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(910, 602);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartVentas);
            this.Controls.Add(this.dgvTopCajeros);
            this.Controls.Add(this.dgvCajeros);
            this.Name = "EstadísticasUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EstadísticasUsuarios";
            this.Load += new System.EventHandler(this.EstadísticasUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajeros)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVentas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCajeros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCajeros;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVentas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn cajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_ventas;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn promedio_venta;
        private System.Windows.Forms.DataGridView dgvTopCajeros;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cajero3;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_ventas3;
        private System.Windows.Forms.DataGridViewTextBoxColumn monto_total3;
        private System.Windows.Forms.DataGridViewTextBoxColumn promedio_venta3;
    }
}