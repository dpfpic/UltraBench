//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UltraBench
{
    public partial class HistoryForm : Form
    {
        private readonly BenchmarkHistoryManager _historyManager;

        public HistoryForm(BenchmarkHistoryManager historyManager)
        {
            InitializeComponent();
            _historyManager = historyManager;
            this.Load += HistoryForm_Load;
            this.dgvHistory.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvHistory_CellFormatting);
        }

        private void HistoryForm_Load(object sender, System.EventArgs e)
        {
            LoadHistoryData();
        }

        private void LoadHistoryData()
        {
            List<BenchmarkResult> history = _historyManager.GetHistory().OrderByDescending(r => r.Timestamp).ToList();

            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();
            dgvHistory.Rows.Clear();
            dgvHistory.DataSource = null;

            // Column: Date & Time
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Timestamp",
                HeaderText = "Date & Time", // Renamed for simplicity
                DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm:ss", Alignment = DataGridViewContentAlignment.MiddleLeft },
                Width = 115,
                Resizable = DataGridViewTriState.False
            });

            // Column: Configuration
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "SystemConfiguration",
                HeaderText = "Configuration", // Renamed for simplicity
                                              // On revient au mode AllCells pour que le texte soit toujours entièrement visible.
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Column: Test Type
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Title",
                HeaderText = "Test Type", // Renamed for simplicity
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                Width = 40,
                Resizable = DataGridViewTriState.False
            });

            // Column: Score
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "Score",
                HeaderText = "Score", // Renamed for simplicity
                Name = "ScoreColumn", // Added a name for easier reference
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            // Column: Detailed Result
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "DetailedResult",
                HeaderText = "Detailed Result",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                // On définit une largeur minimale pour cette colonne
                MinimumWidth = 200,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft, WrapMode = DataGridViewTriState.False }
            });

            // Column: Duration (ms)
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn()
            {
                DataPropertyName = "ActualDurationMs",
                HeaderText = "Duration (ms)", // Renamed for simplicity
                Name = "DurationColumn", // Added a name for easier reference
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleRight },
                Width = 50,
                Resizable = DataGridViewTriState.False
            });

            // Column: Success
            dgvHistory.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                DataPropertyName = "Success",
                HeaderText = "Success", // Renamed for simplicity
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            });

            dgvHistory.DataSource = history;

            AdjustFormSizeToContent();
        }

        private void AdjustFormSizeToContent()
        {
            dgvHistory.Dock = DockStyle.Fill;
            int newWidth = dgvHistory.RowHeadersWidth + 10;
            foreach (DataGridViewColumn col in dgvHistory.Columns)
            {
                newWidth += col.Width;
            }
            this.ClientSize = new Size(newWidth, this.ClientSize.Height);

            this.ClientSize = new Size(newWidth + SystemInformation.VerticalScrollBarWidth, this.ClientSize.Height);
        }

        private void dgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // First, reset the cell's style to its default.
            // This is crucial to prevent colors from carrying over to other rows.
            if (e.RowIndex % 2 == 0)
            {
                e.CellStyle.BackColor = dgvHistory.DefaultCellStyle.BackColor;
                e.CellStyle.ForeColor = dgvHistory.DefaultCellStyle.ForeColor;
            }
            else
            {
                e.CellStyle.BackColor = dgvHistory.AlternatingRowsDefaultCellStyle.BackColor;
                e.CellStyle.ForeColor = dgvHistory.AlternatingRowsDefaultCellStyle.ForeColor;
            }

            // Now, apply the custom formatting logic for the "ScoreColumn".
            if (dgvHistory.Columns[e.ColumnIndex].Name == "ScoreColumn" && e.Value != null)
            {
                if (double.TryParse(e.Value.ToString(), out double score))
                {
                    // Use the score thresholds from the config file
                    if (score >= ConfigManager.Config.BenchmarkThresholds.CpuScoreGood) // We're using CPU as a placeholder
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.GoodResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }
                    else if (score >= ConfigManager.Config.BenchmarkThresholds.CpuScoreAverage)
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.AverageResult);
                        e.CellStyle.ForeColor = Color.Black; // Use a dark color for better contrast
                    }
                    else
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.BadResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }

                    // Use the score thresholds from the config file
                    if (score >= ConfigManager.Config.BenchmarkThresholds.RamScoreGood) // We're using RAM as a placeholder
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.GoodResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }
                    else if (score >= ConfigManager.Config.BenchmarkThresholds.RamScoreAverage)
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.AverageResult);
                        e.CellStyle.ForeColor = Color.Black; // Use a dark color for better contrast
                    }
                    else
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.BadResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }

                    // Use the score thresholds from the config file
                    if (score >= ConfigManager.Config.BenchmarkThresholds.SsdScoreGood) // We're using SSD as a placeholder
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.GoodResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }
                    else if (score >= ConfigManager.Config.BenchmarkThresholds.SsdScoreAverage)
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.AverageResult);
                        e.CellStyle.ForeColor = Color.Black; // Use a dark color for better contrast
                    }
                    else
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.BadResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }

                    // Use the score thresholds from the config file
                    if (score >= ConfigManager.Config.BenchmarkThresholds.GpuScoreGood) // We're using GPU as a placeholder
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.GoodResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }
                    else if (score >= ConfigManager.Config.BenchmarkThresholds.GpuScoreAverage)
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.AverageResult);
                        e.CellStyle.ForeColor = Color.Black; // Use a dark color for better contrast
                    }
                    else
                    {
                        e.CellStyle.BackColor = ConfigManager.GetColor(ConfigManager.Config.ColorThresholds.BadResult);
                        e.CellStyle.ForeColor = Color.White; // Use a light color for better contrast
                    }
                }
            }
        }
    }
}