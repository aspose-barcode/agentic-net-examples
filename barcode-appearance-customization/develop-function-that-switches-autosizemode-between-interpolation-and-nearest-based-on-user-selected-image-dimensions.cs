// Title: Switch AutoSizeMode based on image dimensions
// Description: Demonstrates how to generate barcodes with Aspose.BarCode and dynamically select the AutoSizeMode (Interpolation or Nearest) according to the chosen image width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator class and its Parameters.AutoSizeMode property. Developers often need to control barcode scaling for different image dimensions, choosing between Interpolation for larger widths or Nearest for taller images. The snippet shows typical setup, dimension assignment, and saving to PNG, useful for applications that require adaptive barcode rendering.
// Prompt: Develop a function that switches AutoSizeMode between Interpolation and Nearest based on user‑selected image dimensions.
// Tags: barcode, generation, autosizemode, interpolation, nearest, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates switching AutoSizeMode based on image dimensions and generating PNG barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, generates two barcodes with different AutoSizeMode settings, and reports the location.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "AutoSizeDemo");
        Directory.CreateDirectory(outputDir);

        // Generate a barcode using Interpolation when width >= height.
        SwitchAutoSizeMode(300, 200, Path.Combine(outputDir, "barcode_interpolation.png"));
        // Generate a barcode using Nearest when height > width.
        SwitchAutoSizeMode(200, 300, Path.Combine(outputDir, "barcode_nearest.png"));

        Console.WriteLine("Barcodes generated in: " + outputDir);
    }

    /// <summary>
    /// Generates a barcode image with AutoSizeMode selected based on the provided dimensions.
    /// </summary>
    /// <param name="widthPixels">Desired image width in pixels.</param>
    /// <param name="heightPixels">Desired image height in pixels.</param>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    static void SwitchAutoSizeMode(int widthPixels, int heightPixels, string outputPath)
    {
        // Choose Interpolation if the image is wider than tall; otherwise, use Nearest.
        AutoSizeMode mode = widthPixels >= heightPixels ? AutoSizeMode.Interpolation : AutoSizeMode.Nearest;

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply the selected AutoSizeMode and set image dimensions.
            generator.Parameters.AutoSizeMode = mode;
            generator.Parameters.ImageWidth.Pixels = widthPixels;
            generator.Parameters.ImageHeight.Pixels = heightPixels;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;

            // Save the barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode using {mode} mode to {outputPath}");
        }
    }
}