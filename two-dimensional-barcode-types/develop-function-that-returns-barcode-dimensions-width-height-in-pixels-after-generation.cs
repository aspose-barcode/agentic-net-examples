// Title: Get Barcode Image Dimensions in Pixels
// Description: Demonstrates how to generate a barcode image with Aspose.BarCode and retrieve its width and height in pixels.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and Bitmap to create barcode images and inspect their properties. Developers often need to know the exact pixel dimensions of generated barcodes for layout, printing, or UI integration. The key API classes shown are BarcodeGenerator, BaseEncodeType, EncodeTypes, and Aspose.Drawing.Bitmap.
// Prompt: Develop a function that returns barcode dimensions (width, height) in pixels after generation.
// Tags: barcode, dimensions, image, aspose.barcode, barcodegenerator, bitmap, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides functionality to generate a barcode and obtain its pixel dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image for the specified text and symbology, then returns its width and height in pixels.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    static (int width, int height) GetBarcodeDimensions(string codeText, BaseEncodeType encodeType)
    {
        // Create a BarcodeGenerator with the desired symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Return the bitmap's pixel dimensions.
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Demonstrates retrieving barcode dimensions and writing them to the console.
    /// </summary>
    static void Main()
    {
        // Generate dimensions for a Code128 barcode containing "ASPOSE".
        var (width, height) = GetBarcodeDimensions("ASPOSE", EncodeTypes.Code128);
        // Output the dimensions.
        Console.WriteLine($"Barcode dimensions: Width={width} px, Height={height} px");
    }
}