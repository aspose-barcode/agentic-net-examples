using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating MaxiCode barcodes with different aspect ratios
/// and retrieving their pixel dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a MaxiCode barcode using the specified aspect ratio
    /// and returns its width and height in pixels.
    /// </summary>
    /// <param name="aspectRatio">The desired aspect ratio (height / width) for the barcode modules.</param>
    /// <returns>A tuple containing the image width and height.</returns>
    static (int Width, int Height) GenerateMaxiCodeDimensions(float aspectRatio)
    {
        // Sample codetext for MaxiCode; any non‑empty string is acceptable.
        const string codeText = "Sample MaxiCode";

        // Create a barcode generator for MaxiCode with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Apply the requested aspect ratio to the generator's parameters.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = aspectRatio;

            // Use a memory stream to hold the generated PNG image.
            using (var ms = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the stream to obtain its actual pixel dimensions.
                using (var image = Image.FromStream(ms))
                {
                    return (image.Width, image.Height);
                }
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Generates barcodes with two different aspect ratios,
    /// prints their dimensions, and verifies that the dimensions change accordingly.
    /// </summary>
    static void Main()
    {
        // Generate two barcodes with different aspect ratios.
        var dimsRatio1 = GenerateMaxiCodeDimensions(1.0f);
        var dimsRatio2 = GenerateMaxiCodeDimensions(2.0f);

        // Output the dimensions for each aspect ratio.
        Console.WriteLine($"AspectRatio 1.0 -> Width: {dimsRatio1.Width}, Height: {dimsRatio1.Height}");
        Console.WriteLine($"AspectRatio 2.0 -> Width: {dimsRatio2.Width}, Height: {dimsRatio2.Height}");

        // Simple verification: the heights (or widths) should differ when the aspect ratio changes.
        bool heightChanged = dimsRatio1.Height != dimsRatio2.Height;
        bool widthChanged = dimsRatio1.Width != dimsRatio2.Width;

        if (heightChanged || widthChanged)
        {
            Console.WriteLine("PASS: Changing the aspect ratio affects the barcode dimensions.");
        }
        else
        {
            Console.WriteLine("FAIL: Barcode dimensions did not change with aspect ratio adjustments.");
        }
    }
}