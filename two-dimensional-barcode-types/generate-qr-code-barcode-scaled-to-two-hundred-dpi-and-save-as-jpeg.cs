// Title: Generate QR Code at 200 DPI and Save as JPEG
// Description: This example creates a QR Code barcode with a resolution of 200 DPI and saves it as a JPEG image file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR Code symbology, focusing on image resolution settings and JPEG output. Uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating high‑resolution QR codes for print media, marketing materials, or product labeling where precise DPI control is required.
// Prompt: Generate a QR Code barcode scaled to two hundred DPI and save as JPEG.
// Tags: qr code, barcode generation, resolution, dpi, jpeg, aspose.barcode, encode types, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode at 200 DPI and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory path.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Build the full output file path.
        string outputPath = Path.Combine(outputDir, "qr_200dpi.jpg");

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, QR!"))
        {
            // Set the image resolution to 200 DPI.
            generator.Parameters.Resolution = 200f;
            // Save the generated barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}