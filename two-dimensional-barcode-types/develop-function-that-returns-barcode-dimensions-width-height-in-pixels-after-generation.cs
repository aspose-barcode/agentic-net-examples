// Title: Get barcode image dimensions in pixels
// Description: Demonstrates how to generate a barcode using Aspose.BarCode and retrieve its width and height in pixels.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator and Bitmap classes to create barcode images and extract their size. Developers often need to know exact pixel dimensions for layout, UI integration, or further image processing, making this pattern common in barcode rendering scenarios.
// Prompt: Develop a function that returns barcode dimensions (width, height) in pixels after generation.
// Tags: barcode, dimensions, image, generation, aspose.barcode, bitmap, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates retrieving barcode dimensions after generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Returns the width and height (in pixels) of a generated barcode image.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <returns>A tuple containing the width and height in pixels.</returns>
    static (int width, int height) GetBarcodeDimensions(string codeText, BaseEncodeType encodeType)
    {
        // Initialize the barcode generator with the specified symbology and text.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Bitmap.Width and Bitmap.Height are measured in pixels.
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point. Generates a sample Code128 barcode and prints its pixel dimensions.
    /// </summary>
    static void Main()
    {
        // Sample barcode data: Code128 symbology with text "123ABC".
        string sampleText = "123ABC";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Retrieve dimensions of the generated barcode.
        var (width, height) = GetBarcodeDimensions(sampleText, encodeType);

        // Output the dimensions to the console.
        Console.WriteLine($"Barcode dimensions: Width = {width}px, Height = {height}px");
    }
}