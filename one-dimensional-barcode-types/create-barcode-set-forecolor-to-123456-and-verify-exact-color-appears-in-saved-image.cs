// Title: Generate Code128 Barcode with Custom Foreground Color and Verify Color Presence
// Description: This example creates a Code128 barcode, sets its foreground color to the hexadecimal value #123456, saves it as a PNG, and checks that the saved image contains the exact color.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and image analysis, focusing on the BarcodeGenerator class, BarCodeImageFormat, and Aspose.Drawing bitmap handling. Typical use cases include customizing barcode appearance and programmatically validating visual properties, which developers often need when integrating barcodes into branding or UI workflows.
// Prompt: Create a barcode, set ForeColor to #123456, and verify the exact color appears in the saved image.
// Tags: barcode, code128, forecolor, color verification, png, aspose.barcode, aspose.drawing, generation, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode with a custom foreground color and verifying the color in the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and validates the color.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory to store the generated barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Define the full path for the output PNG file
        string imagePath = Path.Combine(tempDir, "barcode.png");

        // Create a Code128 barcode with the text "123456" and set its foreground color to #123456
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0x12, 0x34, 0x56);
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the exact foreground color appears somewhere in the saved image
        bool colorFound = false;
        Color expectedColor = Color.FromArgb(0x12, 0x34, 0x56);

        using (var bitmap = new Bitmap(imagePath))
        {
            // Scan each pixel until the expected color is found
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    if (bitmap.GetPixel(x, y).ToArgb() == expectedColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }
        }

        // Output the verification result to the console
        Console.WriteLine(colorFound
            ? "Success: Expected color #123456 found in the barcode image."
            : "Failure: Expected color #123456 not found in the barcode image.");
    }
}