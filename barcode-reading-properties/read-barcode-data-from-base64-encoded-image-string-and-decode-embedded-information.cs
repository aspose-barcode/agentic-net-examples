// Title: Decode QR Code from Base64 Image String
// Description: Demonstrates generating a QR code, converting it to a Base64 string, then decoding the string back to an image and reading the embedded barcode data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a QR code, how to serialize the image to Base64, and how to employ BarCodeReader with DecodeType to extract the encoded information. Developers working with QR codes, image transport, or data exchange often need to encode barcodes to text formats and later decode them without persisting files.
// Prompt: Read barcode data from a base64‑encoded image string and decode the embedded information.
// Tags: qr, barcode, base64, decode, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code, encodes it as a Base64 string,
/// then decodes the string back to an image and reads the barcode data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs QR code generation, Base64 conversion,
    /// and barcode decoding using Aspose.BarCode APIs.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a QR code and obtain its Base64 representation
        // ------------------------------------------------------------
        string base64Image;
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, World!"))
        {
            // Set the module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Save the generated QR code to a memory stream in PNG format
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the PNG byte array to a Base64 string
                base64Image = Convert.ToBase64String(ms.ToArray());
            }
        }

        // Output the Base64 string (optional, for demonstration)
        Console.WriteLine("Base64 Image:");
        Console.WriteLine(base64Image);
        Console.WriteLine();

        // ------------------------------------------------------------
        // Decode the Base64 string back to an image and read the barcode
        // ------------------------------------------------------------
        // Convert the Base64 string back to a byte array representing the image
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream decodeStream = new MemoryStream(imageBytes))
        {
            // Specify that we expect a QR code during decoding
            BaseDecodeType decodeType = DecodeType.QR;

            // Initialize the barcode reader with the image stream and decode type
            using (BarCodeReader reader = new BarCodeReader(decodeStream, decodeType))
            {
                // Iterate through all detected barcodes (should be one in this case)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                }
            }
        }
    }
}