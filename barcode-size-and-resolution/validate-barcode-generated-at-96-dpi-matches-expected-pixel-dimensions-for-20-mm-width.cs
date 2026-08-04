// Title: Validate barcode pixel dimensions at 96 dpi
// Description: Demonstrates generating a Code128 barcode at 96 dpi and verifying its pixel width matches a 20 mm physical size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control image resolution, canvas size, and auto‑size mode using BarcodeGenerator, ImageWidth, and Resolution properties. Typical use cases include creating barcodes for print layouts where exact physical dimensions are required. Developers often need to validate that generated images meet size specifications for downstream processing or compliance.
/// Prompt: Validate barcode generated at 96 dpi matches expected pixel dimensions for 20 mm width.
// Tags: code128, generation, png, resolution, autosizemode, imagewidth, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode at a specific DPI and validates that its pixel width matches the expected size for a 20 mm physical width.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Creates the barcode, saves it to a memory stream, and checks the image width against the expected pixel count.
    /// </summary>
    static void Main()
    {
        // Desired physical width in millimeters.
        const float targetWidthMm = 20f;
        // Target DPI resolution.
        const float dpi = 96f;

        // Convert millimeters to inches (1 inch = 25.4 mm).
        double inches = targetWidthMm / 25.4;
        // Calculate expected pixel width (rounded to nearest integer).
        int expectedPixels = (int)Math.Round(inches * dpi);

        // Use a short Code128 text to ensure it fits the target width.
        const string codeText = "12";

        // Initialize the barcode generator with Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the image resolution.
            generator.Parameters.Resolution = dpi;
            // Force the canvas size using interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            // Set the canvas width to the expected pixel count.
            generator.Parameters.ImageWidth.Point = expectedPixels;
            // Height can be arbitrary; let the generator decide (set to 100 pixels here).
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the memory stream.
                using (var image = Image.FromStream(ms))
                {
                    int actualWidth = image.Width; // Pixel width of the generated image.

                    // Allow a tolerance of ±2 pixels due to rounding/rendering differences.
                    int tolerance = 2;
                    bool matches = Math.Abs(actualWidth - expectedPixels) <= tolerance;

                    // Output the validation results.
                    Console.WriteLine($"Target width: {targetWidthMm} mm ({expectedPixels} px at {dpi} DPI)");
                    Console.WriteLine($"Actual image width: {actualWidth} px");
                    Console.WriteLine(matches
                        ? "The generated barcode matches the expected pixel dimensions."
                        : "The generated barcode does NOT match the expected pixel dimensions.");
                }
            }
        }
    }
}