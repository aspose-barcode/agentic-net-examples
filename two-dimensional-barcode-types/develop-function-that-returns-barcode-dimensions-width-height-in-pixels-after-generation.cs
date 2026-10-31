// Title: Get barcode dimensions in pixels after generation
// Description: Demonstrates how to generate a barcode image using Aspose.BarCode and retrieve its pixel width and height.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing the use of BarcodeGenerator and the generated image object to obtain size information. Developers often need to know exact dimensions for layout, UI placement, or further image processing, and this pattern illustrates the typical workflow with the BarcodeGenerator, BaseEncodeType, and Aspose.Drawing.Bitmap classes.
// Prompt: Develop a function that returns barcode dimensions (width, height) in pixels after generation.
// Tags: barcode, dimensions, generation, code128, aspose.barcode, aspose.drawing, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates retrieving barcode image dimensions using Aspose.BarCode.
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
            // Generate the barcode image as a bitmap.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Return the bitmap's pixel dimensions.
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point. Generates a Code128 barcode and prints its width and height in pixels.
    /// </summary>
    static void Main()
    {
        // Obtain dimensions for a sample barcode.
        var dimensions = GetBarcodeDimensions("ASPOSE", EncodeTypes.Code128);
        Console.WriteLine($"Width: {dimensions.width}px, Height: {dimensions.height}px");
    }
}