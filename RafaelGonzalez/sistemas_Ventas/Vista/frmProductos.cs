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
    public partial class frmProductos : Form
    {
        producto pro = new producto();
        DialogResult R;
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            cargarDatos();
            btnGuardar.Enabled = false;
            desactivarEditar();

        }

        public void cargarDatos()
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var lista = from pro in db.producto
                            select new
                            {
                                ID = pro.idProducto,
                                PRDUCTO= pro.nombreProducto,
                                PRECIO = pro.precioProducto,
                                ESTADO = pro.estadoProducto
                            };
                dgvProductos.DataSource = lista.ToList();

            }
        }
        public void desactivarEditar()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
          
            txtPrecio.Enabled = false;
            txtEstado.Enabled = false;
            txtNombre.Enabled = false;
        }
        public void activarTextBox()
        {
            
            txtPrecio.Enabled = true;
            txtEstado.Enabled = true;
            txtNombre.Enabled = true;
        }
        public void activareditar()
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnGuardar.Enabled = false;
        }
        public void limpiar()
        {
            txtEstado.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            desactivarEditar();
            activarTextBox();
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                pro.nombreProducto = txtNombre.Text;
                pro.precioProducto = decimal.Parse(txtPrecio.Text);
                pro.estadoProducto = txtEstado.Text;
                db.producto.Add(pro);
                db.SaveChanges();
                desactivarEditar();
                limpiar();
                cargarDatos();
                btnGuardar.Enabled = false;
            }
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nombre = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            string precio = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            string estado = dgvProductos.CurrentRow.Cells[3].Value.ToString();

            txtNombre.Text = nombre;
            txtPrecio.Text = precio;
            txtEstado.Text = estado;
            activareditar();
            activarTextBox();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                R = MessageBox.Show("¿Desea guardar los cambios","Alerta",MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    string id = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    pro = db.producto.Where(verificarID => verificarID.idProducto == ID).First();
                    pro.nombreProducto = txtNombre.Text;
                    pro.precioProducto = decimal.Parse(txtPrecio.Text);
                    pro.estadoProducto = txtEstado.Text;
                    db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    limpiar();
                    desactivarEditar();
                    cargarDatos();
                }
                else
                {
                    limpiar();
                    desactivarEditar();
                    cargarDatos(); 

                }

            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                R = MessageBox.Show("¿Desea eliminar el registro", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    string id = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    pro = db.producto.Find(ID);
                    db.producto.Remove(pro);
                    db.SaveChanges();
                    limpiar();
                    desactivarEditar();
                    cargarDatos();
                }
                else
                {
                    limpiar();
                    desactivarEditar();
                    cargarDatos();

                }

            }
        }
    }
}
