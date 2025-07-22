//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

using System;
using System.Globalization;
using System.Threading;
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
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Application.Run(new Form1());
        }
    }
}
