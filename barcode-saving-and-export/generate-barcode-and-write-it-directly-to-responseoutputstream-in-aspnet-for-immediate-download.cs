// Title: Generate Code128 barcode and output as Base64 (console demo)
// Description: Demonstrates creating a Code128 barcode image using Aspose.BarCode, converting it to a Base64 string for display. In ASP.NET you would write the image directly to Response.OutputStream for download.
// Prompt: Generate a barcode and write it directly to Response.OutputStream in ASP.NET for immediate download.
// Tags: barcode, code128, generation, png, memorystream, base64, aspose.barcode, asp.net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console application that generates a Code128 barcode, converts it to a Base64 string,
/// and writes the result to the console. In a web scenario the image would be sent
/// directly to the HTTP response stream for immediate download.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // NOTE: The original ASP.NET example cannot use Response.OutputStream in a console app,
        // so we generate the barcode, store it in a memory stream, and output the image as Base64.

        // Create a barcode generator for Code128 with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: customize barcode appearance (foreground and background colors).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Use a memory stream to hold the generated PNG image.
            using (var memory = new MemoryStream())
            {
                // Save the barcode image into the memory stream in PNG format.
                generator.Save(memory, BarCodeImageFormat.Png);

                // Retrieve the raw image bytes from the memory stream.
                byte[] imageBytes = memory.ToArray();

                // Convert the image bytes to a Base64 string for console output.
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64);
            }
        }
    }
}