// Title: Generate QR Code and Convert to Base64 for ASP.NET MVC
// Description: This example creates a QR Code barcode, saves it as a PNG in memory, and converts the image to a Base64 string that can be embedded directly in an ASP.NET MVC view.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for QR symbology, focusing on in‑memory image handling and Base64 encoding. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for web scenarios where the barcode image is rendered on the client without writing files. Developers often need to embed barcodes in HTML or MVC views, requiring a data URI format.
// Prompt: Generate QR Code barcode and embed it into an ASP.NET MVC view as base64 image source.
// Tags: qr code, barcode generation, base64, asp.net mvc, aspose.barcode, png, in-memory

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and converting it to a Base64 string for embedding in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the QR Code, encodes it as PNG, and outputs the Base64 string.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the QR Code.
        string codeText = "https://example.com";

        // Variable to hold the resulting Base64 string.
        string base64Image;

        // Use a memory stream to avoid writing a temporary file to disk.
        using (var ms = new MemoryStream())
        {
            // Initialize the barcode generator with QR symbology and the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Optional: set the QR error correction level to improve readability.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading its contents.
            ms.Position = 0;

            // Convert the stream's bytes to a Base64 string.
            byte[] imageBytes = ms.ToArray();
            base64Image = Convert.ToBase64String(imageBytes);
        }

        // Output the Base64 string; in an MVC view it can be used as:
        // <img src="data:image/png;base64,{base64Image}" />
        Console.WriteLine(base64Image);
    }
}