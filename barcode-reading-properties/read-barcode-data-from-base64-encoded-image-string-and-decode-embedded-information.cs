// Title: Base64 Barcode Generation and Decoding
// Description: Generates a Code128 barcode, encodes it as a Base64 string, then decodes the string back to an image and reads the barcode data.
// Prompt: Read barcode data from a base64‑encoded image string and decode the embedded information.
// Tags: code128, barcode generation, barcode decoding, base64, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a barcode, convert it to a Base64 string,
/// decode the string back to an image, and read the barcode information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, Base64 conversion, and decoding.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode
        string sampleText = "1234567890";

        // Generate a barcode image and obtain its Base64 representation
        string base64Image;
        using (MemoryStream generationStream = new MemoryStream())
        {
            // Create a barcode generator for Code128 with the sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(generationStream, BarCodeImageFormat.Png);
            }

            // Convert the generated image bytes to a Base64 string
            base64Image = Convert.ToBase64String(generationStream.ToArray());
        }

        // Decode the Base64 string back to image bytes
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Read the barcode from the image bytes
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Initialize a barcode reader that supports all barcode types
            using (BarCodeReader reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their type and decoded text
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}