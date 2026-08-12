// Title: DotCode Barcode Margin Verification Unit Test
// Description: Demonstrates how to verify that the DotCode barcode generator respects the padding (margin) settings by checking the surrounding whitespace in the generated image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode appearance customization. It showcases the use of BarcodeGenerator, EncodeTypes, and image handling classes (Bitmap, MemoryStream) to test padding (margin) effects. Developers often need to ensure that barcode margins are correctly applied for layout and scanning reliability, making such unit‑test patterns valuable in automated validation suites.
// Prompt: Write unit test ensuring DotCode margin property correctly influences surrounding whitespace.
// Tags: dotcode, barcode, margin, padding, unit-test, aspose.barcode, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains a simple unit‑test‑style method that validates the margin (padding) applied to a DotCode barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the DotCode margin verification test.
    /// </summary>
    static void Main()
    {
        // Run the unit test for DotCode margin (padding) handling
        TestDotCodeMargin();
    }

    /// <summary>
    /// Generates a DotCode barcode with explicit padding on all sides, renders it to a PNG image,
    /// and checks that the surrounding whitespace matches the expected background color.
    /// </summary>
    static void TestDotCodeMargin()
    {
        // Define padding values (in points) for each side of the barcode
        const float leftPadding = 10f;
        const float topPadding = 10f;
        const float rightPadding = 10f;
        const float bottomPadding = 10f;

        // Create a DotCode barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "12345"))
        {
            // Apply explicit padding on each side via the generator's parameters
            generator.Parameters.Barcode.Padding.Left.Point = leftPadding;
            generator.Parameters.Barcode.Padding.Top.Point = topPadding;
            generator.Parameters.Barcode.Padding.Right.Point = rightPadding;
            generator.Parameters.Barcode.Padding.Bottom.Point = bottomPadding;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the image from the memory stream for pixel inspection
                using (var bitmap = new Bitmap(ms))
                {
                    // Expected background color (default is white)
                    var expectedBg = Aspose.Drawing.Color.White;

                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    bool success = true;

                    // Helper local function to verify that a rectangular region consists solely of the expected background color
                    bool CheckRegion(int startX, int startY, int regionWidth, int regionHeight)
                    {
                        for (int y = startY; y < startY + regionHeight; y++)
                        {
                            for (int x = startX; x < startX + regionWidth; x++)
                            {
                                if (bitmap.GetPixel(x, y).ToArgb() != expectedBg.ToArgb())
                                    return false;
                            }
                        }
                        return true;
                    }

                    // Verify left padding region
                    if (!CheckRegion(0, 0, (int)leftPadding, height))
                        success = false;

                    // Verify right padding region
                    if (!CheckRegion(width - (int)rightPadding, 0, (int)rightPadding, height))
                        success = false;

                    // Verify top padding region
                    if (!CheckRegion(0, 0, width, (int)topPadding))
                        success = false;

                    // Verify bottom padding region
                    if (!CheckRegion(0, height - (int)bottomPadding, width, (int)bottomPadding))
                        success = false;

                    // Output test result to the console
                    if (success)
                    {
                        Console.WriteLine("PASSED: DotCode margin correctly applied.");
                    }
                    else
                    {
                        Console.WriteLine("FAILED: DotCode margin not applied as expected.");
                    }
                }
            }
        }
    }
}