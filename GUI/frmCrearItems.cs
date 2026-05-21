using BE.Entidades;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmCrearItems : Form
    {
        private readonly UnidadVentaBLL _unidadVentaBLL;
        private readonly UsuarioBE _adminSesion;
        private LoteBE _loteRaiz;
        public frmCrearItems(UsuarioBE usuario, UnidadVentaBLL unidadVentaBLL)
        {
            InitializeComponent();
            _adminSesion = usuario;
            _unidadVentaBLL = unidadVentaBLL;
        }
        private void rbArticulo_CheckedChanged(object sender, EventArgs e)
        {
            numPrecio.Enabled = rbArticulo.Checked;
            if (rbLote.Checked) numPrecio.Value = 0;
        }
        private void btnAgregarNodo_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tvItems.Nodes.Count == 0)
            {
                _loteRaiz = new LoteBE { Nombre = nombre, Descripcion = descripcion };
                TreeNode nodoRaiz = new TreeNode($"[LOTE] {_loteRaiz.Nombre}") { Tag = _loteRaiz };
                tvItems.Nodes.Add(nodoRaiz);
                rbArticulo.Enabled = true;
                numPrecio.Enabled = true;
            }
            else
            {
                if (tvItems.SelectedNode == null)
                {
                    MessageBox.Show("Seleccione un Lote en el árbol para agregarle un ítem.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UnidadVentaBE padre = (UnidadVentaBE)tvItems.SelectedNode.Tag;
                if (padre is ArticuloBE)
                {
                    MessageBox.Show("No se le pueden agregar ítems a un Artículo. Seleccione un Lote.", "Operación Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                UnidadVentaBE nuevoHijo;
                TreeNode nodoHijo;
                if (rbLote.Checked)
                {
                    nuevoHijo = new LoteBE { Nombre = nombre, Descripcion = descripcion };
                    nodoHijo = new TreeNode($"[LOTE] {nuevoHijo.Nombre}") { Tag = nuevoHijo };
                }
                else
                {
                    nuevoHijo = new ArticuloBE
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        PrecioBaseHistorico = numPrecio.Value
                    };
                    nodoHijo = new TreeNode($"[ART] {nuevoHijo.Nombre} (${nuevoHijo.PrecioBaseHistorico})") { Tag = nuevoHijo };
                }
                ((LoteBE)padre).Agregar(nuevoHijo);
                tvItems.SelectedNode.Nodes.Add(nodoHijo);
                tvItems.SelectedNode.ExpandAll();
            }
            txtNombre.Clear();
            txtDescripcion.Clear();
            numPrecio.Value = 0;
            txtNombre.Focus();
        }
        private void frmCrearItems_Load(object sender, EventArgs e)
        {
            numPrecio.Maximum = 1000000;
            rbLote.Checked = true;
            rbArticulo.Enabled = false;
            numPrecio.Value = 0;
            numPrecio.Enabled = false;
        }
        private void btnGuardarBD_Click(object sender, EventArgs e)
        {
            if (_loteRaiz == null)
            {
                MessageBox.Show("Debe crear al menos un Lote padre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _unidadVentaBLL.GuardarUnidadVenta(_loteRaiz);
                MessageBox.Show("Lote guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
