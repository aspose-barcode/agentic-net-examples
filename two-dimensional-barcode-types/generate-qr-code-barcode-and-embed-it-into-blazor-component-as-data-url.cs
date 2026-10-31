// Title: Generate QR Code and embed as Data URL in Blazor
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG Base64 data URL, and outputting the string for use in a Blazor component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image export. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat for rendering barcodes as images. Developers commonly use these APIs to generate barcodes on the fly and embed them in web UI frameworks like Blazor via data URLs.
// Prompt: Generate QR Code barcode and embed it into a Blazor component as data URL.
// Tags: qr code, barcode generation, data url, base64, aspnet blazor, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and converting it to a Base64 data URL for embedding in a Blazor component.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, saves it as PNG in memory, and prints the data URL.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string codeText = "Hello Aspose QR!";

        // Initialize the QR code generator with the desired symbology and text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: adjust the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Optional: set the error correction level (LevelM provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Render the barcode to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the PNG bytes to a Base64 string.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build the data URL that can be used directly in an <img> tag in Blazor.
                string dataUrl = $"data:image/png;base64,{base64}";

                // Output the data URL to the console (or capture it for further use).
                Console.WriteLine(dataUrl);
            }
        }
    }
}