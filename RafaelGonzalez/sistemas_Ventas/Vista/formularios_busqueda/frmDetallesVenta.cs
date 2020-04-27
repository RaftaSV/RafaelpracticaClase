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
    public partial class frmDetallesVenta : Form
    {
        public frmDetallesVenta()
        {
            InitializeComponent();
        }
 

        private void frmDetallesVenta_Load(object sender, EventArgs e)
        {
            filtro();
           
           

        }
        public void filtro()
        {


            
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                dgvDet.Rows.Clear();
                var lista = from det in db.detalleVenta
                            from pro in db.producto
                            where det.idProducto == pro.idProducto
                         
                           
                            select new
                            {   det.idVenta,
                                pro.nombreProducto,
                                pro.precioProducto,
                                det.cantidad,
                                det.total
                            };
                foreach (var i in lista)
                {
                    try
                    {
                        if (int.Parse(txtIdVenta.Text) > 0)
                        {
                            if (i.idVenta == int.Parse(txtIdVenta.Text))
                            {
                                dgvDet.Rows.Add(i.nombreProducto, i.precioProducto, i.cantidad, i.total);
                            }
                        }

                    }
                    catch { }
                }
            }
        }

        private void txtIdVenta_TextChanged(object sender, EventArgs e)
        {
            filtro();
        }
    }
}
