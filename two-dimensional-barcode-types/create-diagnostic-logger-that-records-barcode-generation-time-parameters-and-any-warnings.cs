// Title: Barcode generation with diagnostic logging
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode, measuring generation time, and logging parameters and warnings.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class and its Parameters API. Typical use cases include creating barcodes for product labeling, inventory systems, and printing, where developers need to capture generation metrics and diagnostic information for troubleshooting and performance monitoring.
// Prompt: Create a diagnostic logger that records barcode generation time, parameters, and any warnings.
// Tags: barcode, code128, generation, png, logging, diagnostics, aspose.barcode, aspose.drawing

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with diagnostic logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, measures execution time, logs parameters and any errors.
    /// </summary>
    static void Main()
    {
        // Prepare log file path in the temporary folder
        string logFile = Path.Combine(Path.GetTempPath(), "BarcodeDiagnosticLog.txt");
        var logBuilder = new StringBuilder();

        // Sample barcode settings
        string codeText = "Sample123";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Stopwatch for measuring generation time
        Stopwatch sw = new Stopwatch();

        try
        {
            // Initialize the barcode generator with the chosen symbology and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure image and barcode parameters
                generator.Parameters.Resolution = 300f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.ImageWidth.Pixels = 300f;
                generator.Parameters.ImageHeight.Pixels = 150f;
                generator.Parameters.RotationAngle = 0f;

                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.Barcode.BarWidthReduction.Point = 0f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;
                generator.Parameters.Barcode.Padding.Right.Point = 5f;
                generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                generator.Parameters.Border.Color = Aspose.Drawing.Color.Blue;
                generator.Parameters.Border.Width.Pixels = 2f;

                // Start timing the generation process
                sw.Start();

                // Save the generated barcode image to a temporary PNG file
                string outputPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);

                // Stop timing after the image is saved
                sw.Stop();

                // Log successful generation details
                logBuilder.AppendLine("Barcode generation succeeded.");
                logBuilder.AppendLine($"Output file: {outputPath}");
                logBuilder.AppendLine($"Generation time (ms): {sw.ElapsedMilliseconds}");
                logBuilder.AppendLine("Parameters:");
                logBuilder.AppendLine($"  EncodeType: {encodeType}");
                logBuilder.AppendLine($"  CodeText: {codeText}");
                logBuilder.AppendLine($"  Resolution: {generator.Parameters.Resolution}");
                logBuilder.AppendLine($"  AutoSizeMode: {generator.Parameters.AutoSizeMode}");
                logBuilder.AppendLine($"  ImageWidth (px): {generator.Parameters.ImageWidth.Pixels}");
                logBuilder.AppendLine($"  ImageHeight (px): {generator.Parameters.ImageHeight.Pixels}");
                logBuilder.AppendLine($"  RotationAngle: {generator.Parameters.RotationAngle}");
                logBuilder.AppendLine($"  XDimension (pt): {generator.Parameters.Barcode.XDimension.Point}");
                logBuilder.AppendLine($"  BarHeight (pt): {generator.Parameters.Barcode.BarHeight.Point}");
                logBuilder.AppendLine($"  BarWidthReduction (pt): {generator.Parameters.Barcode.BarWidthReduction.Point}");
                logBuilder.AppendLine($"  FilledBars: {generator.Parameters.Barcode.FilledBars}");
                logBuilder.AppendLine($"  ThrowExceptionWhenCodeTextIncorrect: {generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect}");
                logBuilder.AppendLine($"  Padding (pt) L/T/R/B: {generator.Parameters.Barcode.Padding.Left.Point}/{generator.Parameters.Barcode.Padding.Top.Point}/{generator.Parameters.Barcode.Padding.Right.Point}/{generator.Parameters.Barcode.Padding.Bottom.Point}");
                logBuilder.AppendLine($"  BarColor: {generator.Parameters.Barcode.BarColor}");
                logBuilder.AppendLine($"  BackColor: {generator.Parameters.BackColor}");
                logBuilder.AppendLine($"  Border Color: {generator.Parameters.Border.Color}");
                logBuilder.AppendLine($"  Border Width (px): {generator.Parameters.Border.Width.Pixels}");
            }
        }
        catch (Exception ex)
        {
            // Ensure stopwatch is stopped in case of failure
            sw.Stop();

            // Log failure details and any warning messages
            logBuilder.AppendLine("Barcode generation failed with warning:");
            logBuilder.AppendLine($"  Message: {ex.Message}");
            logBuilder.AppendLine($"  Generation time before failure (ms): {sw.ElapsedMilliseconds}");
        }

        // Output log to console
        string logContent = logBuilder.ToString();
        Console.WriteLine(logContent);

        // Append log to the diagnostic file
        try
        {
            File.AppendAllText(logFile, logContent);
            Console.WriteLine($"Log written to: {logFile}");
        }
        catch (Exception logEx)
        {
            Console.WriteLine($"Failed to write log file: {logEx.Message}");
        }
    }
}