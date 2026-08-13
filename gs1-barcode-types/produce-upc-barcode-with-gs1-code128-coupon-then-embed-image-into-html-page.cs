// Title: Generate UPC‑A barcode with GS1 Code128 coupon and embed in HTML
// Description: Demonstrates creating a UPC‑A barcode that includes a GS1 Code128 coupon, saving it as a PNG image, and embedding the image directly into an HTML page using a data URI.
// Category-Description: This example belongs to the Aspose.BarCode generation and rendering category, showcasing how to use the BarcodeGenerator class with composite symbologies (UPC‑A with GS1 Code128 coupon) to produce barcode images. Typical use cases include retail product labeling and coupon integration where a single barcode encodes both product and promotional data. Developers often need to render barcodes to image formats and embed them in web pages or documents, which this sample illustrates.
// Prompt: Produce a UPC‑A barcode with a GS1 Code128 coupon, then embed the image into an HTML page.
// Tags: upc-a, code128, gs1, coupon, barcode, image, html, aspose.barcode, generation, data-uri

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A barcode with a GS1 Code128 coupon and embedding it in an HTML page.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it as PNG, creates a Base64 data URI,
    /// and writes an HTML file that displays the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the barcode text that includes the UPC‑A data and the GS1 Code128 coupon segment.
        const string codeText = "514141100906(8102)03";

        // File names for the generated PNG image and the resulting HTML page.
        const string imageFile = "barcode.png";
        const string htmlFile = "barcode.html";

        // Variable to hold the Base64 representation of the PNG image for embedding.
        string base64Image;

        // Create a BarcodeGenerator for the composite symbology (UPC‑A with GS1 Code128 coupon).
        using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, codeText))
        {
            // Use a memory stream to capture the generated PNG without writing to disk first.
            using (var ms = new MemoryStream())
            {
                // Render the barcode into the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Persist the PNG image to a file for external use or inspection.
                using (var fileStream = new FileStream(imageFile, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileStream);
                }

                // Convert the PNG bytes to a Base64 string for embedding in an HTML data URI.
                base64Image = Convert.ToBase64String(ms.ToArray());
            }
        }

        // Build a simple HTML document that displays the barcode using a data URI.
        using (var writer = new StreamWriter(htmlFile, false))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head><meta charset=\"UTF-8\"><title>Barcode Example</title></head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<h1>UPC‑A with GS1 Code128 Coupon</h1>");
            writer.WriteLine($"<img src=\"data:image/png;base64,{base64Image}\" alt=\"Barcode\"/>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        // Output the locations of the generated files for user reference.
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(imageFile)}");
        Console.WriteLine($"HTML page saved to: {Path.GetFullPath(htmlFile)}");
    }
}