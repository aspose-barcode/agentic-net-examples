// Title: Generate QR Code and embed as Base64 image in ASP.NET MVC view
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to PNG, encoding it to Base64, and outputting an HTML <img> tag suitable for embedding in an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code images. Typical use cases include rendering barcodes directly in web applications, emails, or reports without writing files to disk. Developers often need to convert generated images to Base64 strings for seamless integration into HTML markup, especially in ASP.NET MVC or Razor views.
// Prompt: Generate QR Code barcode and embed it into an ASP.NET MVC view as base64 image source.
// Tags: qr code, barcode generation, base64, asp.net mvc, png, aspose.barcode, image encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode, converts it to a Base64‑encoded PNG,
/// and writes an HTML <img> tag that can be placed directly in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code and outputs the HTML markup.
    /// </summary>
    static void Main()
    {
        // Text to encode in the QR Code
        string codeText = "Hello Aspose QR";

        // Initialize the barcode generator for QR Code symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: adjust module size (pixel dimension) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Optional: set error correction level (LevelM provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Use a memory stream to avoid writing a temporary file to disk
            using (var ms = new MemoryStream())
            {
                // Render the barcode image into the stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Convert the PNG byte array to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Write an HTML <img> tag with a data URI containing the Base64 image
                Console.WriteLine("<img src=\"data:image/png;base64," + base64 + "\" alt=\"QR Code\" />");
            }
        }
    }
}