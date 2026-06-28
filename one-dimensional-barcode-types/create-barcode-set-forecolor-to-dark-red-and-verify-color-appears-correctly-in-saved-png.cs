using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with dark red color and verifies the saved PNG contains the color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it as PNG, and checks for dark red pixels.
    /// </summary>
    static void Main()
    {
        // Define output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a barcode generator for Code128 with the text "Test123"
        // Set the barcode bar color to dark red and save as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkRed;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Flag to indicate whether a dark red pixel was found in the saved image
        bool darkRedFound = false;

        // Load the saved PNG image for pixel inspection
        using (var image = (Bitmap)Image.FromFile(outputPath))
        {
            int width = image.Width;
            int height = image.Height;
            // ARGB value of the target dark red color
            int targetArgb = Aspose.Drawing.Color.DarkRed.ToArgb();

            // Scan each pixel until a matching dark red pixel is found
            for (int y = 0; y < height && !darkRedFound; y++)
            {
                for (int x = 0; x < width && !darkRedFound; x++)
                {
                    // Compare the pixel's ARGB value with the target color
                    if (image.GetPixel(x, y).ToArgb() == targetArgb)
                    {
                        darkRedFound = true;
                    }
                }
            }
        }

        // Output verification result to the console
        Console.WriteLine(darkRedFound
            ? "Dark red color verified in the saved PNG."
            : "Dark red color verification failed.");
    }
}