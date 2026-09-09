// Title: Generate QR Code barcode and save as PNG image
// Description: Demonstrates how to create a QR Code using Aspose.BarCode, set resolution and error correction, and save the result as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image export APIs. Developers commonly generate QR Code images for embedding in documents, web pages, or mobile apps, and need to control resolution and error correction levels. The snippet shows typical steps for creating, configuring, and perserving a barcode image.
// Prompt: Generate QR Code barcode and embed it into a Word document using Open XML SDK.
// Tags: qr code, barcode generation, image output, aspose.barcode, openxml sdk, word document

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code
        const string qrText = "https://www.example.com";

        // Desired image resolution (dpi)
        const int resolution = 300;

        // Output file name for the generated PNG image
        const string imagePath = "qr_code.png";

        // ------------------------------------------------------------
        // Generate QR Code barcode using Aspose.BarCode
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set image resolution
            generator.Parameters.Resolution = resolution;

            // Optional: set high error correction level (Level H)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Create the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the Bitmap to a PNG file
                using (var stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(stream, ImageFormat.Png);
                }
            }
        }

        // Inform the user where the image was saved
        Console.WriteLine($"QR Code image saved to '{Path.GetFullPath(imagePath)}'.");

        // ------------------------------------------------------------
        // Note: Embedding the image into a Word document via Open XML SDK
        // is not demonstrated here due to missing API documentation.
        // ------------------------------------------------------------
        Console.WriteLine("Embedding the barcode into a Word document via Open XML SDK is not implemented due to unavailable API documentation.");
    }
}