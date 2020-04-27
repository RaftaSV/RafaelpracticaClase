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
    public partial class frmRoles : Form
    {
        string usuariocombo = "";
        rol_Usuarios rol=new rol_Usuarios();
        DialogResult R;
        public frmRoles()
        {
            InitializeComponent();
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            CargarCombo();
            CargarDatos();
            desactivareditar();
            


        }
        public void CargarDatos()
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                dgvRol.Rows.Clear();
                var jointablas = from tbU in db.Usuarios
                                 from tbR in db.rol_Usuarios
                                 where tbU.Id == tbR.id_Usuario
                                 select new
                                 {
                                     Id = tbR.id_Rol_Usuario,
                                     Email = tbU.Email,
                                     Rol = tbR.tipo_rol,
                                     IDU = tbR.id_Usuario
                                 };
                foreach (var lis in jointablas)
                {
                    dgvRol.Rows.Add(lis.Id, lis.Email, lis.Rol, lis.IDU);
                }
            }
        }

        private void cmbUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            usuariocombo = cmbUsuario.SelectedValue.ToString();

        }
        public void CargarCombo()
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4()) {
                var lista = db.Usuarios.ToList();

                cmbUsuario.DataSource = lista;
                cmbUsuario.DisplayMember = "Email";
                cmbUsuario.ValueMember = "Id";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                int usu = int.Parse(usuariocombo);
                rol.id_Usuario = usu;
                rol.tipo_rol = txtRolUsuario.Text;
                db.rol_Usuarios.Add(rol);
                db.SaveChanges();
                CargarDatos();
                txtRolUsuario.Text = "";
                cmbUsuario.Enabled = false;
            }
        }
        public void ActivarBotones()
        {
            cmbUsuario.Enabled = true;
            txtRolUsuario.Enabled = true;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }
        public void desactivareditar (){
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;

            cmbUsuario.Enabled = false;
            txtRolUsuario.Enabled = false;

        }
        public void ActivarTextbox()
        {
            cmbUsuario.Enabled = true;
            txtRolUsuario.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtRolUsuario.Text="";
            desactivareditar();
            ActivarTextbox();
            btnGuardar.Enabled = true;
        }

       

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                R = MessageBox.Show("¿Desea guardar los cambios?","Alerta",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    string id = dgvRol.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    rol = db.rol_Usuarios.Where(VerificarID => VerificarID.id_Rol_Usuario == ID).First();
                    rol.id_Usuario = int.Parse(usuariocombo);
                    rol.tipo_rol = txtRolUsuario.Text;
                    db.Entry(rol).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    CargarDatos();
                    desactivareditar();
                    txtRolUsuario.Text = "";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    CargarDatos();
                    desactivareditar();
                    txtRolUsuario.Text = "";
                    btnGuardar.Enabled = false;

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                R = MessageBox.Show("¿Desea eliminar este registro?", "Alerta",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2 );
                if (R==DialogResult.Yes)
                {
                    string id = dgvRol.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    rol = db.rol_Usuarios.Find(ID);
                    db.rol_Usuarios.Remove(rol);
                    db.SaveChanges();
                    CargarDatos();
                    desactivareditar();
                    txtRolUsuario.Text = "";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    CargarDatos();
                    desactivareditar();
                    txtRolUsuario.Text = "";
                    btnGuardar.Enabled = false;
                }
            }
        }

        private void dgvRol_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
                string usuario = dgvRol.CurrentRow.Cells[3].Value.ToString();
                string rol = dgvRol.CurrentRow.Cells[2].Value.ToString();
                usuariocombo = usuario;
                txtRolUsuario.Text = rol;
                ActivarBotones();
                ActivarTextbox();
            

        }

        private void txtRolUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
