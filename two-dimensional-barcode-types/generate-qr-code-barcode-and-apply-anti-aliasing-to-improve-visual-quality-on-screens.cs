// Title: Generate QR Code with Anti-Aliasing
// Description: Demonstrates creating a QR Code barcode and enabling anti‑aliasing to improve on‑screen visual quality.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image rendering options. It showcases the use of BarcodeGenerator, EncodeTypes, and rendering parameters such as UseAntiAlias and XDimension to produce high‑quality PNG images. Developers often need to generate QR codes for web links or app integration and require clear rendering for display on screens.
// Prompt: Generate QR Code barcode and apply anti‑aliasing to improve visual quality on screens.
// Tags: qr code, anti-aliasing, barcode generation, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode with anti‑aliasing enabled and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Creates the output folder, generates the QR code, and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Determine and create the output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "qr_anti_alias.png");

        // Initialize the barcode generator for a QR code with the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Enable anti‑aliasing for smoother on‑screen rendering
            generator.Parameters.UseAntiAlias = true;
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved QR code image
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}