using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom bar color,
/// saving it as a PNG file, and verifying that the specified color appears in the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and checks for the presence of the specified bar color.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a barcode generator for Code128 with the data "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode's bar color to blue.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Ensure the barcode image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Error: Barcode image was not created.");
            return;
        }

        // Flag to indicate whether the target color was found in the image.
        bool colorFound = false;

        // Load the saved image for pixel inspection.
        using (var bitmap = new Bitmap(outputPath))
        {
            // Iterate over each pixel until the blue color is found.
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    // Retrieve the color of the current pixel.
                    Color pixelColor = bitmap.GetPixel(x, y);

                    // Compare the pixel's ARGB value with the target blue color.
                    if (pixelColor.ToArgb() == Aspose.Drawing.Color.Blue.ToArgb())
                    {
                        colorFound = true; // Blue color detected.
                    }
                }
            }
        }

        // Output the verification result to the console.
        Console.WriteLine(colorFound
            ? "Success: The barcode image contains the specified bar color."
            : "Failure: The specified bar color was not found in the barcode image.");
    }
}