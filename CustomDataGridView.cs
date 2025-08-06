//*********************************************
// UltraBench – System Benchmark Tool
// Version : 1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Licensed under the MIT License
// Repository: https://github.com/dpfpic/UltraBench
//
// Description:
// Custom DataGridView to enable DoubleBuffering.
//*********************************************

using System.Windows.Forms;

namespace UltraBench
{
    public class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            // Active le double buffering pour un rendu plus fluide.
            // Cela réduit le scintillement et les artefacts visuels.
            this.DoubleBuffered = true;
        }
    }
}