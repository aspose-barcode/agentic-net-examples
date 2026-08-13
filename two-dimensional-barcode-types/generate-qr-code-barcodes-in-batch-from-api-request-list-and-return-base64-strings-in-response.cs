// Title: Batch QR Code Generation and Base64 Output
// Description: Demonstrates how to generate multiple QR Code barcodes from a list of strings and return each image as a Base64‑encoded PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class for QR symbology. Typical use cases include creating QR codes in bulk for API responses, embedding them in web pages, or storing them in databases. Developers often need to convert generated images to Base64 strings for transport over JSON or HTML.
// Prompt: Generate QR Code barcodes in batch from API request list and return base64 strings in response.
// Tags: qr code, batch generation, base64, png, aspose.barcode, barcodegenerator, encode types

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example of generating QR Code barcodes in batch and returning them as Base64 strings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that simulates an API request list, generates QR codes, and prints Base64 results.
    /// </summary>
    static void Main()
    {
        // Simulated API request payload: list of texts to encode as QR codes
        List<string> requestTexts = new List<string>
        {
            "https://example.com",
            "Hello, World!",
            "1234567890",
            "Aspose.BarCode QR",
            "Base64 Test"
        };

        // Collection to hold the Base64 representations of generated QR codes
        List<string> base64Results = new List<string>();

        // Generate a QR code for each text entry and store the Base64 string
        foreach (string text in requestTexts)
        {
            string base64 = GenerateQrBase64(text);
            base64Results.Add(base64);
        }

        // Output the Base64 strings to the console (e.g., for API response verification)
        for (int i = 0; i < base64Results.Count; i++)
        {
            Console.WriteLine($"QR {i + 1} Base64: {base64Results[i]}");
        }
    }

    /// <summary>
    /// Generates a QR Code barcode for the specified text and returns the image as a Base64‑encoded PNG string.
    /// </summary>
    /// <param name="codeText">The text to encode in the QR Code.</param>
    /// <returns>Base64 string representing the QR Code image in PNG format.</returns>
    private static string GenerateQrBase64(string codeText)
    {
        // Create QR code generator with the provided text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set error correction level to improve readability under damage
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save barcode image to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                // Convert the image bytes to a Base64 string for easy transport
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}