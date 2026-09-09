// Title: Generate QR Code with Version 10 and Save as BMP
// Description: Demonstrates creating a QR Code barcode with a specific version (10) using Aspose.BarCode and exporting it to a BMP image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation with custom version settings. It showcases the use of BarcodeGenerator, EncodeTypes, QRVersion, and BarCodeImageFormat classes to produce high‑resolution QR symbols for applications such as product labeling, authentication, or data sharing. Developers often need to control QR version to fit data size and desired image dimensions.
// Prompt: Generate a QR Code barcode with version ten specified and export as BMP.
// Tags: qr code, barcode generation, version10, bmp, aspose.barcode, encode types, qrversion

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with version 10 and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code and writes it to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Build full file path for BMP output
        string filePath = Path.Combine(outputDir, "QrCodeVersion10.bmp");

        // Initialize generator with QR encoding and data
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set module size (XDimension) in pixels
            gen.Parameters.Barcode.XDimension.Pixels = 4;

            // Specify QR version 10
            gen.Parameters.Barcode.QR.Version = QRVersion.Version10;

            // Save barcode as BMP
            gen.Save(filePath, BarCodeImageFormat.Bmp);
        }

        // Inform user of saved file location
        Console.WriteLine($"QR Code saved to {filePath}");
    }
}