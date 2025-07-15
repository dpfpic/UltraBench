using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace UltraBench
{
    public static class Tools
    {
        /// <summary>
        /// Ouvre un dossier dans l'explorateur Windows.
        /// </summary>
        public static void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
        }

        /// <summary>
        /// Formate un score numérique (avec 2 décimales).
        /// </summary>
        public static string FormatScore(double score)
        {
            return $"{score:N2} pts";
        }

        /// <summary>
        /// Génère un timestamp au format yyyy-MM-dd_HHmmss
        /// </summary>
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd_HHmmss", CultureInfo.InvariantCulture);
        }
    }
}
