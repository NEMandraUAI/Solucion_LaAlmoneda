using BLL;

namespace GUI
{
    public partial class frmPrincipal : Form
    {
        private readonly SubastaBLL _subastaBLL;
        private readonly UsuarioBLL _usuarioBLL;
        public frmPrincipal()
        {
            InitializeComponent();
            _subastaBLL = new SubastaBLL();
            _usuarioBLL = new UsuarioBLL();
        }
        private void iniciarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin _frmLogin = new frmLogin(_usuarioBLL, _subastaBLL);
            _frmLogin.MdiParent = this;
            _frmLogin.Show();
        }
    }
}
