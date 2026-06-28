using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image and outputting it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code for a sample URL, encodes the image to Base64, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Sample QR code text (the data to encode in the QR code)
        string codeText = "https://example.com";

        // Initialize a QR Code generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the QR code error correction level to high (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Create a memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the QR code image into the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Retrieve the image bytes from the memory stream
                byte[] imageBytes = memoryStream.ToArray();

                // Convert the image bytes to a Base64 string (simulating a web response)
                string base64Image = Convert.ToBase64String(imageBytes);

                // Output the Base64 string to the console
                Console.WriteLine(base64Image);
            }
        }
    }
}