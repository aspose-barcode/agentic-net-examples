// Title: Generate Code128 Barcode and Output as Base64 String
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode, converting it to PNG, and encoding the image as a Base64 string, which can be returned from an ASP.NET MVC controller as a FileResult.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce barcode images. Typical scenarios include generating barcodes on-the-fly for web applications, embedding them in PDFs, or returning them via API endpoints. Developers often need to render barcodes in common image formats and stream them directly to HTTP responses.
// Prompt: Integrate barcode generation into an ASP.NET MVC controller and return the image as a FileResult.
// Tags: barcode generation, code128, png, base64, aspnet mvc, fileresult, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation logic suitable for an ASP.NET MVC controller.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode, encodes it to PNG, and writes the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // In a real MVC controller this code would be placed inside an action method
        // and the resulting byte array would be returned as a FileResult.

        // Initialize a BarcodeGenerator for the Code128 symbology with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set visual appearance: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Render the barcode to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string.
                // In an MVC action you would return File(imageBytes, "image/png").
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine("Generated barcode image (Base64):");
                Console.WriteLine(base64);
            }
        }

        // Program ends successfully.
    }
}