// Title: Barcode generation with JSON logging of parameters and outcomes
// Description: Demonstrates creating multiple barcodes using Aspose.BarCode, applying common and symbology‑specific settings, and logging generation details to a structured JSON file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters, handle different symbologies, and persist results. It uses BarcodeGenerator, EncodeTypes, and related parameter classes, which developers frequently employ for automated barcode creation, customization, and error handling in batch processing scenarios.
// Prompt: Implement logging of barcode generation parameters and outcomes to a structured JSON log file.
// Tags: barcode generation, json logging, code128, qr, datamatrix, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with parameter logging to JSON using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, captures settings, saves images, and writes a JSON log.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output folder for generated images and the log file.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the path for the JSON log file.
        string logPath = Path.Combine(outputDir, "barcode_log.json");
        var logEntries = new List<LogEntry>();

        // Define sample barcode data covering different symbologies.
        var samples = new List<BarcodeSample>
        {
            new BarcodeSample { SymbologyName = "Code128", CodeText = "ABC123" },
            new BarcodeSample { SymbologyName = "QR", CodeText = "https://example.com" },
            new BarcodeSample { SymbologyName = "DataMatrix", CodeText = "DMTest" }
        };

        // Process each sample barcode.
        foreach (var sample in samples)
        {
            // Initialize a log entry with default values.
            var entry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Symbology = sample.SymbologyName,
                CodeText = sample.CodeText,
                OutputPath = null,
                Success = false,
                ErrorMessage = null,
                Parameters = new Dictionary<string, object>()
            };

            try
            {
                // Resolve the EncodeTypes field that matches the symbology name.
                var field = typeof(EncodeTypes).GetField(sample.SymbologyName);
                if (field == null)
                    throw new ArgumentException($"Unknown symbology: {sample.SymbologyName}");

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Create a barcode generator for the resolved symbology and text.
                using (var generator = new BarcodeGenerator(encodeType, sample.CodeText))
                {
                    // Apply common appearance settings.
                    generator.Parameters.Barcode.XDimension.Pixels = 4f;
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;
                    generator.Parameters.RotationAngle = 0f;

                    // Record common parameters for logging.
                    entry.Parameters["XDimensionPixels"] = generator.Parameters.Barcode.XDimension.Pixels;
                    entry.Parameters["BarColor"] = generator.Parameters.Barcode.BarColor.ToString();
                    entry.Parameters["BackColor"] = generator.Parameters.BackColor.ToString();
                    entry.Parameters["RotationAngle"] = generator.Parameters.RotationAngle;

                    // Apply and log symbology‑specific settings.
                    if (encodeType == EncodeTypes.QR)
                    {
                        generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                        entry.Parameters["QRErrorLevel"] = generator.Parameters.Barcode.QR.ErrorLevel.ToString();
                    }
                    else if (encodeType == EncodeTypes.DataMatrix)
                    {
                        generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                        entry.Parameters["DataMatrixVersion"] = generator.Parameters.Barcode.DataMatrix.Version.ToString();
                    }

                    // Save the generated barcode image as PNG.
                    string fileName = $"{sample.SymbologyName}_{Guid.NewGuid().ToString("N")}.png";
                    string outputPath = Path.Combine(outputDir, fileName);
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    entry.OutputPath = outputPath;
                    entry.Success = true;
                }
            }
            catch (Exception ex)
            {
                // Capture any errors that occurred during generation.
                entry.Success = false;
                entry.ErrorMessage = ex.Message;
            }

            // Add the completed entry to the log collection.
            logEntries.Add(entry);
        }

        // Serialize the log entries to a formatted JSON string.
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(logEntries, jsonOptions);
        File.WriteAllText(logPath, json);

        // Output locations of generated barcodes and the log file.
        Console.WriteLine($"Barcodes generated in: {outputDir}");
        Console.WriteLine($"Log file created at: {logPath}");
    }

    /// <summary>
    /// Simple DTO representing a barcode sample to be generated.
    /// </summary>
    class BarcodeSample
    {
        public string SymbologyName { get; set; }
        public string CodeText { get; set; }
    }

    /// <summary>
    /// DTO for logging the outcome and parameters of a barcode generation attempt.
    /// </summary>
    class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputPath { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}