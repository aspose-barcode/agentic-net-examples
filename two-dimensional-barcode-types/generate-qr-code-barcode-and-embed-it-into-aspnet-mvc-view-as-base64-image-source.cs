// Title: Generate QR Code and embed as Base64 image in ASP.NET MVC view
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG image, encoding it to Base64, and showing how to embed the result in an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and image handling. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and image format options, illustrating typical use cases such as generating barcodes for web applications and embedding them directly into HTML via Base64 strings. Developers often need to produce barcodes on the fly without saving files, and this pattern provides a reusable approach.
// Prompt: Generate QR Code barcode and embed it into an ASP.NET MVC view as base64 image source.
// Tags: qr code, barcode generation, base64, asp.net mvc, image png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode, converts it to a Base64 PNG image,
/// and demonstrates how the string can be used in an ASP.NET MVC view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates the QR Code and writes the Base64 string and sample HTML tag to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string codeText = "Hello Aspose QR!";

        // Initialize the QR code generator with the desired symbology and data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: adjust the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Optional: set the error correction level (LevelM provides a good balance).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Create a memory stream to hold the generated PNG image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Save the barcode image directly to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading.
                ms.Position = 0;

                // Convert the image bytes to a Base64 string.
                byte[] imageBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(imageBytes);

                // Output the Base64 string and an example <img> tag for use in a view.
                Console.WriteLine("Base64 Image:");
                Console.WriteLine(base64);
                Console.WriteLine();
                Console.WriteLine("<img src=\"data:image/png;base64," + base64 + "\" alt=\"QR Code\" />");
            }
        }

        // In an actual ASP.NET MVC view, the Base64 string can be assigned to the src attribute of an <img> element.
    }
}