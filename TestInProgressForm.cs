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

using System.Windows.Forms;

public partial class TestInProgressForm : Form
{
    public TestInProgressForm(string message)
    {
        InitializeComponent();
        this.labelMessage.Text = message;
        this.ControlBox = false; // Pas de bouton fermer
        this.StartPosition = FormStartPosition.CenterParent;
    }
}
