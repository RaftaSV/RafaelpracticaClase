using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using sistemas_Ventas.Vista;
using sistemas_Ventas.Vista.formularios_busqueda;
using sistemas_Ventas.Vista;

namespace sistemas_Ventas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fInicio());
        }
    }
}
