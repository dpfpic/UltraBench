//*********************************************
// UltraBench Ver:1.0.0
// Created by Dpfpic (Fabrice Piscia)
// Site : https://github.com/dpfpic/UltraBench
// Licensed under the MIT License
//*********************************************

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace UltraBench
{
    public static class Tools
    {
        // <summary>
        // Opens a folder dans l'explorateur Windows.
        // </summary>
        public static void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
        }

        // <summary>
        // Formats a score numérique (avec 2 décimales).
        // </summary>
        public static string FormatScore(double score)
        {
            return $"{score:N2} pts";
        }

        // <summary>
        // Génère un timestamp au format yyyy-MM-dd_HHmmss
        // </summary>
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd_HHmmss", CultureInfo.InvariantCulture);
        }
    }
}
