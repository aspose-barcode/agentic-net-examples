// Title: Generate Code128 Barcode and Return as Image File
// Description: Demonstrates generating a Code128 barcode image using Aspose.BarCode and saving it to a temporary file, which can be returned from an ASP.NET MVC controller as a FileResult.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to encode data, specify symbology, and export the result to common image formats such as PNG. Developers creating web applications often need to produce barcodes on‑the‑fly for invoices, shipping labels, or inventory tracking, and typically return the image directly from a controller action using FileResult.
// Prompt: Integrate barcode generation into an ASP.NET MVC controller and return the image as a FileResult.
// Tags: code128, barcode generation, image output, aspnet mvc, fileresult, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Simple console program that demonstrates the core barcode generation logic
/// which can be reused inside an ASP.NET MVC controller.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, saves it as a PNG file,
    /// and writes the file location to the console.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "12345678";

        // Choose the barcode symbology (Code128) using the EncodeTypes enum.
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Build a unique temporary file path for the generated PNG image.
        string tempPath = Path.Combine(
            Path.GetTempPath(),
            "barcode_" + Guid.NewGuid().ToString("N") + ".png");

        // Create a BarcodeGenerator instance with the selected type and data.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Save the generated barcode image to the temporary file in PNG format.
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated image (useful for debugging).
        Console.WriteLine("Barcode image generated at: " + tempPath);

        // In an ASP.NET MVC controller the return would be:
        // return File(tempPath, "image/png");
    }
}