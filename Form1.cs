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

        // Assuming you have _benchmarkResults already
        private BenchmarkHistoryManager _historyManager;

        // Paths of third-party executables
        // private const string PassMarkExePath = @"C:\Program Files\PerformanceTest\PerformanceTest64.exe";

        // This method should be added to your class (e.g., in MainForm.cs)
        private string GetDriveDetails(string drivePath)

        {
            try
            {
                // Get general info about the drive
                DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(drivePath));

                // Get the disk model (more robust version)
                string diskModel = "Unknown";
                string driveLetter = Path.GetPathRoot(drivePath).Substring(0, 1);

                // First, find the logical disk (the partition C:)
                ManagementObjectSearcher logicalDiskSearcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '" + driveLetter + ":'");

                foreach (ManagementObject logicalDisk in logicalDiskSearcher.Get())
                {
                    string logicalDeviceId = logicalDisk["DeviceID"].ToString();

                    // Then, find the physical disk associated with this logical disk
                    ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_LogicalDisk.DeviceID='" + logicalDeviceId + "'} WHERE AssocClass = Win32_LogicalDiskToPartition");

                    foreach (ManagementObject partition in partitionSearcher.Get())
                    {
                        ManagementObjectSearcher physicalDiskSearcher = new ManagementObjectSearcher(
                            "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition");

                        foreach (ManagementObject physicalDisk in physicalDiskSearcher.Get())
                        {
                            diskModel = physicalDisk["Model"].ToString();
                            break;
                        }
                        break;
                    }
                    break;
                }

                return $"{diskModel} ({driveInfo.Name})";
            }
            catch
            {
                // Return a generic name if an error occurs
                return $"Selected Drive ({drivePath})";
            }
        }

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

            // --- Initialisation du BenchmarkHistoryManager ---
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ultraBenchFolderPath = Path.Combine(appDataPath, "UltraBench"); // Crée un dossier "UltraBench" dans AppData/Roaming

            // Crée le dossier s'il n'existe pas
            if (!Directory.Exists(ultraBenchFolderPath))
            {
                Directory.CreateDirectory(ultraBenchFolderPath);
            }

            string historyFilePath = Path.Combine(ultraBenchFolderPath, "benchmark_history.json");
            _historyManager = new BenchmarkHistoryManager(historyFilePath);
            // --- Fin de l'initialisation ---

            HistoryForm historyForm = new HistoryForm(_historyManager);
            //historyForm.ShowDialog();

            //LoadBenchmarkSettings(); // Assuming you have this for other settings
            //ApplyBenchmarkSettings();

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
                        return $"{Math.Ceiling((totalMemoryBytes / (1024.0 * 1024.0 * 1024.0))):F2} GB";
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

            // Access the maximum score from the configuration manager
            double SsdScoreMax = ConfigManager.Config.MaximumPossibleScores.SsdScoreMax;

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

                return new BenchmarkResult { Title = "SSD", Score = (int)score, DetailedResult = $"Write: {writeSpeed:F2} MB/s, Read: {readSpeed:F2} MB/s", ActualDurationMs = totalDuration, Success = true, MaximumPossibleScore = SsdScoreMax, TestedDrive = drivePath };
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

        private string GetSimplifiedSystemConfiguration(string ssdDrivePath)
        {
            // Fetches full component names
            string cpuNameFull = GetProcessorName();
            string ramAmount = GetTotalRamAmount();
            string gpuNameFull = GetGpuName();

            // Simplification logic for CPU
            string simplifiedCpuName = cpuNameFull;
            if (cpuNameFull.Contains("Core(TM)"))
            {
                int startIndex = cpuNameFull.IndexOf("Core(TM)") + "Core(TM)".Length;
                int endIndex = cpuNameFull.IndexOf(" CPU");
                if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                {
                    simplifiedCpuName = cpuNameFull.Substring(startIndex, endIndex - startIndex).Trim();
                }
            }
            else if (cpuNameFull.Contains("Ryzen"))
            {
                int startIndex = cpuNameFull.IndexOf("Ryzen");
                if (startIndex != -1)
                {
                    simplifiedCpuName = cpuNameFull.Substring(startIndex).Trim();
                }
            }

            // Simplification logic for GPU
            string simplifiedGpuName = gpuNameFull;
            if (gpuNameFull.Contains("GeForce"))
            {
                int startIndex = gpuNameFull.IndexOf("GeForce");
                if (startIndex != -1)
                {
                    simplifiedGpuName = gpuNameFull.Substring(startIndex).Trim();
                }
            }
            else if (gpuNameFull.Contains("Radeon"))
            {
                int startIndex = gpuNameFull.IndexOf("Radeon");
                if (startIndex != -1)
                {
                    simplifiedGpuName = gpuNameFull.Substring(startIndex).Trim();
                }
            }

            // New: Get the simplified SSD name using the path provided as a parameter
            string simplifiedSsdName = GetDriveDetails(ssdDrivePath);

            // Combines all simplified names into a single string
            return $"{simplifiedCpuName}, {ramAmount} , {simplifiedGpuName}, {simplifiedSsdName}";
        }

        private BenchmarkResult RunRamBenchmark(Action<int, string> progressAction)
        {
            string systemDrivePath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

            string systemConfig = GetSimplifiedSystemConfiguration(systemDrivePath);

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
                SystemConfiguration = systemConfig,
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

            double GpuScoreMax = ConfigManager.Config.MaximumPossibleScores.GpuScoreMax;

            // This script runs the GPU Compute test
            string scriptContent = $@"
RUN G3D_DIRECTCOMPUTE
EXPORTHTML ""{reportPath}""
EXIT
";

            // --- Retrieve System Configuration Information ---
            string cpuName = GetProcessorName();
            string ramAmount = GetTotalRamAmount();
            string gpuName = GetGpuName();
            string systemConfig = $"{cpuName}, {ramAmount} RAM, {gpuName}";
            // -------------------------------------------------

            if (!File.Exists(PassMarkExePath))
            {
                MessageBox.Show("PassMark PerformanceTest is not found at the specified location.\n" +
                                "Please install it or modify the path in the code.",
                                "Error: PassMark Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new BenchmarkResult
                {
                    Title = "GPU Test",
                    Score = 0,
                    DetailedResult = "PassMark PerformanceTest not found.",
                    SystemConfiguration = systemConfig
                };
            }

            BenchmarkResult result = null;
            Stopwatch stopwatch = new Stopwatch();

            try
            {
                progressForm.UpdateProgress("Preparing GPU test...", BenchmarkSettingsConstants.GpuProgressInitial);
                await File.WriteAllTextAsync(scriptPath, scriptContent);

                result = await Task.Run(() =>
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = PassMarkExePath,
                        Arguments = $"/s \"{scriptPath}\"",
                        UseShellExecute = true,
                        Verb = "runas",
                        CreateNoWindow = false
                    };

                    using (Process process = new Process { StartInfo = startInfo })
                    {
                        progressForm.Invoke((MethodInvoker)delegate
                        {
                            progressForm.UpdateProgress("Launching GPU test with PassMark PerformanceTest...", BenchmarkSettingsConstants.GpuProgressCopyReport);
                        });
                        stopwatch.Start();
                        process.Start();
                        progressForm.Invoke((MethodInvoker)delegate
                        {
                            progressForm.UpdateProgress("GPU test in progress (this may take several minutes)...", BenchmarkSettingsConstants.GpuProgressParseReport);
                        });
                        process.WaitForExit(BenchmarkSettingsConstants.GpuBenchmarkTimeoutMs);
                        stopwatch.Stop();

                        if (!process.HasExited)
                        {
                            try { process.Kill(); } catch (InvalidOperationException) { }
                            return new BenchmarkResult
                            {
                                Title = "GPU",
                                Score = 0,
                                DetailedResult = "The PassMark test did not complete in time or was manually closed. The report may not have been generated.",
                                ActualDurationMs = stopwatch.ElapsedMilliseconds,
                                Success = false,
                                Timestamp = DateTime.Now,
                                SystemConfiguration = systemConfig
                            };
                        }
                    }

                    progressForm.Invoke((MethodInvoker)delegate
                    {
                        progressForm.UpdateProgress("Retrieving GPU test results...", BenchmarkSettingsConstants.GpuProgressFinalizing);
                    });

                    if (File.Exists(reportPath))
                    {
                        string reportContent = File.ReadAllText(reportPath, Encoding.Default);
                        var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                        htmlDoc.LoadHtml(reportContent);

                        double gpuScore = 0;
                        string detailedResult = "No GPU scores found.";

                        // Dictionary to hold all found GPU scores
                        var gpuDetailedScores = new Dictionary<string, double>();
                        var testRows = htmlDoc.DocumentNode.SelectNodes("//tr")?.Where(tr => tr.SelectNodes("td")?.FirstOrDefault()?.InnerText.Contains("GPU", StringComparison.OrdinalIgnoreCase) == true);

                        if (testRows != null)
                        {
                            foreach (var row in testRows)
                            {
                                var tds = row.SelectNodes("td");
                                if (tds != null && tds.Count >= 2)
                                {
                                    string testName = tds.First().InnerText.Trim();
                                    string rawScoreText = tds.Last().InnerText.Trim();

                                    int parenthesisIndex = rawScoreText.IndexOf(" (");
                                    if (parenthesisIndex != -1)
                                    {
                                        rawScoreText = rawScoreText.Substring(0, parenthesisIndex);
                                    }

                                    if (rawScoreText != "-")
                                    {
                                        double score;
                                        if (double.TryParse(rawScoreText, NumberStyles.Any, CultureInfo.InvariantCulture, out score))
                                        {
                                            // Use the full test name as the key for accurate unit matching
                                            gpuDetailedScores[testName] = score;
                                        }
                                    }
                                }
                            }
                        }

                        if (gpuDetailedScores.Any())
                        {
                            var formattedScores = gpuDetailedScores.Select(kvp =>
                            {
                                string key = kvp.Key.Replace("DirectX ", "DX").Replace(" (Frames/Sec.)", "").Replace(" (Ops./Sec.)", "").Replace(" (Composite average)", "");
                                string unit = "FPS";
                                if (kvp.Key.Contains("Ops./Sec.", StringComparison.OrdinalIgnoreCase))
                                {
                                    unit = "Ops./Sec.";
                                }
                                return $"{key}: {kvp.Value:N1} {unit}";
                            });
                            detailedResult = string.Join(", ", formattedScores);
                        }

                        if (gpuDetailedScores.ContainsKey("GPU Compute (Ops./Sec.)"))
                        {
                            gpuScore = gpuDetailedScores["GPU Compute (Ops./Sec.)"];
                        }
                        else if (gpuDetailedScores.ContainsKey("3D Graphics Mark (Composite average)"))
                        {
                            gpuScore = gpuDetailedScores["3D Graphics Mark (Composite average)"];
                        }

                        return new BenchmarkResult
                        {
                            Title = "GPU",
                            Score = (int)Math.Round(gpuScore),
                            DetailedResult = detailedResult,
                            MaximumPossibleScore = GpuScoreMax,
                            Success = true,
                            Timestamp = DateTime.Now,
                            ActualDurationMs = stopwatch.ElapsedMilliseconds,
                            SystemConfiguration = systemConfig
                        };
                    }
                    else
                    {
                        return new BenchmarkResult
                        {
                            Title = "GPU",
                            Score = 0,
                            DetailedResult = "The PassMark PerformanceTest report was not generated.",
                            Success = false,
                            Timestamp = DateTime.Now,
                            ActualDurationMs = stopwatch.ElapsedMilliseconds,
                            SystemConfiguration = systemConfig
                        };
                    }
                });
            }
            catch (Exception ex)
            {
                if (stopwatch.IsRunning)
                {
                    stopwatch.Stop();
                }
                return new BenchmarkResult
                {
                    Title = "GPU",
                    Score = 0,
                    DetailedResult = $"An error occurred during PassMark benchmark execution: {ex.Message}",
                    Success = false,
                    Timestamp = DateTime.Now,
                    ActualDurationMs = stopwatch.ElapsedMilliseconds,
                    SystemConfiguration = systemConfig
                };
            }
            finally
            {
                if (File.Exists(scriptPath))
                {
                    File.Delete(scriptPath);
                }
                if (File.Exists(reportPath))
                {
                    File.Delete(reportPath);
                }
            }
            return result;
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

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            // Create an instance of HistoryForm, passing the existing history manager
            string _historyFilePath = Path.Combine(Application.StartupPath, "benchmark_history.json");
            HistoryForm historyForm = new HistoryForm(_historyManager);
            historyForm.ShowDialog(); // Show it as a modal dialog (blocks parent until closed)
                                      // Or historyForm.Show(); // Show as non-modal (parent form remains usable)
                                      // ShowDialog() is generally preferred for this kind of window.
        }

        private async Task<BenchmarkResult> RunBenchmarkAndGetResult(string testType, int durationSeconds, ProgressForm progressForm, string specificDrivePath = null)
        {
            BenchmarkResult result = null;
            // We get the system drive path once here, so we can use it for all tests
            string systemDrivePath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

            // Access the maximum score from the configuration manager
            double CpuScoreMax = ConfigManager.Config.MaximumPossibleScores.CpuScoreMax;
            double GpuScoreMax = ConfigManager.Config.MaximumPossibleScores.GpuScoreMax;

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

                            // We pass the systemDrivePath to the new simplification method
                            string systemConfig = GetSimplifiedSystemConfiguration(systemDrivePath);
                            // ----------------------------------------------------
                           
                            result = new BenchmarkResult
                            {
                                Title = "CPU",
                                Score = (int)score,
                                DetailedResult = $"Primes found: {primesFound}, Time: {finalSw.ElapsedMilliseconds} ms",
                                ActualDurationMs = finalSw.ElapsedMilliseconds,
                                Success = true,
                                Timestamp = DateTime.Now,
                                MaximumPossibleScore = GpuScoreMax,
                                SystemConfiguration = systemConfig
                            };
                            updateProgressAction(100, "CPU : Benchmark Complete.");
                        });
                        break;

                    case "SSD":
                        progressForm.UpdateProgress("SSD : Starting SSD benchmark...", 0);
                        result = await Task.Run(() => RunSsdBenchmarkCombined(specificDrivePath, updateProgressAction));
                        if (result != null)
                        {
                            result.Timestamp = DateTime.Now;
                            result.SystemConfiguration = GetSimplifiedSystemConfiguration(systemDrivePath);
                        }
                        break;

                    case "RAM":
                        progressForm.UpdateProgress("RAM : Starting RAM benchmark...", 0);
                        result = await Task.Run(() => RunRamBenchmark(updateProgressAction));
                        if (result != null)
                        {
                            result.Timestamp = DateTime.Now;
                            result.SystemConfiguration = GetSimplifiedSystemConfiguration(systemDrivePath);
                        }
                        break;

                    case "GPU":
                        result = await RunGpuBenchmarkPassMark(progressForm);
                        if (result != null)
                        {
                            result.Timestamp = DateTime.Now;
                            result.SystemConfiguration = GetSimplifiedSystemConfiguration(systemDrivePath);
                        }
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
                if (result != null)
                {
                    result.Timestamp = DateTime.Now;
                    // On peut ajouter la configuration ici aussi en cas d'erreur
                    result.SystemConfiguration = GetSimplifiedSystemConfiguration(systemDrivePath);
                }
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
                    _historyManager.AddResult(cpuResult);
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
                    _historyManager.AddResult(ramResult);
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

            // Get the system drive path
            string systemDrivePath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

            using (ProgressForm progressForm = new ProgressForm("SSD Test Progress", $"Starting SSD test on {systemDrivePath}..."))
            {
                progressForm.Show();
                BenchmarkResult ssdResult = await Task.Run(() => RunBenchmarkAndGetResult("SSD", 0, progressForm, systemDrivePath));

                if (lblSsdResult != null)
                {
                    UpdateResultLabel(lblSsdResult, "SSD", ssdResult.Score,
                                            BenchmarkDisplayConstants.SSD_SCORE_GOOD_THRESHOLD,
                                            BenchmarkDisplayConstants.SSD_SCORE_AVERAGE_THRESHOLD);
                }
                _benchmarkResults.Add(ssdResult);
                _historyManager.AddResult(ssdResult);
                progressForm.Close();
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
                _historyManager.AddResult(gpuResult);
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
                    _historyManager.AddResult(cpuResult);
                }
                else
                {
                    SetResultNotExecuted(lblCpuResult, "CPU");
                }
                progressForm.Close();
            }

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
                    _historyManager.AddResult(ramResult);
                }
                else
                {
                    SetResultNotExecuted(lblRamResult, "RAM");
                }
                progressForm.Close();
            }

            // Clearing TextBoxes or Display Labels
            SetResultNotExecuted(lblSsdResult, "SSD");

            // Get the system drive path
            string systemDrivePath = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

            using (ProgressForm progressForm = new ProgressForm("SSD Test Progress", $"Starting SSD test on {systemDrivePath}..."))
            {
                progressForm.Show();
                BenchmarkResult ssdResult = await Task.Run(() => RunBenchmarkAndGetResult("SSD", 0, progressForm, systemDrivePath));

                if (lblSsdResult != null)
                {
                    UpdateResultLabel(lblSsdResult, "SSD", ssdResult.Score,
                                            BenchmarkDisplayConstants.SSD_SCORE_GOOD_THRESHOLD,
                                            BenchmarkDisplayConstants.SSD_SCORE_AVERAGE_THRESHOLD);
                }
                _benchmarkResults.Add(ssdResult);
                _historyManager.AddResult(ssdResult);
                progressForm.Close();
            }

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
                        _historyManager.AddResult(result);
                        break;
                    case "SSD":
                        lblSsdResult.Text = $"SSD Test: {result.Score} ({result.DetailedResult})";
                        if (lblSsdResult != null)
                        {
                            UpdateResultLabel(lblSsdResult, "SSD", result.Score,
                                              BenchmarkDisplayConstants.SSD_SCORE_GOOD_THRESHOLD,
                                              BenchmarkDisplayConstants.SSD_SCORE_AVERAGE_THRESHOLD);
                        }
                        _historyManager.AddResult(result);
                        break;
                    case "RAM":
                        lblRamResult.Text = $"RAM Test: {result.Score} ({result.DetailedResult})";
                        if (lblRamResult != null)
                        {
                            UpdateResultLabel(lblRamResult, "RAM", result.Score,
                                              BenchmarkDisplayConstants.RAM_SCORE_GOOD_THRESHOLD,
                                              BenchmarkDisplayConstants.RAM_SCORE_AVERAGE_THRESHOLD);
                        }
                        _historyManager.AddResult(result);
                        break;
                    case "GPU":
                        lblGpuResult.Text = $"GPU Test: {result.Score} ({result.DetailedResult})";
                        if (lblGpuResult != null)
                        {
                            UpdateResultLabel(lblGpuResult, "GPU", result.Score,
                                              BenchmarkDisplayConstants.GPU_SCORE_GOOD_THRESHOLD,
                                              BenchmarkDisplayConstants.GPU_SCORE_AVERAGE_THRESHOLD);
                        }
                        _historyManager.AddResult(result);
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

        private void lblCpuInfo_Click(object sender, EventArgs e)
        {

        }
    }
}