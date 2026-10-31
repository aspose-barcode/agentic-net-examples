// Title: Generate QR Code and Save as JPEG with Automatic Version Selection
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, letting the library automatically choose the optimal QR version, and exporting the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code symbologies. Typical use cases include encoding URLs, contact information, or other data for mobile scanning. Developers often need to configure visual parameters and export the result in common image formats such as JPEG, PNG, or BMP.
// Prompt: Generate a QR Code barcode with automatic version selection and export as JPEG.
// Tags: qr code, barcode generation, jpeg output, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a QR Code barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "QRCodeAuto.jpg");

        // Initialize the barcode generator with QR encoding and the data to encode.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the module (pixel) size of the QR Code; the library will select the appropriate version automatically.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated QR Code as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved QR Code image.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}