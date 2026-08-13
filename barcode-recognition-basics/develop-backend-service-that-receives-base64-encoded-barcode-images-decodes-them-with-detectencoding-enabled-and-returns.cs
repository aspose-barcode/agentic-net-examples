// Title: Decode Base64‑Encoded Code128 Barcode with DetectEncoding
// Description: Demonstrates receiving a Base64‑encoded barcode image, decoding it using Aspose.BarCode with DetectEncoding enabled, and outputting the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeReader and BarcodeGenerator for end‑to‑end barcode processing. Typical use cases include backend services that accept barcode images (e.g., from mobile apps) and need to extract encoded information. Developers often need to handle various image formats, enable encoding detection, and process multiple symbologies.
// Prompt: Develop a backend service that receives base64‑encoded barcode images, decodes them with DetectEncoding enabled, and returns decoded text.
// Tags: code128, decode, text, barcodegenerator, barcodereader, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Example program that generates a Code128 barcode, encodes it to Base64,
/// decodes the Base64 string back to an image, and reads the barcode text
/// with encoding detection enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, Base64 conversion,
    /// and barcode recognition with DetectEncoding set to true.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string sampleText = "HelloWorld";

        // Create a barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
        {
            // Store the generated barcode image in a memory stream as PNG.
            using (var imageStream = new MemoryStream())
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string (simulating received data).
                string base64Image = Convert.ToBase64String(imageStream.ToArray());

                // Decode the Base64 string back to raw image bytes.
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (var decodeStream = new MemoryStream(imageBytes))
                {
                    // Initialize a barcode reader that supports all barcode types.
                    using (var reader = new BarCodeReader(decodeStream, DecodeType.AllSupportedTypes))
                    {
                        // Enable automatic detection of character encoding (e.g., UTF‑8).
                        reader.BarcodeSettings.DetectEncoding = true;

                        // Iterate through all detected barcodes and output their decoded text.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine("Decoded Text: " + result.CodeText);
                        }
                    }
                }
            }
        }
    }
}