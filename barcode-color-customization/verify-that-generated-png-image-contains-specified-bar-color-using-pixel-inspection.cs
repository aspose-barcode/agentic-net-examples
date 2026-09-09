// Title: Generate Code128 barcode PNG with custom bar color and verify via pixel inspection
// Description: This example creates a Code128 barcode, sets the bar color to blue, saves it as a PNG, and checks that the specified color appears in the image by scanning its pixels.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image analysis, covering the BarcodeGenerator class, barcode parameters, and Aspose.Drawing bitmap handling. Typical use cases include customizing barcode appearance and programmatically validating visual output, which developers often need when integrating barcodes into automated workflows or CI pipelines.
// Prompt: Verify that the generated PNG image contains the specified bar color using pixel inspection.
// Tags: barcode, code128, color, png, pixel inspection, aspose.barcode, aspose.drawing, image verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom bar color,
/// saving it as PNG, and verifying the color via pixel inspection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it,
    /// inspects pixels for the target color, reports the result,
    /// and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define barcode parameters
        string codeText = "12345678";
        BaseEncodeType encodeType = EncodeTypes.Code128;
        Aspose.Drawing.Color targetBarColor = Aspose.Drawing.Color.Blue;

        // Create a unique temporary file path for the PNG image
        string tempFile = Path.Combine(Path.GetTempPath(),
            "BarcodeColorTest_" + Guid.NewGuid().ToString("N") + ".png");

        // Generate the barcode and set the custom bar color
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.BarColor = targetBarColor;
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the generated PNG and inspect each pixel for the target color
        bool colorFound = false;
        using (Bitmap bitmap = new Bitmap(tempFile))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            for (int y = 0; y < height && !colorFound; y++)
            {
                for (int x = 0; x < width && !colorFound; x++)
                {
                    Aspose.Drawing.Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.ToArgb() == targetBarColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        // Output the verification result to the console
        if (colorFound)
        {
            Console.WriteLine("Verification succeeded: bar color found in the image.");
        }
        else
        {
            Console.WriteLine("Verification failed: bar color not found in the image.");
        }

        // Attempt to delete the temporary file; ignore any errors
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Cleanup errors are intentionally ignored
        }
    }
}