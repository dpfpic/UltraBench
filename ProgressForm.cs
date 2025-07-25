//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

using System; // Make sure to have this import for 'Action' if you use it elsewhere or Math
using System.Windows.Forms; // For Form, Label, ProgressBar, MethodInvoker

namespace UltraBench // Make sure this namespace is the same as your Form1
{
    public partial class ProgressForm : Form
    {
        // IMPORTANT: lblProgressText and progressBar1 are user interface controls.
        // They are automatically declared in ProgressForm.Designer.cs.
        // DO NOT declare them here in ProgressForm.cs.

        public ProgressForm(string title, string initialMessage) // Parameters are 'title' and 'initialMessage'
        {
            InitializeComponent(); // Do not modify this line, it is generated by the designer

            // The lblProgressText and progressBar1 controls are initialized by InitializeComponent().
            // Make sure they are correctly added and named in your ProgressForm's designer.

            // Correction: Use 'title' which is the constructor parameter
            this.Text = title; // Sets the title of the progress window

            // Correction: Use 'initialMessage' which is the constructor parameter
            if (lblProgressText != null)
            {
                lblProgressText.Text = initialMessage; // Displays the initial message
            }
            else
            {
                // Fallback if lblProgressText is null (e.g., if designer didn't generate it yet or properly)
                // This would indicate a problem in the designer, but useful for robustness
                Console.WriteLine("Warning: lblProgressText is null in ProgressForm constructor.");
            }

            // Set initial progress bar values
            if (progressBar1 != null)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0; // Start at 0%
            }
        }

        /// <summary>
        /// Updates the progress bar and the progress text label.
        /// This method is thread-safe and can be called from any thread.
        /// </summary>
        /// <param name="message">The text message to display.</param>
        /// <param name="percentage">The progress percentage (0-100).</param>
        public void UpdateProgress(string message, int percentage)
        {
            if (!this.IsDisposed) // Checks if the window is not already closed
            {
                // Use BeginInvoke for thread safety if this method can be called from a non-UI thread
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        if (progressBar1 != null)
                        {
                            if (percentage >= progressBar1.Minimum && percentage <= progressBar1.Maximum)
                            {
                                progressBar1.Value = percentage;
                            }
                        }
                        if (lblProgressText != null)
                        {
                            lblProgressText.Text = message;
                            lblProgressText.Update();
                        }
                    });
                }
                else
                {
                    if (progressBar1 != null)
                    {
                        if (percentage >= progressBar1.Minimum && percentage <= progressBar1.Maximum)
                        {
                            progressBar1.Value = percentage;
                        }
                    }
                    if (lblProgressText != null)
                    {
                        lblProgressText.Text = message;
                    }
                }
            }
        }
    }
}