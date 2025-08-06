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

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks; // Potentially for async file operations, good to have it here

namespace UltraBench
{
    public class BenchmarkHistoryManager
    {
        private readonly string _historyFilePath; // Path to the JSON history file
        private List<BenchmarkResult> _history;   // In-memory list of benchmark results

        /// <summary>
        /// Initializes a new instance of the BenchmarkHistoryManager class.
        /// Loads existing history from the specified file or starts with an empty history.
        /// </summary>
        /// <param name="filePath">The full path to the history JSON file.</param>
        public BenchmarkHistoryManager(string filePath)
        {
            _historyFilePath = filePath;
            _history = new List<BenchmarkResult>();
            LoadHistory(); // Load history when the manager is initialized
        }

        /// <summary>
        /// Adds a new benchmark result to the history and saves the updated history to file.
        /// </summary>
        /// <param name="result">The BenchmarkResult object to add.</param>
        public void AddResult(BenchmarkResult result)
        {
            // It's highly recommended to add a Timestamp property to your BenchmarkResult class
            // to accurately track when each benchmark was run. If you've added it, uncomment below:
            // result.Timestamp = DateTime.Now; 

            _history.Add(result);
            SaveHistory(); // Save immediately after adding a new result
        }

        /// <summary>
        /// Retrieves a copy of the current benchmark history.
        /// </summary>
        /// <returns>A new List<BenchmarkResult> containing all historical results.</returns>
        public List<BenchmarkResult> GetHistory()
        {
            // Return a copy to prevent external modifications to the internal list
            return new List<BenchmarkResult>(_history);
        }

        /// <summary>
        /// Saves the current in-memory benchmark history to the JSON file.
        /// </summary>
        private void SaveHistory()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true }; // For human-readable JSON output
                string jsonString = JsonSerializer.Serialize(_history, options);
                File.WriteAllText(_historyFilePath, jsonString);
            }
            catch (Exception ex)
            {
                // Handle save errors (e.g., log the error, show a message to the user)
                Console.WriteLine($"Error saving history: {ex.Message}");
                // In a real application, you might want a more robust error handling
            }
        }

        /// <summary>
        /// Loads the benchmark history from the JSON file into memory.
        /// If the file does not exist or an error occurs during loading, initializes an empty history.
        /// </summary>
        private void LoadHistory()
        {
            if (File.Exists(_historyFilePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(_historyFilePath);
                    // Deserialize the JSON string. If it's empty or invalid, new List<BenchmarkResult>() will be assigned.
                    _history = JsonSerializer.Deserialize<List<BenchmarkResult>>(jsonString) ?? new List<BenchmarkResult>();
                }
                catch (Exception ex)
                {
                    // Handle load errors (e.g., corrupted file). Start with an empty history.
                    Console.WriteLine($"Error loading history: {ex.Message}");
                    _history = new List<BenchmarkResult>(); // Initialize with an empty list on error
                }
            }
            else
            {
                _history = new List<BenchmarkResult>(); // File doesn't exist, start with an empty list
            }
        }
    }
}