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
            ((System.ComponentModel.ISupportInitialize)logo_UlraBench).BeginInit();
            flowLayoutPanelTests.SuspendLayout();
            SuspendLayout();
            // 
            // btnLaunchHWMonitor
            // 
            btnLaunchHWMonitor.Location = new System.Drawing.Point(5, 117);
            btnLaunchHWMonitor.Margin = new System.Windows.Forms.Padding(5, 33, 5, 4);
            btnLaunchHWMonitor.Name = "btnLaunchHWMonitor";
            btnLaunchHWMonitor.Size = new System.Drawing.Size(229, 47);
            btnLaunchHWMonitor.TabIndex = 0;
            btnLaunchHWMonitor.Text = "Launch HWMonitor";
            btnLaunchHWMonitor.UseVisualStyleBackColor = true;
            btnLaunchHWMonitor.Click += btnLaunchHWMonitor_Click;
            // 
            // btnOpenReports
            // 
            btnOpenReports.Location = new System.Drawing.Point(5, 201);
            btnOpenReports.Margin = new System.Windows.Forms.Padding(5, 33, 5, 4);
            btnOpenReports.Name = "btnOpenReports";
            btnOpenReports.Size = new System.Drawing.Size(229, 47);
            btnOpenReports.TabIndex = 1;
            btnOpenReports.Text = "Open Reports Folder";
            btnOpenReports.UseVisualStyleBackColor = true;
            btnOpenReports.Click += btnOpenReports_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(624, 816);
            btnClose.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(229, 47);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblCpuInfo
            // 
            lblCpuInfo.AutoSize = true;
            lblCpuInfo.Location = new System.Drawing.Point(32, 79);
            lblCpuInfo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblCpuInfo.Name = "lblCpuInfo";
            lblCpuInfo.Size = new System.Drawing.Size(75, 20);
            lblCpuInfo.TabIndex = 3;
            lblCpuInfo.Text = "Processor:";
            // 
            // lblRamInfo
            // 
            lblRamInfo.AutoSize = true;
            lblRamInfo.Location = new System.Drawing.Point(32, 111);
            lblRamInfo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblRamInfo.Name = "lblRamInfo";
            lblRamInfo.Size = new System.Drawing.Size(44, 20);
            lblRamInfo.TabIndex = 4;
            lblRamInfo.Text = "RAM:";
            // 
            // lblGpuInfo
            // 
            lblGpuInfo.AutoSize = true;
            lblGpuInfo.Location = new System.Drawing.Point(30, 143);
            lblGpuInfo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblGpuInfo.Name = "lblGpuInfo";
            lblGpuInfo.Size = new System.Drawing.Size(104, 20);
            lblGpuInfo.TabIndex = 5;
            lblGpuInfo.Text = "Graphics Card:";
            // 
            // Title_Info
            // 
            Title_Info.AutoSize = true;
            Title_Info.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            Title_Info.ForeColor = System.Drawing.Color.DodgerBlue;
            Title_Info.Location = new System.Drawing.Point(29, 25);
            Title_Info.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            Title_Info.Name = "Title_Info";
            Title_Info.Size = new System.Drawing.Size(199, 28);
            Title_Info.TabIndex = 6;
            Title_Info.Text = "System Information";
            Title_Info.Click += Title_Info_Click;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            versionLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            versionLabel.Location = new System.Drawing.Point(15, 867);
            versionLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new System.Drawing.Size(64, 20);
            versionLabel.TabIndex = 7;
            versionLabel.Text = "Version: ";
            // 
            // btnTestCPU
            // 
            btnTestCPU.Location = new System.Drawing.Point(32, 579);
            btnTestCPU.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btnTestCPU.Name = "btnTestCPU";
            btnTestCPU.Size = new System.Drawing.Size(229, 47);
            btnTestCPU.TabIndex = 8;
            btnTestCPU.Text = "Test CPU";
            btnTestCPU.UseVisualStyleBackColor = true;
            btnTestCPU.Click += btnTestCPU_Click;
            // 
            // btnTestGPU
            // 
            btnTestGPU.Location = new System.Drawing.Point(5, 33);
            btnTestGPU.Margin = new System.Windows.Forms.Padding(5, 33, 5, 4);
            btnTestGPU.Name = "btnTestGPU";
            btnTestGPU.Size = new System.Drawing.Size(229, 47);
            btnTestGPU.TabIndex = 9;
            btnTestGPU.Text = "Test GPU";
            btnTestGPU.UseVisualStyleBackColor = true;
            btnTestGPU.Click += btnTestGPU_Click;
            // 
            // btnTestSSD
            // 
            btnTestSSD.Location = new System.Drawing.Point(32, 747);
            btnTestSSD.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btnTestSSD.Name = "btnTestSSD";
            btnTestSSD.Size = new System.Drawing.Size(229, 47);
            btnTestSSD.TabIndex = 10;
            btnTestSSD.Text = "Test SSD/HDD";
            btnTestSSD.UseVisualStyleBackColor = true;
            btnTestSSD.Click += btnTestSSD_Click;
            // 
            // btnTestRAM
            // 
            btnTestRAM.Location = new System.Drawing.Point(32, 663);
            btnTestRAM.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btnTestRAM.Name = "btnTestRAM";
            btnTestRAM.Size = new System.Drawing.Size(229, 47);
            btnTestRAM.TabIndex = 11;
            btnTestRAM.Text = "Test RAM";
            btnTestRAM.UseVisualStyleBackColor = true;
            btnTestRAM.Click += btnTestRAM_Click;
            // 
            // btnFullStress
            // 
            btnFullStress.Location = new System.Drawing.Point(624, 663);
            btnFullStress.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            btnFullStress.Name = "btnFullStress";
            btnFullStress.Size = new System.Drawing.Size(229, 47);
            btnFullStress.TabIndex = 12;
            btnFullStress.Text = "Full Stress Test";
            btnFullStress.UseVisualStyleBackColor = true;
            btnFullStress.Click += btnFullStress_Click;
            // 
            // logo_UlraBench
            // 
            logo_UlraBench.Image = (System.Drawing.Image)resources.GetObject("logo_UlraBench.Image");
            logo_UlraBench.Location = new System.Drawing.Point(696, 41);
            logo_UlraBench.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            logo_UlraBench.Name = "logo_UlraBench";
            logo_UlraBench.Size = new System.Drawing.Size(177, 177);
            logo_UlraBench.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            logo_UlraBench.TabIndex = 13;
            logo_UlraBench.TabStop = false;
            // 
            // Title_Test
            // 
            Title_Test.AutoSize = true;
            Title_Test.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            Title_Test.ForeColor = System.Drawing.Color.DodgerBlue;
            Title_Test.Location = new System.Drawing.Point(30, 520);
            Title_Test.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            Title_Test.Name = "Title_Test";
            Title_Test.Size = new System.Drawing.Size(154, 28);
            Title_Test.TabIndex = 14;
            Title_Test.Text = "Start Test Here";
            // 
            // Title_Bench
            // 
            Title_Bench.AutoSize = true;
            Title_Bench.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            Title_Bench.ForeColor = System.Drawing.Color.DodgerBlue;
            Title_Bench.Location = new System.Drawing.Point(30, 220);
            Title_Bench.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            Title_Bench.Name = "Title_Bench";
            Title_Bench.Size = new System.Drawing.Size(192, 28);
            Title_Bench.TabIndex = 15;
            Title_Bench.Text = "Benchmark Results";
            // 
            // Separator_2
            // 
            Separator_2.BackColor = System.Drawing.SystemColors.ControlDark;
            Separator_2.Location = new System.Drawing.Point(30, 480);
            Separator_2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Separator_2.Name = "Separator_2";
            Separator_2.Size = new System.Drawing.Size(858, 4);
            Separator_2.TabIndex = 16;
            // 
            // Separator_1
            // 
            Separator_1.BackColor = System.Drawing.SystemColors.ControlDark;
            Separator_1.Location = new System.Drawing.Point(32, 195);
            Separator_1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Separator_1.Name = "Separator_1";
            Separator_1.Size = new System.Drawing.Size(549, 4);
            Separator_1.TabIndex = 17;
            // 
            // lblCpuResult
            // 
            lblCpuResult.AutoSize = true;
            lblCpuResult.Location = new System.Drawing.Point(29, 272);
            lblCpuResult.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblCpuResult.Name = "lblCpuResult";
            lblCpuResult.Size = new System.Drawing.Size(162, 20);
            lblCpuResult.TabIndex = 18;
            lblCpuResult.Text = "CPU Test: Not Executed";
            // 
            // lblRamResult
            // 
            lblRamResult.AutoSize = true;
            lblRamResult.Location = new System.Drawing.Point(30, 320);
            lblRamResult.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblRamResult.Name = "lblRamResult";
            lblRamResult.Size = new System.Drawing.Size(167, 20);
            lblRamResult.TabIndex = 19;
            lblRamResult.Text = "RAM Test: Not Executed";
            // 
            // lblSsdResult
            // 
            lblSsdResult.AutoSize = true;
            lblSsdResult.Location = new System.Drawing.Point(32, 369);
            lblSsdResult.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblSsdResult.Name = "lblSsdResult";
            lblSsdResult.Size = new System.Drawing.Size(162, 20);
            lblSsdResult.TabIndex = 20;
            lblSsdResult.Text = "SSD Test: Not Executed";
            // 
            // lblGpuResult
            // 
            lblGpuResult.AutoSize = true;
            lblGpuResult.Location = new System.Drawing.Point(29, 420);
            lblGpuResult.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblGpuResult.Name = "lblGpuResult";
            lblGpuResult.Size = new System.Drawing.Size(163, 20);
            lblGpuResult.TabIndex = 21;
            lblGpuResult.Text = "GPU Test: Not Executed";
            // 
            // deveTitle
            // 
            deveTitle.AutoSize = true;
            deveTitle.ForeColor = System.Drawing.SystemColors.ControlDark;
            deveTitle.Location = new System.Drawing.Point(138, 867);
            deveTitle.Name = "deveTitle";
            deveTitle.Size = new System.Drawing.Size(253, 20);
            deveTitle.TabIndex = 22;
            deveTitle.Text = "Developed by Dpfpic (Fabrice Piscia)";
            // 
            // flowLayoutPanelTests
            // 
            flowLayoutPanelTests.Controls.Add(btnTestGPU);
            flowLayoutPanelTests.Controls.Add(btnLaunchHWMonitor);
            flowLayoutPanelTests.Controls.Add(btnOpenReports);
            flowLayoutPanelTests.Location = new System.Drawing.Point(318, 545);
            flowLayoutPanelTests.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            flowLayoutPanelTests.Name = "flowLayoutPanelTests";
            flowLayoutPanelTests.Size = new System.Drawing.Size(239, 261);
            flowLayoutPanelTests.TabIndex = 23;
            // 
            // Separator_3
            // 
            Separator_3.BackColor = System.Drawing.SystemColors.Control;
            Separator_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            Separator_3.Location = new System.Drawing.Point(15, 901);
            Separator_3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Separator_3.Name = "Separator_3";
            Separator_3.Size = new System.Drawing.Size(858, 4);
            Separator_3.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(922, 908);
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
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "UltraBench by Dpfpic";
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
    }
}
