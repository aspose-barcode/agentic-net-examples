// Title: Batch QR Code Generation with Base64 Output
// Description: Demonstrates generating multiple QR Code barcodes from a list of strings and returning each as a Base64‑encoded PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation, image rendering, and data encoding. It showcases the BarcodeGenerator class, QR-specific parameters, and image format handling, which are common tasks for developers building APIs that need to deliver barcode images as Base64 strings for web or mobile clients.
// Prompt: Generate QR Code barcodes in batch from API request list and return base64 strings in response.
// Tags: qr code, batch generation, base64, image, aspose.barcode, barcode generation, png

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the QR code batch generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates QR codes for each input string, encodes them as Base64 PNGs, and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Sample list of QR code data that would normally come from an API request
        var requestData = new List<string>
        {
            "Hello, World!",
            "Aspose.BarCode",
            "https://www.example.com",
            "QR Code Batch",
            "1234567890"
        };

        // Store the resulting Base64 strings
        var base64Results = new List<string>();

        // Iterate over each text value and generate a QR code image
        foreach (var text in requestData)
        {
            // Create a QR code generator for the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set QR error correction level (Level M) and enable auto‑size mode
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Define image dimensions (250x250 points)
                generator.Parameters.ImageWidth.Point = 250f;
                generator.Parameters.ImageHeight.Point = 250f;

                // Render the barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Convert the PNG bytes to a Base64 string and store it
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    base64Results.Add(base64);
                }
            }
        }

        // Output the Base64 strings (one per line) to the console
        foreach (var base64 in base64Results)
        {
            Console.WriteLine(base64);
        }
    }
}