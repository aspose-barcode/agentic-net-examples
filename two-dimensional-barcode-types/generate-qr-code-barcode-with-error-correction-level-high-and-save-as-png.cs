// Title: Generate QR Code with High Error Correction and Save as PNG
// Description: This example generates a QR Code barcode with a high error correction level and saves it as a PNG image.
// Category-Description: Demonstrates Aspose.BarCode generation of QR Code symbology, focusing on configuring error correction levels. The example uses BarcodeGenerator, EncodeTypes, and QRErrorLevel classes to create a QR Code suitable for scenarios where data integrity is critical, such as marketing materials or product packaging. Developers often need to adjust error correction to balance readability and data capacity, and this snippet shows the typical workflow for generating and exporting the barcode image.
// Prompt: Generate a QR Code barcode with error correction level high and save as PNG.
// Tags: qr code, barcode generation, error correction, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR Code with high error correction and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_high_error.png");

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code with high error correction"))
        {
            // Set the QR Code error correction level to high (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR Code as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}