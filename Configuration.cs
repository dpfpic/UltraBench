// Configuration.cs
public class Configuration
{
    public BenchmarkThresholds BenchmarkThresholds { get; set; }
    public ColorThresholds ColorThresholds { get; set; }
    // Add the new MaximumPossibleScores property.
    public MaximumPossibleScores MaximumPossibleScores { get; set; } = new MaximumPossibleScores();
}

// BenchmarkThresholds.cs
// This class holds all the score thresholds.
public class BenchmarkThresholds
{
    public double CpuScoreGood { get; set; }
    public double CpuScoreAverage { get; set; }
    public double RamScoreGood { get; set; }
    public double RamScoreAverage { get; set; }
    public double SsdScoreGood { get; set; }
    public double SsdScoreAverage { get; set; }
    public double GpuScoreGood { get; set; }
    public double GpuScoreAverage { get; set; }
}

// ColorThresholds.cs
// This class holds the string names for the colors.
public class ColorThresholds
{
    public string GoodResult { get; set; }
    public string AverageResult { get; set; }
    public string BadResult { get; set; }
    public string NotExecuted { get; set; }
}