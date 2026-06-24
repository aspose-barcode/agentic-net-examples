using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to obtain the pixel dimensions of a generated barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Retrieves the actual pixel width and height of a generated barcode.
    /// The dimensions are obtained from the bitmap after saving the barcode to a memory stream.
    /// </summary>
    /// <param name="type">The barcode symbology type.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="resolution">Resolution (dpi) for the generated image.</param>
    /// <param name="imageWidthPoints">Optional explicit image width in points.</param>
    /// <param name="imageHeightPoints">Optional explicit image height in points.</param>
    /// <returns>A tuple containing the width and height in pixels.</returns>
    static (int Width, int Height) GetBarcodePixelDimensions(
        BaseEncodeType type,
        string codeText,
        float resolution,
        float? imageWidthPoints = null,
        float? imageHeightPoints = null)
    {
        // Create a barcode generator with the specified type and text
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Set the desired resolution (dots per inch)
            generator.Parameters.Resolution = resolution;

            // If explicit image dimensions are provided, configure auto‑size mode and set size in points
            if (imageWidthPoints.HasValue && imageHeightPoints.HasValue)
            {
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = imageWidthPoints.Value;
                generator.Parameters.ImageHeight.Point = imageHeightPoints.Value;
            }

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the image into a bitmap to read its pixel dimensions
                using (var bitmap = new Bitmap(ms))
                {
                    return (bitmap.Width, bitmap.Height);
                }
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Demonstrates usage of GetBarcodePixelDimensions.
    /// </summary>
    static void Main()
    {
        // Example: generate a Code128 barcode at 300 DPI with a target size of 200pt x 100pt
        var dimensions = GetBarcodePixelDimensions(
            EncodeTypes.Code128,
            "1234567890",
            300f,
            200f,
            100f);

        // Output the resulting pixel dimensions to the console
        Console.WriteLine($"Barcode pixel dimensions: Width = {dimensions.Width}px, Height = {dimensions.Height}px");
    }
}