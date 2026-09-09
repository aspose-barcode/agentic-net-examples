// Title: Generate QR Code at 200 DPI and save as JPEG
// Description: This example creates a QR Code barcode, sets its resolution to 200 DPI, and saves the image as a JPEG file.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation category, demonstrating how to configure barcode parameters such as resolution and output format using the BarcodeGenerator class. Typical use cases include creating high‑resolution QR codes for printing or digital distribution. Developers often need to adjust DPI and choose image formats when integrating barcode generation into applications.
// Prompt: Generate a QR Code barcode scaled to two hundred DPI and save as JPEG.
// Tags: qr code, barcode generation, resolution, dpi, jpeg, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode at 200 DPI and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the output path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary output directory for the generated barcode image
        string outputDirectory = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        // Ensure the directory exists
        Directory.CreateDirectory(outputDirectory);
        // Build the full file path for the JPEG image
        string outputPath = Path.Combine(outputDirectory, "qr_200dpi.jpg");

        // Initialize the barcode generator with QR code type and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the resolution to 200 DPI
            generator.Parameters.Resolution = 200f;
            // Save the generated barcode as a JPEG file
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved QR Code image
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}