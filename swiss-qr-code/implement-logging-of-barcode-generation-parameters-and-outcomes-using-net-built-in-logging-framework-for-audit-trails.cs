// Title: Barcode Generation with File-Based Audit Logging using Aspose.BarCode
// Description: Demonstrates creating barcodes of different symbologies, saving them as PNG files, and recording generation parameters and outcomes to an audit log.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes. It illustrates typical use cases such as batch barcode creation and audit trail logging, which developers often need for compliance and troubleshooting. The code logs each operation to a plain‑text file, providing a simple audit mechanism without external dependencies.
// Prompt: Implement logging of barcode generation parameters and outcomes using .NET built‑in logging framework for audit trails.
// Tags: barcode, symbology, generation, logging, audit, aspose.barcode, png, encode types

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates barcodes of various symbologies, saves them as PNG images, and logs the process to an audit file.
/// </summary>
class Program
{
    // Path to the audit log file (created in the current working directory)
    private static readonly string LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "audit.log");

    /// <summary>
    /// Entry point of the application. Iterates over sample barcode definitions, generates each barcode,
    /// and records success or failure details in the audit log.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define sample barcodes: symbology name, code text, and output file name
        var samples = new (string Symbology, string CodeText, string FileName)[]
        {
            ("Code128", "ABC123", "code128.png"),
            ("QR", "https://example.com", "qr.png"),
            ("DataMatrix", "DM12345", "datamatrix.png")
        };

        // Initialise the audit log with a header containing the UTC timestamp
        File.WriteAllText(LogFilePath, $"Audit Log - {DateTime.UtcNow:u}{Environment.NewLine}");

        // Process each sample barcode definition
        for (int i = 0; i < samples.Length; i++)
        {
            var (symbology, codeText, fileName) = samples[i];
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            GenerateAndLog(symbology, codeText, outputPath);
        }

        // Inform the user that processing is complete
        Console.WriteLine("Barcode generation completed. See audit.log for details.");
    }

    // Generates a barcode, saves it to the specified path, and logs the result.
    private static void GenerateAndLog(string symbologyName, string codeText, string outputPath)
    {
        // Resolve the symbology name to a BaseEncodeType using reflection.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
        if (field == null)
        {
            AppendLog($"[{DateTime.UtcNow:u}] UNKNOWN SYMBOLOGY: '{symbologyName}'. Skipping.");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        try
        {
            // Create a barcode generator for the resolved symbology and provided code text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example of setting a parameter (optional): reduce module size.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode image as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Log successful generation with details.
            AppendLog($"[{DateTime.UtcNow:u}] SUCCESS: Symbology={encodeType.TypeName}, CodeText=\"{codeText}\", Output=\"{outputPath}\"");
        }
        catch (Exception ex)
        {
            // Log failure with error message.
            AppendLog($"[{DateTime.UtcNow:u}] FAILURE: Symbology={encodeType.TypeName}, CodeText=\"{codeText}\", Error={ex.Message}");
        }
    }

    // Appends a single line to the audit log file; falls back to console output on error.
    private static void AppendLog(string message)
    {
        try
        {
            File.AppendAllText(LogFilePath, message + Environment.NewLine);
        }
        catch
        {
            // If logging fails, write to console as a fallback.
            Console.WriteLine("Logging error: " + message);
        }
    }
}