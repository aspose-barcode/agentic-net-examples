// Title: Demonstrate MaxiCode barcode generation with varying aspect ratios
// Description: Shows how changing the AspectRatio property influences the generated MaxiCode barcode dimensions, useful for layout tuning.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It illustrates the use of BarcodeGenerator, EncodeTypes, and the MaxiCode aspect ratio setting to control image size. Developers often need to adjust barcode dimensions for fitting into UI or print layouts, and this snippet demonstrates the impact of aspect ratio adjustments.
// Prompt: Write unit tests to verify aspect ratio adjustments affect MaxiCode barcode dimensions as expected.
// Tags: maxicode, aspectratio, barcode, generation, dimensions, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates MaxiCode barcodes with different aspect ratios and compares their dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes with default and reduced aspect ratios, prints dimensions, and validates height reduction.
    /// </summary>
    static void Main()
    {
        // Generate MaxiCode with the default aspect ratio (1.0) and capture its dimensions.
        var dimensionsDefault = GenerateMaxiCode(1.0f);

        // Generate MaxiCode with a reduced aspect ratio (0.5) and capture its dimensions.
        var dimensionsReduced = GenerateMaxiCode(0.5f);

        // Output the width and height for each generated barcode.
        Console.WriteLine($"AspectRatio 1.0 dimensions: {dimensionsDefault.width}x{dimensionsDefault.height}");
        Console.WriteLine($"AspectRatio 0.5 dimensions: {dimensionsReduced.width}x{dimensionsReduced.height}");

        // Verify that reducing the aspect ratio results in a smaller height.
        bool heightReduced = dimensionsReduced.height < dimensionsDefault.height;
        if (heightReduced)
        {
            Console.WriteLine("PASS: Height reduced when aspect ratio decreased.");
        }
        else
        {
            Console.WriteLine("FAIL: Height not reduced as expected.");
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode image using the specified aspect ratio and returns its width and height.
    /// </summary>
    /// <param name="aspectRatio">The desired aspect ratio for the MaxiCode barcode.</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    private static (int width, int height) GenerateMaxiCode(float aspectRatio)
    {
        // Initialize the barcode generator for MaxiCode symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Test"))
        {
            // Set the X-dimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 15;

            // Apply the specified aspect ratio to the MaxiCode settings.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = aspectRatio;

            // Generate the barcode image and retrieve its dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }
}