// Title: Diagnostic Logger for Barcode Generation with Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode, measure generation time, and log parameters and warnings to a text file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It shows typical tasks such as configuring barcode appearance, setting resolution, and capturing diagnostic information for troubleshooting and performance monitoring. Developers working with barcode creation often need to log generation details to audit processes or debug issues.
// Prompt: Create a diagnostic logger that records barcode generation time, parameters, and any warnings.
// Tags: barcode, code128, generation, logging, diagnostics, aspose.barcode, png, performance

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with diagnostic logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, measures execution time, and writes a detailed log.
    /// </summary>
    static void Main()
    {
        // Define file paths for the barcode image and the diagnostic log
        string outputImage = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
        string logFile = Path.Combine(Directory.GetCurrentDirectory(), "barcode_log.txt");

        // Start a stopwatch to measure how long barcode generation takes
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Initialize the barcode generator with Code128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure visual appearance and technical parameters
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;          // Bar color
            generator.Parameters.Barcode.XDimension.Point = 2f;                         // Module size (points)
            generator.Parameters.Barcode.BarHeight.Point = 50f;                         // Bar height (points)
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;                      // Disable auto-sizing
            generator.Parameters.Resolution = 300f;                                      // Image resolution (DPI)
            generator.Parameters.Barcode.FilledBars = true;                             // Use filled bars

            try
            {
                // Save the generated barcode as a PNG image
                generator.Save(outputImage, BarCodeImageFormat.Png);
                stopwatch.Stop();

                // Build a comprehensive log entry with timestamp, duration, and all parameters
                string logEntry = $"Timestamp: {DateTime.Now:O}{Environment.NewLine}" +
                                  $"Duration (ms): {stopwatch.ElapsedMilliseconds}{Environment.NewLine}" +
                                  $"Output Image: {outputImage}{Environment.NewLine}" +
                                  $"Parameters:{Environment.NewLine}" +
                                  $"  BarColor: {generator.Parameters.Barcode.BarColor}{Environment.NewLine}" +
                                  $"  XDimension (pt): {generator.Parameters.Barcode.XDimension.Point}{Environment.NewLine}" +
                                  $"  BarHeight (pt): {generator.Parameters.Barcode.BarHeight.Point}{Environment.NewLine}" +
                                  $"  AutoSizeMode: {generator.Parameters.AutoSizeMode}{Environment.NewLine}" +
                                  $"  Resolution (dpi): {generator.Parameters.Resolution}{Environment.NewLine}" +
                                  $"  FilledBars: {generator.Parameters.Barcode.FilledBars}{Environment.NewLine}" +
                                  $"---{Environment.NewLine}";

                // Append the log entry to the log file
                File.AppendAllText(logFile, logEntry);
                Console.WriteLine("Barcode generated successfully.");
                Console.WriteLine($"Log written to: {logFile}");
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                // Record any warnings or errors that occurred during generation
                string warningEntry = $"Timestamp: {DateTime.Now:O}{Environment.NewLine}" +
                                      $"Duration (ms): {stopwatch.ElapsedMilliseconds}{Environment.NewLine}" +
                                      $"Warning: {ex.Message}{Environment.NewLine}" +
                                      $"---{Environment.NewLine}";
                File.AppendAllText(logFile, warningEntry);
                Console.WriteLine($"Error generating barcode: {ex.Message}");
            }
        }
    }
}