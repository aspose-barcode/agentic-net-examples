// Title: Generate QR Code and save as PNG with maximum compression
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and saving it as a PNG file. The example shows how to generate the barcode and store it in a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical use cases include creating QR codes for URLs, contact information, or product data and exporting them to common image formats such as PNG. Developers often need to generate barcodes programmatically and control output settings like image format and file location.
// Prompt: Generate QR Code barcode and apply compression level 9 to PNG output for minimal file size.
// Tags: qr code, barcode generation, png output, compression, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and writes the file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the system's temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "QrCode.png");

        // Ensure the target directory exists.
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Create a QR Code generator for the specified data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // PNG compression level is not configurable via Aspose.BarCode API.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}