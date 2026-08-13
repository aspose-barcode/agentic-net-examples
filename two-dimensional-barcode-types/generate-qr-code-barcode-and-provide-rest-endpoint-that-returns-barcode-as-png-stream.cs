// Title: Generate QR Code and Return as PNG via Simulated REST Endpoint
// Description: Demonstrates how to generate a QR Code barcode using Aspose.BarCode and return the image as a PNG byte array, suitable for serving through a REST API.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image export. It showcases the BarcodeGenerator class with EncodeTypes.QR, configuring QR error correction, and saving the result in PNG format using BarCodeImageFormat. Developers building web services or APIs often need to produce barcode images on‑the‑fly for client applications, and this pattern illustrates the typical steps required.
// Prompt: Generate QR Code barcode and provide a REST endpoint that returns barcode as PNG stream.
// Tags: qr code,barcode generation,rest endpoint,png output,aspose.barcode,aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple demonstration of generating a QR Code barcode and returning it as a PNG byte array,
/// mimicking a REST endpoint response.
/// </summary>
class Program
{
    // Simulated REST endpoint method that returns QR code PNG as a byte array
    static byte[] GetQrCodePng(string text)
    {
        // Create QR code generator with QR symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = text;

            // Configure error correction level (optional, LevelM provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Return the PNG image as a byte array
                return memoryStream.ToArray();
            }
        }
    }

    /// <summary>
    /// Entry point that generates a sample QR Code, saves it to a file, and displays basic information.
    /// </summary>
    static void Main()
    {
        // Sample QR code content
        string sampleText = "Hello, Aspose QR!";

        // Call the simulated endpoint to obtain PNG data
        byte[] pngData = GetQrCodePng(sampleText);

        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Write the PNG data to a file for verification
        File.WriteAllBytes(outputPath, pngData);

        // Output result information to the console
        Console.WriteLine($"QR code generated and saved to: {outputPath}");
        Console.WriteLine($"PNG size: {pngData.Length} bytes");
    }
}