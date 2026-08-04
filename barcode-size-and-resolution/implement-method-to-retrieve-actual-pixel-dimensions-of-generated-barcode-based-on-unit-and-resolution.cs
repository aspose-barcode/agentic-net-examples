// Title: Retrieve barcode pixel dimensions based on unit and resolution
// Description: Demonstrates how to obtain the actual pixel width and height of a generated barcode image using Aspose.BarCode, taking into account the specified resolution and size units.
// Category-Description: This example belongs to the Aspose.BarCode image generation and measurement category. It shows how to configure barcode parameters such as resolution, auto‑size mode, and unit‑based dimensions, then retrieve the resulting pixel dimensions via the generated bitmap. Developers working with barcode rendering often need to know the exact pixel size for layout, printing, or further image processing, and typically use classes like BarcodeGenerator, BarcodeParameters, and System.Drawing.Bitmap.
// Prompt: Implement method to retrieve actual pixel dimensions of generated barcode based on unit and resolution.
// Tags: barcode, code128, pixel-dimensions, resolution, autosizemode, aspnet, aspose.barcode, image-generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example of generating a barcode, configuring its size and resolution,
/// and retrieving the actual pixel dimensions of the resulting image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode image and returns its pixel width and height.
    /// </summary>
    /// <param name="generator">Configured <see cref="BarcodeGenerator"/> instance.</param>
    /// <returns>Tuple containing the image width and height in pixels.</returns>
    static (int Width, int Height) GetBarcodePixelDimensions(BarcodeGenerator generator)
    {
        // Generate the barcode image as a bitmap.
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            // Width and Height properties are expressed in pixels.
            return (bitmap.Width, bitmap.Height);
        }
    }

    /// <summary>
    /// Entry point of the example. Configures barcode parameters, obtains pixel dimensions,
    /// writes them to the console, and saves the image to a file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a custom resolution (dots per inch) to influence pixel size.
            generator.Parameters.Resolution = 300f; // 300 DPI

            // Use interpolation mode to control size via ImageWidth/ImageHeight.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;   // Desired width in points.
            generator.Parameters.ImageHeight.Point = 80f;   // Desired height in points.

            // Optionally set XDimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Retrieve the actual pixel dimensions after generation.
            (int width, int height) = GetBarcodePixelDimensions(generator);

            // Output the dimensions to the console.
            Console.WriteLine($"Generated barcode pixel dimensions: Width = {width}px, Height = {height}px");

            // Save the barcode image for visual verification (optional).
            generator.Save("barcode.png");
        }
    }
}