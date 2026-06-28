using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, converting it to a Base64-encoded PNG,
/// and embedding it in an HTML snippet.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, encodes it as Base64, builds an HTML email body,
    /// and writes the HTML to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample text "123ABC"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Configure the barcode background to be transparent
            generator.Parameters.BackColor = Color.Transparent;

            // Create a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the PNG byte array to a Base64 string for embedding
                string base64Image = Convert.ToBase64String(ms.ToArray());

                // Construct an HTML snippet that embeds the barcode using a data URI
                string htmlBody = $"<html><body>" +
                                  $"<h2>Generated Barcode</h2>" +
                                  $"<img src=\"data:image/png;base64,{base64Image}\" alt=\"Barcode\" />" +
                                  $"</body></html>";

                // Output the HTML to the console (placeholder for actual email sending)
                Console.WriteLine(htmlBody);
            }
        }
    }
}