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

namespace sistemas_Ventas.Vista
{
    public partial class frmUsuarios : Form
    {
        
        public frmUsuarios()

        {
            InitializeComponent();
        }
         Usuarios user = new Usuarios();
        DialogResult R;
        void Limpiartext()
        {
            txtEmail.Text = "";
            txtContraseña.Text = "";
        }
        void cargardatos()
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var lista = from u in db.Usuarios
                            select new { u.Id,u.Email,u.Contrasena};

                dtvUsuarios.DataSource = lista.ToList();

           
            }
        }



        private void Usuarios_Load(object sender, EventArgs e)
        {
            cargardatos();
            DesactivarbotonesEditarEliminar();
            btnGuardar.Enabled = false;
        }
        public void activarTex()
        {
            txtContraseña.Enabled = true;
            txtEmail.Enabled = true;
        }

      

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
               
                    user.Contrasena = txtContraseña.Text;
                    user.Email = txtEmail.Text;

                    db.Usuarios.Add(user);
                    db.SaveChanges();
                    Limpiartext();
                    cargardatos();
               

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
           
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
            R = MessageBox.Show("Desea guardar los cambios", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if (R == DialogResult.Yes)
            {

                string Id = dtvUsuarios.CurrentRow.Cells[0].Value.ToString();
                int Idc = int.Parse(Id);
                user = db.Usuarios.Where(verificarId => verificarId.Id == Idc).First();
                user.Email = txtEmail.Text;
                user.Contrasena = txtContraseña.Text;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Limpiartext();
                cargardatos();
                }
                else
                {
                    Limpiartext();
                    cargardatos();
                }
                DesactivarbotonesEditarEliminar();
            }

         }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string Id = dtvUsuarios.CurrentRow.Cells[0].Value.ToString();
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                R = MessageBox.Show("Desea eliminar el registro", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    user = db.Usuarios.Find(int.Parse(Id));
                    db.Usuarios.Remove(user);
                    db.SaveChanges();
                    Limpiartext();
                    cargardatos();
                }
                else
                {
                    Limpiartext();
                    cargardatos();

                }
                DesactivarbotonesEditarEliminar();
                
            }

        }

        void dtvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string Email = dtvUsuarios.CurrentRow.Cells[1].Value.ToString();
            string Contra = dtvUsuarios.CurrentRow.Cells[2].Value.ToString();

            txtContraseña.Text = Contra;
            txtEmail.Text = Email;
            activarBotones();
        }
        void DesactivarbotonesEditarEliminar()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
            txtContraseña.Enabled = false;
            txtEmail.Enabled = false;
        }

        void activarBotones()
        {
            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            DesactivarbotonesEditarEliminar();
            activarTex();
            
        }
    }
}
