// Title: Generate DataMatrix Barcode and Embed as Base64 Image Tag in ASP.NET MVC
// Description: Demonstrates creating a DataMatrix barcode with Aspose.BarCode, converting it to a Base64 data URI, and rendering it in an HTML <img> tag suitable for ASP.NET MVC views.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce DataMatrix symbology, customize appearance, and output the result as PNG. Typical use cases include embedding barcodes directly into web pages, emails, or reports without storing physical image files. Developers often need to render barcodes server‑side and deliver them as data URIs for seamless integration into HTML markup.
// Prompt: Integrate DataMatrix generation into ASP.NET MVC view using server‑side rendering and an image tag.
// Tags: datamatrix, barcode, generation, base64, aspnet-mvc, image, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console demonstration of generating a DataMatrix barcode,
/// converting it to a Base64‑encoded PNG, and outputting an HTML <img> tag.
/// In a real ASP.NET MVC application the same logic would be used to embed
/// the barcode directly into a view.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it to a file,
    /// creates a Base64 data URI, and writes the corresponding <img> tag to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the temporary file name.
        const string codeText = "Hello";
        const string outputFile = "datamatrix.png";

        // Initialize the generator for DataMatrix symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Optional: set foreground (barcode) and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Persist the barcode as a PNG file for reference or debugging.
            generator.Save(outputFile, BarCodeImageFormat.Png);

            // Render the barcode to a memory stream to build a Base64 data URI.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(imageBytes);

                // Construct the HTML <img> tag with the data URI.
                string imgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"DataMatrix Barcode\" />";
                Console.WriteLine(imgTag);
            }
        }
    }
}