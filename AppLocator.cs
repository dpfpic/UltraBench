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

using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using UltraBench;

public static class AppLocator
{
    // <summary>
    // Searches for the installation path of an application via le registre Windows ou les chemins par défaut.
    // </summary>
    // <param name="appName">Name of the application tel qu'il apparaît dans "DisplayName" du registre (ex: "HWMonitor" ou "PerformanceTest").</param>
    // <param name="exeName">Executable name (ex: "HWMonitor.exe" ou "PerformanceTest64.exe").</param>
    // <returns>Full path of the executable if found, otherwise null.</returns>
    public static string LocateAppExecutable(string appName, string exeName)
    {
        // 1. Recherche dans les chemins par défaut (Program Files, Program Files (x86))
        string[] defaultPaths = new string[]
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), appName, exeName),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), appName, exeName)
        };

        foreach (string path in defaultPaths)
        {
            if (File.Exists(path))
            {
                return path;
            }
        }

        // 2. Recherche in the registry via GetInstalledProgramPath (plus générique)
        string registryInstallLocation = GetInstalledProgramPath(appName);
        if (!string.IsNullOrEmpty(registryInstallLocation))
        {
            string fullPath = Path.Combine(registryInstallLocation, exeName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        // Si l'exécutable n'est pas directement sous InstallLocation,
        // on peut chercher dans le dossier d'installation le fichier .exe
        if (!string.IsNullOrEmpty(registryInstallLocation))
        {
            try
            {
                // Cherche directement l'exe dans le dossier d'installation trouvé
                string[] files = Directory.GetFiles(registryInstallLocation, exeName, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    return files[0]; // Retourne le premier trouvé
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AppLocator: Error searching for {exeName} in '{registryInstallLocation}': {ex.Message}");
            }
        }

        return null;
    }

    // <summary>
    // Recherche le chemin d'installation de PassMark PerformanceTest en utilisant sa clé de registre spécifique,
    // puis une recherche générique si la première échoue.
    // </summary>
    // <returns>Chemin complet de l'exécutable de PassMark si trouvé, sinon null.</returns>
    public static string LocatePerformanceTestExecutable()
    {
        // 1. Recherche spécifique via la clé de registre "ExePath" de PassMark (la plus fiable)
        try
        {
            using (var key = Registry.LocalMachine.OpenSubKey(RegistryPaths.PERFORMANCETEST_REGISTRY_KEY_64))
            {
                if (key != null)
                {
                    var regPath = key.GetValue(RegistryValueNames.EXE_PATH) as string;
                    if (!string.IsNullOrEmpty(regPath) && File.Exists(regPath))
                        return regPath;
                }
            }
            // Fallback pour une version 32-bit ou si la clé est à un autre endroit
            using (var key = Registry.LocalMachine.OpenSubKey(RegistryPaths.PERFORMANCETEST_REGISTRY_KEY_32))
            {
                if (key != null)
                {
                    var regPath = key.GetValue(RegistryValueNames.EXE_PATH) as string;
                    if (!string.IsNullOrEmpty(regPath) && File.Exists(regPath))
                        return regPath;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"AppLocator: Error finding PassMark via specific registry key: {ex.Message}");
        }

        // 2. Fallback : Utilise la recherche générique LocateAppExecutable
        string path = LocateAppExecutable(AppConstants.PERFORMANCE_TEST_APP_NAME, AppConstants.PERFORMANCE_TEST_64_EXE_NAME);
        if (File.Exists(path))
        {
            return path;
        }

        // 3. Fallback : chemins par défaut connus pour PassMark
        // Utilisation de Path.Combine pour construire les chemins de manière robuste
        string fallback32bit = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), AppConstants.PERFORMANCE_TEST_APP_NAME, AppConstants.PERFORMANCE_TEST_32_EXE_NAME);
        if (File.Exists(fallback32bit))
            return fallback32bit;

        string fallback64bit = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), AppConstants.PERFORMANCE_TEST_APP_NAME, AppConstants.PERFORMANCE_TEST_64_EXE_NAME);
        if (File.Exists(fallback64bit))
            return fallback64bit;


        return null;
    }


    // <summary>
    // Recherche le chemin d'installation d'un programme in the registry Windows (Uninstall keys).
    // </summary>
    // <param name="appName">Name of the application tel qu'il apparaît dans "DisplayName" du registre.</param>
    // <returns>Chemin d'installation si trouvé, sinon null.</returns>
    private static string GetInstalledProgramPath(string appName)
    {
        // Chemins de désinstallation du registre à vérifier
        string[] registryUninstallPaths = new string[]
        {
            RegistryPaths.LOCAL_MACHINE_UNINSTALL_64,
            RegistryPaths.LOCAL_MACHINE_UNINSTALL_32
        };

        // --- Recherche dans Registry.LocalMachine ---
        foreach (string registryPath in registryUninstallPaths)
        {
            using (RegistryKey baseKey = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (baseKey != null)
                {
                    foreach (string subKeyName in baseKey.GetSubKeyNames())
                    {
                        using (RegistryKey subKey = baseKey.OpenSubKey(subKeyName))
                        {
                            try
                            {
                                string displayName = subKey?.GetValue(RegistryValueNames.DISPLAY_NAME) as string;
                                if (!string.IsNullOrEmpty(displayName) && displayName.Contains(appName, StringComparison.OrdinalIgnoreCase))
                                {
                                    string installLocation = subKey?.GetValue(RegistryValueNames.INSTALL_LOCATION) as string;
                                    if (!string.IsNullOrEmpty(installLocation) && Directory.Exists(installLocation))
                                    {
                                        return installLocation;
                                    }
                                    // AJOUT : Vérifie DisplayIcon si InstallLocation n'est pas trouvée ou n'est pas un répertoire valide
                                    string displayIcon = subKey?.GetValue(RegistryValueNames.DISPLAY_ICON) as string;
                                    if (!string.IsNullOrEmpty(displayIcon))
                                    {
                                        // Le chemin peut contenir ",0" à la fin (chemin icône), on prend la partie avant la virgule
                                        string iconPath = displayIcon.Split(',')[0].Trim('"');
                                        if (File.Exists(iconPath))
                                        {
                                            return Path.GetDirectoryName(iconPath);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"AppLocator: Error accessing registry key for {appName} (LocalMachine): {ex.Message}");
                            }
                        }
                    }
                }
            }
        }

        // --- AJOUT : Recherche dans Registry.CurrentUser ---
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPaths.CURRENT_USER_UNINSTALL, false))
        {
            if (key != null)
            {
                foreach (string subkeyName in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkeyName, false))
                    {
                        if (subkey != null)
                        {
                            try
                            {
                                string displayName = subkey.GetValue(RegistryValueNames.DISPLAY_NAME) as string;
                                if (!string.IsNullOrEmpty(displayName) && displayName.Contains(appName, StringComparison.OrdinalIgnoreCase))
                                {
                                    string installLocation = subkey.GetValue(RegistryValueNames.INSTALL_LOCATION) as string;
                                    if (!string.IsNullOrEmpty(installLocation) && Directory.Exists(installLocation))
                                    {
                                        return installLocation;
                                    }
                                    string displayIcon = subkey.GetValue(RegistryValueNames.DISPLAY_ICON) as string;
                                    if (!string.IsNullOrEmpty(displayIcon))
                                    {
                                        string iconPath = displayIcon.Split(',')[0].Trim('"');
                                        if (File.Exists(iconPath))
                                        {
                                            return Path.GetDirectoryName(iconPath);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"AppLocator: Error accessing registry key for {appName} (CurrentUser): {ex.Message}");
                            }
                        }
                    }
                }
            }
        }

        return null;
    }
    public static class BenchmarkDisplayConstants
    {
        // Result colors
        public static readonly Color COLOR_GOOD_RESULT = Color.Green;
        public static readonly Color COLOR_AVERAGE_RESULT = Color.Orange;
        public static readonly Color COLOR_BAD_RESULT = Color.Red;
        public static readonly Color COLOR_NOT_EXECUTED = Color.Gray; // Ou SystemColors.ControlText

        // Score thresholds (example values, you should adjust them selon tes tests réels)
        public const double CPU_SCORE_GOOD_THRESHOLD = 1000.0;
        public const double CPU_SCORE_AVERAGE_THRESHOLD = 500.0;

        public const double RAM_SCORE_GOOD_THRESHOLD = 2000.0;
        public const double RAM_SCORE_AVERAGE_THRESHOLD = 1000.0;

        public const double SSD_SCORE_GOOD_THRESHOLD = 500.0; // Exemple
        public const double SSD_SCORE_AVERAGE_THRESHOLD = 200.0; // Exemple

        public const double GPU_SCORE_GOOD_THRESHOLD = 3000.0; // Exemple
        public const double GPU_SCORE_AVERAGE_THRESHOLD = 1500.0; // Exemple
    }

    // NOUVELLE CLASSE POUR LES CONSTANTES DE CONFIGURATION DES BENCHMARKS
    public static class BenchmarkSettingsConstants
    {
        // --- Adaptive CPU Benchmark Constants ---
        public const int TargetCpuBenchmarkDurationMs = 6000;       // Target duration for CPU benchmark (6 seconds)
        public const int CpuCalibrationLimitInitial = 50000;        // Initial small limit for calibration test
        public const double CpuComplexityPower = 1.5;               // Complexity exponent for CalculatePrimes (N^1.5 approx)

        // --- SSD Benchmark Constants ---
        public const long SsdTestFileSizeMb = 1000;                 // Size of the test file in MB (1 GB)
        public const int SsdBufferSizeKb = 4;                       // Read/write buffer size in KB (4 KB is common)

        // --- RAM Benchmark Constants ---
        public const long RamTestSizeMb = 1024;                     // Amount of memory to test in MB (1 GB)
        public const int RamBufferSizeKb = 4;                       // Buffer size for memory operations in KB

        // --- Score Calculation Multipliers ---
        public const double CpuScoreMultiplier = 1000000.0;         // Multiplicateur pour le score CPU
        public const double RamScoreMultiplier = 100.0;             // Multiplicateur pour le score RAM
        public const double SsdScoreMultiplier = 100.0;             // Multiplicateur pour le score SSD

        // --- GPU Benchmark Timeout ---
        public const int GpuBenchmarkTimeoutMs = 120000;            // Timeout pour le processus PassMark en millisecondes (2 minutes)

        // --- Progress Update Percentages (Optionnel, mais pour la clarté) ---
        public const int GpuProgressInitial = 5;                    // Pourcentage de progression initial pour le GPU
        public const int GpuProgressCopyReport = 10;                // Pourcentage après copie du rapport
        public const int GpuProgressParseReport = 20;               // Pourcentage après parsing du rapport
        public const int GpuProgressFinalizing = 90;                // Pourcentage de finalisation avant fin du test
    }
}