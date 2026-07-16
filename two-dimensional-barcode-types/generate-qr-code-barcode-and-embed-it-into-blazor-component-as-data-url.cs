// Title: Generate QR Code and embed as Data URL in Blazor
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a Base64 data URL, and using it in a Blazor component's image source.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR Code creation, error correction configuration, and image export. It showcases the BarcodeGenerator, QRErrorLevel, and BarCodeImageFormat classes, typical for developers needing to render barcodes as web-friendly data URLs for UI frameworks like Blazor.
// Prompt: Generate QR Code barcode and embed it into a Blazor component as data URL.
// Tags: qr code, barcode generation, data url, blazor, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode, converts it to a Base64 data URL,
/// and writes the URL to the console for use in a Blazor component.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and outputs the data URL.
    /// </summary>
    static void Main()
    {
        // Create a QR code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Configure the QR code to use the highest error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64-encoded data URL.
                string base64 = Convert.ToBase64String(imageBytes);
                string dataUrl = $"data:image/png;base64,{base64}";

                // Write the data URL to the console; it can be used as the src attribute in a Blazor <img> tag.
                Console.WriteLine(dataUrl);
            }
        }
    }
}