// Title: Generate Code128 barcode and output Base64 PNG
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode, converting it to a PNG image, and displaying its Base64 string. This pattern mirrors returning the image from an ASP.NET MVC controller.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes and BarCodeImageFormat to produce barcode images. Developers often need to generate barcodes on the fly for web applications, reports, or inventory systems, and then return them as file responses such as FileResult in ASP.NET MVC.
// Prompt: Integrate barcode generation into an ASP.NET MVC controller and return the image as a FileResult.
// Tags: code128, barcode generation, png, base64, aspnet mvc, fileresult, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point for the console demonstration. Generates a Code128 barcode,
    /// saves it to a memory stream as PNG, converts to Base64, and writes to console.
    /// In an MVC controller the same image bytes would be returned as a FileResult.
    /// </summary>
    static void Main()
    {
        // Define the barcode text and symbology.
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Initialize the barcode generator with the specified type and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Create a memory stream to hold the generated image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);

                // Retrieve the image bytes from the stream.
                byte[] imageBytes = ms.ToArray();

                // Convert the image bytes to a Base64 string for display or transport.
                string base64 = Convert.ToBase64String(imageBytes);

                // Output the Base64 string to the console (for demo purposes).
                Console.WriteLine("Barcode Base64 PNG:");
                Console.WriteLine(base64);
            }
        }
    }
}