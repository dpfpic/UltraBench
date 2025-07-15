using System.Windows.Forms;

partial class TestInProgressForm
{
    private System.ComponentModel.IContainer components = null;
    private Label labelMessage;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.labelMessage = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // labelMessage
        // 
        this.labelMessage.Dock = System.Windows.Forms.DockStyle.Fill;
        this.labelMessage.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.labelMessage.Location = new System.Drawing.Point(0, 0);
        this.labelMessage.Name = "labelMessage";
        this.labelMessage.Size = new System.Drawing.Size(400, 100);
        this.labelMessage.TabIndex = 0;
        this.labelMessage.Text = "Test in progress...";
        this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // TestInProgressForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 100);
        this.Controls.Add(this.labelMessage);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Name = "TestInProgressForm";
        this.Text = "Test in Progress";
        this.ResumeLayout(false);
    }
}
