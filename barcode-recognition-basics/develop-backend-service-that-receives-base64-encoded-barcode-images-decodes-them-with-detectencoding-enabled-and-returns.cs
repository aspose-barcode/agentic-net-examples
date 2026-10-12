// Title: Decode Base64 QR Barcode with DetectEncoding
// Description: Demonstrates generating a QR barcode, converting it to a Base64 string, then decoding it back and reading the barcode text with encoding detection enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a QR code, how to serialize the image to Base64, and how to employ BarCodeReader with DetectEncoding to correctly decode Unicode text. Developers building backend services that process barcode images received as Base64 strings can use this pattern for QR, DataMatrix, and other symbologies.
// Prompt: Develop a backend service that receives base64‑encoded barcode images, decodes them with DetectEncoding enabled, and returns decoded text.
// Tags: qr, barcode, base64, decode, detectencoding, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, encodes it to Base64,
/// then decodes the Base64 string back to an image and reads the barcode
/// with DetectEncoding enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, Base64 conversion,
    /// and barcode recognition with encoding detection.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a QR barcode containing Unicode text.
        // ------------------------------------------------------------
        string originalText = "مرحبا Aspose";
        string base64Image;

        using (var generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            // Set the module size (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG byte array to a Base64 string.
                base64Image = Convert.ToBase64String(imageBytes);
            }
        }

        // Output the Base64 representation (simulating a response payload).
        Console.WriteLine("Generated Base64 Barcode Image:");
        Console.WriteLine(base64Image);
        Console.WriteLine();

        // ------------------------------------------------------------
        // 2. Decode the Base64 string back to an image and read the barcode.
        // ------------------------------------------------------------
        byte[] decodedBytes = Convert.FromBase64String(base64Image);

        using (var imageStream = new MemoryStream(decodedBytes))
        {
            // Specify the expected barcode type for faster detection.
            BaseDecodeType decodeType = DecodeType.QR;

            using (var reader = new BarCodeReader(imageStream, decodeType))
            {
                // Enable automatic detection of the text encoding (important for Unicode).
                reader.BarcodeSettings.DetectEncoding = true;

                // Iterate through all detected barcodes (normally one for this example).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
    }
}