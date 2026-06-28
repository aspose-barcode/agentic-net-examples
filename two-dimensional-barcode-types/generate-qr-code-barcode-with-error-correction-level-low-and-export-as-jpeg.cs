using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code image using Aspose.BarCode and saving it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code with low error correction level and saves it.
    /// </summary>
    /// <param name="args">Command-line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine the output file path; the file will be saved in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_low.jpg");

        // Initialize a QR Code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Configure the QR Code to use the lowest error correction level (Level L).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;

            // Render and save the QR Code as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}