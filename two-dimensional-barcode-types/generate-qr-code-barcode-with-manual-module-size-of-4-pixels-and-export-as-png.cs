// Title: Generate QR Code with Custom Module Size and Save as PNG
// Description: Demonstrates how to create a QR Code barcode with a manually specified module size of 4 pixels and export it to a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating QR Code images with custom dimensions for web, print, or mobile applications. Developers often need to control module size to meet scanning requirements or design specifications.
// Prompt: Generate a QR Code barcode with manual module size of 4 pixels and export as PNG.
// Tags: qr code, barcode generation, module size, png output, aspose.barcode, encode types, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with a specific module size and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the module (X) dimension to 4 pixels for manual sizing.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated QR Code image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR Code image.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}