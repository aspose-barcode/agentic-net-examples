// Title: Validate barcode image dimensions at 96 dpi
// Description: Generates a Code128 barcode at 96 dpi and verifies that its physical width matches 20 mm within a half‑millimeter tolerance.
// Category-Description: This example belongs to the Aspose.BarCode generation and measurement category. It demonstrates how to use BarcodeGenerator, set resolution, control AutoSizeMode, and retrieve the resulting Bitmap to calculate physical dimensions. Developers often need to ensure that generated barcodes meet exact size specifications for printing or scanning requirements, making resolution and size validation a common task.
// Prompt: Validate barcode generated at 96 dpi matches expected pixel dimensions for 20 mm width.
// Tags: code128, barcode, resolution, size, validation, aspose.barcode, generation, image

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and validating its physical dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcode, calculates physical size, and checks width against expected value.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the value "1234"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234"))
        {
            // Explicitly set the image resolution to 96 dpi
            generator.Parameters.Resolution = 96f;

            // Disable automatic sizing so the dimensions are based solely on the content
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Generate the barcode image as a Bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve pixel dimensions of the generated image
                int widthPixels = bitmap.Width;
                int heightPixels = bitmap.Height;

                // Convert pixel dimensions to physical size in millimeters using the resolution
                double physicalWidthMm = widthPixels * 25.4 / generator.Parameters.Resolution;
                double physicalHeightMm = heightPixels * 25.4 / generator.Parameters.Resolution;

                // Define the expected physical width and allowable tolerance
                const double expectedWidthMm = 20.0;
                const double toleranceMm = 0.5; // half‑millimeter tolerance

                // Determine whether the actual width falls within the tolerance range
                bool widthMatches = Math.Abs(physicalWidthMm - expectedWidthMm) <= toleranceMm;

                // Output the results
                Console.WriteLine($"Barcode image size: {widthPixels}×{heightPixels} pixels");
                Console.WriteLine($"Physical size: {physicalWidthMm:F2} mm × {physicalHeightMm:F2} mm");
                Console.WriteLine($"Expected width: {expectedWidthMm} mm");
                Console.WriteLine($"Width matches expected (±{toleranceMm} mm): {widthMatches}");
            }
        }
    }
}