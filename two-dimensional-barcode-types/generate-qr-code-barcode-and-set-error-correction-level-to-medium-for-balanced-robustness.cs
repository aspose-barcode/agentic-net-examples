// Title: Generate QR Code with Medium Error Correction Level
// Description: Demonstrates creating a QR Code barcode with medium error correction (Level M) for balanced robustness and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure QR Code parameters such as error correction level using the BarcodeGenerator class. Typical use cases include generating QR codes for URLs, contact information, or product data where a moderate level of data recovery is desired. Developers often need to adjust QRErrorLevel to meet scanning reliability requirements across various environments.
// Prompt: Generate QR Code barcode and set error correction level to medium for balanced robustness.
// Tags: qr code, error correction, barcode generation, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode with medium error correction level and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates the QR Code, and writes the file path to console.
    /// </summary>
    static void Main(string[] args)
    {
        // Determine a temporary output folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG image
        string outputPath = Path.Combine(outputDir, "qr_medium.png");

        // Initialize the barcode generator for QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, QR!"))
        {
            // Set QR Code error correction level to Medium (Level M)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}