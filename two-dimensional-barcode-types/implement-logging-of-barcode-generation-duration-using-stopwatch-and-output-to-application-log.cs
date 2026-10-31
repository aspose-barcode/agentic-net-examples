// Title: Generate Code128 Barcode and Log Generation Time
// Description: Demonstrates creating a Code128 barcode image, measuring generation duration with Stopwatch, and logging the result to a file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical use cases include generating barcodes for inventory, shipping labels, or product tracking, where developers often need to measure performance and maintain logs of generation activities. The example illustrates best practices for timing operations and persisting log entries, useful for debugging and monitoring in production environments.
// Prompt: Implement logging of barcode generation duration using Stopwatch and output to application log.
// Tags: barcode, code128, generation, performance, logging, stopwatch, aspose.barcode, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, measures the generation time,
/// and writes a log entry with the duration and file location.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output image path and log file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
        string logPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_log.txt");
        string codeText = "Sample123";

        // Stopwatch instance for measuring barcode generation duration.
        Stopwatch stopwatch = new Stopwatch();

        try
        {
            // Start timing before barcode generation.
            stopwatch.Start();

            // Create a BarcodeGenerator for Code128 symbology with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set barcode foreground and background colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the generated barcode as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Stop timing after the barcode has been saved.
            stopwatch.Stop();

            // Build a success message including elapsed time and output location.
            string message = $"Barcode generated in {stopwatch.ElapsedMilliseconds} ms. Saved to {outputPath}";
            Console.WriteLine(message);

            // Append the message to the log file with a timestamp.
            File.AppendAllText(logPath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            // Ensure the stopwatch is stopped in case of an exception.
            stopwatch.Stop();

            // Build an error message with exception details.
            string error = $"Error generating barcode: {ex.Message}";
            Console.WriteLine(error);

            // Append the error information to the log file with a timestamp.
            File.AppendAllText(logPath, $"{DateTime.Now}: {error}{Environment.NewLine}");
        }
    }
}