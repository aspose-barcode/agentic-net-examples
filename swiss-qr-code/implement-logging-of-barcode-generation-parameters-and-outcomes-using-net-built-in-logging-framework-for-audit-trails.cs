// Title: Generate a Code128 barcode image and log generation details
// Description: Demonstrates creating a Code128 barcode, configuring its appearance, saving as PNG, and logging parameters and outcomes for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, set encoding type, customize visual parameters, and handle image output. Developers often need to generate barcodes programmatically, adjust size, colors, and capture success or error information for compliance and troubleshooting. The snippet shows typical usage of Aspose.BarCode.Generation classes combined with .NET file I/O for logging.
// Prompt: Implement logging of barcode generation parameters and outcomes using .NET built‑in logging framework for audit trails.
// Tags: barcode, code128, generation, logging, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with Aspose.BarCode and logs the process for audit trails.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and records the operation outcome.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for output and log files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string logFile = Path.Combine(outputDir, "generation.log");

        // Define barcode parameters: symbology and data to encode
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Construct the output file name using the symbology name and encoded text
        string barcodeFile = Path.Combine(outputDir, $"{encodeType.TypeName}_{codeText}.png");

        try
        {
            // Initialize the generator with the chosen symbology and data
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure visual appearance and image properties
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Resolution = 300f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.ImageWidth.Pixels = 300f;
                generator.Parameters.ImageHeight.Pixels = 150f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save the generated barcode as a PNG image
                generator.Save(barcodeFile, BarCodeImageFormat.Png);
            }

            // Log a successful generation event
            Log(logFile, $"SUCCESS: Generated barcode '{encodeType.TypeName}' with text '{codeText}'. Saved to '{barcodeFile}'.");
        }
        catch (Exception ex)
        {
            // Log any error that occurs during generation
            Log(logFile, $"ERROR: Failed to generate barcode '{encodeType.TypeName}' with text '{codeText}'. Exception: {ex.Message}");
        }

        // Inform the user where the log file is located
        Console.WriteLine($"Log written to: {logFile}");
    }

    /// <summary>
    /// Appends a timestamped message to the specified log file using UTF‑8 encoding.
    /// </summary>
    /// <param name="logPath">Full path to the log file.</param>
    /// <param name="message">Message to record.</param>
    static void Log(string logPath, string message)
    {
        string entry = $"{DateTime.UtcNow:O} {message}{Environment.NewLine}";
        File.AppendAllText(logPath, entry, Encoding.UTF8);
    }
}