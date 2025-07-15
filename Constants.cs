using System.Drawing;

namespace UltraBench
{
    public static class AppConstants
    {
        // Noms des applications tels qu'ils peuvent apparaître dans le registre ou les chemins par défaut
        public const string HW_MONITOR_APP_NAME = "HWMonitor";
        public const string PERFORMANCE_TEST_APP_NAME = "PerformanceTest";
        // Ajoute d'autres noms d'applications si nécessaire

        // Noms des exécutables
        public const string HW_MONITOR_EXE_NAME = "HWMonitor.exe";
        public const string PERFORMANCE_TEST_64_EXE_NAME = "PerformanceTest64.exe";
        public const string PERFORMANCE_TEST_32_EXE_NAME = "PerformanceTest.exe";
        // ...
    }

    public static class RegistryPaths
    {
        public const string LOCAL_MACHINE_UNINSTALL_32 = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
        public const string LOCAL_MACHINE_UNINSTALL_64 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        public const string CURRENT_USER_UNINSTALL = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"; // Sous CurrentUser

        public const string PERFORMANCETEST_REGISTRY_KEY_32 = @"SOFTWARE\PerformanceTest";
        public const string PERFORMANCETEST_REGISTRY_KEY_64 = @"SOFTWARE\WOW6432Node\PerformanceTest";
    }

    public static class RegistryValueNames
    {
        public const string DISPLAY_NAME = "DisplayName";
        public const string INSTALL_LOCATION = "InstallLocation";
        public const string DISPLAY_ICON = "DisplayIcon";
        public const string EXE_PATH = "ExePath"; // Spécifique à la clé PerformanceTest
    }

    public static class BenchmarkDisplayConstants
    {
        // Couleurs des résultats
        public static readonly Color COLOR_GOOD_RESULT = Color.Green;
        public static readonly Color COLOR_AVERAGE_RESULT = Color.Orange;
        public static readonly Color COLOR_BAD_RESULT = Color.Red;
        public static readonly Color COLOR_NOT_EXECUTED = Color.Gray; // Ou SystemColors.ControlText

        // Seuils de score (valeurs d'exemple, tu devras les ajuster selon tes tests réels)
        public const double CPU_SCORE_GOOD_THRESHOLD = 1000.0;
        public const double CPU_SCORE_AVERAGE_THRESHOLD = 500.0;

        public const double RAM_SCORE_GOOD_THRESHOLD = 2000.0;
        public const double RAM_SCORE_AVERAGE_THRESHOLD = 1000.0;

        public const double SSD_SCORE_GOOD_THRESHOLD = 500.0; // Exemple
        public const double SSD_SCORE_AVERAGE_THRESHOLD = 200.0; // Exemple

        public const double GPU_SCORE_GOOD_THRESHOLD = 500.0; // Exemple
        public const double GPU_SCORE_AVERAGE_THRESHOLD = 200.0; // Exemple
    }
}