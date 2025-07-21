// UltraBench – Created by Dpfpic (Fabrice Piscia)
// Licensed under the MIT License
//
using System;
using System.Windows.Forms;

namespace UltraBench
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
