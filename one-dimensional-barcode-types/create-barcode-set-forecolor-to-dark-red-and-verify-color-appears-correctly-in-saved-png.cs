// Title: Generate Code128 barcode with dark red bars and verify PNG output
// Description: Demonstrates creating a Code128 barcode, setting its bar color to dark red, saving as PNG, and programmatically confirming the color exists in the image.
// Category-Description: This example belongs to the Aspose.BarCode generation and image verification category. It showcases the use of BarcodeGenerator, setting bar color via Parameters.Barcode.BarColor, saving with BarCodeImageFormat, and reading the resulting PNG with Aspose.Drawing.Bitmap. Developers often need to customize barcode appearance and validate output images in automated pipelines or unit tests.
// Prompt: Create a barcode, set ForeColor to dark red, and verify color appears correctly in saved PNG.
// Tags: barcode, code128, colormodification, png, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with custom bar color and verification of the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves it as PNG, and checks for the specified bar color.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        string codeText = "123456";
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");
        Color targetBarColor = Color.FromArgb(139, 0, 0); // Dark red

        // Generate barcode with dark red bars
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the bar (foreground) color
            generator.Parameters.Barcode.BarColor = targetBarColor;
            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved PNG contains the dark red bar color
        bool colorFound = false;
        using (var bitmap = new Bitmap(outputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            // Scan each pixel until the target color is found
            for (int y = 0; y < height && !colorFound; y++)
            {
                for (int x = 0; x < width && !colorFound; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Skip background (assumed white)
                    if (!pixel.Equals(Color.White))
                    {
                        if (pixel.Equals(targetBarColor))
                        {
                            colorFound = true;
                        }
                    }
                }
            }
        }

        // Output verification result
        if (colorFound)
        {
            Console.WriteLine("Verification succeeded: dark red bar color is present in the PNG.");
        }
        else
        {
            Console.WriteLine("Verification failed: dark red bar color not found in the PNG.");
        }
    }
}