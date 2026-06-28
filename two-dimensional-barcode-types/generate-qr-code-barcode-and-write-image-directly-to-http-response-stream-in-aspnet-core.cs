using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode and outputting it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, encodes the image to Base64, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: Full ASP.NET Core integration cannot be demonstrated in this console snippet.
        // The core barcode generation logic is shown below, and the resulting PNG image
        // is output as a Base64 string, which could be written to an HTTP response in a real web app.

        // Create a QR Code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set error correction level to Medium.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode image to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the image bytes to a Base64 string (simulating HTTP response content).
                string base64Image = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64Image);
            }
        }
    }
}