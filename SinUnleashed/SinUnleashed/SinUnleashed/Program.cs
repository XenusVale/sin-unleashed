using System;
using System.Windows.Forms;

namespace SinUnleashed
{
    class Launcher
    {
        [STAThread]

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
        }
    }
}
