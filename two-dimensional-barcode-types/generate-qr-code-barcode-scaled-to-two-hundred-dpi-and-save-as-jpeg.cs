// Title: Generate QR Code barcode at 200 DPI and save as JPEG
// Description: This example creates a QR Code barcode, sets its resolution to 200 DPI, and saves it as a JPEG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.QR. Typical scenarios include creating high‑resolution QR codes for print media, marketing materials, or product packaging. Developers often need to adjust image resolution and output format, leveraging the Parameters.Resolution property and BarCodeImageFormat enumeration.
// Prompt: Generate a QR Code barcode scaled to two hundred DPI and save as JPEG.
// Tags: qr code, barcode generation, resolution, jpeg, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode, sets a 200 DPI resolution, and saves it as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path (saved in the current working directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_code.jpg");

        // Initialize a QR Code generator within a using block to ensure proper disposal
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the data to be encoded in the QR Code
            generator.CodeText = "https://example.com";

            // Configure the image resolution to 200 DPI for higher quality output
            generator.Parameters.Resolution = 200f;

            // Persist the generated barcode as a JPEG image at the specified path
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}