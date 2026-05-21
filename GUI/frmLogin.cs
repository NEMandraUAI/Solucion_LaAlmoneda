using BE.Entidades;
using BE.Excepciones;
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
    public partial class frmLogin : Form
    {
        private readonly UsuarioBLL _usuarioBLL;
        private readonly SubastaBLL _subastaBLL;
        public frmLogin(UsuarioBLL pUsuarioBLL, SubastaBLL pSubastaBLL)
        {
            InitializeComponent();
            _usuarioBLL = pUsuarioBLL;
            _subastaBLL = pSubastaBLL;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UsuarioBE usuarioAutenticado = _usuarioBLL.Login(txtEmail.Text, txtContrasena.Text);
                if (usuarioAutenticado.EsAdmin)
                {
                    frmAdminSubastas frmAdmin = new frmAdminSubastas(usuarioAutenticado, _subastaBLL);
                    frmAdmin.MdiParent = this.MdiParent;
                    frmAdmin.Show();
                }
                else
                {
                    frmCliente frmCli = new frmCliente(_subastaBLL, usuarioAutenticado);
                    frmCli.MdiParent = this.MdiParent;
                    frmCli.Show();
                }
                this.Close();
            }
            catch (LoginException ex)
            {
                MessageBox.Show(ex.Message, "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico del sistema: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
