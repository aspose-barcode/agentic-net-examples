// Title: Verify barcode image respects configured maximum dimensions
// Description: Demonstrates generating a Code128 barcode image with explicit width and height limits and validates that the resulting PNG does not exceed those limits.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to control image size using BarcodeGenerator.Parameters.AutoSizeMode, ImageWidth, and ImageHeight. Developers often need to enforce size constraints for UI layout or printing, and this snippet shows the typical API usage for setting dimensions and validating the output.
// Prompt: Create a test ensuring barcode image size respects maximum dimensions set in configuration.
// Tags: code128, generation, png, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a barcode image with predefined maximum dimensions and verifies that the output respects those limits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Code128 barcode, saves it as PNG, and checks its size against configured maximums.
    /// </summary>
    static void Main()
    {
        // Configuration: maximum allowed dimensions (in pixels)
        const int maxWidth = 300;
        const int maxHeight = 150;

        // Prepare output path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode_test.png");

        // Ensure any existing file is removed before generation
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a barcode generator with a short code text to fit within limits
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Disable automatic sizing so the explicit dimensions are used
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set desired image dimensions (these act as maximums for this test)
            generator.Parameters.ImageWidth.Point = maxWidth;
            generator.Parameters.ImageHeight.Point = maxHeight;

            // Optionally adjust XDimension to help the barcode fit within the specified size
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode image to file in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the generated image file exists
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Load the generated image to inspect its actual dimensions
        using (var image = Image.FromFile(outputPath))
        {
            int actualWidth = image.Width;
            int actualHeight = image.Height;

            Console.WriteLine($"Generated image size: {actualWidth}x{actualHeight} pixels");
            Console.WriteLine($"Maximum allowed size: {maxWidth}x{maxHeight} pixels");

            // Determine whether the image respects the configured limits
            bool withinLimits = actualWidth <= maxWidth && actualHeight <= maxHeight;
            Console.WriteLine(withinLimits
                ? "Test passed: Image size respects maximum dimensions."
                : "Test failed: Image size exceeds maximum dimensions.");
        }

        // Clean up the generated file (optional)
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}