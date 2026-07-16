// Title: Generate QR Code and embed as Base64 in ASP.NET MVC view
// Description: Demonstrates creating a QR Code with Aspose.BarCode, converting it to a PNG Base64 string suitable for embedding in an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image conversion. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce PNG images, then encodes them to Base64 for web display. Developers building web applications often need to render barcodes directly in HTML without saving files, making this pattern common.
// Prompt: Generate QR Code barcode and embed it into an ASP.NET MVC view as base64 image source.
// Tags: qr code, generation, base64, aspnet mvc, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode, converting it to a Base64 PNG string,
/// and outputting the result for embedding in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates the QR Code and writes the Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator with QR symbology and the target data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: configure QR error correction level to improve readability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string for embedding in HTML.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Output the Base64 string; in MVC this can be used as:
                // <img src="data:image/png;base64,{base64}" />
                Console.WriteLine(base64);
            }
        }
    }
}