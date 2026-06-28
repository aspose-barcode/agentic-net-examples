using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode,
/// converting it to a Base64 string, and outputting basic information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, stores it in a memory stream, and prints its size and a Base64 preview.
    /// </summary>
    static void Main()
    {
        // Sample QR code text to encode
        string qrText = "Hello, World!";

        // Create a memory stream to hold the generated barcode image
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator for a QR code with the sample text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Optional: set a high error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated QR code image directly into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning for subsequent reads
            memoryStream.Position = 0;

            // Convert the image bytes from the memory stream to a Base64 string (useful for API payloads)
            byte[] imageBytes = memoryStream.ToArray();
            string base64Image = Convert.ToBase64String(imageBytes);

            // Output the size of the generated image in bytes
            Console.WriteLine($"Generated QR code image size: {imageBytes.Length} bytes");

            // Output the first 100 characters of the Base64 string as a preview
            Console.WriteLine($"Base64 representation (first 100 chars): {base64Image.Substring(0, Math.Min(100, base64Image.Length))}...");
        }
    }
}