// Title: Generate QR Code and Save as PNG in ASP.NET Core
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, configuring error correction, and saving the image to a file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical scenarios include generating QR codes for URLs, product information, or authentication tokens, where developers need to control image resolution and error correction level and output the result in common image formats such as PNG.
// Prompt: Generate QR Code barcode and write image directly to HTTP response stream in ASP.NET Core.
// Tags: qr code, barcode generation, aspnet core, png, aspose.barcode, encode types, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode, configures its parameters,
/// and saves the resulting PNG image to the local file system.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code.
        string qrText = "https://example.com";

        // Initialize the barcode generator with QR symbology and the text to encode.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set high error correction level (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the image resolution (dots per inch).
            generator.Parameters.Resolution = 300f;

            // Create a memory stream to hold the generated image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position to the beginning for subsequent reads.
                ms.Position = 0;

                // Output the size of the generated image for diagnostic purposes.
                Console.WriteLine($"Generated QR code PNG, {ms.Length} bytes.");

                // Determine the full path for the output file in the current directory.
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

                // Write the image bytes from the memory stream to the file system.
                File.WriteAllBytes(outputPath, ms.ToArray());

                // Confirm that the file has been saved.
                Console.WriteLine($"Saved QR code image to {outputPath}");
            }
        }
    }
}