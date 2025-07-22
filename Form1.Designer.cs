//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

namespace UltraBench
{
    partial class Form1
    {
        // <summary>
        // Required designer variable.
        // </summary>
        private System.ComponentModel.IContainer components = null;

        // --- Declarations of all UI controls used in Form1.cs ---
        // Visual Studio generally generates these declarations here
        private System.Windows.Forms.Button btnLaunchHWMonitor;
        private System.Windows.Forms.Button btnOpenReports;
        private System.Windows.Forms.Button btnTestCPU;
        private System.Windows.Forms.Button btnTestGPU;
        private System.Windows.Forms.Button btnTestSSD;
        private System.Windows.Forms.Button btnTestRAM;
        private System.Windows.Forms.Button btnFullStress; // Declaration added/corrected
        private System.Windows.Forms.Button btnClose;

        private System.Windows.Forms.Label lblCpuInfo;
        private System.Windows.Forms.Label lblRamInfo; // Declaration added/corrected
        private System.Windows.Forms.Label lblGpuInfo; // Declaration added/corrected
        private System.Windows.Forms.Label Title_Info; // Declaration added/corrected
        private System.Windows.Forms.Label versionLabel;


        // <summary>
        // Clean up any resources being used.
        // </summary>
        // <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        // <summary>
        // Required method for Designer support - do not modify
        // the contents of this method with the code editor.
        // </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnLaunchHWMonitor = new System.Windows.Forms.Button();
            btnOpenReports = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            lblCpuInfo = new System.Windows.Forms.Label();
            lblRamInfo = new System.Windows.Forms.Label();
            lblGpuInfo = new System.Windows.Forms.Label();
            Title_Info = new System.Windows.Forms.Label();
            versionLabel = new System.Windows.Forms.Label();
            btnTestCPU = new System.Windows.Forms.Button();
            btnTestGPU = new System.Windows.Forms.Button();
            btnTestSSD = new System.Windows.Forms.Button();
            btnTestRAM = new System.Windows.Forms.Button();
            btnFullStress = new System.Windows.Forms.Button();
            logo_UlraBench = new System.Windows.Forms.PictureBox();
            Title_Test = new System.Windows.Forms.Label();
            Title_Bench = new System.Windows.Forms.Label();
            Separator_2 = new System.Windows.Forms.Panel();
            Separator_1 = new System.Windows.Forms.Panel();
            lblCpuResult = new System.Windows.Forms.Label();
            lblRamResult = new System.Windows.Forms.Label();
            lblSsdResult = new System.Windows.Forms.Label();
            lblGpuResult = new System.Windows.Forms.Label();
            deveTitle = new System.Windows.Forms.Label();
            flowLayoutPanelTests = new System.Windows.Forms.FlowLayoutPanel();
            Separator_3 = new System.Windows.Forms.Panel();
            btnShowHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)logo_UlraBench).BeginInit();
            flowLayoutPanelTests.SuspendLayout();
            SuspendLayout();
            // 
            // btnLaunchHWMonitor
            // 
            resources.ApplyResources(btnLaunchHWMonitor, "btnLaunchHWMonitor");
            btnLaunchHWMonitor.Name = "btnLaunchHWMonitor";
            btnLaunchHWMonitor.UseVisualStyleBackColor = true;
            btnLaunchHWMonitor.Click += btnLaunchHWMonitor_Click;
            // 
            // btnOpenReports
            // 
            resources.ApplyResources(btnOpenReports, "btnOpenReports");
            btnOpenReports.Name = "btnOpenReports";
            btnOpenReports.UseVisualStyleBackColor = true;
            btnOpenReports.Click += btnOpenReports_Click;
            // 
            // btnClose
            // 
            resources.ApplyResources(btnClose, "btnClose");
            btnClose.Name = "btnClose";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblCpuInfo
            // 
            resources.ApplyResources(lblCpuInfo, "lblCpuInfo");
            lblCpuInfo.Name = "lblCpuInfo";
            // 
            // lblRamInfo
            // 
            resources.ApplyResources(lblRamInfo, "lblRamInfo");
            lblRamInfo.Name = "lblRamInfo";
            // 
            // lblGpuInfo
            // 
            resources.ApplyResources(lblGpuInfo, "lblGpuInfo");
            lblGpuInfo.Name = "lblGpuInfo";
            // 
            // Title_Info
            // 
            resources.ApplyResources(Title_Info, "Title_Info");
            Title_Info.ForeColor = System.Drawing.Color.DodgerBlue;
            Title_Info.Name = "Title_Info";
            Title_Info.Click += Title_Info_Click;
            // 
            // versionLabel
            // 
            resources.ApplyResources(versionLabel, "versionLabel");
            versionLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            versionLabel.Name = "versionLabel";
            // 
            // btnTestCPU
            // 
            resources.ApplyResources(btnTestCPU, "btnTestCPU");
            btnTestCPU.Name = "btnTestCPU";
            btnTestCPU.UseVisualStyleBackColor = true;
            btnTestCPU.Click += btnTestCPU_Click;
            // 
            // btnTestGPU
            // 
            resources.ApplyResources(btnTestGPU, "btnTestGPU");
            btnTestGPU.Name = "btnTestGPU";
            btnTestGPU.UseVisualStyleBackColor = true;
            btnTestGPU.Click += btnTestGPU_Click;
            // 
            // btnTestSSD
            // 
            resources.ApplyResources(btnTestSSD, "btnTestSSD");
            btnTestSSD.Name = "btnTestSSD";
            btnTestSSD.UseVisualStyleBackColor = true;
            btnTestSSD.Click += btnTestSSD_Click;
            // 
            // btnTestRAM
            // 
            resources.ApplyResources(btnTestRAM, "btnTestRAM");
            btnTestRAM.Name = "btnTestRAM";
            btnTestRAM.UseVisualStyleBackColor = true;
            btnTestRAM.Click += btnTestRAM_Click;
            // 
            // btnFullStress
            // 
            resources.ApplyResources(btnFullStress, "btnFullStress");
            btnFullStress.Name = "btnFullStress";
            btnFullStress.UseVisualStyleBackColor = true;
            btnFullStress.Click += btnFullStress_Click;
            // 
            // logo_UlraBench
            // 
            resources.ApplyResources(logo_UlraBench, "logo_UlraBench");
            logo_UlraBench.Name = "logo_UlraBench";
            logo_UlraBench.TabStop = false;
            // 
            // Title_Test
            // 
            resources.ApplyResources(Title_Test, "Title_Test");
            Title_Test.ForeColor = System.Drawing.Color.DodgerBlue;
            Title_Test.Name = "Title_Test";
            // 
            // Title_Bench
            // 
            resources.ApplyResources(Title_Bench, "Title_Bench");
            Title_Bench.ForeColor = System.Drawing.Color.DodgerBlue;
            Title_Bench.Name = "Title_Bench";
            // 
            // Separator_2
            // 
            Separator_2.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(Separator_2, "Separator_2");
            Separator_2.Name = "Separator_2";
            // 
            // Separator_1
            // 
            Separator_1.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(Separator_1, "Separator_1");
            Separator_1.Name = "Separator_1";
            // 
            // lblCpuResult
            // 
            resources.ApplyResources(lblCpuResult, "lblCpuResult");
            lblCpuResult.Name = "lblCpuResult";
            // 
            // lblRamResult
            // 
            resources.ApplyResources(lblRamResult, "lblRamResult");
            lblRamResult.Name = "lblRamResult";
            // 
            // lblSsdResult
            // 
            resources.ApplyResources(lblSsdResult, "lblSsdResult");
            lblSsdResult.Name = "lblSsdResult";
            // 
            // lblGpuResult
            // 
            resources.ApplyResources(lblGpuResult, "lblGpuResult");
            lblGpuResult.Name = "lblGpuResult";
            // 
            // deveTitle
            // 
            resources.ApplyResources(deveTitle, "deveTitle");
            deveTitle.ForeColor = System.Drawing.SystemColors.ControlDark;
            deveTitle.Name = "deveTitle";
            // 
            // flowLayoutPanelTests
            // 
            flowLayoutPanelTests.Controls.Add(btnTestGPU);
            flowLayoutPanelTests.Controls.Add(btnLaunchHWMonitor);
            flowLayoutPanelTests.Controls.Add(btnOpenReports);
            resources.ApplyResources(flowLayoutPanelTests, "flowLayoutPanelTests");
            flowLayoutPanelTests.Name = "flowLayoutPanelTests";
            // 
            // Separator_3
            // 
            Separator_3.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(Separator_3, "Separator_3");
            Separator_3.Name = "Separator_3";
            // 
            // btnShowHistory
            // 
            resources.ApplyResources(btnShowHistory, "btnShowHistory");
            btnShowHistory.Name = "btnShowHistory";
            btnShowHistory.UseVisualStyleBackColor = true;
            btnShowHistory.Click += btnShowHistory_Click;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(btnShowHistory);
            Controls.Add(Separator_3);
            Controls.Add(flowLayoutPanelTests);
            Controls.Add(btnFullStress);
            Controls.Add(deveTitle);
            Controls.Add(btnTestRAM);
            Controls.Add(btnTestSSD);
            Controls.Add(lblGpuResult);
            Controls.Add(btnTestCPU);
            Controls.Add(lblSsdResult);
            Controls.Add(lblRamResult);
            Controls.Add(lblCpuResult);
            Controls.Add(Separator_1);
            Controls.Add(btnClose);
            Controls.Add(Separator_2);
            Controls.Add(Title_Bench);
            Controls.Add(Title_Test);
            Controls.Add(logo_UlraBench);
            Controls.Add(lblGpuInfo);
            Controls.Add(Title_Info);
            Controls.Add(lblRamInfo);
            Controls.Add(lblCpuInfo);
            Controls.Add(versionLabel);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)logo_UlraBench).EndInit();
            flowLayoutPanelTests.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox logo_UlraBench;
        private System.Windows.Forms.Label Title_Test;
        private System.Windows.Forms.Label Title_Bench;
        private System.Windows.Forms.Panel Separator_2;
        private System.Windows.Forms.Panel Separator_1;
        private System.Windows.Forms.Label lblCpuResult;
        private System.Windows.Forms.Label lblRamResult;
        private System.Windows.Forms.Label lblSsdResult;
        private System.Windows.Forms.Label lblGpuResult; // Declaration added/corrected
        private System.Windows.Forms.Label deveTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTests;
        private System.Windows.Forms.Panel Separator_3;
        private System.Windows.Forms.Button btnShowHistory;
    }
}
