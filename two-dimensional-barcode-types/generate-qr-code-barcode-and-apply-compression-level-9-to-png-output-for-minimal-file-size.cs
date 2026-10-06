// Title: Generate QR Code and save as compressed PNG
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and saving it as a PNG file. Shows how to configure basic barcode parameters and store the image in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.QR to produce QR Code symbols. It covers setting barcode parameters such as X‑dimension and exporting the result to common image formats like PNG. Developers working on barcode creation for web or mobile applications often need to generate QR codes and save them efficiently, making this snippet a useful reference for quick integration.
// Prompt: Generate QR Code barcode and apply compression level 9 to PNG output for minimal file size.
// Tags: qr code, barcode generation, png, compression, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the PNG image
        string outputPath = Path.Combine(outputDir, "QRCode.png");

        // Create a BarcodeGenerator for QR Code with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Optionally set the X dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG file.
            // Note: Aspose.BarCode uses default lossless PNG compression; explicit compression level is not exposed.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}