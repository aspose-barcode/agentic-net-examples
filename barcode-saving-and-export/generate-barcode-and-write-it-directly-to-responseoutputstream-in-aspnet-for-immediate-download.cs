// Title: Generate Code128 Barcode and Save as PNG
// Description: Demonstrates generating a Code128 barcode using Aspose.BarCode, saving it to a PNG image via a memory stream, and writing the bytes to a file (simulating an ASP.NET response).
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to create barcodes, configure parameters, and export images in common formats. Typical use cases include creating product labels, tickets, or any scenario where a barcode image must be generated on‑the‑fly for web or desktop applications. Developers often need to write the generated image directly to an HTTP response stream for immediate download, but this console sample uses a file write to illustrate the process.
// Prompt: Generate a barcode and write it directly to Response.OutputStream in ASP.NET for immediate download.
// Tags: barcode, code128, generation, png, aspnet, memorystream, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode image and writes it to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode, saves it to a memory stream as PNG, and writes the image bytes to a file.
    /// In an ASP.NET context the same bytes would be written to Response.OutputStream for download.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology.
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Initialize the barcode generator with the chosen symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: customize barcode appearance, e.g., set the font size for the code text.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Create a memory stream to hold the generated image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position to the beginning.

                // Write the image bytes to a file (simulating a download response).
                File.WriteAllBytes("barcode.png", ms.ToArray());
                Console.WriteLine("Barcode image generated and saved as 'barcode.png'.");
            }
        }
    }
}