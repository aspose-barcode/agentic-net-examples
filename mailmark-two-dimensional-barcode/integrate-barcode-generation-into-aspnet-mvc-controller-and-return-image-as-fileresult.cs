// Title: Generate Code128 Barcode and Return as FileResult in ASP.NET MVC
// Description: This example creates a Code128 barcode image with Aspose.BarCode, writes it to a MemoryStream, and demonstrates how the stream could be returned from an MVC controller as a FileResult.
// Category-Description: Aspose.BarCode generation examples illustrate how to produce various barcode symbologies using the BarcodeGenerator class. Typical scenarios include creating printable labels, receipts, or embedding barcodes in web applications. Developers often need to customize colors, image formats, and return the generated image directly from ASP.NET MVC actions using FileResult.
// Prompt: Integrate barcode generation into an ASP.NET MVC controller and return the image as a FileResult.
// Tags: code128, barcode generation, aspnet mvc, filereturn, png, aspose.barcode, image output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode suitable for returning from an ASP.NET MVC controller.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a Code128 barcode, writes it to a memory stream, and outputs image size and Base64 representation (simulating MVC FileResult).
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        const string codeText = "12345678";

        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Customize barcode appearance.
            generator.Parameters.Barcode.BarColor = Color.Black;   // Set barcode bars to black.
            generator.Parameters.BackColor = Color.White;          // Set background to white.

            // Create a memory stream to hold the generated image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Output image size for diagnostic purposes.
                Console.WriteLine($"Generated barcode image size: {ms.Length} bytes");

                // Convert the image to Base64 (useful for embedding in HTML or JSON).
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("Base64 PNG:");
                Console.WriteLine(base64);

                // In an ASP.NET MVC action you would return:
                // return File(ms, "image/png", "barcode.png");
            }
        }
    }
}