// Title: Generate QR Code barcode using Aspose.BarCode
// Description: Demonstrates creating a QR Code image with Aspose.BarCode and saving it to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to configure QR Code parameters such as X‑dimension, error correction level, and version using the BarcodeGenerator class. Typical use cases include generating QR codes for URLs, product information, or authentication tokens in web and mobile applications. Developers often need to produce QR images in various formats (PNG, JPEG, etc.) and store them programmatically.
// Prompt: Generate QR Code barcode and use Docker container to run generation in isolated environment.
// Tags: qr code, barcode generation, aspnet, aspose.barcode, png, temporary directory

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and saves the image to a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code for a sample URL, configures its appearance, and writes the PNG file path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for output
        string outputDir = Path.Combine(Path.GetTempPath(), "QrGen_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define full file path for the PNG image
        string filePath = Path.Combine(outputDir, "qr.png");

        // Initialize the barcode generator for QR type with the target data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the size of each QR module (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Configure QR error correction level (M) and let the library choose the version automatically
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            generator.Parameters.Barcode.QR.Version = QRVersion.Auto;

            // Save the generated QR code as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated image
        Console.WriteLine($"QR code saved to: {filePath}");
    }
}