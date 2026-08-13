// Title: Generate QR Code and embed as Data URL in Blazor
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG Base64 data URL, and shows how the URL can be used in a Blazor component.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image handling. It showcases the BarcodeGenerator class, QR error correction settings, and Aspose.Drawing bitmap manipulation to produce a data URL. Developers building web UI components, such as Blazor, often need to embed barcodes directly in HTML without saving files, making this pattern a common solution.
// Prompt: Generate QR Code barcode and embed it into a Blazor component as data URL.
// Tags: qr code, barcode generation, data url, base64, aspose.barcode, aspose.drawing, blazor, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and outputs it as a Base64 data URL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR Code, converts it to PNG, encodes to Base64, and prints the data URL.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code.
        string codeText = "https://example.com";

        // Initialize the barcode generator for QR Code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: configure the QR Code error correction level.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the QR Code as a bitmap image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Encode the bitmap to PNG and store it in a memory stream.
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert the PNG bytes to a Base64-encoded data URL.
                    string base64 = Convert.ToBase64String(imageBytes);
                    string dataUrl = $"data:image/png;base64,{base64}";

                    // Output the data URL; in Blazor this string can be assigned to an <img> src attribute.
                    Console.WriteLine("QR Code Data URL:");
                    Console.WriteLine(dataUrl);
                }
            }
        }

        // Note: In a real Blazor component you would bind the dataUrl string to the src attribute of an <img> tag.
    }
}