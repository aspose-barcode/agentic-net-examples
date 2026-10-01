// Title: Generate and Decode a Base64‑encoded Code128 barcode with DetectEncoding
// Description: This example creates a Code128 barcode, converts it to a Base64 string, then decodes it back while automatically detecting character encoding.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs for handling barcode images in Base64 format. Shows use of BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and the DetectEncoding setting—common tasks for backend services that need to process barcode data received as text payloads.
// Prompt: Develop a backend service that receives base64‑encoded barcode images, decodes them with DetectEncoding enabled, and returns decoded text.
// Tags: barcode, base64, decode, detectencoding, code128, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, encodes it to Base64,
/// and decodes it back using Aspose.BarCode with automatic encoding detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point: generates a barcode, converts to Base64, decodes it, and writes results to console.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image (Code128) and obtain its Base64 representation.
        string originalText = "HelloWorld";
        string base64Image = GenerateBarcodeBase64(originalText);

        // Decode the barcode from the Base64 string with DetectEncoding enabled.
        string decodedText = DecodeBarcodeFromBase64(base64Image);

        // Output the results.
        Console.WriteLine($"Original text: {originalText}");
        Console.WriteLine($"Decoded text : {decodedText}");
    }

    // Generates a barcode image, saves it to a MemoryStream, and returns the Base64 string.
    private static string GenerateBarcodeBase64(string codeText)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the image bytes to a Base64 string.
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    // Decodes a barcode image provided as a Base64 string.
    private static string DecodeBarcodeFromBase64(string base64Image)
    {
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (var ms = new MemoryStream(imageBytes))
        {
            // Use AllSupportedTypes to detect any barcode symbology.
            using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
            {
                // Enable automatic detection of encoding (e.g., UTF-8, Unicode).
                reader.BarcodeSettings.DetectEncoding = true;

                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    // Return the first decoded text found.
                    return result.CodeText;
                }
            }
        }

        // If no barcode was detected, return an empty string.
        return string.Empty;
    }
}