// Title: Generate Code128 barcode and save to temporary file (ASP.NET stream example)
// Description: Demonstrates creating a Code128 barcode using Aspose.BarCode and saving it to a file, which can be streamed directly to an HTTP response for immediate download.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes to produce barcodes. Typical use cases include generating barcodes on-the-fly in web applications and sending the image directly to the client via Response.OutputStream. Developers often need to create a barcode, choose an image format, and write the output stream without intermediate files.
// Prompt: Generate a barcode and write it directly to Response.OutputStream in ASP.NET for immediate download.
// Tags: code128, barcode generation, asp.net, response outputstream, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves it to a temporary file, and notes how it could be streamed to an ASP.NET response.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "12345678";

        // Determine a temporary file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Create a BarcodeGenerator for Code128 symbology and encode the data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Open a file stream to write the barcode image.
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Save the barcode image to the stream in PNG format.
                generator.Save(fileStream, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode generated and saved to: {outputPath}");

        // Note: In an ASP.NET application, the same stream would be written to Response.OutputStream for immediate download.
    }
}