// Title: Generate QR Code and embed as Data URL in Blazor
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG Base64 data URL, and outputting the string for use in a Blazor component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image handling. It showcases the BarcodeGenerator class, QR-specific parameters, and image export via BarCodeImageFormat. Developers often need to embed generated barcodes directly into web UI frameworks like Blazor, requiring conversion to a Base64 data URL for seamless client‑side rendering.
// Prompt: Generate QR Code barcode and embed it into a Blazor component as data URL.
// Tags: qr code, barcode generation, data url, base64, aspnet blazor, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation and conversion to a Base64 data URL for embedding in Blazor.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code, saves it as PNG in memory, and prints the data URL.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string codeText = "Hello, Aspose QR!";

        // Initialize the barcode generator for QR encoding.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: configure module size (pixel dimension) and error correction level.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG byte array to a Base64-encoded string.
                string base64 = Convert.ToBase64String(imageBytes);
                // Build the data URL that can be used directly in an <img> tag or Blazor component.
                string dataUrl = $"data:image/png;base64,{base64}";

                // Output the data URL to the console (copy/paste into Blazor markup as needed).
                Console.WriteLine(dataUrl);
            }
        }
    }
}