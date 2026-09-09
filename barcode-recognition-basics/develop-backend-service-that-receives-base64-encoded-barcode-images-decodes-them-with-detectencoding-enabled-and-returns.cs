// Title: Base64 QR Code Generation and Decoding with DetectEncoding
// Description: Demonstrates generating a QR code, converting it to a Base64 string, and decoding it back while enabling automatic encoding detection.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes, BarCodeReader for recognizing barcodes from image streams, and the DetectEncoding setting to automatically handle different character encodings. Developers building services that process barcode images—such as mobile scanning back‑ends or document automation pipelines—can use these APIs to encode data, transmit images as Base64, and reliably extract the original text.
// Prompt: Develop a backend service that receives base64‑encoded barcode images, decodes them with DetectEncoding enabled, and returns decoded text.
// Tags: qr,barcode,generation,recognition,base64,detectencoding,aspose.barcode,csharp

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, encodes it as Base64, and then decodes it
/// using Aspose.BarCode with automatic encoding detection enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, converts it to Base64,
    /// decodes it back, and writes the original and decoded text to the console.
    /// </summary>
    static void Main()
    {
        // Sample text to encode (includes Arabic characters to demonstrate encoding detection)
        string originalText = "بالقمة Aspose";

        // Generate a QR code image and obtain its Base64 representation
        string base64Image = GenerateBarcodeBase64(originalText);

        // Decode the Base64‑encoded barcode image with DetectEncoding enabled
        string decodedText = DecodeBase64Barcode(base64Image);

        // Output the results
        Console.WriteLine($"Original Text: {originalText}");
        Console.WriteLine($"Decoded Text : {decodedText ?? "No barcode detected"}");
    }

    /// <summary>
    /// Generates a QR code for the specified text, saves it to a memory stream as PNG,
    /// and returns the image as a Base64‑encoded string.
    /// </summary>
    /// <param name="text">The text to encode into the QR code.</param>
    /// <returns>Base64 string representing the generated QR code image.</returns>
    static string GenerateBarcodeBase64(string text)
    {
        // Use a memory stream to avoid writing to disk
        using (var ms = new MemoryStream())
        {
            // Create a QR code generator with the provided text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set the module size (pixel dimension) for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode image to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the image bytes to a Base64 string
            byte[] imageBytes = ms.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }

    /// <summary>
    /// Decodes a Base64‑encoded barcode image, enabling automatic encoding detection,
    /// and returns the decoded text if a barcode is found.
    /// </summary>
    /// <param name="base64">Base64 string representing the barcode image.</param>
    /// <returns>Decoded text from the barcode, or null if no barcode is detected.</returns>
    static string DecodeBase64Barcode(string base64)
    {
        // Convert the Base64 string back to raw image bytes
        byte[] imageBytes = Convert.FromBase64String(base64);

        // Load the image bytes into a memory stream for reading
        using (var ms = new MemoryStream(imageBytes))
        {
            // Specify that we expect a QR code
            BaseDecodeType decodeType = DecodeType.QR;

            // Initialize the barcode reader with the image stream and expected type
            using (var reader = new BarCodeReader(ms, decodeType))
            {
                // Enable automatic detection of the text encoding used in the barcode
                reader.BarcodeSettings.DetectEncoding = true;

                // Read all barcodes found in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Return the first decoded text if any barcode was detected
                if (results != null && results.Length > 0)
                {
                    return results[0].CodeText;
                }
            }
        }

        // No barcode detected
        return null;
    }
}