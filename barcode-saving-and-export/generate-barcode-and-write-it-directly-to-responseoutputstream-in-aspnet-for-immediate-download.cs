// Title: Generate Code128 barcode and stream it for immediate download in ASP.NET
// Description: Demonstrates creating a Code128 barcode image using Aspose.BarCode, storing it in a memory stream, and showing how to write it directly to the ASP.NET Response.OutputStream for download.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include generating barcodes on-the-fly for web applications, e‑commerce receipts, or inventory systems where the barcode image must be sent directly to the client without intermediate files. Developers often need to customize barcode dimensions and output formats before streaming the result.
// Prompt: Generate a barcode and write it directly to Response.OutputStream in ASP.NET for immediate download.
// Tags: barcode, code128, aspnet, streaming, response, outputstream, png, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and streaming for ASP.NET download.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves it to a memory stream, and (in a real ASP.NET scenario) would write it to Response.OutputStream.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        string codeText = "12345678";

        // Use a memory stream to hold the generated image (simulating Response.OutputStream).
        using (var memoryStream = new MemoryStream())
        {
            // Create a BarcodeGenerator for Code128 symbology with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: customize the barcode's X dimension (module width) for better readability.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the barcode image as PNG into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // In an ASP.NET context you would write the image directly to the response:
            // Response.ContentType = "image/png";
            // Response.AddHeader("Content-Disposition", "attachment; filename=barcode.png");
            // memoryStream.CopyTo(Response.OutputStream);
            // Response.End();

            // For this console demonstration, write the image to a file on disk.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(fileStream);
            }

            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}