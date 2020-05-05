using sistemas_Ventas.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemas_Ventas.Vista.formularios_busqueda
{
    public partial class frmProductosB : Form
    {
        public frmProductosB()
        {
            InitializeComponent();
            txtBuscar.Focus();
        }

        private void frmProductosB_Load(object sender, EventArgs e)
        {
           filtro();
            
        }
        public void filtro()
        {
            string nombre = txtBuscar.Text;
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var lista = from pro in db.producto
                            where pro.nombreProducto.Contains(nombre)
                            select new
                            {
                                ID = pro.idProducto,
                                PRDUCTO = pro.nombreProducto,
                                PRECIO = pro.precioProducto,
                                ESTADO = pro.estadoProducto
                            };
                dgvProductos.DataSource = lista.ToList();

            }
        }
        string id;
        string producto;
        string precio;
        private void dgvProductos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            envio();
              
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            filtro();
        }


        void envio()
        {
            id = dgvProductos.CurrentRow.Cells[0].Value.ToString();
            producto = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            precio = dgvProductos.CurrentRow.Cells[2].Value.ToString();


            frmMenu.venta.txtID.Text = id;
            frmMenu.venta.txtProducto.Text = producto;
            frmMenu.venta.txtPrecio.Text = precio;
            frmMenu.venta.txtCantidad.Focus();

            this.Close();
        }

        private void dgvProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                envio();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
