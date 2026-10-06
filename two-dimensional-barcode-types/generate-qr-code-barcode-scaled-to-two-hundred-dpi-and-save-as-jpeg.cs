// Title: Generate QR Code at 200 DPI and Save as JPEG
// Description: This example creates a QR Code barcode with a resolution of 200 DPI and saves it as a JPEG image.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class. It shows how to configure encoding type, set image resolution, and export the barcode to a common image format. Developers working with QR codes for URLs, product tracking, or mobile scanning often need to control DPI for print quality and choose JPEG for web-friendly distribution.
// Prompt: Generate a QR Code barcode scaled to two hundred DPI and save as JPEG.
// Tags: qr code, barcode generation, resolution, jpeg, aspose.barcode, encode types, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a specific DPI and saving it as a JPEG file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an output folder, generates a QR Code, sets resolution, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the resulting JPEG image.
        string outputPath = Path.Combine(outputDir, "qr_200dpi.jpg");

        // Initialize the barcode generator for a QR code with the desired text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the image resolution to 200 DPI.
            generator.Parameters.Resolution = 200f;

            // Save the generated barcode as a JPEG file.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}