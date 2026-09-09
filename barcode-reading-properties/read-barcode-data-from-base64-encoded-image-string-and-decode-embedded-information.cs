// Title: Decode QR Code from Base64 Image String
// Description: Generates a QR code, converts it to a Base64 string, then decodes the string back to an image and reads the embedded barcode data.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create a barcode image with BarcodeGenerator, export it as Base64, and then use BarCodeReader to extract the encoded information. Developers working with QR codes, data exchange via text streams, or image serialization will find this pattern useful for encoding and decoding barcodes in web services, APIs, or storage scenarios.
// Prompt: Read barcode data from a base64‑encoded image string and decode the embedded information.
// Tags: qr, barcode, base64, decode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a QR code, convert it to a Base64 string,
/// and then decode the barcode from the Base64 image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs QR code generation, Base64 conversion,
    /// and barcode decoding without requiring interactive console input.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a QR code and obtain its Base64 representation.
        // ------------------------------------------------------------
        string base64Image;
        using (var ms = new MemoryStream())
        {
            // Create a QR code containing the text "Hello Aspose".
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
            {
                // Set the module size (pixel dimension) for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the image bytes to a Base64 string for transport or storage.
            base64Image = Convert.ToBase64String(ms.ToArray());
        }

        // Output the Base64 string (optional, for demonstration purposes).
        Console.WriteLine("Base64 Image:");
        Console.WriteLine(base64Image);
        Console.WriteLine();

        // ------------------------------------------------------------
        // 2. Decode the Base64 string back to an image and read the barcode.
        // ------------------------------------------------------------
        // Convert the Base64 string back to a byte array representing the PNG image.
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Use a memory stream to feed the image data to the barcode reader.
        using (var imageStream = new MemoryStream(imageBytes))
        {
            // Specify that we expect a QR code during decoding.
            BaseDecodeType decodeType = DecodeType.QR;

            // Initialize the barcode reader with the image stream and decode type.
            using (var reader = new BarCodeReader(imageStream, decodeType))
            {
                // Iterate through all detected barcodes (typically one for this example).
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Symbology   : {result.CodeTypeName}");
                }
            }
        }
    }
}