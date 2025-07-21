//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AppLocator;
using DrawingPoint = System.Drawing.Point;
using Path = System.IO.Path;

namespace UltraBench
{
    public partial class Form1 : Form
    {
        private Random _random = new Random();
        private List<BenchmarkResult> _benchmarkResults;

        // Paths of third-party executables
        // private const string PassMarkExePath = @"C:\Program Files\PerformanceTest\PerformanceTest64.exe";

        // Property for HWMonitor path that searches for an installed version
        private string HWMonitorExePath
        {
            get
            {
                return AppLocator.LocateAppExecutable("CPUID", "HWMonitor.exe");
            }
        }

        private string PassMarkExePath
        {
            get
            {
                return AppLocator.LocatePerformanceTestExecutable();
            }
        }

        // <summary>
        // Constructor for the main form. Initializes components.
        // </summary>
        public Form1()
        {
            InitializeComponent();
            InitializeHelpScoresButton();
            InitializeResultLabels();

            _benchmarkResults = new List<BenchmarkResult>();

            if (lblCpuResult != null) lblCpuResult.Text = "CPU Test: Not Executed";
            if (lblGpuResult != null) lblGpuResult.Text = "GPU Test: Not Executed";
            if (lblSsdResult != null) lblSsdResult.Text = "SSD Test: Not Executed";
            if (lblRamResult != null) lblRamResult.Text = "RAM Test: Not Executed";
        }

        // Method to initialize the state of result labels at startup
        private void InitializeResultLabels()
        {
            SetResultNotExecuted(lblCpuResult, "CPU"); // Initialise le texte et la couleur
            SetResultNotExecuted(lblRamResult, "RAM");
            SetResultNotExecuted(lblSsdResult, "SSD");
            SetResultNotExecuted(lblGpuResult, "GPU");
        }

        // Generic method to mettre à jour un label de résultat avec couleur
        private void UpdateResultLabel(Label resultLabel, string testName, double score, double goodThreshold, double averageThreshold)
        {
            resultLabel.Text = $"{testName} Test: Score {score:F0}"; // :F2 pour formater avec 2 décimales si besoin

            if (score >= goodThreshold)
            {
                resultLabel.ForeColor = BenchmarkDisplayConstants.COLOR_GOOD_RESULT;
            }
            else if (score >= averageThreshold)
            {
                resultLabel.ForeColor = BenchmarkDisplayConstants.COLOR_AVERAGE_RESULT;
            }
            else
            {
                resultLabel.ForeColor = BenchmarkDisplayConstants.COLOR_BAD_RESULT;
            }
        }

        // Method for a label not executed or in case of error
        private void SetResultNotExecuted(Label resultLabel, string testName)
        {
            resultLabel.Text = $"{testName} Test: Not Executed";
            resultLabel.ForeColor = BenchmarkDisplayConstants.COLOR_NOT_EXECUTED;
        }

        // <summary>
        // Event handler for the Form1 Load event.
        // This is where you put code that interacts with the UI and system after the form is fully initialized.
        // </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            versionLabel.Text = "Version : 1.0.0";
            this.Text = "UltraBench by Dpfpic - " + versionLabel.Text;

            if (lblCpuInfo != null) lblCpuInfo.Text = $"Processor: {GetProcessorName()}";
            if (lblRamInfo != null) lblRamInfo.Text = $"RAM: {GetTotalRamAmount()}";
            if (lblGpuInfo != null) lblGpuInfo.Text = $"Graphics Card: {GetGpuName()}";

            if (btnTestGPU != null) // Assurez-vous que le bouton est initialisé
            {
                btnTestGPU.Visible = File.Exists(PassMarkExePath); // Nouvelle logique
            }

            // IMPORTANT CHECK: This is where the method is called when the form loads
            CheckExternalExecutablesPresence();
        }


