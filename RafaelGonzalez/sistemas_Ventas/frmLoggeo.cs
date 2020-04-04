using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemas_Ventas.Model;
using sistemas_Ventas.Vista;

namespace sistemas_Ventas
{
    public partial class fInicio : Form
    {
        public fInicio()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '#';

        }

      

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities2 db = new sistema_ventasEntities2())
            {
                var Lista = from usuario in db.Usuarios
                            where usuario.Email == txtUsuario.Text
                            && usuario.Contrasena ==  txtContraseña.Text
                            select usuario;

                if (Lista.Count()>0) {
                    
                   frmMenu u = new frmMenu();
                    this.Hide();
                    u.ShowDialog();

                }
                else { MessageBox.Show("El usuario"); }
            }
        }
    }
}
