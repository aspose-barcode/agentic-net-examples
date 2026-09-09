// Title: Generate QR Code and measure generation time
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, saving it as PNG, and logging the time taken for generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with QR Code symbology. It shows setting barcode parameters such as X‑dimension and error correction level, saving the image, and measuring performance with Stopwatch. Developers working on barcode creation, image output, or performance profiling can reference this pattern.
// Prompt: Generate a QR Code barcode and log generation time for performance analysis.
// Tags: qr code, barcode generation, performance measurement, aspose.barcode, png output

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation and performance timing using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code, saves it, and logs the elapsed time.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory with a unique name
        string outputPath = Path.Combine(Path.GetTempPath(), $"qr_{Guid.NewGuid():N}.png");

        // Start the stopwatch to measure generation time
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Initialize the barcode generator with QR symbology and the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, Aspose!"))
        {
            // Set the X dimension (pixel size) of the barcode modules
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Configure the QR Code error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop the stopwatch after generation completes
        stopwatch.Stop();

        // Output the elapsed time and the location of the saved image
        Console.WriteLine($"QR Code generated in {stopwatch.ElapsedMilliseconds} ms. Saved to: {outputPath}");
    }
}