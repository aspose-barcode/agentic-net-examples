// Title: Generate QR Code and Save as PNG using Aspose.BarCode
// Description: This example creates a QR Code barcode from an alphanumeric string and saves it as a PNG image file.
// Category-Description: Demonstrates the Aspose.BarCode generation workflow for QR Code symbology. It showcases the use of EncodeTypes, BarcodeGenerator, and BarCodeImageFormat classes to encode data, configure QR parameters (error correction, module size), and output the result as a PNG image. Ideal for developers needing quick QR Code creation for URLs, product IDs, or authentication tokens.
// Prompt: Generate a QR Code barcode from alphanumeric text and save as PNG image file.
// Tags: qr code, barcode generation, png, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode from a given text and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_code.png");

        // Text to be encoded into the QR Code.
        string codeText = "ABC123XYZ";

        // Create a BarcodeGenerator for QR Code with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set a high error correction level (Level H) to improve readability after damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}