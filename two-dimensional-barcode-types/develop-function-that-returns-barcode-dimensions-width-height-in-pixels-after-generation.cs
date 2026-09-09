// Title: Get barcode dimensions in pixels after generation
// Description: Demonstrates how to generate a barcode using Aspose.BarCode and retrieve its pixel width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and related parameter settings to create barcode images. Developers often need to know the exact image dimensions for layout or further processing; this snippet shows how to obtain those dimensions using the generated Bitmap object. Typical use cases include dynamic UI rendering, printing, or image compositing where precise size information is required.
// Prompt: Develop a function that returns barcode dimensions (width, height) in pixels after generation.
// Tags: barcode, dimensions, generation, code128, aspose.barcode, bitmap, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and retrieval of image dimensions using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode for "ASPOSE" and prints its width and height in pixels.
    /// </summary>
    static void Main()
    {
        // Define the text to encode and the barcode symbology.
        string codeText = "ASPOSE";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Obtain the dimensions of the generated barcode image.
        var dimensions = GetBarcodeDimensions(encodeType, codeText);

        // Output the width and height to the console.
        Console.WriteLine($"Width: {dimensions.width}px, Height: {dimensions.height}px");
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text, then returns its pixel dimensions.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use (e.g., Code128).</param>
    /// <param name="codeText">The text to encode into the barcode.</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    static (int width, int height) GetBarcodeDimensions(BaseEncodeType encodeType, string codeText)
    {
        // Create a BarcodeGenerator with the desired type and content.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for finer resolution.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Return the bitmap's width and height.
                return (bitmap.Width, bitmap.Height);
            }
        }
    }
}