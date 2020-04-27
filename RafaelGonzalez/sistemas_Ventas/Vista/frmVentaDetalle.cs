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
using sistemas_Ventas.Vista.formularios_busqueda;

namespace sistemas_Ventas.Vista
{
    public partial class frmVentaDetalle : Form
    {
        public frmVentaDetalle()
        {
            InitializeComponent();
           
        }
        public void cargar()
        {
            dgvVenta.Rows.Clear();
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var lista = from venta in db.tb_venta
                            from doc in db.tb_documento
                            from cli in db.tb_cliente
                            from usu in db.Usuarios
                            where venta.idDocumento==doc.iDDocumento
                            && venta.iDCliente  == cli.iDCliente
                            && venta.iDUsuario == usu.Id
                            select new
                            {
                                venta.idVenta,doc.nombreDocumento,cli.nombreCliente,usu.Email,venta.totalVenta,venta.fecha

                            };
                foreach (var i in lista)
                {
                    dgvVenta.Rows.Add(i.idVenta,i.nombreDocumento,i.nombreCliente,i.Email,i.totalVenta,i.fecha);
                }
            }
        }

        private void frmVentaDetalle_Load(object sender, EventArgs e)
        {
            cargar();
            txtmensaje.Text = "Si ha realizado ventas actualmente por favor pulsar Recargar." + "\r\n"+"" +
                "Desea ver los productos de la venta selecione y de enter";
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            cargar();
        }

       public static frmDetallesVenta d = new frmDetallesVenta();
        void abrirdetalles()
        {
            d.txtIdVenta.Text = dgvVenta.CurrentRow.Cells[0].Value.ToString();
            d.ShowDialog();
          

        }

        private void dgvVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                abrirdetalles();
                
            }
        }
    }
}
