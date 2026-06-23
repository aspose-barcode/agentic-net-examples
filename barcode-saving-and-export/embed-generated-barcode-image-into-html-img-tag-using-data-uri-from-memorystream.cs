using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to a Base64-encoded PNG,
/// and embedding it in an HTML img tag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, encodes it, and writes an HTML img element to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Create a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning before reading its contents
                ms.Position = 0;

                // Convert the image bytes from the memory stream to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build an HTML <img> tag that embeds the Base64 PNG using a data URI
                string html = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\" />";

                // Write the HTML string to the console output
                Console.WriteLine(html);
            }
        }
    }
}