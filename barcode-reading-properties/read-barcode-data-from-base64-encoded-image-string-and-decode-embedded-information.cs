// Title: Decode QR Code from Base64 Image String
// Description: Demonstrates generating a QR code, converting it to a Base64 string, and then decoding the barcode data from that string.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code, BarCodeImageFormat for image handling, and BarCodeReader for decoding. Typical scenarios include processing barcode images received as Base64 strings from web services or APIs, where developers need to extract embedded information without persisting files.
// Prompt: Read barcode data from a base64‑encoded image string and decode the embedded information.
// Tags: qr, barcode, decode, base64, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, encodes it as Base64, and decodes it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample QR code, prints its Base64 representation, and decodes it.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR code and obtain its Base64 representation
        string base64Image = GenerateSampleBarcodeBase64();

        // Output the Base64 string to the console
        Console.WriteLine("Base64 Image:");
        Console.WriteLine(base64Image);
        Console.WriteLine();

        // Decode the barcode from the Base64 string
        DecodeBarcodeFromBase64(base64Image);
    }

    // Generates a QR code with text "Hello, World!" and returns the image as a Base64 string
    private static string GenerateSampleBarcodeBase64()
    {
        // Create a memory stream to hold the barcode image
        using (var ms = new MemoryStream())
        {
            // Create a QR code generator with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, World!"))
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the image bytes to a Base64 string
            byte[] imageBytes = ms.ToArray();
            return Convert.ToBase64String(imageBytes);
        }
    }

    // Decodes barcode data from a Base64‑encoded image string
    private static void DecodeBarcodeFromBase64(string base64Image)
    {
        // Convert Base64 string back to byte array
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Load the image bytes into a memory stream
        using (var ms = new MemoryStream(imageBytes))
        {
            // Specify that all supported barcode types should be detected
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

            // Create a barcode reader for the image stream
            using (var reader = new BarCodeReader(ms, decodeType))
            {
                // Perform the reading operation
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the image.");
                }
                else
                {
                    // Output each detected barcode's type and decoded text
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}