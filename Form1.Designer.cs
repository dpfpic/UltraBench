namespace UltraBench
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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
        private System.Windows.Forms.Label lblSystemInfoTitl; // Declaration added/corrected
        private System.Windows.Forms.Label versionLabel;


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnLaunchHWMonitor = new System.Windows.Forms.Button();
            btnOpenReports = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            lblCpuInfo = new System.Windows.Forms.Label();
            lblRamInfo = new System.Windows.Forms.Label();
            lblGpuInfo = new System.Windows.Forms.Label();
            lblSystemInfoTitl = new System.Windows.Forms.Label();
            versionLabel = new System.Windows.Forms.Label();
            btnTestCPU = new System.Windows.Forms.Button();
            btnTestGPU = new System.Windows.Forms.Button();
            btnTestSSD = new System.Windows.Forms.Button();
            btnTestRAM = new System.Windows.Forms.Button();
            btnFullStress = new System.Windows.Forms.Button();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            btnTitle = new System.Windows.Forms.Label();
            lblResultsTitle = new System.Windows.Forms.Label();
            panelSeparator2 = new System.Windows.Forms.Panel();
            panelSeparator1 = new System.Windows.Forms.Panel();
            lblCpuResult = new System.Windows.Forms.Label();
            lblRamResult = new System.Windows.Forms.Label();
            lblSsdResult = new System.Windows.Forms.Label();
            lblGpuResult = new System.Windows.Forms.Label();
            deveTitle = new System.Windows.Forms.Label();
            flowLayoutPanelTests = new System.Windows.Forms.FlowLayoutPanel();
            Bontton = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            // lblSystemInfoTitl
            // 
            lblSystemInfoTitl.AutoSize = true;
            lblSystemInfoTitl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblSystemInfoTitl.ForeColor = System.Drawing.Color.DodgerBlue;
            lblSystemInfoTitl.Location = new System.Drawing.Point(29, 25);
            lblSystemInfoTitl.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblSystemInfoTitl.Name = "lblSystemInfoTitl";
            lblSystemInfoTitl.Size = new System.Drawing.Size(199, 28);
            lblSystemInfoTitl.TabIndex = 6;
            lblSystemInfoTitl.Text = "System Information";
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
            // pictureBox1
            // 
            pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new System.Drawing.Point(696, 41);
            pictureBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(177, 207);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // btnTitle
            // 
            btnTitle.AutoSize = true;
            btnTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            btnTitle.Location = new System.Drawing.Point(30, 520);
            btnTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            btnTitle.Name = "btnTitle";
            btnTitle.Size = new System.Drawing.Size(154, 28);
            btnTitle.TabIndex = 14;
            btnTitle.Text = "Start Test Here";
            // 
            // lblResultsTitle
            // 
            lblResultsTitle.AutoSize = true;
            lblResultsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblResultsTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            lblResultsTitle.Location = new System.Drawing.Point(30, 220);
            lblResultsTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lblResultsTitle.Name = "lblResultsTitle";
            lblResultsTitle.Size = new System.Drawing.Size(192, 28);
            lblResultsTitle.TabIndex = 15;
            lblResultsTitle.Text = "Benchmark Results";
            // 
            // panelSeparator2
            // 
            panelSeparator2.BackColor = System.Drawing.SystemColors.ControlDark;
            panelSeparator2.Location = new System.Drawing.Point(30, 480);
            panelSeparator2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            panelSeparator2.Name = "panelSeparator2";
            panelSeparator2.Size = new System.Drawing.Size(858, 4);
            panelSeparator2.TabIndex = 16;
            // 
            // panelSeparator1
            // 
            panelSeparator1.BackColor = System.Drawing.SystemColors.ControlDark;
            panelSeparator1.Location = new System.Drawing.Point(32, 195);
            panelSeparator1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            panelSeparator1.Name = "panelSeparator1";
            panelSeparator1.Size = new System.Drawing.Size(549, 4);
            panelSeparator1.TabIndex = 17;
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
            deveTitle.Size = new System.Drawing.Size(198, 20);
            deveTitle.TabIndex = 22;
            deveTitle.Text = "Developed by Dpfpic and IA";
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
            // Bontton
            // 
            Bontton.BackColor = System.Drawing.SystemColors.Control;
            Bontton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            Bontton.Location = new System.Drawing.Point(15, 901);
            Bontton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Bontton.Name = "Bontton";
            Bontton.Size = new System.Drawing.Size(858, 4);
            Bontton.TabIndex = 17;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(922, 908);
            Controls.Add(Bontton);
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
            Controls.Add(panelSeparator1);
            Controls.Add(btnClose);
            Controls.Add(panelSeparator2);
            Controls.Add(lblResultsTitle);
            Controls.Add(btnTitle);
            Controls.Add(pictureBox1);
            Controls.Add(lblGpuInfo);
            Controls.Add(lblSystemInfoTitl);
            Controls.Add(lblRamInfo);
            Controls.Add(lblCpuInfo);
            Controls.Add(versionLabel);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "UltraBench by Dpfpic";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            flowLayoutPanelTests.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label btnTitle;
        private System.Windows.Forms.Label lblResultsTitle;
        private System.Windows.Forms.Panel panelSeparator2;
        private System.Windows.Forms.Panel panelSeparator1;
        private System.Windows.Forms.Label lblCpuResult;
        private System.Windows.Forms.Label lblRamResult;
        private System.Windows.Forms.Label lblSsdResult;
        private System.Windows.Forms.Label lblGpuResult; // Declaration added/corrected
        private System.Windows.Forms.Label deveTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTests;
        private System.Windows.Forms.Panel Bontton;
    }
}
