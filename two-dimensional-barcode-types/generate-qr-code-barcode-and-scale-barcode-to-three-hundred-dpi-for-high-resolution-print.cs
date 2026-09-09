// Title: Generate QR Code and Save at 300 DPI PNG
// Description: This example creates a QR Code barcode containing a URL and saves it as a high‑resolution 300 DPI PNG image suitable for printing.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.QR. Shows how to configure barcode parameters such as resolution and export the result to a PNG file. Ideal for developers needing high‑resolution barcodes for print media, packaging, or marketing materials.
// Prompt: Generate QR Code barcode and scale barcode to three hundred DPI for high‑resolution print.
// Tags: qr code, barcode generation, resolution, png, aspose.barcode, high-resolution, print

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code and saves it as a 300 DPI PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_300dpi.png");

        // Create a BarcodeGenerator for QR Code with the desired text (URL).
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the barcode resolution to 300 DPI for high‑resolution printing.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}