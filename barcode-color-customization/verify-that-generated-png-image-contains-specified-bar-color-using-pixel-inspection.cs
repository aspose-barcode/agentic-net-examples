// Title: Generate Code128 barcode PNG with custom bar color and verify via pixel inspection
// Description: This example creates a Code128 barcode, sets the bar color to blue, saves it as a PNG, and checks that the image contains the specified color by scanning its pixels.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image analysis. It uses BarcodeGenerator, BarcodeParameters, and Aspose.Drawing.Bitmap to customize barcode appearance and perform pixel-level verification. Typical for developers needing to validate visual properties of generated barcodes, such as color, size, or format, across automated tests or CI pipelines.
// Prompt: Verify that the generated PNG image contains the specified bar color using pixel inspection.
// Tags: code128, barcode generation, color verification, png, aspose.barcode, aspose.drawing, pixel inspection

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a custom bar color,
/// saves it as a PNG file, and verifies the presence of the specified color
/// by inspecting the image pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the image,
    /// and validates the bar color through pixel inspection.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Barcode content and desired bar color.
        string codeText = "12345678";
        Aspose.Drawing.Color targetBarColor = Aspose.Drawing.Color.Blue;

        // Generate the barcode with the custom bar color and save as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = targetBarColor;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the generated PNG contains the specified bar color.
        bool colorFound = false;
        if (File.Exists(outputPath))
        {
            using (Aspose.Drawing.Bitmap bitmap = new Aspose.Drawing.Bitmap(outputPath))
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                // Scan each pixel until the target color is found.
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
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Output the verification result.
        Console.WriteLine($"Bar color verification result: {colorFound}");
    }
}