// Title: Barcode to Base64 Converter Example
// Description: Demonstrates generating a barcode image with Aspose.BarCode and converting it to a Base64 string for embedding directly in HTML.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcode graphics. Typical use cases include embedding barcodes in web pages, emails, or reports without storing physical image files. Developers often need to convert generated images to Base64 for inline HTML or JSON payloads.
// Prompt: Create a utility that converts generated barcode images to Base64 strings for embedding in HTML.
// Tags: barcode, code128, base64, html, image, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeBase64Utility
{
    /// <summary>
    /// Provides a console utility that generates a barcode and returns its Base64 representation for HTML embedding.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Generates a Code128 barcode, converts it to Base64, and prints an HTML <img> tag.
        /// </summary>
        static void Main()
        {
            // Define the text to encode and the barcode symbology.
            string codeText = "Sample123";
            BaseEncodeType encodeType = EncodeTypes.Code128;

            // Generate the barcode image and obtain its Base64 string.
            string base64 = GenerateBarcodeBase64(encodeType, codeText);

            // Build an HTML <img> tag that embeds the Base64 image data.
            string htmlImgTag = $"<img src=\"data:image/png;base64,{base64}\" alt=\"Barcode\"/>";

            // Output the HTML tag to the console.
            Console.WriteLine(htmlImgTag);
        }

        /// <summary>
        /// Generates a barcode image using the specified encoding type and text, then returns the image as a Base64 string.
        /// </summary>
        /// <param name="encodeType">The barcode symbology to use (e.g., Code128).</param>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>Base64-encoded PNG image data.</returns>
        static string GenerateBarcodeBase64(BaseEncodeType encodeType, string codeText)
        {
            // Create a BarcodeGenerator with the desired symbology and data.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save the generated barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Convert the memory stream to a byte array and then to a Base64 string.
                    byte[] imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
    }
}