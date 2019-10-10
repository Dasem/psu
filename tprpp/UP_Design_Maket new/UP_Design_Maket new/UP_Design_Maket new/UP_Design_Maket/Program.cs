using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UP_Design_Maket
{
    static class Program
    {
        public static CreateMatrix fr1;
        public static MainForm fr2;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);;
            //fr1 = new Form1();
            Application.Run(fr2 = new MainForm());
        }
    }
}
