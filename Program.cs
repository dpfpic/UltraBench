/**
* UltraBench – System Benchmark Tool
 * Version : 1.0.0
 * Created by Dpfpic (Fabrice Piscia)
 * Licensed under the MIT License
 * Repository: https://github.com/dpfpic/UltraBench
 *
 * Description:
 * This file is part of the UltraBench project,
 * a tool for benchmarking CPU, RAM, SSD and GPU.
**/

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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Application.Run(new Form1());
        }
    }
}
