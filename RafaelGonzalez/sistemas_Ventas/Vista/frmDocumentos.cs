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
    public partial class frmDocumentos : Form
    {
        public frmDocumentos()
        {
            InitializeComponent();
        }

        tb_documento doc = new tb_documento();
        DialogResult R;

        private void frmDocumentos_Load(object sender, EventArgs e)
        {
            cargarDatos();
            DesactivarbotonesEditarEliminar();
            btnGuardar.Enabled = false;

        }
        void DesactivarbotonesEditarEliminar()
        {
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txtNombre.Enabled=false;
        }

        void activarBotones()
        {
            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            txtNombre.Enabled = true;
        }

        public void cargarDatos()
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var lista = from doc in db.tb_documento
                            select new
                            {
                                ID = doc.iDDocumento,
                                DOCUMENTO = doc.nombreDocumento
                            };
                dgvDoc.DataSource = lista.ToList();
            }

        }
        public void limpiar()
        {
            txtNombre.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                 doc.nombreDocumento = txtNombre.Text;
                    db.tb_documento.Add(doc);
                    db.SaveChanges();
                    limpiar();
                    btnGuardar.Enabled = false;
                    cargarDatos();
              

            }
        }

        private void dgvDoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nombre = dgvDoc.CurrentRow.Cells[1].Value.ToString();
            txtNombre.Text = nombre;
            activarBotones();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            txtNombre.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4 ())
            {
                R = MessageBox.Show("Desea guardar los cambios", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    string id = dgvDoc.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    doc = db.tb_documento.Where(verificarID => verificarID.iDDocumento == (ID)).First();
                    doc.nombreDocumento = txtNombre.Text;
                    db.Entry(doc).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    limpiar();
                    DesactivarbotonesEditarEliminar();
                    cargarDatos();
                }
                else
                {
                    limpiar();
                    DesactivarbotonesEditarEliminar();
                }
            }  
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                R = MessageBox.Show("Desea eliminar el registro", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (R == DialogResult.Yes)
                {
                    string id = dgvDoc.CurrentRow.Cells[0].Value.ToString();
                    int ID = int.Parse(id);
                    doc = db.tb_documento.Find(ID);
                    db.tb_documento.Remove(doc);
                    db.SaveChanges();
                    cargarDatos();
                    limpiar();
                    DesactivarbotonesEditarEliminar();
                }
                else
                {
                    limpiar();
                    DesactivarbotonesEditarEliminar();
                }
            }
        }
    }
}
