using BE.Entidades;
using BE.Interfaces;
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
    public partial class frmAdminSubastas : Form, INotificable
    {
        private readonly SubastaBLL _subastaBLL;
        private readonly UnidadVentaBLL _unidadVentaBLL;
        private readonly UsuarioBE _adminSesion;
        public frmAdminSubastas(UsuarioBE usuario, SubastaBLL subastaBLL)
        {
            InitializeComponent();
            _adminSesion = usuario;
            _subastaBLL = subastaBLL;
            _unidadVentaBLL = new UnidadVentaBLL();
        }
        private void frmAdminSubastas_Load(object sender, EventArgs e)
        {
            dgvSubastasActivas.MultiSelect = false;
            dgvSubastasActivas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ActualizarPantalla();
        }
        private void ActualizarPantalla()
        {
            try
            {
                cmbItemsDisponibles.DataSource = null;
                cmbItemsDisponibles.DataSource = _unidadVentaBLL.ObtenerDisponiblesParaSubasta();
                cmbItemsDisponibles.DisplayMember = "Nombre";
                dgvSubastasActivas.DataSource = null;
                var subastasActivas = _subastaBLL.ObtenerSubastasActivas();
                foreach (var subasta in subastasActivas)
                {
                    subasta.Suscribir(this);
                }
                dgvSubastasActivas.DataSource = new BindingList<SubastaBE>(subastasActivas);
                if (dgvSubastasActivas.Columns["PrecioActual"] != null)
                {
                    dgvSubastasActivas.Columns["PrecioActual"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Actualizar(SubastaBE subasta)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => dgvSubastasActivas.Refresh()));
            }
            else
            {
                dgvSubastasActivas.Refresh();
            }
        }
        private void btnCrearSubasta_Click(object sender, EventArgs e)
        {
            try
            {
                UnidadVentaBE itemSeleccionado = (UnidadVentaBE)cmbItemsDisponibles.SelectedItem;
                if (itemSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un ítem válido para subastar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _subastaBLL.CrearSubasta(itemSeleccionado);
                MessageBox.Show("¡Subasta publicada y notificada en tiempo real!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCerrarConGanador_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSubastasActivas.CurrentRow == null) return;
                SubastaBE subasta = (SubastaBE)dgvSubastasActivas.CurrentRow.DataBoundItem;
                _subastaBLL.CerrarSubastaConGanador(subasta);
                MessageBox.Show("Subasta cerrada. El ganador ha sido notificado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cerrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelarSubasta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSubastasActivas.CurrentRow == null) return;
                SubastaBE subasta = (SubastaBE)dgvSubastasActivas.CurrentRow.DataBoundItem;
                _subastaBLL.CancelarSubastaSinGanador(subasta);
                MessageBox.Show("Subasta cancelada con éxito. El ítem vuelve a estar disponible.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarPantalla();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cancelar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCrearLote_Click(object sender, EventArgs e)
        {
            frmCrearItems formCrear = new frmCrearItems(_adminSesion, _unidadVentaBLL);
            if (formCrear.ShowDialog() == DialogResult.OK)
            {
                ActualizarPantalla();
            }
        }
    }
}
