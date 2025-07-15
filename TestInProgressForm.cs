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
