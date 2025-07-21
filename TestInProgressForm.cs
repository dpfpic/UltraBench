//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

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
