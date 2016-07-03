using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PowerShareCard
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            FrmLogin login = new FrmLogin();
            login.ShowDialog();
            if (login.IsLogin)
            {
                Application.Run(new FrmMain());
            }  
        }
    }
}
