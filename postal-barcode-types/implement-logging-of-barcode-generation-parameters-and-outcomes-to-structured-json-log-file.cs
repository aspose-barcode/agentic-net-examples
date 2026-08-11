// Title: Barcode generation with JSON logging of parameters and outcomes
// Description: Demonstrates creating barcodes using Aspose.BarCode and logging each generation's parameters, results, and any errors to a structured JSON file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images. Typical use cases include batch barcode creation for inventory, shipping, or marketing, where developers need to record generation details for auditing or troubleshooting. The pattern of logging to JSON helps integrate barcode workflows into automated pipelines and monitoring systems.
// Prompt: Implement logging of barcode generation parameters and outcomes to a structured JSON log file.
// Tags: barcode generation, json logging, aspose.barcode, encode types, png output

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates barcodes for a set of sample data and logs generation details to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Prepares output directories, iterates over sample barcodes, generates each barcode, and records results.
    /// </summary>
    static void Main()
    {
        // Define where barcode images and the log file will be stored
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        string logFile = Path.Combine(Directory.GetCurrentDirectory(), "barcode_log.json");

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample data: each tuple contains a symbology name and the text to encode
        var samples = new (string Symbology, string CodeText)[]
        {
            ("Code128", "123ABC"),
            ("QR", "https://example.com"),
            ("EAN13", "5901234123457")
        };

        // Process each sample, generating a barcode and logging the outcome
        foreach (var sample in samples)
        {
            string outputPath = Path.Combine(outputDir, $"{sample.Symbology}_{DateTime.Now:yyyyMMddHHmmssfff}.png");
            GenerateAndLogBarcode(sample.Symbology, sample.CodeText, outputPath, logFile);
        }

        // Inform the user that processing is complete
        Console.WriteLine("Barcode generation completed. Log written to:");
        Console.WriteLine(logFile);
    }

    /// <summary>
    /// Generates a barcode image for the specified symbology and text, then appends a JSON log entry describing the operation.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    /// <param name="outputPath">Full file path where the generated PNG image will be saved.</param>
    /// <param name="logPath">Full file path of the JSON log file to which the operation details will be appended.</param>
    static void GenerateAndLogBarcode(string symbologyName, string codeText, string outputPath, string logPath)
    {
        bool success = false;
        string errorMessage = null;
        string resolvedSymbology = null;

        try
        {
            // Resolve the symbology name to an EncodeTypes field using reflection
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {symbologyName}");

            var encodeType = (BaseEncodeType)field.GetValue(null);
            resolvedSymbology = encodeType.TypeName;

            // Create a barcode generator with the resolved type and the provided code text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example of setting an optional parameter (X-dimension in points)
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            success = true;
        }
        catch (Exception ex)
        {
            // Capture any exception message for logging
            errorMessage = ex.Message;
        }

        // Build a log entry object containing all relevant details
        var logEntry = new
        {
            Timestamp = DateTime.UtcNow,
            SymbologyRequested = symbologyName,
            SymbologyResolved = resolvedSymbology,
            CodeText = codeText,
            OutputFile = outputPath,
            Success = success,
            ErrorMessage = errorMessage
        };

        // Serialize the log entry to a single-line JSON string and append it to the log file
        string json = JsonSerializer.Serialize(logEntry);
        File.AppendAllText(logPath, json + Environment.NewLine);
    }
}