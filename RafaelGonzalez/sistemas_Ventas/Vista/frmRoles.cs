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
        public frmRoles()
        {
            InitializeComponent();
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            using (sistema_ventasEntities2 db = new sistema_ventasEntities2())
            {
                var jointablas = from tbU in db.Usuarios
                                 from tbR in db.rol_Usuarios
                                 where tbU.Id == tbR.id_Usuario
                                 select new
                                 {
                                     Id = tbU.Id,
                                     Email = tbU.Email,
                                     Rol = tbR.tipo_rol
                                 };
                dataGridView1.DataSource= jointablas.ToList();
            }


        }
    }
}
