using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistemas_Ventas.Vista
{
    public partial class frmMenu : Form
    {
        public static string usuario;
       
        public frmMenu()
        {
            InitializeComponent();
            
        }
        
        public static frmRoles rol = new frmRoles();
        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rol.CargarCombo();
            rol.CargarDatos();
            rol.MdiParent= this;
            rol.FormBorderStyle = FormBorderStyle.None;
            rol.Dock = DockStyle.Fill;
            rol.BringToFront();
            rol.Show();

            

        }

        public static frmUsuarios usuarios = new frmUsuarios();
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            usuarios.MdiParent = this;
            usuarios.FormBorderStyle = FormBorderStyle.None;
            usuarios.Dock = DockStyle.Fill;
            usuarios.BringToFront();
            usuarios.Show();

        }
        public static frmClientes clientes = new frmClientes();
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientes.cargarDatos();
            clientes.MdiParent = this;
            clientes.FormBorderStyle = FormBorderStyle.None;
            clientes.Dock = DockStyle.Fill;
            clientes.BringToFront();
            clientes.Show();
            
        }
        public static frmDocumentos doc = new frmDocumentos();
        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc.cargarDatos();
            doc.MdiParent = this;
            doc.FormBorderStyle = FormBorderStyle.None;
            doc.Dock = DockStyle.Fill;
            doc.BringToFront();
            doc.Show();
        }
        public static frmProductos producto = new frmProductos();
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            producto.cargarDatos();
            producto.MdiParent = this;
            producto.FormBorderStyle = FormBorderStyle.None;
            producto.Dock = DockStyle.Fill;
            producto.BringToFront();
            producto.Show();
        }

        public void frmMenu_Load(object sender, EventArgs e)
        {
            
            venta.MdiParent = this;
            venta.FormBorderStyle = FormBorderStyle.None;
            venta.Dock = DockStyle.Fill;
            venta.BringToFront();
            venta.Show();
          



        }

       

        public static frmVentas venta = new frmVentas();
        public frmVentas ventas = new frmVentas();
        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            venta.cargarcombo();
            venta.cargarBuscarpro();
            venta.MdiParent= this;
            venta.FormBorderStyle = FormBorderStyle.None;
            venta.Dock = DockStyle.Fill;
            venta.BringToFront();
            venta.Show();
    }

        public static frmVentaDetalle ventaDetalle = new frmVentaDetalle();
        
        private void ventaDetallesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ventaDetalle.cargar();
            ventaDetalle.MdiParent = this;
            ventaDetalle.FormBorderStyle = FormBorderStyle.None;
            ventaDetalle.Dock = DockStyle.Fill;
            ventaDetalle.BringToFront();
            ventaDetalle.Show();
        }
        

        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
                Application.Exit();
         
        }
    }
}
