// Title: Generate QR Code and Return as PNG via Simulated REST Endpoint
// Description: Demonstrates how to create a QR Code barcode using Aspose.BarCode and return the image as a PNG byte array, suitable for serving through a REST API.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image export. It showcases the BarcodeGenerator class with QR encoding, configuring error correction, and saving to common image formats such as PNG. Developers building web services or APIs often need to generate barcodes on‑the‑fly and stream them directly to clients without writing temporary files.
// Prompt: Generate QR Code barcode and provide a REST endpoint that returns barcode as PNG stream.
// Tags: qr code,barcode generation,rest endpoint,png output,aspose.barcode,aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and returns it as a PNG byte array,
/// simulating a REST endpoint response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code PNG, writes it to disk for demo purposes,
    /// and outputs the file location to the console.
    /// </summary>
    static void Main()
    {
        // Simulate a REST endpoint call that returns the QR Code as a PNG byte array
        byte[] pngData = GetQrCodePng("Hello Aspose!");

        // Write the PNG to a file to demonstrate the generated output
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");
        File.WriteAllBytes(outputPath, pngData);
        Console.WriteLine($"QR Code PNG saved to: {outputPath}");
    }

    /// <summary>
    /// Simulated REST handler that creates a QR Code barcode for the specified text
    /// and returns the image as a PNG byte array.
    /// </summary>
    /// <param name="text">The text to encode in the QR Code.</param>
    /// <returns>Byte array containing the PNG representation of the QR Code.</returns>
    static byte[] GetQrCodePng(string text)
    {
        // Validate input to avoid generating an empty barcode
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Text for QR code cannot be null or empty.", nameof(text));

        byte[] result;
        // Initialize the barcode generator with QR encoding and the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Set a high error correction level to improve readability under adverse conditions
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode directly to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                result = ms.ToArray(); // Convert the stream to a byte array for transmission
            }
        }
        return result;
    }
}