// Title: Verify barcode bar color in generated PNG
// Description: Demonstrates generating a Code128 barcode with a custom bar color and programmatically confirming the color exists in the saved PNG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation and recognition category, illustrating how to use BarcodeGenerator to set visual properties (e.g., BarColor) and how to employ Aspose.Drawing.Bitmap for pixel-level inspection. Developers often need to customize barcode appearance and validate output images in automated tests or CI pipelines.
// Prompt: Verify that the generated PNG image contains the specified bar color using pixel inspection.
// Tags: barcode, code128, barcolor, png, pixel-inspection, aspose.barcode, aspose.drawing, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode with a custom bar color, saves it as PNG,
/// and verifies that the specified color appears in the resulting image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, saving, and color verification.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to be encoded in the barcode.
            generator.CodeText = "1234567890";

            // Apply a custom bar color (red) to the barcode.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the image file was created before attempting verification.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Error: Barcode image file was not created.");
            return;
        }

        // Flag indicating whether the expected color was found in the image.
        bool colorFound = false;
        Aspose.Drawing.Color expectedColor = Aspose.Drawing.Color.Red;

        // Load the PNG image for pixel-level inspection.
        using (var bitmap = new Bitmap(outputPath))
        {
            // Iterate over each pixel until the expected color is found.
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    Aspose.Drawing.Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.ToArgb() == expectedColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        // Output the verification result to the console.
        Console.WriteLine(colorFound
            ? "Verification succeeded: Bar color is present in the image."
            : "Verification failed: Bar color not found in the image.");
    }
}