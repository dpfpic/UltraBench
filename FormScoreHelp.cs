//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

using System.Drawing;
using System.Windows.Forms;

namespace UltraBench
{
    public partial class FormScoreHelp : Form
    {
        public FormScoreHelp()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Understanding UltraBench Scores";
            this.Size = new Size(480, 360);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblExplanation = new Label();
            lblExplanation.AutoSize = false;
            lblExplanation.Size = new Size(440, 240);
            lblExplanation.Location = new Point(20, 20);
            lblExplanation.Text =
@"• CPU Score – Measures processing power and multitasking efficiency.
• RAM Score – Reflects memory speed and data bandwidth.
• SSD Score – Indicates read/write speed and data access time.
• GPU Score – Evaluates graphics rendering and gaming performance.

Higher scores mean better performance.

Tip: Use these scores to compare your system before and after hardware upgrades.";
            lblExplanation.Font = new Font("Segoe UI", 10);
            lblExplanation.TextAlign = ContentAlignment.TopLeft;

            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Size = new Size(80, 30);
            btnClose.Location = new Point(190, 280);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblExplanation);
            this.Controls.Add(btnClose);
        }
    }
}
