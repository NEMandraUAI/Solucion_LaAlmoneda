using BE.Entidades;
using BE.Excepciones;
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
    public partial class frmCliente : Form, INotificable
    {
        private readonly SubastaBLL _subastaBLL;
        private readonly UsuarioBLL _usuarioBLL;
        private readonly SuscripcionBLL _suscripcionBLL;
        private SubastaBE _subastaActual;
        private UsuarioBE _usuarioActual;
        public frmCliente(SubastaBLL pSubastaBLL, UsuarioBE usuarioLogueado)
        {
            InitializeComponent();
            _subastaBLL = pSubastaBLL;
            _usuarioBLL = new UsuarioBLL();
            _suscripcionBLL = new SuscripcionBLL();
            _usuarioActual = usuarioLogueado;
            this.FormClosed += FormCliente_FormClosed;
        }
        private void frmCliente_Load(object sender, EventArgs e)
        {
            this.Text = $"Panel de Subasta - Logueado como: {_usuarioActual.Nombre}";
            CargarDatosDesdeBaseDatos();
            ConfigurarEventosCombos();
            _subastaBLL.SuscribirClienteGlobal(this);
            List<int> misSuscripciones = _suscripcionBLL.ObtenerIdsSuscripciones(_usuarioActual.Id);
            if (misSuscripciones != null)
            {
                foreach (SubastaBE subasta in cmbSubastas.Items)
                {
                    if (misSuscripciones.Contains(subasta.Id))
                    {
                        subasta.Suscribir(this);
                        lstNotificaciones.Items.Add($"> Recuperaste alerta de: {subasta.UnidadVenta.Nombre}");
                    }
                }
            }
        }
        private void CargarDatosDesdeBaseDatos()
        {
            try
            {
                cmbSubastas.DataSource = _subastaBLL.ObtenerSubastasActivas();
                cmbSubastas.DisplayMember = "NombreDisplay";
                cmbSubastas.ValueMember = "Id";

                if (cmbSubastas.Items.Count > 0) _subastaActual = (SubastaBE)cmbSubastas.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico de conectividad SQL: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigurarEventosCombos()
        {
            cmbSubastas.SelectedIndexChanged += (s, e) =>
            {
                _subastaActual = (SubastaBE)cmbSubastas.SelectedItem;
            };
        }
        private void FormCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            _usuarioBLL.Logout(_usuarioActual.Id);
            _subastaBLL.DesuscribirClienteGlobal(this);
            if (_subastaActual != null && _subastaActual.EstaSuscrito(this))
            {
                _subastaActual.Desuscribir(this);
            }
        }
        public void Actualizar(SubastaBE subasta)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ActualizarUI(subasta)));
            }
            else
            {
                ActualizarUI(subasta);
            }
        }
        private void ActualizarUI(SubastaBE subasta)
        {
            string mensaje = "";
            if (subasta.Estado == "Cancelada")
            {
                mensaje = $"[{DateTime.Now:HH:mm:ss}] ¡ATENCIÓN! La subasta '{subasta.UnidadVenta.Nombre}' fue CANCELADA y retirada.";
            }
            else if (subasta.Estado == "Cerrada")
            {
                string nombreGanador = subasta.Ganador != null ? subasta.Ganador.Nombre : "Desconocido";
                mensaje = $"[{DateTime.Now:HH:mm:ss}] ¡SUBASTA CERRADA! '{subasta.UnidadVenta.Nombre}' adjudicada a {nombreGanador} por ${subasta.PrecioActual}.";
            }
            else if (subasta.Estado == "Abierta" && !subasta.TienePujas)
            {
                mensaje = $"[{DateTime.Now:HH:mm:ss}] ¡NUEVA SUBASTA PUBLICADA! '{subasta.UnidadVenta.Nombre}' disponible con base de ${subasta.PrecioActual}.";
            }
            else
            {
                string nombreLider = subasta.Ganador != null ? subasta.Ganador.Nombre : "Alguien";
                mensaje = $"[{DateTime.Now:HH:mm:ss}] ¡NUEVA PUJA! '{subasta.UnidadVenta.Nombre}' subió a ${subasta.PrecioActual} (Líder: {nombreLider})";
            }
            lstNotificaciones.Items.Add(mensaje);
            lstNotificaciones.TopIndex = lstNotificaciones.Items.Count - 1;
            CargarDatosDesdeBaseDatos();
        }
        private void btnSuscribir_Click(object sender, EventArgs e)
        {
            if (_subastaActual != null)
            {
                if (_subastaActual.EstaSuscrito(this))
                {
                    MessageBox.Show("Ya estás suscrito a esta subasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _suscripcionBLL.SuscribirUsuario(_usuarioActual.Id, _subastaActual.Id);
                _subastaActual.Suscribir(this);
                lstNotificaciones.Items.Add($"> Te suscribiste a las alertas de: {_subastaActual.UnidadVenta.Nombre}");
            }
        }
        private void btnDesuscribir_Click(object sender, EventArgs e)
        {
            if (_subastaActual != null)
            {
                if (!_subastaActual.EstaSuscrito(this))
                {
                    MessageBox.Show("No estás suscrito a esta subasta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                _suscripcionBLL.DesuscribirUsuario(_usuarioActual.Id, _subastaActual.Id);
                _subastaActual.Desuscribir(this);
                lstNotificaciones.Items.Add($"> Cancelaste suscripción a: {_subastaActual.UnidadVenta.Nombre}");
            }
        }
        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (_subastaActual != null)
            {
                MessageBox.Show(_subastaActual.UnidadVenta.ObtenerDetalle(), "Catálogo Desglosado (Patrón Composite)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnPujar_Click(object sender, EventArgs e)
        {
            if (_subastaActual == null || _usuarioActual == null) return;
            try
            {
                if (!_subastaActual.EstaSuscrito(this))
                {
                    _suscripcionBLL.SuscribirUsuario(_usuarioActual.Id, _subastaActual.Id);
                    _subastaActual.Suscribir(this);
                    lstNotificaciones.Items.Add($"> Te suscribiste a las alertas de: {_subastaActual.UnidadVenta.Nombre}");
                }
                decimal monto = decimal.Parse(txtMonto.Text);
                _subastaBLL.ProcesarOferta(_subastaActual, _usuarioActual, monto);
                txtMonto.Clear();
            }
            catch (PujaInvalidaException ex)
            {
                MessageBox.Show(ex.Message, "Validación de Negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ConcurrenciaException ex)
            {
                MessageBox.Show(ex.Message, "Concurrencia Bloqueada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarDatosDesdeBaseDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
