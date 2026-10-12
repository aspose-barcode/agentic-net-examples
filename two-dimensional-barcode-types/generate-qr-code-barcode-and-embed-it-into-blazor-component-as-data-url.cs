// Title: Generate QR Code and embed as Data URL in Blazor
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a Base64‑encoded PNG data URL that can be used directly in a Blazor component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce image output. Typical use cases include rendering barcodes on web pages, mobile apps, or Blazor components without saving files to disk. Developers often need to customize dimensions, error correction levels, and convert the image to a data URL for inline display.
// Prompt: Generate QR Code barcode and embed it into a Blazor component as data URL.
// Tags: qr code, barcode generation, data url, aspose.barcode, png, blazor

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and outputs it as a data URL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, encodes it to PNG, and prints a data URL.
    /// </summary>
    static void Main()
    {
        // Text to be encoded in the QR Code.
        string codeText = "Hello, Aspose QR!";

        // Initialize the barcode generator for QR Code with the specified text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Configure the error correction level (Level M provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to hold the generated PNG image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading.
                ms.Position = 0;

                // Convert the PNG bytes to a Base64 string.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build the data URL that can be used directly in HTML/Blazor.
                string dataUrl = "data:image/png;base64," + base64;

                // Output the data URL to the console (or capture it for further use).
                Console.WriteLine(dataUrl);
            }
        }
    }
}