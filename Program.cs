using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimProvider
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new Graphics.GLTest());
            //Application.Run(new Window.MainWindow());
            //new CSVExport();
        }
    }
}
