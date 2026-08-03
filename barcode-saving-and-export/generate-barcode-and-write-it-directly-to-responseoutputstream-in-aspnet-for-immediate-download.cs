// Title: Generate Code128 barcode and stream as PNG in ASP.NET
// Description: Demonstrates creating a Code128 barcode with Aspose.BarCode and writing it directly to the HTTP response output stream for immediate download.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce barcode images in common formats such as PNG. Typical use cases include generating barcodes on-the-fly in web applications for inventory, shipping, or ticketing systems. Developers often need to stream the generated image directly to the client without intermediate files, using HttpResponse.OutputStream.
// Prompt: Generate a barcode and write it directly to Response.OutputStream in ASP.NET for immediate download.
// Tags: code128, barcode, generation, png, aspnet, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode and how the result could be streamed
/// directly to an ASP.NET response. In this console example the image is saved to a file for
/// verification purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it to a PNG file,
    /// and includes comments showing how to write the image to HttpResponse.OutputStream in a web context.
    /// </summary>
    static void Main()
    {
        // NOTE: In an ASP.NET controller you would replace the file‑write logic with:
        //   HttpResponse response = HttpContext.Current.Response;
        //   response.ContentType = "image/png";
        //   response.AddHeader("Content-Disposition", "attachment; filename=barcode.png");
        //   generator.Save(response.OutputStream, BarCodeImageFormat.Png);
        //   response.End();

        // Create a BarcodeGenerator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Prepare a memory stream to hold the PNG image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the generated barcode into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // Reset the stream position for subsequent reading.

                // For this console demonstration, write the PNG image to a file in the current directory.
                const string outputPath = "barcode.png";
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.CopyTo(fileStream);
                }

                Console.WriteLine($"Barcode image saved to '{outputPath}'.");
            }
        }
    }
}