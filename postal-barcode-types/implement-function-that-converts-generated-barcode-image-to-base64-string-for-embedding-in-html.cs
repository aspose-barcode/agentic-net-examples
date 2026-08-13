// Title: Generate a barcode image and convert it to a Base64 string for HTML embedding
// Description: Demonstrates creating a Code128 barcode with Aspose.BarCode, saving it to a memory stream, and converting the image to a Base64 string that can be embedded directly in an HTML img tag.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to produce barcode images in various formats. Typical use cases include generating barcodes for web pages, emails, or reports where embedding the image as a Base64 string avoids separate file handling. Developers often need to convert generated images to Base64 for seamless HTML integration, and this snippet shows the standard workflow using MemoryStream and Convert.ToBase64String.
// Prompt: Implement a function that converts a generated barcode image to a Base64 string for embedding in HTML.
// Tags: barcode, code128, base64, html, image, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeBase64Example
{
    /// <summary>
    /// Demonstrates barcode generation and conversion to a Base64 string for HTML embedding.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates a Code128 barcode, converts it to Base64, and outputs an HTML img tag.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Define the barcode text and symbology
            string codeText = "1234567890";
            BaseEncodeType encodeType = EncodeTypes.Code128;

            // Initialize the barcode generator with the chosen type and text
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Prepare a memory stream to hold the generated PNG image
                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the barcode image into the memory stream in PNG format
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Retrieve the raw image bytes from the stream
                    byte[] imageBytes = ms.ToArray();

                    // Convert the image bytes to a Base64-encoded string
                    string base64 = Convert.ToBase64String(imageBytes);

                    // Build an HTML <img> tag that embeds the Base64 string
                    string htmlImg = $"<img src=\"data:image/png;base64,{base64}\" alt=\"barcode\" />";

                    // Output the HTML markup to the console
                    Console.WriteLine(htmlImg);
                }
            }
        }
    }
}