        // <summary>
        // Method that checks for the presence of external executables and adjusts button visibility.
        // </summary>
        private void CheckExternalExecutablesPresence()
        {
            // Make the "Test GPU" button visible only if PassMark PerformanceTest is found.
            if (btnTestGPU != null)
            {
                btnTestGPU.Visible = File.Exists(PassMarkExePath); // Assurez-vous que c'est bien PassMarkExePath
            }

            // Make the "Launch HWMonitor" button visible only if HWMonitor is found.
            if (btnLaunchHWMonitor != null)
            {
                btnLaunchHWMonitor.Visible = !string.IsNullOrEmpty(HWMonitorExePath);
            }

            // The "Full Stress Test" button is now always visible,
            // its execution logic will handle the absence of the GPU test.
            // (Removed the line: btnFullStress.Visible = btnTestGPU.Visible;)

            // The "Open Reports Folder" button is supposed to be always visible
            // as it is not linked to an external tool that might be missing.
            // If it's in the FlowLayoutPanel, its visibility doesn't need to be managed here,
            // as it doesn't depend on anything to appear or disappear.
            // The FlowLayoutPanel will simply manage its positioning relative to other visible buttons.
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetProcessorName()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select Name from Win32_Processor"))
                {
                    foreach (var item in searcher.Get())
                    {
                        return item["Name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving CPU name: {ex.Message}");
                return $"Error: {ex.Message}";
            }
            return "Not Detected";
        }

        private string GetTotalRamAmount()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select TotalPhysicalMemory from Win32_ComputerSystem"))
                {
                    foreach (ManagementObject system in searcher.Get())
                    {
                        ulong totalMemoryBytes = (ulong)system["TotalPhysicalMemory"];
                        return $"{(totalMemoryBytes / (1024.0 * 1024.0 * 1024.0)):F2} GB";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving RAM amount: {ex.Message}");
                return $"Error: {ex.Message}";
            }
            return "Not Detected";
        }

        private string GetGpuName()
        {
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select Name from Win32_VideoController"))
                {
                    foreach (ManagementObject gpu in searcher.Get())
                    {
                        return gpu["Name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving GPU name: {ex.Message}");
                return $"Error: {ex.Message}";
            }
            return "Not Detected";
        }

        private BenchmarkResult RunSsdBenchmarkCombined(string drivePath, Action<int, string> progressAction)
        {
            string testDirectory = System.IO.Path.Combine(drivePath, "UltraBenchTemp");
            string testFilePath = System.IO.Path.Combine(testDirectory, "ultrabench_ssd_test.tmp");

            byte[] buffer = new byte[BenchmarkSettingsConstants.SsdBufferSizeKb * 1024];
            long totalBlocks = (BenchmarkSettingsConstants.SsdTestFileSizeMb * 1024 * 1024) / (BenchmarkSettingsConstants.SsdBufferSizeKb * 1024);

            double writeSpeed = 0;
            double readSpeed = 0;
            long totalDuration = 0;

            try
            {
                System.IO.Directory.CreateDirectory(testDirectory);

                // --- Write Test ---
                _random.NextBytes(buffer);
                Stopwatch swWrite = Stopwatch.StartNew();
                progressAction?.Invoke(0, "SSD : Writing test file...");
                using (FileStream fs = new FileStream(testFilePath, FileMode.Create, FileAccess.Write, FileShare.None, buffer.Length, FileOptions.WriteThrough))
                {
                    for (long i = 0; i < totalBlocks; i++)
                    {
                        fs.Write(buffer, 0, buffer.Length);
                        int currentProgress = (int)((double)i / totalBlocks * 50.0);
                        if (currentProgress < 0) currentProgress = 0;
                        if (currentProgress > 50) currentProgress = 50;
                        progressAction?.Invoke(currentProgress, $"SSD : Writing... {currentProgress}%");
                    }
                }
                swWrite.Stop();
                totalDuration += swWrite.ElapsedMilliseconds;
                if (swWrite.ElapsedMilliseconds > 0)
                {
                    writeSpeed = (BenchmarkSettingsConstants.SsdTestFileSizeMb * 1000.0) / swWrite.ElapsedMilliseconds;
                }
                progressAction?.Invoke(50, $"SSD : Write Complete ({writeSpeed:F2} MB/s)");

                // --- Read Test ---
                Stopwatch swRead = Stopwatch.StartNew();
                progressAction?.Invoke(50, "SSD : Reading test file...");
                using (FileStream fs = new FileStream(testFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, buffer.Length, FileOptions.None))
                {
                    for (long i = 0; i < totalBlocks; i++)
                    {
                        int bytesRead = fs.Read(buffer, 0, buffer.Length);

                        int totalBytesRead = 0;
                        int bytesReadThisIteration;
                        while (totalBytesRead < buffer.Length && (bytesReadThisIteration = fs.Read(buffer, totalBytesRead, buffer.Length - totalBytesRead)) > 0)
                        {
                            totalBytesRead += bytesReadThisIteration;
                        }
                        int currentProgress = 50 + (int)((double)i / totalBlocks * 50.0);
                        if (currentProgress < 50) currentProgress = 50;
                        if (currentProgress > 100) currentProgress = 100;
                        progressAction?.Invoke(currentProgress, $"SSD : Reading... {currentProgress}%");
                    }
                }
                swRead.Stop();
                totalDuration += swRead.ElapsedMilliseconds;
                if (swRead.ElapsedMilliseconds > 0)
                {
                    readSpeed = (BenchmarkSettingsConstants.SsdTestFileSizeMb * 1000.0) / swRead.ElapsedMilliseconds;
                }
                progressAction?.Invoke(100, $"SSD : Read Complete ({readSpeed:F2} MB/s)");


                long score = (long)(((writeSpeed + readSpeed) / 2.0) * BenchmarkSettingsConstants.SsdScoreMultiplier);

                return new BenchmarkResult { Title = "SSD", Score = (int)score, DetailedResult = $"Write: {writeSpeed:F2} MB/s, Read: {readSpeed:F2} MB/s", ActualDurationMs = totalDuration, Success = true, TestedDrive = drivePath };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SSD Benchmark Error: {ex.Message}");
                progressAction?.Invoke(0, $"SSD : Error: {ex.Message}");
                return new BenchmarkResult
                {
                    Title = "SSD",
                    Score = 0,
                    DetailedResult = $"Error: {ex.Message}",
                    ActualDurationMs = 0,
                    Success = false,
                    TestedDrive = drivePath
                };
            }
            finally
            {
                if (System.IO.File.Exists(testFilePath))
                {
                    System.IO.File.Delete(testFilePath);
                }
                if (System.IO.Directory.Exists(testDirectory))
                {
                    try
                    {
                        if (!System.IO.Directory.EnumerateFileSystemEntries(testDirectory).Any())
                        {
                            System.IO.Directory.Delete(testDirectory);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Could not delete temporary SSD directory '{testDirectory}': {ex.Message}");
                    }
                }
            }
        }

        private BenchmarkResult RunRamBenchmark(Action<int, string> progressAction)
        {
            byte[] buffer = new byte[BenchmarkSettingsConstants.RamBufferSizeKb * 1024];
            long totalBytesToProcess = BenchmarkSettingsConstants.RamTestSizeMb * 1024 * 1024;

            Stopwatch sw = Stopwatch.StartNew();

            for (long i = 0; i < totalBytesToProcess; i += buffer.Length)
            {
                _random.NextBytes(buffer);

                int percentage = (int)(((double)i / totalBytesToProcess) * 100.0);
                if (percentage % 5 == 0 || i == 0 || i + buffer.Length >= totalBytesToProcess)
                {
                    progressAction?.Invoke(percentage, $"RAM : Processing {percentage}%...");
                }
            }

            sw.Stop();

            double ramSpeed = 0;
            if (sw.ElapsedMilliseconds > 0)
            {
                ramSpeed = (totalBytesToProcess / (1024.0 * 1024.0)) / (sw.ElapsedMilliseconds / 1000.0);
            }

            long score = (long)(ramSpeed * BenchmarkSettingsConstants.RamScoreMultiplier);

            progressAction?.Invoke(100, "RAM : Benchmark Complete.");

            return new BenchmarkResult
            {
                Title = "RAM",
                Score = (int)score,
                DetailedResult = $"Throughput: {ramSpeed:F2} MB/s",
                ActualDurationMs = sw.ElapsedMilliseconds,
                Success = true
            };
        }

        private long CalculatePrimes(int limit)
        {
            long count = 0;
            for (int i = 2; i <= limit; i++)
            {
                bool isPrime = true;
                for (int j = 2; j * j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    count++;
                }
            }
            return count;
        }

        public async Task<BenchmarkResult> RunGpuBenchmarkPassMark(ProgressForm progressForm)
        {
            string reportFileName = $"PassMark_GPU_Report_{DateTime.Now:yyyyMMdd_HHmmss}.html";
            string reportPath = Path.Combine(Path.GetTempPath(), reportFileName);

            string scriptFileName = $"PassMark_GPU_Script_{DateTime.Now:yyyyMMmmss}.ptscript";
            string scriptPath = Path.Combine(Path.GetTempPath(), scriptFileName);

            string scriptContent = $@"
RUN G3D_DIRECTCOMPUTE
EXPORTHTML ""{reportPath}""
EXIT
";
            if (!File.Exists(PassMarkExePath))
            {
                MessageBox.Show("PassMark PerformanceTest is not found at the specified location.\n" +
                                "Please install it or modify the path in the code.",
                                "Error: PassMark Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new BenchmarkResult
                {
                    Title = "GPU Test",
                    Score = 0,
                    DetailedResult = "PassMark PerformanceTest not found."
                };
            }

            try
            {
                progressForm.UpdateProgress("Preparing GPU test...", BenchmarkSettingsConstants.GpuProgressInitial);
                await File.WriteAllTextAsync(scriptPath, scriptContent);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = PassMarkExePath,
                    Arguments = $"/s \"{scriptPath}\"",
                    UseShellExecute = true, // Utiliser true pour "runas"
                    Verb = "runas",       // Nécessite des droits admin pour démarrer PassMark
                    CreateNoWindow = false
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    progressForm.UpdateProgress("Launching GPU test with PassMark PerformanceTest...", BenchmarkSettingsConstants.GpuProgressCopyReport);
                    process.Start();
                    progressForm.UpdateProgress("GPU test in progress (this may take several minutes)...", BenchmarkSettingsConstants.GpuProgressParseReport);
                    await Task.Run(() => process.WaitForExit(BenchmarkSettingsConstants.GpuBenchmarkTimeoutMs));

                    if (!process.HasExited)
                    {
                        // Attempt to kill the process si le timeout est atteint et qu'il ne s'est pas arrêté
                        try
                        {
                            process.Kill();
                        }
                        catch (InvalidOperationException)
                        {
                            // Le processus a peut-être déjà quitté ou n'existe plus
                        }
                        return new BenchmarkResult
                        {
                            Title = "GPU Test",
                            Score = 0,
                            DetailedResult = "The PassMark test did not complete in time or was manually closed. The report may not have been generated."
                        };
                    }
                }

                progressForm.UpdateProgress("Retrieving GPU test results...", BenchmarkSettingsConstants.GpuProgressFinalizing);
                if (File.Exists(reportPath))
                {
                    string reportContent = await File.ReadAllTextAsync(reportPath, Encoding.Default); // Change Encoding.Unicode to Encoding.Default or Encoding.UTF8 as HTML is often not Unicode

                    // --- START OF NEW LOGIC AVEC HTML AGILITY PACK ---
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(reportContent);

                    double gpuScore = 0;
                    string detailedResult = "Could not find GPU Compute score in the report.";

                    // On cherche le <tr> qui contient "GPU Compute (Ops./Sec.)" dans son premier <td>
                    var targetRow = htmlDoc.DocumentNode.SelectNodes("//tr")
                                            ?.FirstOrDefault(tr => tr.SelectNodes("td")?
                                                                    .FirstOrDefault()?.InnerText.Contains("GPU Compute (Ops./Sec.)", StringComparison.OrdinalIgnoreCase) == true);

                    if (targetRow != null)
                    {
                        // On récupère tous les <td> de cette ligne
                        var tds = targetRow.SelectNodes("td");

                        if (tds != null && tds.Any())
                        {
                            // La valeur est dans le dernier <td>
                            var lastTd = tds.LastOrDefault();

                            if (lastTd != null)
                            {
                                string rawScoreText = lastTd.InnerText.Trim(); // Supprime les espaces blancs

                                // On extrait le nombre avant le " (" s'il existe
                                int parenthesisIndex = rawScoreText.IndexOf(" (");
                                if (parenthesisIndex != -1)
                                {
                                    rawScoreText = rawScoreText.Substring(0, parenthesisIndex);
                                }

                                if (double.TryParse(rawScoreText, NumberStyles.Any, CultureInfo.InvariantCulture, out gpuScore))
                                {
                                    detailedResult = ""; // Score trouvé, pas d'erreur détaillée
                                }
                                else
                                {
                                    detailedResult = $"Could not convert score '{rawScoreText}' to a number. Original content: '{lastTd.InnerText}'";
                                    gpuScore = 0; // S'assurer que le score est à 0 en cas d'échec de parsing
                                }
                            }
                        }
                    }
                    // --- END OF NEW LOGIC ---

                    return new BenchmarkResult
                    {
                        Title = "GPU Test",
                        Score = gpuScore,
                        DetailedResult = detailedResult
                    };
                }
                else
                {
                    return new BenchmarkResult
                    {
                        Title = "GPU Test",
                        Score = 0,
                        DetailedResult = "The PassMark PerformanceTest report was not generated."
                    };
                }
            }
            catch (Exception ex)
            {
                // Don't forget to log l'exception réelle pour le débogage !
                // Console.WriteLine($"Erreur: {ex.Message}");
                return new BenchmarkResult
                {
                    Title = "GPU Test",
                    Score = 0,
                    DetailedResult = $"An error occurred during PassMark benchmark execution: {ex.Message}"
                };
            }
            finally
            {
                // Cleanup temporary files temporaires, très bien ça !
                if (File.Exists(scriptPath))
                {
                    File.Delete(scriptPath);
                }
                // You may also consider de supprimer le rapport HTML si tu n'en as plus besoin après lecture
                if (File.Exists(reportPath))
                {
                    File.Delete(reportPath);
                }
            }
        }

        private void btnLaunchHWMonitor_Click(object sender, EventArgs e)
        {
            // C'est ici que tu devrais t'assurer que HWMonitorExePath a la bonne valeur.
            // Par exemple: HWMonitorExePath = AppLocator.LocateAppExecutable(AppConstants.HW_MONITOR_APP_NAME, AppConstants.HW_MONITOR_EXE_NAME);

            if (!string.IsNullOrEmpty(HWMonitorExePath))
            {
                bool isUltraBenchAdmin = IsAdministrator();

                if (!isUltraBenchAdmin)
                {
                    // Messagebox: Pre-UAC warning
                    DialogResult result = MessageBox.Show(
                        "HWMonitor requires administrator privileges to function correctly. " +
                        "A User Account Control (UAC) prompt will appear. " +
                        "Do you want to continue?",
                        "Launching HWMonitor",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (result == DialogResult.No)
                    {
                        return; // User cancelled launch
                    }
                }

                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = HWMonitorExePath,
                        UseShellExecute = true, // Required for 'Verb'
                        Verb = "runas"
                    };
                    Process.Start(startInfo);
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    // User cancelled UAC prompt (Error Code 1223)
                    if (ex.NativeErrorCode == 1223)
                    {
                        MessageBox.Show("HWMonitor launch was cancelled by the user (UAC prompt).", "Launch Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // General Win32Exception error
                        MessageBox.Show($"An error occurred while launching HWMonitor: {ex.Message}\n\n" +
                                        "Please ensure HWMonitor is correctly installed and your system allows launching applications in administrator mode.", "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    System.Diagnostics.Debug.WriteLine($"HWMonitor launch error (Win32Exception): {ex.Message}");
                }
                catch (Exception ex)
                {
                    // General unexpected error
                    MessageBox.Show($"Could not launch HWMonitor: {ex.Message}", "Launch Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine($"HWMonitor launch error (General Exception): {ex.Message}");
                }
            }
            else
            {
                // HWMonitor not found error
                MessageBox.Show("HWMonitor not found. Please ensure it is correctly installed on your system.", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // <summary>
        // Checks if the current application (UltraBench) is running with administrator privileges.
        // </summary>
        // <returns>True if the application is administrator, otherwise False.</returns>
        private bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void btnOpenReports_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            System.IO.Directory.CreateDirectory(path);
            //Process.Start("explorer.exe", path);
            Tools.OpenFolder(path);

        }

        private async Task<BenchmarkResult> RunBenchmarkAndGetResult(string testType, int durationSeconds, ProgressForm progressForm, string specificDrivePath = null)
        {
            BenchmarkResult result = null;
            Action<int, string> updateProgressAction = (percentage, message) =>
            {
                progressForm.UpdateProgress(message, percentage);
            };

            try
            {
                switch (testType)
                {
                    case "CPU":
                        progressForm.UpdateProgress("CPU : Starting CPU benchmark...", 0);
                        await Task.Run(() =>
                        {
                            long currentLimit = BenchmarkSettingsConstants.CpuCalibrationLimitInitial;
                            long primesFound = 0;
                            Stopwatch calibrationSw = new Stopwatch();

                            for (int i = 0; i < 5; i++)
                            {
                                calibrationSw.Restart();
                                primesFound = CalculatePrimes((int)currentLimit);
                                calibrationSw.Stop();

                                if (calibrationSw.ElapsedMilliseconds > 0)
                                {
                                    double factor = (double)BenchmarkSettingsConstants.TargetCpuBenchmarkDurationMs / calibrationSw.ElapsedMilliseconds;
                                    currentLimit = (long)(currentLimit * Math.Pow(factor, 1.0 / BenchmarkSettingsConstants.CpuComplexityPower));
                                    if (currentLimit < 10000) currentLimit = 10000;
                                    if (currentLimit > 50000000) currentLimit = 50000000;
                                }
                                updateProgressAction((i + 1) * 10, $"CPU : Calibrating... {((i + 1) * 10)}%");
                            }

                            Stopwatch finalSw = Stopwatch.StartNew();
                            primesFound = CalculatePrimes((int)currentLimit);
                            finalSw.Stop();

                            double primeCalculationSpeed = 0;
                            if (finalSw.ElapsedMilliseconds > 0)
                            {
                                primeCalculationSpeed = (double)primesFound / finalSw.ElapsedMilliseconds;
                            }

                            long score = (long)(primeCalculationSpeed * BenchmarkSettingsConstants.CpuScoreMultiplier);
                            result = new BenchmarkResult
                            {
                                Title = "CPU",
                                Score = (int)score,
                                DetailedResult = $"Primes found: {primesFound}, Time: {finalSw.ElapsedMilliseconds} ms",
                                ActualDurationMs = finalSw.ElapsedMilliseconds,
                                Success = true
                            };
                            updateProgressAction(100, "CPU : Benchmark Complete.");
                        });
                        break;

                    case "SSD":
                        progressForm.UpdateProgress("SSD : Starting SSD benchmark...", 0);
                        result = await Task.Run(() => RunSsdBenchmarkCombined(specificDrivePath, updateProgressAction));
                        break;

                    case "RAM":
                        progressForm.UpdateProgress("RAM : Starting RAM benchmark...", 0);
                        result = await Task.Run(() => RunRamBenchmark(updateProgressAction));
                        break;

                    case "GPU":
                        result = await RunGpuBenchmarkPassMark(progressForm);
                        break;

                    default:
                        result = new BenchmarkResult { Title = testType, Score = 0, DetailedResult = "Test type not recognized.", Success = false };
                        break;
                }
            }
            catch (Exception ex)
            {
                result = new BenchmarkResult { Title = testType, Score = 0, DetailedResult = $"Error: {ex.Message}", Success = false };
                updateProgressAction(0, $"{testType} : Error: {ex.Message}");
                MessageBox.Show($"An unexpected error occurred during the {testType} test: {ex.Message}", "Benchmark Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private async void btnTestCPU_Click(object sender, EventArgs e)
        {
            // Clearing TextBoxes or Display Labels
            SetResultNotExecuted(lblCpuResult, "CPU");

            using (ProgressForm progressForm = new ProgressForm("CPU Test Progress", "Starting CPU test..."))
            {
                progressForm.Show();
                BenchmarkResult cpuResult = await RunBenchmarkAndGetResult("CPU", BenchmarkSettingsConstants.TargetCpuBenchmarkDurationMs / 1000, progressForm);
                if (cpuResult != null)
                {
                    if (lblCpuResult != null)
                    {
                        UpdateResultLabel(lblCpuResult, "CPU", cpuResult.Score,
                            BenchmarkDisplayConstants.CPU_SCORE_GOOD_THRESHOLD,
                            BenchmarkDisplayConstants.CPU_SCORE_AVERAGE_THRESHOLD);
                    }
                    _benchmarkResults.Add(cpuResult);
                }
                else
                {
                    SetResultNotExecuted(lblCpuResult, "CPU");
                }
                progressForm.Close();
            }
        }

        private async void btnTestRAM_Click(object sender, EventArgs e)
        {
            // Clearing TextBoxes or Display Labels
            SetResultNotExecuted(lblRamResult, "RAM");

            // Ajoute un court délai pour permettre à l'UI de rafraîchir le label
            await Task.Delay(20); // Attendre 20 millisecondes (ajuste si besoin)

            using (ProgressForm progressForm = new ProgressForm("RAM Test Progress", "Starting RAM test..."))
            {
                progressForm.Show();
                BenchmarkResult ramResult = await Task.Run(() => RunBenchmarkAndGetResult("RAM", 0, progressForm));
                if (ramResult != null)
                {
                    if (lblRamResult != null)
                    {
                        UpdateResultLabel(lblRamResult, "RAM", ramResult.Score,
                                        BenchmarkDisplayConstants.RAM_SCORE_GOOD_THRESHOLD,
                                        BenchmarkDisplayConstants.RAM_SCORE_AVERAGE_THRESHOLD);
                    }
                    _benchmarkResults.Add(ramResult);
                }
                else
                {
                    SetResultNotExecuted(lblRamResult, "RAM");
                }
                progressForm.Close();
            }
        }

        private async void btnTestSSD_Click(object sender, EventArgs e)
        {
            // Clearing TextBoxes or Display Labels
            SetResultNotExecuted(lblSsdResult, "SSD");

            // --- ADDED: Explanation MessageBox for SSD Drive Selection (now in English) ---
            MessageBox.Show(
                "The SSD/HDD performance test requires you to select the drive (e.g., C:\\ or D:\\) where temporary test files will be created. " +
                "Please choose a drive with enough free space (around 1 GB) and click 'OK' in the next window.",
                "SSD/HDD Drive Selection Required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                // UPDATED: Removed the description text from the FolderBrowserDialog
                fbd.Description = "";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string selectedDrivePath = fbd.SelectedPath;
                    using (ProgressForm progressForm = new ProgressForm("SSD Test Progress", $"Starting SSD test on {selectedDrivePath}..."))
                    {
                        progressForm.Show();
                        BenchmarkResult ssdResult = await Task.Run(() => RunBenchmarkAndGetResult("SSD", 0, progressForm, selectedDrivePath));
                        if (lblSsdResult != null)
                        {
                            UpdateResultLabel(lblSsdResult, "SSD", ssdResult.Score,
                                              BenchmarkDisplayConstants.SSD_SCORE_GOOD_THRESHOLD,
                                              BenchmarkDisplayConstants.SSD_SCORE_AVERAGE_THRESHOLD);
                        }
                        _benchmarkResults.Add(ssdResult);
                        progressForm.Close();
                    }
                }
                else
                {
                    MessageBox.Show("SSD test cancelled. Please select a drive to continue.", "Test Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Add a result for SSD test indicating it was skipped/cancelled
                    _benchmarkResults.Add(new BenchmarkResult
                    {
                        Title = "SSD",
                        Score = 0,
                        DetailedResult = "SSD Test skipped (Drive selection cancelled)",
                        ActualDurationMs = 0,
                        Success = false
                    });
                }
            }
        }

        private async void btnTestGPU_Click(object sender, EventArgs e)
        {
            // Clearing TextBoxes or Display Labels
            SetResultNotExecuted(lblGpuResult, "GPU");

            if (string.IsNullOrEmpty(PassMarkExePath) || !File.Exists(PassMarkExePath))
            {
                MessageBox.Show("PassMark PerformanceTest is not installed or cannot be found.", "Feature Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (ProgressForm progressForm = new ProgressForm("GPU Test Progress", "Starting GPU test..."))
            {
                progressForm.Show();
                BenchmarkResult gpuResult = await RunBenchmarkAndGetResult("GPU", 0, progressForm);

                if (lblGpuResult != null)
                {
                    UpdateResultLabel(lblGpuResult, "GPU", gpuResult.Score,
                                      BenchmarkDisplayConstants.GPU_SCORE_GOOD_THRESHOLD,
                                      BenchmarkDisplayConstants.GPU_SCORE_AVERAGE_THRESHOLD);
                }
                _benchmarkResults.Add(gpuResult);
                progressForm.Close();
            }
        }

        private async void btnFullStress_Click(object sender, EventArgs e)
        {
            // Clearing TextBoxes or Display Labels
            InitializeResultLabels();

            _benchmarkResults.Clear();

            // Guard to ensure the button is not clickable if it is hidden (though it should always be visible now)
            if (!btnFullStress.Visible)
            {
                MessageBox.Show("The full stress test is unavailable because PassMark PerformanceTest is not found.", "Feature Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Declare selectedDrivePath here to make it accessible later
            string selectedDrivePath = null;

            // Execute tests sequentially in the desired order: CPU, RAM, GPU (conditional), SSD
            await ExecuteSingleBenchmark("CPU", BenchmarkSettingsConstants.TargetCpuBenchmarkDurationMs / 1000, null);
            await ExecuteSingleBenchmark("RAM", 0, null);

            // --- UPDATED: Explanation MessageBox for SSD Drive Selection (now in English) ---
            MessageBox.Show(
                "The SSD/HDD performance test requires you to select the drive (e.g., C:\\ or D:\\) where temporary test files will be created. " +
                "Please choose a drive with enough free space (around 1 GB) and click 'OK' in the next window.",
                "SSD/HDD Drive Selection Required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // --- SSD Drive Selection (THIS IS THE CORRECT PLACE) ---
            // This dialog will now appear AFTER CPU, RAM, and GPU tests have started/finished.
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                // UPDATED: Removed the description text from the FolderBrowserDialog
                fbd.Description = "";
                if (fbd.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Full stress test cancelled. Please select a drive for the SSD test.", "Test Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Add a result for SSD test indicating it was skipped/cancelled
                    _benchmarkResults.Add(new BenchmarkResult
                    {
                        Title = "SSD",
                        Score = 0,
                        DetailedResult = "SSD Test skipped (Drive selection cancelled)",
                        ActualDurationMs = 0,
                        Success = false
                    });
                    return; // Exit the Full Stress Test if SSD selection is cancelled
                }
                selectedDrivePath = fbd.SelectedPath;
            }

            await ExecuteSingleBenchmark("SSD", 0, selectedDrivePath); // SSD test after selection

            // --- Execute GPU test (conditional) ---
            if (btnTestGPU.Visible) // Check if the GPU test is available (i.e., PassMark is installed)
            {
                await ExecuteSingleBenchmark("GPU", 0, null);
            }
            else
            {
                // If the GPU test is not available, add a result indicating it was skipped
                _benchmarkResults.Add(new BenchmarkResult
                {
                    Title = "GPU",
                    Score = 0,
                    DetailedResult = "GPU Test skipped (PassMark PerformanceTest not found)",
                    ActualDurationMs = 0,
                    Success = false
                });
            }

            MessageBox.Show("Full stress test completed!", "UltraBench", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (_benchmarkResults != null && _benchmarkResults.Any())
            {
                try
                {
                    PdfReportGenerator.GenerateReport(
                        _benchmarkResults,
                        GetProcessorName(),
                        GetTotalRamAmount(),
                        GetGpuName(),
                        "Baseline 2025" // <-- C'est LA LIGNE À AJOUTER
                    );
                    // Removed the lines that open the reports folder automatically after Full Stress Test
                    // string reportsDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                    // System.IO.Directory.CreateDirectory(reportsDir);
                    // Process.Start("explorer.exe", reportsDir);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating PDF report: {ex.Message}", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async Task ExecuteSingleBenchmark(string testType, int durationSeconds, string specificDrivePath)
        {
            using (ProgressForm progressForm = new ProgressForm($"{testType} Test Progress", $"Starting {testType} test..."))
            {
                progressForm.Show();
                BenchmarkResult result = await RunBenchmarkAndGetResult(testType, durationSeconds, progressForm, specificDrivePath);
                switch (testType)
                {
                    case "CPU":
                        lblCpuResult.Text = $"CPU Test: {result.Score} ({result.DetailedResult})";
                        if (lblCpuResult != null)
                        {
                            UpdateResultLabel(lblCpuResult, "CPU", result.Score,
                                BenchmarkDisplayConstants.CPU_SCORE_GOOD_THRESHOLD,
                                BenchmarkDisplayConstants.CPU_SCORE_AVERAGE_THRESHOLD);
                        }
                        break;
                    case "SSD":
                        lblSsdResult.Text = $"SSD Test: {result.Score} ({result.DetailedResult})";
                        if (lblSsdResult != null)
                        {
                            UpdateResultLabel(lblSsdResult, "SSD", result.Score,
                                              BenchmarkDisplayConstants.SSD_SCORE_GOOD_THRESHOLD,
                                              BenchmarkDisplayConstants.SSD_SCORE_AVERAGE_THRESHOLD);
                        }
                        break;
                    case "RAM":
                        lblRamResult.Text = $"RAM Test: {result.Score} ({result.DetailedResult})";
                        if (lblRamResult != null)
                        {
                            UpdateResultLabel(lblRamResult, "RAM", result.Score,
                                              BenchmarkDisplayConstants.RAM_SCORE_GOOD_THRESHOLD,
                                              BenchmarkDisplayConstants.RAM_SCORE_AVERAGE_THRESHOLD);
                        }
                        break;
                    case "GPU":
                        lblGpuResult.Text = $"GPU Test: {result.Score} ({result.DetailedResult})";
                        if (lblGpuResult != null)
                        {
                            UpdateResultLabel(lblGpuResult, "GPU", result.Score,
                                              BenchmarkDisplayConstants.GPU_SCORE_GOOD_THRESHOLD,
                                              BenchmarkDisplayConstants.GPU_SCORE_AVERAGE_THRESHOLD);
                        }
                        break;
                }
                _benchmarkResults.Add(result);
                progressForm.Close();
            }
        }

        // --- Event handler for the "Generate Report" button ---
        private void btnOpenReportsFolder_Click(object sender, EventArgs e)
        {
            try
            {
                PdfReportGenerator.GenerateReport(
                   _benchmarkResults,
                   GetProcessorName(),
                   GetTotalRamAmount(),
                   GetGpuName(),
                   "Baseline 2025" // <-- C'est LA LIGNE À AJOUTER
                );
                MessageBox.Show("PDF report generated successfully! The reports folder will open.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string reportsDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                System.IO.Directory.CreateDirectory(reportsDir);
                //Process.Start("explorer.exe", reportsDir);
                Tools.OpenFolder(reportsDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PDF report: {ex.Message}", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeHelpScoresButton()
        {
            Button btnHelpScores = new Button();
            btnHelpScores.Text = "Help Scores";
            btnHelpScores.Size = new Size(199, 35);
            btnHelpScores.Location = new DrawingPoint(548, 435);
            btnHelpScores.Click += BtnHelpScores_Click;
            this.Controls.Add(btnHelpScores);
            btnHelpScores.BringToFront();
        }

        private void BtnHelpScores_Click(object sender, EventArgs e)
        {
            FormScoreHelp helpForm = new FormScoreHelp();
            helpForm.ShowDialog();
        }

        private void Title_Info_Click(object sender, EventArgs e)
        {

        }
    }
}