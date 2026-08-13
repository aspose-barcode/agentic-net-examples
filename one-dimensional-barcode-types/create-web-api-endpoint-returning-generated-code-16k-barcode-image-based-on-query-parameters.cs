// Title: Generate Code 16K barcode image and return as Base64 string
// Description: Demonstrates creating a Code 16K barcode with custom aspect ratio and quiet zones, rendering it to PNG, and outputting the image as a Base64 string for use in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters (EncodeTypes, aspect ratio, quiet zones) with the BarcodeGenerator class, render the barcode to an image format (PNG), and retrieve the binary data. Typical use cases include web APIs that need to return barcode images on‑the‑fly, mobile apps generating barcodes for scanning, or batch processes creating printable barcode assets. Developers often need to adjust size, layout, and output format, making this pattern a common starting point for barcode‑related services.
// Prompt: Create web API endpoint returning generated Code 16K barcode image based on query parameters.
// Tags: code16k, barcode, generation, image, png, base64, aspose.barcode, aspnet, webapi

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample console application that simulates a web API endpoint generating a Code 16K barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, encodes it to PNG, and writes the image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated request parameters that would normally come from query string values
        string codeText = "1234567890";
        float aspectRatio = 2.0f;          // Height/Width ratio for the barcode
        int quietZoneLeftCoef = 10;        // Minimum allowed quiet zone on the left side
        int quietZoneRightCoef = 1;        // Minimum allowed quiet zone on the right side

        // Initialize the barcode generator for the Code16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply Code16K‑specific settings
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietZoneLeftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietZoneRightCoef;

            // Optionally define the output image size in points (300x150 points in this example)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string to simulate an HTTP response body
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64);
            }
        }
    }
}