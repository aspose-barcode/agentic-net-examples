// Title: Generate QR Code with Medium Error Correction Level
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, setting the error correction level to medium (Level M) for a balance between data capacity and robustness, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel classes to configure QR Code parameters such as error correction. Developers commonly need to generate QR codes for URLs, contact info, or product data, adjusting error correction to meet scanning reliability requirements.
// Prompt: Generate QR Code barcode and set error correction level to medium for balanced robustness.
// Tags: qr code, error correction, barcode generation, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code with medium error correction using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the QR Code image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_medium.png");

        // Initialize the barcode generator for QR Code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, QR Code!"))
        {
            // Set the QR Code error correction level to Medium (Level M) for balanced robustness.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}