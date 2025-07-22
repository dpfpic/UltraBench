//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

using System.Windows.Forms;
using System.Collections.Generic; // For List<BenchmarkResult>
using System.Drawing;

namespace UltraBench
{
    public partial class HistoryForm : Form
    {
        private BenchmarkHistoryManager _historyManager;

        // Constructor that takes the history manager
        public HistoryForm(BenchmarkHistoryManager historyManager)
        {
            InitializeComponent();
            _historyManager = historyManager;
            LoadHistoryData(); // Call this method to load and display data
        }

        private void LoadHistoryData()
        {
            // Retrieve the history from the manager
            List<BenchmarkResult> history = _historyManager.GetHistory();

            // Bind the data to the DataGridView
            // This will automatically create columns based on public properties of BenchmarkResult
            dgvHistory.DataSource = history;

            // Optional: Customize DataGridView columns for better display
            // (e.g., format Timestamp, hide certain columns)
            if (dgvHistory.Columns.Contains("Timestamp"))
            {
                dgvHistory.Columns["Timestamp"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dgvHistory.Columns["Timestamp"].HeaderText = "Date & Time"; // Renomme l'en-tête
            }
            if (dgvHistory.Columns.Contains("DetailedResult"))
            {
                dgvHistory.Columns["DetailedResult"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            // Example of hiding a column if you don't want to show it:
            // if (dgvHistory.Columns.Contains("ActualDurationMs"))
            // {
            //     dgvHistory.Columns["ActualDurationMs"].Visible = false;
            // }
            if (dgvHistory.Columns.Contains("Score"))
            {
                dgvHistory.Columns["Score"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvHistory.Columns.Contains("ActualDurationMs"))
            {
                dgvHistory.Columns["ActualDurationMs"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Pour s'assurer que toutes les colonnes remplissent bien l'espace
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Ou si tu préfères que les colonnes s'ajustent à leur contenu et le reste se remplit
            // dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // dgvHistory.Columns["DetailedResult"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Pour que les détails prennent le reste
        }

        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Ensure the current column is "Score" and the value is not null.
            // This prevents errors and applies formatting only to the relevant column.
            if (dgvHistory.Columns[e.ColumnIndex].Name == "Score" && e.Value != null)
            {
                // Attempt to parse the cell's value to a double to enable numerical comparison.
                if (double.TryParse(e.Value.ToString(), out double score))
                {
                    // Define your score thresholds here. Adjust these values as needed
                    // to reflect what you consider "good", "medium", or "needs improvement" for UltraBench.
                    if (score >= 800) // Excellent score
                    {
                        e.CellStyle.BackColor = Color.LightGreen; // Light green background
                        e.CellStyle.ForeColor = Color.DarkGreen;  // Dark green text
                    }
                    else if (score >= 500) // Good / Medium score
                    {
                        e.CellStyle.BackColor = Color.LightYellow; // Light yellow background
                        e.CellStyle.ForeColor = Color.DarkGoldenrod; // Dark goldenrod text
                    }
                    else // Score needs improvement
                    {
                        e.CellStyle.BackColor = Color.LightCoral;  // Light red background
                        e.CellStyle.ForeColor = Color.DarkRed;    // Dark red text
                    }
                }
            }

            // You can add similar logic for other numerical columns, like "ActualDurationMs".
            // For duration, typically a lower number is better.
            if (dgvHistory.Columns[e.ColumnIndex].Name == "ActualDurationMs" && e.Value != null)
            {
                if (double.TryParse(e.Value.ToString(), out double duration))
                {
                    // Example: Very low duration is excellent
                    if (duration <= 50)
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                    }
                    else if (duration <= 200)
                    {
                        e.CellStyle.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.LightCoral;
                    }
                }
            }
        }
    }
}