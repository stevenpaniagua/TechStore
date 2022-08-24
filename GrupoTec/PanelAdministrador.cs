using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrupoTec
{
    public partial class PanelAdministrador : Form
    {
        public PanelAdministrador()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void clickUsuarios_Click(object sender, EventArgs e)
        {
            // Se abre el menú de registro de usuarios.
            new Usuarios().Show();
        }

        private void clickProductos_Click(object sender, EventArgs e)
        {
            // Se abre el menú de registro de productos.
            new Productos().Show();
        }
    }
}
