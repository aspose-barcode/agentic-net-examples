// Title: Generate QR Code with Manual Module Size and Save as PNG
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode, set a custom module (pixel) size, and export the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as XDimension for QR Code symbology, and how to save the generated barcode in various image formats. Developers commonly use the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce barcodes for web, mobile, or print applications.
// Prompt: Generate a QR Code barcode with manual module size of 4 pixels and export as PNG.
// Tags: qr code, barcode generation, png output, xdimension, aspose.barcode, aspose.barcode.generation, aspose.drawing.imaging

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode with a manual module size
/// and saves it as a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Create a BarcodeGenerator for QR Code symbology with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose"))
        {
            // Set the module (pixel) size to 4 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}