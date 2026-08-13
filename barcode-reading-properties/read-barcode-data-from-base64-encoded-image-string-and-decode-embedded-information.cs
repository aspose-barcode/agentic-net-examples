// Title: Decode Barcode from Base64 Image
// Description: Demonstrates generating a Code128 barcode, converting it to a Base64 string, and decoding the embedded information from the image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, converting images to Base64, and BarCodeReader for extracting data from any supported symbology. Developers often need to exchange barcode images as text (e.g., JSON payloads) and later decode them without persisting files.
// Prompt: Read barcode data from a base64‑encoded image string and decode the embedded information.
// Tags: code128, decode, png, aspose.barcode, generation, recognition, base64, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode, encodes it as Base64, and then decodes the barcode data from the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, converts it to a Base64 string, and reads the barcode back from the image data.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image and obtain its Base64 representation
        string base64Image;
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "HelloWorld"))
        {
            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the image bytes to a Base64 string
                base64Image = Convert.ToBase64String(ms.ToArray());
            }
        }

        // Decode the Base64 string back to image bytes
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (var imageStream = new MemoryStream(imageBytes))
        {
            // Create a BarCodeReader to recognize any supported barcode type
            using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their type and decoded text
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}