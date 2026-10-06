// Title: Barcode Generation with Audit Logging using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode, saving it as a PNG file, and logging all generation parameters and outcomes to an audit log.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode properties, render the image, and record operational details using custom file logging. Developers working with barcode creation, image export, and compliance auditing can use similar patterns with BarcodeGenerator, BarCodeImageFormat, and parameter settings to meet regulatory or diagnostic requirements.
// Prompt: Implement logging of barcode generation parameters and outcomes using .NET built‑in logging framework for audit trails.
// Tags: barcode, symbology, generation, png, logging, audit, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, saves it as a PNG image, and logs generation details for audit purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Prepares output locations, configures the barcode, logs parameters,
    /// saves the image, and records success or error information.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory and file paths for the barcode image and audit log
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeAudit_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "sample.png");
        string logPath = Path.Combine(outputDir, "audit.log");

        // --------------------------------------------------------------------
        // Simple file‑based logging helper that also writes to console
        // --------------------------------------------------------------------
        void Log(string message)
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}{Environment.NewLine}";
            File.AppendAllText(logPath, entry);
            Console.WriteLine(message);
        }

        try
        {
            // --------------------------------------------------------------------
            // Create a barcode generator for Code128 symbology with the desired text
            // --------------------------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // --------------------------------------------------------------------
                // Configure barcode appearance and rendering parameters
                // --------------------------------------------------------------------
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Resolution = 300f;
                generator.Parameters.RotationAngle = 0f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // --------------------------------------------------------------------
                // Log all relevant generation parameters for audit tracking
                // --------------------------------------------------------------------
                Log("Generating barcode with the following parameters:");
                Log($"  Symbology: {generator.BarcodeType.TypeName}");
                Log($"  CodeText: {generator.CodeText}");
                Log($"  XDimension (points): {generator.Parameters.Barcode.XDimension.Point}");
                Log($"  BarHeight (points): {generator.Parameters.Barcode.BarHeight.Point}");
                Log($"  BarColor: {generator.Parameters.Barcode.BarColor}");
                Log($"  BackColor: {generator.Parameters.BackColor}");
                Log($"  Resolution (dpi): {generator.Parameters.Resolution}");
                Log($"  RotationAngle: {generator.Parameters.RotationAngle}");
                Log($"  AutoSizeMode: {generator.Parameters.AutoSizeMode}");

                // --------------------------------------------------------------------
                // Save the generated barcode image to the specified PNG file
                // --------------------------------------------------------------------
                generator.Save(barcodePath, BarCodeImageFormat.Png);
                Log($"Barcode image saved successfully to: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            // --------------------------------------------------------------------
            // Log any exception that occurs during barcode generation
            // --------------------------------------------------------------------
            Log($"Error during barcode generation: {ex.GetType().Name} - {ex.Message}");
        }

        // --------------------------------------------------------------------
        // Final log entry indicating where the audit log is stored
        // --------------------------------------------------------------------
        Log($"Audit log written to: {logPath}");
    }
}