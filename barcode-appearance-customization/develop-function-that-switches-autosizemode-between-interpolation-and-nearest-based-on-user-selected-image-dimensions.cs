using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating barcode images with dynamic <see cref="AutoSizeMode"/> based on dimensions.
/// </summary>
class Program
{
    // Generates a barcode image and switches AutoSizeMode based on the provided dimensions.
    // If both width and height are greater than 200 points, Interpolation mode is used;
    // otherwise Nearest mode is applied.
    static void GenerateBarcodeWithAutoSize(float targetWidth, float targetHeight, string outputPath)
    {
        // Choose AutoSizeMode according to the target dimensions.
        AutoSizeMode mode = (targetWidth > 200f && targetHeight > 200f) ? AutoSizeMode.Interpolation : AutoSizeMode.Nearest;

        // Create a BarcodeGenerator for Code128 with a sample code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Apply the selected AutoSizeMode.
            generator.Parameters.AutoSizeMode = mode;

            // Set the desired image dimensions (in points).
            generator.Parameters.ImageWidth.Point = targetWidth;
            generator.Parameters.ImageHeight.Point = targetHeight;

            // Save the barcode image to the specified path.
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Entry point of the application. Generates sample barcode images with different sizes.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images.
        string outputDir = "Barcodes";

        // Ensure the output directory exists.
        if (!System.IO.Directory.Exists(outputDir))
        {
            System.IO.Directory.CreateDirectory(outputDir);
        }

        // First barcode: larger dimensions -> Interpolation mode.
        GenerateBarcodeWithAutoSize(
            300f,
            150f,
            System.IO.Path.Combine(outputDir, "barcode_interpolation.png")
        );

        // Second barcode: smaller dimensions -> Nearest mode.
        GenerateBarcodeWithAutoSize(
            120f,
            80f,
            System.IO.Path.Combine(outputDir, "barcode_nearest.png")
        );

        // Inform the user that the process completed successfully.
        Console.WriteLine("Barcode images generated successfully.");
    }
}