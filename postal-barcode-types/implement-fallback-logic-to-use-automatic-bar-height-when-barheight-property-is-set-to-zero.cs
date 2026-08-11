// Title: Barcode generation with fallback automatic bar height
// Description: Shows how to configure a barcode generator to use a specified bar height or automatically determine it when the height is zero or negative.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, its Parameters, and AutoSizeMode for dynamic sizing. Developers often need to adjust bar height for different output media while ensuring automatic sizing works as a fallback, a common requirement in barcode rendering pipelines.
// Prompt: Implement fallback logic to use automatic bar height when BarHeight property is set to zero.
// Tags: barcode, barheight, autosizemode, fallback, generation, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to apply fallback logic for bar height when generating barcodes with Aspose.BarCode.
/// If a positive height is provided, it is used; otherwise the generator switches to automatic sizing.
/// </summary>
class Program
{
    /// <summary>
    /// Configures the barcode generator's bar height.
    /// If the supplied height is greater than zero, it is applied.
    /// Otherwise automatic sizing is enabled by setting AutoSizeMode to Interpolation.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to configure.</param>
    /// <param name="barHeight">Desired bar height in points; zero or negative triggers automatic sizing.</param>
    static void ConfigureBarHeight(BarcodeGenerator generator, float barHeight)
    {
        if (barHeight > 0f)
        {
            // Apply explicit bar height (in points).
            generator.Parameters.Barcode.BarHeight.Point = barHeight;
            Console.WriteLine($"BarHeight set to {barHeight} pt.");
        }
        else
        {
            // Enable automatic sizing; do not set BarHeight (zero would throw).
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            Console.WriteLine("BarHeight is zero or negative; using automatic bar height (AutoSizeMode.Interpolation).");
        }
    }

    /// <summary>
    /// Entry point of the example. Generates barcode images using a set of test bar heights,
    /// demonstrating the fallback to automatic sizing when the height is not positive.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated PNG barcode.
        const string outputPath = "barcode.png";

        // Example bar height values to test the fallback logic.
        float[] testBarHeights = { 30f, 0f, -5f };

        foreach (float height in testBarHeights)
        {
            // Ensure any previous file is removed before saving a new one.
            if (File.Exists(outputPath))
                File.Delete(outputPath);

            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "Sample123";

                // Apply the fallback logic for the current test height.
                ConfigureBarHeight(generator, height);

                // Generate the barcode image.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save as PNG using Aspose.Drawing.Imaging.ImageFormat.
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }

            Console.WriteLine($"Barcode saved to '{outputPath}' with BarHeight input {height}.");
            Console.WriteLine();
        }

        // Indicate completion.
        Console.WriteLine("All barcode images generated.");
    }
}