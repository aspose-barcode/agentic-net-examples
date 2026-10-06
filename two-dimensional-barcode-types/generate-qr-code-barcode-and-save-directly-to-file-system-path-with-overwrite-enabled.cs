// Title: Generate QR Code and Save as PNG with Overwrite
// Description: Demonstrates how to generate a QR Code barcode using Aspose.BarCode and save it directly to a file path, overwriting any existing file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.QR, setting barcode parameters such as XDimension and error correction level, and saving the image with BarCodeImageFormat. Developers commonly use these APIs to create QR codes for URLs, product information, or authentication purposes, and need to control output format and file handling.
// Prompt: Generate a QR Code barcode and save directly to file system path with overwrite enabled.
// Tags: qr code, barcode generation, png, overwrite, aspose.barcode, encode types, qrcode, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the QR Code and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the PNG image.
        string outputPath = Path.Combine(outputDir, "qr_code.png");
        // Text to encode in the QR Code (e.g., a URL).
        string codeText = "https://www.example.com";

        // Initialize the barcode generator for QR Code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Optional: set the error correction level to Medium.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
            // Save the generated QR Code image to the specified path, overwriting if it exists.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR Code image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}