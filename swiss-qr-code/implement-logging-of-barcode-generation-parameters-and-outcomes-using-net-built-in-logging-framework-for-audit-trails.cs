// Title: Barcode generation with audit logging using Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode, saving it as PNG, and logging generation parameters and read results for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode creation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to produce and decode barcodes, while recording operational details. Developers often need such patterns for compliance, troubleshooting, and audit trails when integrating barcode functionality into .NET applications.
// Prompt: Implement logging of barcode generation parameters and outcomes using .NET built‑in logging framework for audit trails.
// Tags: barcode, code128, generation, recognition, logging, audit, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, saving, reading, and audit logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, logs parameters, saves the image, reads it back, and records outcomes.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a unique working directory for temporary files
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define paths for the barcode image and the audit log file
        string barcodePath = Path.Combine(workDir, "sample_barcode.png");
        string logPath = Path.Combine(workDir, "audit_log.txt");

        // ------------------------------------------------------------
        // Initialize the audit log with a start timestamp
        // ------------------------------------------------------------
        File.WriteAllText(logPath, $"Audit Log Started at {DateTime.UtcNow:u}{Environment.NewLine}");

        // ------------------------------------------------------------
        // Set barcode generation parameters
        // ------------------------------------------------------------
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        try
        {
            // Create a generator for the specified symbology and data
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Log the basic generation parameters
                Log(logPath, $"Generating barcode:");
                Log(logPath, $"  Symbology: {encodeType.TypeName}");
                Log(logPath, $"  CodeText: {codeText}");

                // Configure visual and technical settings, logging each change
                generator.Parameters.Barcode.XDimension.Point = 2f;
                Log(logPath, $"  XDimension (points): {generator.Parameters.Barcode.XDimension.Point}");

                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                Log(logPath, $"  BarColor: Black");

                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                Log(logPath, $"  BackColor: White");

                generator.Parameters.Resolution = 300f;
                Log(logPath, $"  Resolution (dpi): {generator.Parameters.Resolution}");

                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                Log(logPath, $"  AutoSizeMode: Nearest");

                generator.Parameters.ImageWidth.Pixels = 300f;
                generator.Parameters.ImageHeight.Pixels = 150f;
                Log(logPath, $"  ImageWidth (pixels): {generator.Parameters.ImageWidth.Pixels}");
                Log(logPath, $"  ImageHeight (pixels): {generator.Parameters.ImageHeight.Pixels}");

                // Save the generated barcode image to disk
                generator.Save(barcodePath, BarCodeImageFormat.Png);
                Log(logPath, $"Barcode saved to: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            // Log any generation errors and abort further processing
            Log(logPath, $"Error during barcode generation: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Read the saved barcode and log the decoding results
        // ------------------------------------------------------------
        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                Log(logPath, $"Reading barcode from: {barcodePath}");
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Log(logPath, $"  Decoded CodeText: {result.CodeText}");
                    Log(logPath, $"  Decoded Symbology: {result.CodeType}");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any reading errors
            Log(logPath, $"Error during barcode reading: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Finalize the audit log
        // ------------------------------------------------------------
        Log(logPath, $"Audit Log Completed at {DateTime.UtcNow:u}");
    }

    /// <summary>
    /// Appends a timestamped message to the specified log file using UTF‑8 encoding.
    /// </summary>
    /// <param name="logFilePath">Full path to the audit log file.</param>
    /// <param name="message">Message to record.</param>
    static void Log(string logFilePath, string message)
    {
        string entry = $"{DateTime.UtcNow:u} {message}{Environment.NewLine}";
        File.AppendAllText(logFilePath, entry, Encoding.UTF8);
    }
}