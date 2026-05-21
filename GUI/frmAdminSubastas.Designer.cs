namespace GUI
{
    partial class frmAdminSubastas
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
            cmbItemsDisponibles = new ComboBox();
            btnCrearSubasta = new Button();
            dgvSubastasActivas = new DataGridView();
            btnCerrarConGanador = new Button();
            btnCancelarSubasta = new Button();
            label1 = new Label();
            btnCrearLote = new Button();
            label2 = new Label();
            btnGenerarInforme = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSubastasActivas).BeginInit();
            SuspendLayout();
            // 
            // cmbItemsDisponibles
            // 
            cmbItemsDisponibles.FormattingEnabled = true;
            cmbItemsDisponibles.Location = new Point(12, 36);
            cmbItemsDisponibles.Name = "cmbItemsDisponibles";
            cmbItemsDisponibles.Size = new Size(222, 23);
            cmbItemsDisponibles.TabIndex = 0;
            // 
            // btnCrearSubasta
            // 
            btnCrearSubasta.Location = new Point(12, 86);
            btnCrearSubasta.Name = "btnCrearSubasta";
            btnCrearSubasta.Size = new Size(222, 43);
            btnCrearSubasta.TabIndex = 1;
            btnCrearSubasta.Text = "Lanzar Subasta Abierta";
            btnCrearSubasta.UseVisualStyleBackColor = true;
            btnCrearSubasta.Click += btnCrearSubasta_Click;
            // 
            // dgvSubastasActivas
            // 
            dgvSubastasActivas.AllowUserToAddRows = false;
            dgvSubastasActivas.AllowUserToDeleteRows = false;
            dgvSubastasActivas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSubastasActivas.Location = new Point(12, 184);
            dgvSubastasActivas.Name = "dgvSubastasActivas";
            dgvSubastasActivas.ReadOnly = true;
            dgvSubastasActivas.Size = new Size(776, 353);
            dgvSubastasActivas.TabIndex = 2;
            // 
            // btnCerrarConGanador
            // 
            btnCerrarConGanador.Location = new Point(470, 555);
            btnCerrarConGanador.Name = "btnCerrarConGanador";
            btnCerrarConGanador.Size = new Size(156, 53);
            btnCerrarConGanador.TabIndex = 3;
            btnCerrarConGanador.Text = "Cerrar Subasta";
            btnCerrarConGanador.UseVisualStyleBackColor = true;
            btnCerrarConGanador.Click += btnCerrarConGanador_Click;
            // 
            // btnCancelarSubasta
            // 
            btnCancelarSubasta.Location = new Point(632, 555);
            btnCancelarSubasta.Name = "btnCancelarSubasta";
            btnCancelarSubasta.Size = new Size(156, 53);
            btnCancelarSubasta.TabIndex = 4;
            btnCancelarSubasta.Text = "Cancelar Subasta";
            btnCancelarSubasta.UseVisualStyleBackColor = true;
            btnCancelarSubasta.Click += btnCancelarSubasta_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 5;
            label1.Text = "Lotes Disponibles";
            // 
            // btnCrearLote
            // 
            btnCrearLote.Location = new Point(12, 555);
            btnCrearLote.Name = "btnCrearLote";
            btnCrearLote.Size = new Size(156, 53);
            btnCrearLote.TabIndex = 6;
            btnCrearLote.Text = "Crear Nuevo Lote";
            btnCrearLote.UseVisualStyleBackColor = true;
            btnCrearLote.Click += btnCrearLote_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 166);
            label2.Name = "label2";
            label2.Size = new Size(98, 15);
            label2.TabIndex = 7;
            label2.Text = "Subastas Activas";
            // 
            // btnGenerarInforme
            // 
            btnGenerarInforme.Location = new Point(174, 555);
            btnGenerarInforme.Name = "btnGenerarInforme";
            btnGenerarInforme.Size = new Size(156, 53);
            btnGenerarInforme.TabIndex = 8;
            btnGenerarInforme.Text = "Generar Informe";
            btnGenerarInforme.UseVisualStyleBackColor = true;
            btnGenerarInforme.Click += btnGenerarInforme_Click;
            // 
            // frmAdminSubastas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 620);
            Controls.Add(btnGenerarInforme);
            Controls.Add(label2);
            Controls.Add(btnCrearLote);
            Controls.Add(label1);
            Controls.Add(btnCancelarSubasta);
            Controls.Add(btnCerrarConGanador);
            Controls.Add(dgvSubastasActivas);
            Controls.Add(btnCrearSubasta);
            Controls.Add(cmbItemsDisponibles);
            Name = "frmAdminSubastas";
            Text = "Administrar Subastas";
            Load += frmAdminSubastas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSubastasActivas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbItemsDisponibles;
        private Button btnCrearSubasta;
        private DataGridView dgvSubastasActivas;
        private Button btnCerrarConGanador;
        private Button btnCancelarSubasta;
        private Label label1;
        private Button btnCrearLote;
        private Label label2;
        private Button btnGenerarInforme;
    }
}