// Title: Diagnostic Logger for Barcode Generation
// Description: Demonstrates how to generate a Code128 barcode while logging generation time, parameters, and warnings.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, its Parameters, and saving the image. Developers often need to log barcode creation details for diagnostics, performance monitoring, and troubleshooting. The snippet illustrates typical API calls for setting symbology, visual properties, and handling errors.
// Prompt: Create a diagnostic logger that records barcode generation time, parameters, and any warnings.
// Tags: barcode, code128, generation, logging, diagnostics, aspose.barcode, png, performance

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, logs configuration parameters,
/// measures generation time, and records any warnings to a log file.
/// </summary>
class Program
{
    // Path to the diagnostic log file.
    static readonly string LogFilePath = "barcode_log.txt";

    /// <summary>
    /// Writes a message to the console and appends it to the log file.
    /// Logging failures are silently ignored to avoid crashing the program.
    /// </summary>
    /// <param name="message">The message to log.</param>
    static void Log(string message)
    {
        Console.WriteLine(message);
        try
        {
            File.AppendAllText(LogFilePath, message + Environment.NewLine);
        }
        catch
        {
            // Ignored - logging failure should not crash the program
        }
    }

    /// <summary>
    /// Main entry point. Generates a barcode, logs parameters and timing,
    /// and handles any exceptions by logging warnings.
    /// </summary>
    static void Main()
    {
        // Initialize the log file with a header.
        try
        {
            File.WriteAllText(LogFilePath, "Barcode Generation Log" + Environment.NewLine);
        }
        catch
        {
            // If unable to write, continue with console logging only.
        }

        string outputPath = "sample_barcode.png";
        Stopwatch stopwatch = new Stopwatch();

        try
        {
            // Create a BarcodeGenerator for Code128 with the specified data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Configure visual and technical barcode parameters.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Resolution = 300f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.RotationAngle = 0f;

                // Log the configured parameters for diagnostic purposes.
                Log("Configured Parameters:");
                Log($"  XDimension (points): {generator.Parameters.Barcode.XDimension.Point}");
                Log($"  BarHeight (points): {generator.Parameters.Barcode.BarHeight.Point}");
                Log($"  BarColor: {generator.Parameters.Barcode.BarColor}");
                Log($"  BackColor: {generator.Parameters.BackColor}");
                Log($"  Resolution (dpi): {generator.Parameters.Resolution}");
                Log($"  AutoSizeMode: {generator.Parameters.AutoSizeMode}");
                Log($"  RotationAngle: {generator.Parameters.RotationAngle}");

                // Measure the time taken to generate and save the barcode image.
                stopwatch.Start();
                generator.Save(outputPath, BarCodeImageFormat.Png);
                stopwatch.Stop();

                // Log the elapsed time and output location.
                Log($"Barcode generated in {stopwatch.ElapsedMilliseconds} ms and saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any warnings or errors that occur during generation.
            Log($"Warning: {ex.GetType().Name} - {ex.Message}");
        }
    }
}