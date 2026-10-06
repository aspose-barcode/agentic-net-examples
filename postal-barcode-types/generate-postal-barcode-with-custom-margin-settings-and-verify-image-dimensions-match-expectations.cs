// Title: Generate a Planet postal barcode with custom margins and verify dimensions
// Description: Demonstrates creating a Planet postal barcode, applying custom padding, and checking that the resulting image size reflects the margin changes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the BarcodeGenerator class, EncodeTypes enumeration, and image handling via Aspose.Drawing. Typical use cases include generating postal barcodes with specific layout requirements, adjusting padding for printing constraints, and validating output dimensions. Developers often need to customize barcode appearance and verify rendering results programmatically.
// Prompt: Generate a postal barcode with custom margin settings and verify image dimensions match expectations.
// Tags: postal barcode, margin, padding, image dimensions, aspose.barcode, aspose.drawing, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Planet postal barcode with and without custom padding,
/// saving the images, and verifying that padding affects the image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates two barcode images,
    /// and prints verification results to the console.
    /// </summary>
    static void Main()
    {
        // Define and create the output folder for generated barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "PostalBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // -------------------------------------------------
        // Generate default barcode (no custom padding)
        // -------------------------------------------------
        int defaultWidth, defaultHeight;
        string defaultPath = Path.Combine(outputDir, "PlanetDefault.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set basic barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Render the barcode to a bitmap and capture its size
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                defaultWidth = bmp.Width;
                defaultHeight = bmp.Height;
                // Save the default image as PNG
                bmp.Save(defaultPath, ImageFormat.Png);
            }
        }

        // -------------------------------------------------
        // Generate barcode with custom margins (padding)
        // -------------------------------------------------
        int paddedWidth, paddedHeight;
        string paddedPath = Path.Combine(outputDir, "PlanetPadded.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set basic barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Apply custom padding on all sides (20 pixels each)
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Render the padded barcode to a bitmap and capture its size
            using (Bitmap bmp = generator.GenerateBarCodeImage())
            {
                paddedWidth = bmp.Width;
                paddedHeight = bmp.Height;
                // Save the padded image as PNG
                bmp.Save(paddedPath, ImageFormat.Png);
            }
        }

        // -------------------------------------------------
        // Verification: compare dimensions of default vs padded images
        // -------------------------------------------------
        bool widthIncreased = paddedWidth > defaultWidth;
        bool heightIncreased = paddedHeight > defaultHeight;

        Console.WriteLine($"Default image size: {defaultWidth}x{defaultHeight}");
        Console.WriteLine($"Padded image size: {paddedWidth}x{paddedHeight}");
        Console.WriteLine($"Width increased: {widthIncreased}");
        Console.WriteLine($"Height increased: {heightIncreased}");

        if (widthIncreased && heightIncreased)
        {
            Console.WriteLine("Custom margin settings applied successfully.");
        }
        else
        {
            Console.WriteLine("Margin settings did not affect image dimensions as expected.");
        }
    }
}