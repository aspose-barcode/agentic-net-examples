// Title: Convert generated barcode image to Base64 for HTML embedding
// Description: Demonstrates generating a Code128 barcode with Aspose.BarCode, converting the PNG image to a Base64 string, and outputting an HTML <img> tag that embeds the image data.
// Category-Description: This example belongs to the Aspose.BarCode image generation and encoding category. It showcases the use of BarcodeGenerator to create barcode images, Aspose.Drawing.Bitmap for image handling, and conversion of the image to a Base64 data URI for web integration. Developers often need to embed barcodes directly into HTML emails or web pages without storing separate image files.
// Prompt: Create a utility that converts generated barcode images to Base64 strings for embedding in HTML.
// Tags: barcode, code128, generation, base64, html, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple console utility that generates a barcode, converts it to a Base64 string,
/// and prints an HTML <img> tag containing the image data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, converts it to Base64, and writes an HTML image tag to the console.
    /// </summary>
    static void Main()
    {
        // Define the barcode symbology and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "Sample123";

        // Generate the barcode image and obtain its Base64 representation.
        string base64Image = ConvertBarcodeToBase64(encodeType, codeText);

        // Output an HTML <img> tag that embeds the Base64-encoded PNG image.
        Console.WriteLine("<img src=\"data:image/png;base64,{0}\" alt=\"Barcode\" />", base64Image);
    }

    /// <summary>
    /// Generates a barcode image using Aspose.BarCode and returns its Base64 representation.
    /// </summary>
    /// <param name="type">The barcode symbology type.</param>
    /// <param name="text">The text to encode.</param>
    /// <returns>Base64 string of the PNG image.</returns>
    private static string ConvertBarcodeToBase64(BaseEncodeType type, string text)
    {
        // Create the barcode generator with the specified type and text.
        using (var generator = new BarcodeGenerator(type, text))
        {
            // Generate the barcode image as an Aspose.Drawing.Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Convert the byte array to a Base64 string.
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
    }
}