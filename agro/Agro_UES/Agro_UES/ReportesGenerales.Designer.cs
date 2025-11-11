namespace Agro_UES
{
    partial class ReportesGenerales
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
            this.cbMeses = new System.Windows.Forms.ComboBox();
            this.cbAños = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvIdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvVentasTotales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPromedioVentasMensuales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPromedioVentasSemanales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProductosVendidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbMeses
            // 
            this.cbMeses.FormattingEnabled = true;
            this.cbMeses.Items.AddRange(new object[] {
            "Todo",
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Septiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"});
            this.cbMeses.Location = new System.Drawing.Point(645, 68);
            this.cbMeses.Name = "cbMeses";
            this.cbMeses.Size = new System.Drawing.Size(121, 24);
            this.cbMeses.TabIndex = 0;
            this.cbMeses.SelectedIndexChanged += new System.EventHandler(this.cbMeses_SelectedIndexChanged);
            // 
            // cbAños
            // 
            this.cbAños.FormattingEnabled = true;
            this.cbAños.Items.AddRange(new object[] {
            "Todo",
            "2024",
            "2025"});
            this.cbAños.Location = new System.Drawing.Point(518, 68);
            this.cbAños.Name = "cbAños";
            this.cbAños.Size = new System.Drawing.Size(121, 24);
            this.cbAños.TabIndex = 0;
            this.cbAños.SelectedIndexChanged += new System.EventHandler(this.cbAños_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvIdProducto,
            this.dgvNombreProducto,
            this.dgvVentasTotales,
            this.dgvPromedioVentasMensuales,
            this.dgvPromedioVentasSemanales,
            this.dgvProductosVendidos});
            this.dataGridView1.Location = new System.Drawing.Point(12, 113);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(776, 340);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dgvIdProducto
            // 
            this.dgvIdProducto.HeaderText = "id producto";
            this.dgvIdProducto.MinimumWidth = 6;
            this.dgvIdProducto.Name = "dgvIdProducto";
            this.dgvIdProducto.ReadOnly = true;
            // 
            // dgvNombreProducto
            // 
            this.dgvNombreProducto.HeaderText = "Nombre del producto";
            this.dgvNombreProducto.MinimumWidth = 6;
            this.dgvNombreProducto.Name = "dgvNombreProducto";
            this.dgvNombreProducto.ReadOnly = true;
            // 
            // dgvVentasTotales
            // 
            this.dgvVentasTotales.HeaderText = "Ventas totales";
            this.dgvVentasTotales.MinimumWidth = 6;
            this.dgvVentasTotales.Name = "dgvVentasTotales";
            this.dgvVentasTotales.ReadOnly = true;
            // 
            // dgvPromedioVentasMensuales
            // 
            this.dgvPromedioVentasMensuales.HeaderText = "Promedio (mensual)";
            this.dgvPromedioVentasMensuales.MinimumWidth = 6;
            this.dgvPromedioVentasMensuales.Name = "dgvPromedioVentasMensuales";
            this.dgvPromedioVentasMensuales.ReadOnly = true;
            // 
            // dgvPromedioVentasSemanales
            // 
            this.dgvPromedioVentasSemanales.HeaderText = "Promedio (semanal)";
            this.dgvPromedioVentasSemanales.MinimumWidth = 6;
            this.dgvPromedioVentasSemanales.Name = "dgvPromedioVentasSemanales";
            this.dgvPromedioVentasSemanales.ReadOnly = true;
            // 
            // dgvProductosVendidos
            // 
            this.dgvProductosVendidos.HeaderText = "Productos Vendidos";
            this.dgvProductosVendidos.MinimumWidth = 6;
            this.dgvProductosVendidos.Name = "dgvProductosVendidos";
            this.dgvProductosVendidos.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Showcard Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(580, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "Venta de productos por intervalos de tiempo";
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExportarPDF.Location = new System.Drawing.Point(404, 68);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(85, 24);
            this.btnExportarPDF.TabIndex = 3;
            this.btnExportarPDF.Text = "Exportar";
            this.btnExportarPDF.UseVisualStyleBackColor = false;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // ReportesGenerales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(800, 493);
            this.Controls.Add(this.btnExportarPDF);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbAños);
            this.Controls.Add(this.cbMeses);
            this.Name = "ReportesGenerales";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportesGenerales_FormClosing);
            this.Load += new System.EventHandler(this.ReportesGenerales_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMeses;
        private System.Windows.Forms.ComboBox cbAños;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvIdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvVentasTotales;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPromedioVentasMensuales;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPromedioVentasSemanales;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProductosVendidos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportarPDF;
    }
}