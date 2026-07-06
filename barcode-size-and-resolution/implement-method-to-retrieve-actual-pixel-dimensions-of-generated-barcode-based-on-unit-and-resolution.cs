// Title: Retrieve pixel dimensions of a generated barcode image
// Description: Demonstrates how to obtain the actual width and height in pixels of a barcode generated with Aspose.BarCode, considering unit settings and resolution.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and resolution settings to control barcode size. Developers often need to know the exact pixel dimensions for layout, printing, or further image processing, making this a common requirement in barcode rendering scenarios.
// Prompt: Implement method to retrieve actual pixel dimensions of generated barcode based on unit and resolution.
// Tags: barcode symbology, image generation, pixel dimensions, resolution, autosizemode, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, configures its size and resolution,
/// and retrieves the actual pixel dimensions of the resulting image.
/// </summary>
class Program
{
    /// <summary>
    /// Retrieves the actual pixel dimensions of the generated barcode image.
    /// </summary>
    /// <param name="generator">The configured <see cref="BarcodeGenerator"/> instance.</param>
    /// <returns>A tuple containing the width and height in pixels.</returns>
    static (int Width, int Height) GetBarcodePixelDimensions(BarcodeGenerator generator)
    {
        // Generate the barcode image and obtain its pixel size.
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        {
            return (bitmap.Width, bitmap.Height);
        }
    }

    /// <summary>
    /// Entry point of the example. Configures barcode generation parameters,
    /// obtains pixel dimensions, and optionally saves the image to disk.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set resolution (dpi) – this influences unit-to-pixel conversion.
            generator.Parameters.Resolution = 300f; // 300 dpi

            // Use interpolation mode to control image size via ImageWidth/ImageHeight.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;   // 200 points width
            generator.Parameters.ImageHeight.Point = 80f;   // 80 points height

            // Retrieve actual pixel dimensions after generation.
            var (width, height) = GetBarcodePixelDimensions(generator);

            Console.WriteLine($"Generated barcode pixel dimensions: Width = {width}px, Height = {height}px");

            // Optionally save the barcode image.
            generator.Save("barcode.png");
        }
    }
}