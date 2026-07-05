// Title: QR barcode generation, Base64 encoding, and decoding with encoding detection
// Description: Demonstrates generating a QR barcode, converting it to a Base64 string, then decoding it back while detecting Unicode text encoding.
// Prompt: Develop a backend service that receives base64‑encoded barcode images, decodes them with DetectEncoding enabled, and returns decoded text.
// Tags: qr, barcode, base64, encoding detection, aspnet, aspose.barcode, csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, encodes it to Base64, and decodes it with encoding detection enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR barcode from sample Unicode text, converts it to Base64, then reads it back detecting encoding.
    /// </summary>
    static void Main()
    {
        // Sample text containing Unicode characters to test encoding detection.
        const string sampleText = "Привет";

        // Generate a QR barcode image and encode it to a Base64 string.
        string base64Image;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the PNG bytes to a Base64 string.
                base64Image = Convert.ToBase64String(ms.ToArray());
            }
        }

        // Output the Base64 string (optional, can be removed in production).
        Console.WriteLine("Base64 Barcode Image:");
        Console.WriteLine(base64Image);
        Console.WriteLine();

        // Decode the Base64 string back to an image stream.
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (var imageStream = new MemoryStream(imageBytes))
        {
            // Create a barcode reader that checks all supported symbologies.
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                // Enable detection of text encoding for Unicode code sets.
                reader.BarcodeSettings.DetectEncoding = true;

                // Read and output all detected barcodes.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("Decoded Text: " + result.CodeText);
                }
            }
        }
    }
}