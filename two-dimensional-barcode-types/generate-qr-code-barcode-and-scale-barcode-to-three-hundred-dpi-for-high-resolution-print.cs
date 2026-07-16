// Title: Generate QR Code with 300 DPI resolution
// Description: This example creates a QR Code barcode from a URL and sets the image resolution to 300 DPI for high‑resolution printing.
// Category-Description: Demonstrates Aspose.BarCode barcode generation focusing on QR Code symbology and image resolution settings. It uses the BarcodeGenerator class and its Parameters.Resolution property to produce high‑quality PNG output, a common requirement for print media and marketing materials. Developers looking for examples on scaling barcodes, configuring DPI, and exporting to raster formats will find this useful.
// Prompt: Generate QR Code barcode and scale barcode to three hundred DPI for high‑resolution print.
// Tags: qr code, barcode, resolution, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode at 300 DPI and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a QR Code, sets high‑resolution output, and writes the file to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text (a sample URL)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the image resolution to 300 DPI for high‑resolution print quality
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image file
            generator.Save("qr.png");
        }

        // Output a confirmation message to the console
        Console.WriteLine("QR Code generated at 300 DPI: qr.png");
    }
}