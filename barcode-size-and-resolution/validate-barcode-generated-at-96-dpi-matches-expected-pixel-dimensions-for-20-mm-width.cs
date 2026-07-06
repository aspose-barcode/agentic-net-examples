// Title: Validate barcode pixel width at 96 dpi for 20 mm barcode
// Description: Generates a Code128 barcode at 96 dpi with a width of 20 mm, saves it as PNG, and verifies that the resulting image width matches the expected pixel count.
// Category-Description: This example belongs to the Aspose.BarCode image generation and validation category. It demonstrates how to configure barcode dimensions using the BarcodeGenerator, set resolution via Parameters.Resolution, and validate the output image size using Aspose.Drawing.Image. Developers often need to ensure that generated barcodes meet exact physical size requirements for printing and scanning, making pixel‑to‑millimeter calculations essential.
// Prompt: Validate barcode generated at 96 dpi matches expected pixel dimensions for 20 mm width.
// Tags: code128, barcode generation, image validation, resolution, dimensions, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a barcode with a specific physical width,
/// save it as an image, and validate that the image dimensions correspond to the expected pixel size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it as PNG,
    /// and checks that its pixel width matches the calculated value for 20 mm at 96 dpi.
    /// </summary>
    static void Main()
    {
        // Define the desired barcode width in millimeters and the target resolution.
        const float millimeters = 20f;
        const float dpi = 96f;

        // Convert millimeters to inches (1 inch = 25.4 mm) and calculate expected pixel width.
        float inches = millimeters / 25.4f;
        int expectedPixels = (int)Math.Round(inches * dpi);

        // Output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Ensure a clean start by deleting any existing file with the same name.
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create and configure the barcode generator.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image resolution (dots per inch).
            generator.Parameters.Resolution = dpi;

            // Disable automatic size adjustment; we will set dimensions manually.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Specify the barcode width and a reasonable height in millimeters.
            generator.Parameters.ImageWidth.Millimeters = millimeters;
            generator.Parameters.ImageHeight.Millimeters = 10f;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to verify its actual pixel width.
        using (Image image = Image.FromFile(outputPath))
        {
            int actualWidth = image.Width;

            // Output the expected and actual widths for comparison.
            Console.WriteLine($"Expected width: {expectedPixels} pixels");
            Console.WriteLine($"Actual width  : {actualWidth} pixels");

            // Report validation result.
            if (actualWidth == expectedPixels)
            {
                Console.WriteLine("Validation succeeded: barcode width matches expected dimensions.");
            }
            else
            {
                Console.WriteLine("Validation failed: barcode width does not match expected dimensions.");
            }
        }
    }
}