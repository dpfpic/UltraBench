using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

public static class ConfigManager
{
    private static readonly string _configFilePath = "config.json";

    public static Configuration Config { get; private set; }

    static ConfigManager()
    {
        LoadConfig();
    }

    public static void LoadConfig()
    {
        if (File.Exists(_configFilePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_configFilePath);
                // The JsonSerializer will use the default values we set directly in the classes
                Config = JsonSerializer.Deserialize<Configuration>(jsonString);
            }
            catch (JsonException ex)
            {
                // Log the error instead of showing a MessageBox
                // A real-world application would use a logging framework like Serilog or NLog.
                // For now, a simple Console.WriteLine is a good substitute.
                Console.WriteLine($"ERROR: Failed to deserialize config file. Using default values. Details: {ex.Message}");
                Config = new Configuration(); // Fallback to default values
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: An unexpected error occurred while loading config. Details: {ex.Message}");
                Config = new Configuration(); // Fallback to default values
            }
        }
        else
        {
            // If file doesn't exist, create it with default values
            Config = new Configuration();
            SaveConfig();
        }
    }

    public static void SaveConfig()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(Config, options);
        File.WriteAllText(_configFilePath, jsonString);
    }

    // A helper method to create a default configuration
    private static Configuration GetDefaultConfig()
    {
        return new Configuration
        {
            BenchmarkThresholds = new BenchmarkThresholds
            {
                CpuScoreGood = 1000.0,
                CpuScoreAverage = 500.0,
                RamScoreGood = 2000.0,
                RamScoreAverage = 1000.0,
                SsdScoreGood = 500.0,
                SsdScoreAverage = 200.0,
                GpuScoreGood = 500.0,
                GpuScoreAverage = 200.0
            },
            ColorThresholds = new ColorThresholds
            {
                GoodResult = "Green",
                AverageResult = "Orange",
                BadResult = "Red",
                NotExecuted = "Gray"
            }
        };
    }

    // A method to convert a string color name to a Color object
    public static Color GetColor(string colorName)
    {
        try
        {
            return Color.FromName(colorName);
        }
        catch
        {
            // Fallback to a default color if the name is invalid
            return Color.Gray;
        }
    }
}