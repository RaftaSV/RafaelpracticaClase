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
    public partial class frmClientes : Form
    { tb_cliente cliente = new tb_cliente();
        DialogResult R;
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            cargarDatos();
            DesactivarbotonesEditarEliminar();
            btnGuardar.Enabled = false;
        }
        void cargarDatos()
        {
            using ( sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var lista = from cliente in db.tb_cliente
                            select new
                            {
                                ID = cliente.iDCliente,
                                NOMBRE = cliente.nombreCliente,
                                DIRECCION = cliente.direccionCliente,
                                DUI = cliente.duiCliente
                            };
                dgvClientes.DataSource = lista.ToList();
            }
        }

        void limpiar()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtDui.Text = "";
        }
       
        void DesactivarbotonesEditarEliminar()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txtDireccion.Enabled = false;
            txtDui.Enabled = false;
            txtNombre.Enabled = false;
        }
        public void activarTex()
        {
            txtDireccion.Enabled = true;
            txtDui.Enabled = true;
            txtNombre.Enabled = true;

        }

        void activarBotones()
        {
            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
               
                    cliente.nombreCliente = txtNombre.Text;
                    cliente.direccionCliente = txtDireccion.Text;
                    cliente.duiCliente = txtDui.Text;
                    db.tb_cliente.Add(cliente);
                    db.SaveChanges();
                    cargarDatos();
                    limpiar();
                    btnGuardar.Enabled = false;
            }

        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nombre = dgvClientes.CurrentRow.Cells[1].Value.ToString();
            string direccion = dgvClientes.CurrentRow.Cells[2].Value.ToString();
            string dui = dgvClientes.CurrentRow.Cells[3].Value.ToString();
            txtNombre.Text = nombre;
            txtDireccion.Text = direccion;
            txtDui.Text = dui;
            btnGuardar.Enabled = false;
            activarBotones();
            activarTex();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                R = MessageBox.Show("Desea gurdar los cambios", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    String id = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    cliente = db.tb_cliente.Where(VerificarId => VerificarId.iDCliente == ID).First();
                    cliente.nombreCliente = txtNombre.Text;
                    cliente.direccionCliente = txtDireccion.Text;
                    cliente.duiCliente = txtDui.Text;
                    db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    limpiar();
                    cargarDatos();
                    DesactivarbotonesEditarEliminar();
                }
                else
                {
                    limpiar();
                    cargarDatos();
                    DesactivarbotonesEditarEliminar();

                }

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            DesactivarbotonesEditarEliminar();
            activarTex();
            limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {

                R = MessageBox.Show("Desea eliminar el registro", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    string id = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    cliente = db.tb_cliente.Find(ID);
                    db.tb_cliente.Remove(cliente);
                    db.SaveChanges();
                    limpiar();
                    cargarDatos();
                    DesactivarbotonesEditarEliminar();
                }
                else 
                {
                    limpiar();
                    cargarDatos();
                    DesactivarbotonesEditarEliminar();
                }
            }
        }

        private void lblDui_Click(object sender, EventArgs e)
        {

        }
    }
}
