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
        public int id;
        public fInicio()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '#';
         

        }

      

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var Lista = from usuario in db.Usuarios
                            where usuario.Email == txtUsuario.Text
                            && usuario.Contrasena ==  txtContraseña.Text
                            select usuario;
                var usu = from us in db.Usuarios
                          where us.Email == txtUsuario.Text
                          select us;
                foreach (var usuario in usu)
                {
                    frmMenu.venta.txtUsuario.Text = usuario.Id.ToString();
                    frmMenu.venta.txtUsuarioNombre.Text = txtUsuario.Text;
                }

                if (Lista.Count() > 0) {
                    
                    string id = txtUsuario.Text;
                    
                    frmMenu u = new frmMenu();
                    this.Hide();
                    u.ShowDialog();
                  

                }
                else { MessageBox.Show("El usuarion ó contaseña es invalido"); }
            }
            
            
            
        }

        private void fInicio_Load(object sender, EventArgs e)
        {

        }
    }
}
