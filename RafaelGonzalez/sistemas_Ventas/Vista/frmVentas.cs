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
    public partial class frmVentas : Form
    {
       
        string idDoc = "";
        string IdCliente = "";
        int IDVenta;
      
        public frmVentas()
        {
            InitializeComponent();
        
            
        }
        
        
        public void cargarcombo() {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
              
                var lista = db.tb_cliente.ToList();
                cmbCliente.DataSource = lista;
                cmbCliente.ValueMember = "iDCliente";
                cmbCliente.DisplayMember = "nombreCliente";

                var lis = db.tb_documento.ToList();
                cmbDoc.DataSource = lis;
                cmbDoc.ValueMember = "iDDocumento";
                cmbDoc.DisplayMember = "nombreDocumento";
                cmbCliente.SelectedIndex = 0;
                cmbDoc.SelectedIndex = 0;

            }


        }
        void cargarNUmeroVenta()
        {using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                var venta = db.tb_venta.ToList();
                foreach (var iterar in venta)
                {
                    lblNumeroR.Text = iterar.idVenta.ToString();
                    IDVenta = iterar.idVenta;

                }
            }
            
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            IdCliente = cmbCliente.SelectedValue.ToString();
        }

        private void cmbDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            idDoc = cmbDoc.SelectedValue.ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmProductosB f = new frmProductosB();
            f.ShowDialog();
            txtCantidad.Focus();

        }
        double total;
        double totalV;

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                total = double.Parse(txtPrecio.Text) * double.Parse(txtCantidad.Text);
                txtTotal.Text = total.ToString();

            }
            catch (Exception ex)
            {

            }
           
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            
            cargarcombo();
            
            cargarNUmeroVenta();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string ID = txtBuscar.Text;
                int id = int.Parse(ID);
                using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
                {
                    var lista = from pro in db.producto
                                where pro.idProducto == (id)
                                select new
                                {
                                    ID = pro.idProducto,
                                    PRODUCTO = pro.nombreProducto,
                                    PRECIO = pro.precioProducto,
                                    ESTADO = pro.estadoProducto
                                };

                    foreach (var iterar in lista)
                    {
                        txtID.Text = iterar.ID.ToString();
                        txtProducto.Text = iterar.PRODUCTO.ToString();
                        txtPrecio.Text = iterar.PRECIO.ToString();

                    }
                    
                }
            } catch (Exception ex)
            {
                limpiar();
            }
        }

      

        private void txtAgregar_Click_1(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "" && txtCantidad.Text != "0" && txtID.Text!="")
            {
                dgvVenta.Rows.Add(txtID.Text, txtProducto.Text, txtPrecio.Text, txtCantidad.Text, total.ToString());

                totalV = double.Parse(txtTotalVenta.Text) + total;
                txtTotalVenta.Text = totalV.ToString();
                limpiar();
            }
        }
        void limpiar()
        {
            txtID.Text = "";
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Text = "";
            txtTotal.Text = "";
            txtBuscar.Text = "";
            txtMensaje.Text = "";
        }
        detalleVenta detalle = new detalleVenta();
        tb_venta venta = new tb_venta();
        private void btnCobrar_Click(object sender, EventArgs e)
        {
          
            guardarventa();
            guardardetalle();


            limpiar();
            cargarcombo();
            cargarNUmeroVenta();
            txtTotalVenta.Text = "0";
            txtEfectivo.Text = "";
        }
        void guardarventa() {
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {
                venta.idDocumento = int.Parse(idDoc);
                venta.iDCliente = int.Parse(IdCliente);
                venta.iDUsuario = int.Parse(txtUsuario.Text);
                venta.totalVenta = decimal.Parse(txtTotalVenta.Text);
                venta.fecha = DateTime.Today;
                db.tb_venta.Add(venta);
                db.SaveChanges();
            }
        }
        void guardardetalle()
        {
            IDVenta = IDVenta + 1; 
            using (sistema_ventasEntities4 db = new sistema_ventasEntities4())
            {


                foreach (DataGridViewRow dgv in dgvVenta.Rows)
                {
                    detalle.idVenta = IDVenta;
                    detalle.idProducto = int.Parse(dgv.Cells[0].Value.ToString());
                    detalle.cantidad = int.Parse(dgv.Cells[3].Value.ToString());
                    detalle.precio = decimal.Parse(dgv.Cells[2].Value.ToString());
                    detalle.total = decimal.Parse(dgv.Cells[4].Value.ToString());
                    db.detalleVenta.Add(detalle);
                    db.SaveChanges();
                    
                }

            }
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(txtEfectivo.Text) > 0 && double.Parse(txtTotalVenta.Text) <= double.Parse(txtEfectivo.Text))
                {
                    double cambio = Math.Round  ( double.Parse(txtEfectivo.Text) - double.Parse(txtTotalVenta.Text),2) ;
                    txtMensaje.Text = ("Total de la venta es: $" + txtTotalVenta.Text + "\r\n"
                        + "Efectivo ingresado es:$" + txtEfectivo.Text + "\r\n" +
                        "El Cambio es: $" + cambio.ToString());
                }
                else
                {
                    limpiar();
                }
            }
            catch
            {
                limpiar();
            }

        
        }

    }
    
}
