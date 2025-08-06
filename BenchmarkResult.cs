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

namespace UltraBench // Ensure this is the same namespace as your Form1
{
    public class BenchmarkResult
    {
        public string Title { get; set; }          // Test title (e.g., "CPU", "SSD")
        public double Score { get; set; }          // Numeric score of the test
        public string DetailedResult { get; set; } // Detailed results (e.g., "Read: 500MB/s, Write: 450MB/s")
        public long ActualDurationMs { get; set; } // Actual test duration in milliseconds
        public bool Success { get; set; } = true;  // Indicates if the test was successful (defaults to true)
        public string TestedDrive { get; set; }    // Specific to SSD benchmark (e.g., "C:\")
        public double MaximumPossibleScore { get; set; }
        public DateTime Timestamp { get; set; }
        public string SystemConfiguration { get; set; }
    }
}