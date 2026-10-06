// Title: Batch QR Code Generation with Base64 Output
// Description: Demonstrates how to generate multiple QR Code barcodes using Aspose.BarCode, encode them as PNG, and return the images as Base64 strings.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce QR symbols, configure error correction levels, and output images in common formats. Developers building APIs that need to deliver QR codes as Base64 strings for web or mobile clients will find this pattern useful.
/// Prompt: Generate QR Code barcodes in batch from API request list and return base64 strings in response.
/// Tags: qr code, barcode generation, base64, aspose.barcode, png, batch processing

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of batch QR Code generation and conversion to Base64 strings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR codes for a list of requests,
    /// encodes each image as PNG, converts it to a Base64 string, and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample list of QR code generation requests (simulating an API payload)
        var requests = new List<QrRequest>
        {
            new QrRequest { Text = "Hello World", ErrorLevel = QRErrorLevel.LevelL },
            new QrRequest { Text = "Aspose.BarCode QR", ErrorLevel = QRErrorLevel.LevelM },
            new QrRequest { Text = "https://www.example.com", ErrorLevel = QRErrorLevel.LevelH }
        };

        // Collection to hold the Base64-encoded PNG images
        var responses = new List<string>();

        // Process each request: generate QR code, save to memory, convert to Base64
        foreach (var req in requests)
        {
            // Initialize the barcode generator for QR symbology with the provided text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, req.Text))
            {
                // Configure QR-specific parameters
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                generator.Parameters.Barcode.QR.ErrorLevel = req.ErrorLevel;
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the generated barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Convert the PNG byte array to a Base64 string and store it
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    responses.Add(base64);
                }
            }
        }

        // Output each Base64 string to the console for verification
        for (int i = 0; i < responses.Count; i++)
        {
            Console.WriteLine($"Barcode {i + 1} Base64:");
            Console.WriteLine(responses[i]);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Simple DTO representing a QR code generation request.
    /// </summary>
    class QrRequest
    {
        /// <summary>
        /// Text to encode in the QR code.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Desired error correction level for the QR code.
        /// </summary>
        public QRErrorLevel ErrorLevel { get; set; }
    }
}