// Title: Barcode Generation with JSON Logging Example
// Description: This example generates several barcodes using Aspose.BarCode and records the generation parameters and outcomes in a structured JSON log file.
// Category-Description: The sample belongs to the Aspose.BarCode barcode creation and logging category, illustrating how to use EncodeTypes, BarcodeGenerator, and related parameter settings. Developers often need to automate barcode production while capturing metadata for auditing, debugging, or downstream processing; this example shows a typical pattern for such tasks.
// Prompt: Implement logging of barcode generation parameters and outcomes to a structured JSON log file.
// Tags: barcode, symbology, generation, json, logging, aspose.barcode, encodetypes

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeLoggingExample
{
    // Represents a single log entry for barcode generation
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputFile { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Demonstrates barcode generation and logging of each operation to a JSON file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates sample barcodes, logs details, and writes a JSON log.
        /// </summary>
        static void Main()
        {
            // Define sample barcode specifications: symbology name and associated code text
            var samples = new (string Symbology, string CodeText)[]
            {
                ("Code128", "ABC123456"),
                ("QR", "https://example.com"),
                ("DataMatrix", "SampleDM")
            };

            // Collection to hold log entries for each barcode generation attempt
            var logEntries = new List<LogEntry>();

            // Iterate over each sample definition
            foreach (var (symbologyName, codeText) in samples)
            {
                // Initialize a new log entry with basic information
                var entry = new LogEntry
                {
                    Timestamp = DateTime.UtcNow,
                    Symbology = symbologyName,
                    CodeText = codeText,
                    OutputFile = $"{symbologyName}.png"
                };

                // Resolve the symbology name to the corresponding EncodeTypes field using reflection
                var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                {
                    // Symbology not found – record failure and continue to next sample
                    entry.Success = false;
                    entry.ErrorMessage = $"Unknown symbology: {symbologyName}";
                    logEntries.Add(entry);
                    continue;
                }

                try
                {
                    // Retrieve the EncodeTypes value (BaseEncodeType) for the given symbology
                    var encodeType = (BaseEncodeType)field.GetValue(null);

                    // Create a barcode generator with the resolved type and provided code text
                    using (var generator = new BarcodeGenerator(encodeType, codeText))
                    {
                        // Optional: adjust barcode parameters (e.g., X-dimension and image resolution)
                        generator.Parameters.Barcode.XDimension.Point = 2f;
                        generator.Parameters.Resolution = 150; // DPI

                        // Save the generated barcode image to the specified file
                        generator.Save(entry.OutputFile);
                    }

                    // Mark the operation as successful
                    entry.Success = true;
                }
                catch (Exception ex)
                {
                    // Capture any exception details for the log entry
                    entry.Success = false;
                    entry.ErrorMessage = ex.Message;
                }

                // Add the completed log entry to the collection
                logEntries.Add(entry);
            }

            // Serialize the list of log entries to a formatted JSON string
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string jsonLog = JsonSerializer.Serialize(logEntries, jsonOptions);

            // Write the JSON log to a file in the application directory
            const string logFileName = "barcode_log.json";
            File.WriteAllText(logFileName, jsonLog);
        }
    }
}