// Title: Generate QR Code barcode and save as PNG in a temporary folder
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, configure its appearance, and write the image to a temporary directory.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.QR, setting XDimension and error correction level, and exporting to common image formats such as PNG. Developers working on QR Code creation, automated image generation, or integration with containerized workflows can reference this pattern for quick implementation.
// Prompt: Generate QR Code barcode and use Docker container to run generation in isolated environment.
// Tags: qr code, barcode generation, png output, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the QR Code generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code barcode, saves it as a PNG file in a temporary directory, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrBarcode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, "qr.png");

        // Initialize the barcode generator for QR Code with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional appearance settings: set module size and high error correction level
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved
        Console.WriteLine("QR Code generated at: " + outputPath);
    }
}