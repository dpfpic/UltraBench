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
using System.Windows.Forms;

namespace UltraBench
{
    public partial class FormScoreHelp : Form
    {
        public FormScoreHelp()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}