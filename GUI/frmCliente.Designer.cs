namespace GUI
{
    partial class frmCliente
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
            btnVerDetalle = new Button();
            btnSuscribir = new Button();
            btnDesuscribir = new Button();
            btnPujar = new Button();
            cmbSubastas = new ComboBox();
            txtMonto = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lstNotificaciones = new ListBox();
            SuspendLayout();
            // 
            // btnVerDetalle
            // 
            btnVerDetalle.Location = new Point(12, 12);
            btnVerDetalle.Name = "btnVerDetalle";
            btnVerDetalle.Size = new Size(168, 47);
            btnVerDetalle.TabIndex = 0;
            btnVerDetalle.Text = "Ver Detalle Subasta";
            btnVerDetalle.UseVisualStyleBackColor = true;
            btnVerDetalle.Click += btnVerDetalle_Click;
            // 
            // btnSuscribir
            // 
            btnSuscribir.Location = new Point(12, 65);
            btnSuscribir.Name = "btnSuscribir";
            btnSuscribir.Size = new Size(168, 47);
            btnSuscribir.TabIndex = 1;
            btnSuscribir.Text = "Suscribir";
            btnSuscribir.UseVisualStyleBackColor = true;
            btnSuscribir.Click += btnSuscribir_Click;
            // 
            // btnDesuscribir
            // 
            btnDesuscribir.Location = new Point(12, 118);
            btnDesuscribir.Name = "btnDesuscribir";
            btnDesuscribir.Size = new Size(168, 47);
            btnDesuscribir.TabIndex = 2;
            btnDesuscribir.Text = "Desuscribir";
            btnDesuscribir.UseVisualStyleBackColor = true;
            btnDesuscribir.Click += btnDesuscribir_Click;
            // 
            // btnPujar
            // 
            btnPujar.Location = new Point(186, 118);
            btnPujar.Name = "btnPujar";
            btnPujar.Size = new Size(320, 47);
            btnPujar.TabIndex = 3;
            btnPujar.Text = "Pujar";
            btnPujar.UseVisualStyleBackColor = true;
            btnPujar.Click += btnPujar_Click;
            // 
            // cmbSubastas
            // 
            cmbSubastas.FormattingEnabled = true;
            cmbSubastas.Location = new Point(186, 36);
            cmbSubastas.Name = "cmbSubastas";
            cmbSubastas.Size = new Size(320, 23);
            cmbSubastas.TabIndex = 4;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(186, 89);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(320, 23);
            txtMonto.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(186, 18);
            label1.Name = "label1";
            label1.Size = new Size(117, 15);
            label1.TabIndex = 6;
            label1.Text = "Seleccionar Subasta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(186, 71);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 7;
            label2.Text = "Ingresar Monto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 190);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 8;
            label3.Text = "Notificaciones";
            // 
            // lstNotificaciones
            // 
            lstNotificaciones.FormattingEnabled = true;
            lstNotificaciones.Location = new Point(12, 208);
            lstNotificaciones.Name = "lstNotificaciones";
            lstNotificaciones.Size = new Size(776, 229);
            lstNotificaciones.TabIndex = 9;
            // 
            // frmCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lstNotificaciones);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMonto);
            Controls.Add(cmbSubastas);
            Controls.Add(btnPujar);
            Controls.Add(btnDesuscribir);
            Controls.Add(btnSuscribir);
            Controls.Add(btnVerDetalle);
            Name = "frmCliente";
            Text = "Menu Cliente";
            Load += frmCliente_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnVerDetalle;
        private Button btnSuscribir;
        private Button btnDesuscribir;
        private Button btnPujar;
        private ComboBox cmbSubastas;
        private TextBox txtMonto;
        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox lstNotificaciones;
    }
}