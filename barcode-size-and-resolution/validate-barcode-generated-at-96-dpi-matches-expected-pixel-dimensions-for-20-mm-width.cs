// Title: Validate barcode physical width at 96 dpi
// Description: Generates a Code128 barcode at 96 dpi and checks that its pixel width corresponds to a 20 mm physical size.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, demonstrating how to configure resolution, render a barcode to a bitmap, and verify its dimensions. It uses BarcodeGenerator, EncodeTypes, and the Parameters.Resolution property, which are common when developers need precise sizing for printing or scanning applications.
// Prompt: Validate barcode generated at 96 dpi matches expected pixel dimensions for 20 mm width.
// Tags: barcode, code128, resolution, dimension validation, bitmap, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and validating its physical width based on resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, computes its physical width, and reports whether it matches the expected 20 mm size.
    /// </summary>
    static void Main()
    {
        // The text to encode in the barcode.
        const string codeText = "12345";

        // Create a barcode generator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the image resolution to 96 DPI.
            generator.Parameters.Resolution = 96f;

            // Generate the barcode image as a bitmap.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Width in pixels of the generated bitmap.
                int pixelWidth = bitmap.Width;

                // Convert pixel width to physical width in millimeters.
                float physicalWidthMm = pixelWidth / generator.Parameters.Resolution * 25.4f;

                // Expected physical width and tolerance.
                const float expectedWidthMm = 20f;
                const float toleranceMm = 0.5f;

                // Determine if the actual width is within the tolerance range.
                bool matches = Math.Abs(physicalWidthMm - expectedWidthMm) <= toleranceMm;

                // Output the results.
                Console.WriteLine($"Pixel width: {pixelWidth}");
                Console.WriteLine($"Physical width (mm): {physicalWidthMm:F2}");
                Console.WriteLine($"Matches expected 20mm ±{toleranceMm}mm: {matches}");
            }
        }
    }
}