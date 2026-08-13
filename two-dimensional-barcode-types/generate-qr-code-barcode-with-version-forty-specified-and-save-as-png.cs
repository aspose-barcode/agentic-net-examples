// Title: Generate QR Code with Version 40 and Save as PNG
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set its version to the maximum (40), and save the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with QR Code symbology. It shows how to configure QR-specific parameters such as version, which determines the matrix size, and how to export the generated barcode to common image formats like PNG. Developers working on encoding data into QR codes for applications such as product labeling, mobile scanning, or data sharing can reference this pattern.
// Prompt: Generate a QR Code barcode with version forty specified and save as PNG.
// Tags: qr code, barcode generation, png output, aspose.barcode, encode types, qr version

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with version 40 and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Text to be encoded in the QR Code.
        const string codeText = "Hello, QR Code Version 40!";

        // Destination file path for the generated PNG image.
        const string outputPath = "qr_version40.png";

        // Initialize the barcode generator for QR Code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Configure the QR Code to use version 40 (the largest matrix size).
            generator.Parameters.Barcode.QR.Version = QRVersion.Version40;

            // Render and save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user that the file has been created.
        Console.WriteLine($"QR Code saved to '{outputPath}'.");
    }
}