//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            progressBar1 = new System.Windows.Forms.ProgressBar();
            lblProgressText = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblProgressText
            // 
            resources.ApplyResources(lblProgressText, "lblProgressText");
            lblProgressText.Name = "lblProgressText";
            // 
            // ProgressForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(lblProgressText);
            Controls.Add(progressBar1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressForm";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgressText;
    }
}