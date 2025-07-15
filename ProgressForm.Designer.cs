namespace UltraBench
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            progressBar1 = new System.Windows.Forms.ProgressBar();
            lblProgressText = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(23, 81);
            progressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(420, 29);
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 0;
            // 
            // lblProgressText
            // 
            lblProgressText.Location = new System.Drawing.Point(23, 35);
            lblProgressText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblProgressText.Name = "lblProgressText";
            lblProgressText.Size = new System.Drawing.Size(420, 23);
            lblProgressText.TabIndex = 1;
            lblProgressText.Text = "Preparing for the test...";
            lblProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(467, 138);
            ControlBox = false;
            Controls.Add(lblProgressText);
            Controls.Add(progressBar1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Test Progress";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgressText;
    }
}