namespace GUI
{
    partial class frmCrearItems
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
            tvItems = new TreeView();
            txtNombre = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtDescripcion = new TextBox();
            label3 = new Label();
            numPrecio = new NumericUpDown();
            rbArticulo = new RadioButton();
            rbLote = new RadioButton();
            btnAgregarNodo = new Button();
            btnGuardarBD = new Button();
            ((System.ComponentModel.ISupportInitialize)numPrecio).BeginInit();
            SuspendLayout();
            // 
            // tvItems
            // 
            tvItems.Location = new Point(12, 12);
            tvItems.Name = "tvItems";
            tvItems.Size = new Size(420, 426);
            tvItems.TabIndex = 0;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(438, 30);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(223, 23);
            txtNombre.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(438, 12);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 2;
            label1.Text = "Nombre";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(438, 67);
            label2.Name = "label2";
            label2.Size = new Size(72, 15);
            label2.TabIndex = 4;
            label2.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(438, 85);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(223, 23);
            txtDescripcion.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(438, 124);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 6;
            label3.Text = "Precio";
            // 
            // numPrecio
            // 
            numPrecio.Location = new Point(438, 142);
            numPrecio.Name = "numPrecio";
            numPrecio.Size = new Size(223, 23);
            numPrecio.TabIndex = 7;
            // 
            // rbArticulo
            // 
            rbArticulo.AutoSize = true;
            rbArticulo.Location = new Point(438, 180);
            rbArticulo.Name = "rbArticulo";
            rbArticulo.Size = new Size(67, 19);
            rbArticulo.TabIndex = 8;
            rbArticulo.TabStop = true;
            rbArticulo.Text = "Articulo";
            rbArticulo.UseVisualStyleBackColor = true;
            rbArticulo.CheckedChanged += rbArticulo_CheckedChanged;
            // 
            // rbLote
            // 
            rbLote.AutoSize = true;
            rbLote.Location = new Point(438, 205);
            rbLote.Name = "rbLote";
            rbLote.Size = new Size(48, 19);
            rbLote.TabIndex = 9;
            rbLote.TabStop = true;
            rbLote.Text = "Lote";
            rbLote.UseVisualStyleBackColor = true;
            // 
            // btnAgregarNodo
            // 
            btnAgregarNodo.Location = new Point(438, 239);
            btnAgregarNodo.Name = "btnAgregarNodo";
            btnAgregarNodo.Size = new Size(223, 40);
            btnAgregarNodo.TabIndex = 10;
            btnAgregarNodo.Text = "Agregar al Árbol";
            btnAgregarNodo.UseVisualStyleBackColor = true;
            btnAgregarNodo.Click += btnAgregarNodo_Click;
            // 
            // btnGuardarBD
            // 
            btnGuardarBD.Location = new Point(438, 285);
            btnGuardarBD.Name = "btnGuardarBD";
            btnGuardarBD.Size = new Size(223, 40);
            btnGuardarBD.TabIndex = 11;
            btnGuardarBD.Text = "Confirmar y Guardar en BD";
            btnGuardarBD.UseVisualStyleBackColor = true;
            btnGuardarBD.Click += btnGuardarBD_Click;
            // 
            // frmCrearItems
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(676, 450);
            Controls.Add(btnGuardarBD);
            Controls.Add(btnAgregarNodo);
            Controls.Add(rbLote);
            Controls.Add(rbArticulo);
            Controls.Add(numPrecio);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtDescripcion);
            Controls.Add(label1);
            Controls.Add(txtNombre);
            Controls.Add(tvItems);
            Name = "frmCrearItems";
            Text = "Crear Nuevo Lote";
            Load += frmCrearItems_Load;
            ((System.ComponentModel.ISupportInitialize)numPrecio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView tvItems;
        private TextBox txtNombre;
        private Label label1;
        private Label label2;
        private TextBox txtDescripcion;
        private Label label3;
        private NumericUpDown numPrecio;
        private RadioButton rbArticulo;
        private RadioButton rbLote;
        private Button btnAgregarNodo;
        private Button btnGuardarBD;
    }
}