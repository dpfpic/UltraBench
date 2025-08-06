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

// UltraBench – Created by Dpfpic (Fabrice Piscia)
// Licensed under the MIT License
//
﻿namespace UltraBench
{
    partial class FormScoreHelp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScoreHelp));
            lblExplanation = new System.Windows.Forms.Label();
            btnClose = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblExplanation
            // 
            resources.ApplyResources(lblExplanation, "lblExplanation");
            lblExplanation.Name = "lblExplanation";
            // 
            // btnClose
            // 
            resources.ApplyResources(btnClose, "btnClose");
            btnClose.Name = "btnClose";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // FormScoreHelp
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(btnClose);
            Controls.Add(lblExplanation);
            MaximizeBox = false;
            Name = "FormScoreHelp";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblExplanation;
        private System.Windows.Forms.Button btnClose;
    }
}