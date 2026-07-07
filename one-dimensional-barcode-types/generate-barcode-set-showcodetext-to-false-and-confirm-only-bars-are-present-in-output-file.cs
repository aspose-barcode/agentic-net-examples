// Title: Generate Code128 barcode without human‑readable text and verify bar‑only output
// Description: This example creates a Code128 barcode, disables the code text display, saves it as a PNG file, and checks that the image contains only the bar and background colors.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on hiding the human‑readable code text (ShowCodeText) for a clean barcode image. It uses BarcodeGenerator, EncodeTypes, and barcode parameters to customize appearance, then validates the output using Aspose.Drawing bitmap analysis. Ideal for developers needing bar‑only images for printing or scanning workflows.
// Prompt: Generate a barcode, set ShowCodeText to false, and confirm only bars are present in the output file.
// Tags: code128, hidecodetext, png, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode without displaying the code text,
/// saves it as a PNG file, and verifies that the resulting image contains only the bar
/// color and the background color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the image,
    /// and runs a simple verification of the pixel colors.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Hide the human‑readable text (show only the bars).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Optional: set the bar color to black and the background to white.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the barcode image to the specified path (PNG format by default).
            generator.Save(outputPath);
        }

        // Verify that the saved image file exists.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create the barcode image at '{outputPath}'.");
            return;
        }

        // Load the saved image for pixel analysis.
        using (var bitmap = new Bitmap(outputPath))
        {
            var distinctColors = new HashSet<Color>();

            // Scan all pixels (acceptable for small images) to collect distinct colors.
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    distinctColors.Add(bitmap.GetPixel(x, y));

                    // Early exit if more than two distinct colors are found.
                    if (distinctColors.Count > 2)
                        break;
                }

                if (distinctColors.Count > 2)
                    break;
            }

            // Output verification result based on the number of distinct colors.
            if (distinctColors.Count <= 2)
                Console.WriteLine("Verification passed: only bars (and background) are present in the output file.");
            else
                Console.WriteLine("Verification failed: additional elements detected in the output file.");
        }
    }
}