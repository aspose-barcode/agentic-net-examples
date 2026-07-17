// Title: Convert generated barcode images to Base64 strings for HTML embedding
// Description: Demonstrates generating barcodes with Aspose.BarCode, converting the PNG image to a Base64 string, and outputting an HTML <img> tag.
// Category-Description: This example belongs to the Aspose.BarCode image generation and encoding category. It shows how to use BarcodeGenerator, Bitmap, and ImageFormat classes to create barcode images, then encode them as Base64 for web integration. Developers often need to embed barcodes directly into HTML or JSON payloads without saving files, making this pattern common for reporting, email, or UI rendering scenarios.
// Prompt: Create a utility that converts generated barcode images to Base64 strings for embedding in HTML.
// Tags: barcode symbology, generation, png, base64, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and conversion of the resulting image to a Base64 string
/// suitable for embedding directly in HTML markup.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the utility. Generates sample barcodes, converts each image to Base64,
    /// and writes an HTML <img> tag with the embedded data URI to the console.
    /// </summary>
    static void Main()
    {
        // Define a collection of sample barcodes (symbology type and associated text)
        var samples = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "123ABC"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // Iterate over each sample, generate the barcode image, and output as Base64
        foreach (var (type, text) in samples)
        {
            // Initialize the barcode generator with the specified symbology and data
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Generate the barcode as a bitmap image
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Encode the bitmap to PNG format using a memory stream
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        byte[] imageBytes = ms.ToArray();

                        // Convert the PNG byte array to a Base64 string
                        string base64 = Convert.ToBase64String(imageBytes);

                        // Write an HTML <img> tag with the Base64-encoded image data
                        Console.WriteLine($"<img src=\"data:image/png;base64,{base64}\" alt=\"{text}\" />");
                    }
                }
            }
        }
    }
}