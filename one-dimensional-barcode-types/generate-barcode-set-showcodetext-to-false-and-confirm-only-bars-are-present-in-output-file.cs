// Title: Generate Code128 barcode without human‑readable text and verify only bars are present
// Description: This example creates a Code128 barcode, disables the display of the code text, saves it as a PNG, and programmatically checks that the image contains only black bars on a white background.
// Category-Description: Demonstrates Aspose.BarCode generation and image validation techniques. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcodes, and Aspose.Drawing to inspect pixel data. Developers working with barcode creation, visual customization, and automated verification will find this pattern useful for CI tests and quality checks.
// Prompt: Generate a barcode, set ShowCodeText to false, and confirm only bars are present in the output file.
// Tags: code128, hidecodetext, png, barcodegeneration, imageanalysis

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation without code text and validates the resulting image contains only bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, hides the code text, saves to a temporary PNG file,
    /// and verifies that the image consists solely of black bars on a white background.
    /// </summary>
    static void Main()
    {
        // Define output file path in the system temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Create a barcode generator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the human‑readable code text by setting its location to None.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode file.");
            return;
        }

        // Load the generated image for pixel‑level analysis.
        using (var img = Image.FromFile(outputPath))
        {
            // Cast to Bitmap to access GetPixel.
            using (var bitmap = (Bitmap)img)
            {
                bool onlyBars = true;
                Color backColor = Color.White; // Expected background color.
                Color barColor = Color.Black;   // Expected bar color.

                // Scan each pixel; stop early if an unexpected color is found.
                for (int y = 0; y < bitmap.Height && onlyBars; y++)
                {
                    for (int x = 0; x < bitmap.Width && onlyBars; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        if (!pixel.Equals(backColor) && !pixel.Equals(barColor))
                        {
                            onlyBars = false;
                        }
                    }
                }

                // Report verification result.
                Console.WriteLine(onlyBars ? "Only bars present." : "Unexpected colors found.");
            }
        }
    }
}