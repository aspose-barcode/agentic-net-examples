// Title: Barcode Generation with Diagnostic Logging
// Description: Generates a Code128 barcode image while logging generation parameters, timing, and any warnings to a log file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class and its Parameters property to configure barcode appearance, resolution, and layout. Typical use cases include creating barcodes for product labeling, inventory systems, and shipping documents, where developers often need to capture generation details for auditing or troubleshooting. The example also shows how to record diagnostic information such as execution time and potential warnings, a common requirement for robust barcode processing pipelines.
// Prompt: Create a diagnostic logger that records barcode generation time, parameters, and any warnings.
// Tags: barcode symbology, generation, logging, code128, png, aspose.barcode, diagnostics

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode with detailed diagnostic logging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary output folder, configures a BarcodeGenerator,
    /// saves the barcode image, and writes a log containing parameters, timing, and warnings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "sample.png");
        string logPath = Path.Combine(outputDir, "generation.log");

        // Sample barcode data
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Initialize log builder with start time and basic info
        var logBuilder = new StringBuilder();
        logBuilder.AppendLine($"Barcode generation started at {DateTime.UtcNow:O}");
        logBuilder.AppendLine($"Output directory: {outputDir}");
        logBuilder.AppendLine($"Code text: {codeText}");
        logBuilder.AppendLine($"Encode type: {encodeType}");

        // Start timing the generation process
        var stopwatch = Stopwatch.StartNew();

        try
        {
            // Initialize the generator with the chosen symbology and data
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure generation parameters
                generator.Parameters.Resolution = 300f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.ImageWidth.Pixels = 300f;
                generator.Parameters.ImageHeight.Pixels = 150f;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.RotationAngle = 0f;
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                // Log configured parameters for diagnostics
                logBuilder.AppendLine("Generator parameters:");
                logBuilder.AppendLine($"  Resolution: {generator.Parameters.Resolution}");
                logBuilder.AppendLine($"  AutoSizeMode: {generator.Parameters.AutoSizeMode}");
                logBuilder.AppendLine($"  ImageWidth: {generator.Parameters.ImageWidth.Pixels}");
                logBuilder.AppendLine($"  ImageHeight: {generator.Parameters.ImageHeight.Pixels}");
                logBuilder.AppendLine($"  XDimension: {generator.Parameters.Barcode.XDimension.Point}");
                logBuilder.AppendLine($"  BarColor: {generator.Parameters.Barcode.BarColor}");
                logBuilder.AppendLine($"  BackColor: {generator.Parameters.BackColor}");
                logBuilder.AppendLine($"  RotationAngle: {generator.Parameters.RotationAngle}");
                logBuilder.AppendLine($"  Padding (L,T,R,B): {generator.Parameters.Barcode.Padding.Left.Point}, {generator.Parameters.Barcode.Padding.Top.Point}, {generator.Parameters.Barcode.Padding.Right.Point}, {generator.Parameters.Barcode.Padding.Bottom.Point}");

                // Save the barcode image to the specified path
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }

            // Stop timing after successful generation
            stopwatch.Stop();
            logBuilder.AppendLine($"Barcode generated successfully in {stopwatch.ElapsedMilliseconds} ms.");
            logBuilder.AppendLine($"Barcode image saved to: {barcodePath}");
        }
        catch (Exception ex)
        {
            // Capture any exception, stop timing, and log a warning
            stopwatch.Stop();
            logBuilder.AppendLine($"Warning: Exception during barcode generation - {ex.GetType().Name}: {ex.Message}");
            logBuilder.AppendLine($"Elapsed time before failure: {stopwatch.ElapsedMilliseconds} ms.");
        }

        // Write log to file and also output to console for immediate feedback
        try
        {
            File.WriteAllText(logPath, logBuilder.ToString());
            Console.WriteLine(logBuilder.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to write log file: {ex.Message}");
        }
    }
}