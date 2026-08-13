// Title: Load and Decode a QR Code Image using BarCodeReader
// Description: Demonstrates loading a saved QR code PNG file, generating one if missing, and decoding it with Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use the BarCodeReader and DecodeType classes to read QR codes from image files. Typical use cases include scanning saved barcode images, validating encoded data, and integrating barcode reading into automated workflows. Developers often need to generate sample barcodes, verify file existence, and extract decoded text using the Aspose.BarCode API.
// Prompt: Load a saved QR Code PNG image into BarCodeReader and set DecodeType to QR for recognition.
// Tags: qr, barcode, decode, read, aspose.barcode, png, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that loads a QR code image and decodes it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample QR code if missing, then reads and prints decoded data.
    /// </summary>
    static void Main()
    {
        // Path to the QR code image file
        const string imagePath = "qr.png";

        // Ensure the QR code image exists; if not, generate a sample QR code
        if (!File.Exists(imagePath))
        {
            // Generate a QR code with sample text and save it as PNG
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated sample QR code image at '{imagePath}'.");
            }
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        // Create a BarCodeReader for the image, specifying QR as the decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Perform recognition
            var results = reader.ReadBarCodes();

            // Output recognition results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}