using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom foreground and background colors,
/// saving it to a file, and verifying the colors by inspecting pixel data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, saves it, and validates the custom colors.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string imagePath = "barcode_custom.png";

        // Create a BarcodeGenerator for Code128 with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Set the background color to yellow.
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the generated barcode image to the specified file.
            generator.Save(imagePath);
        }

        // Ensure the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Load the saved image for pixel-level color verification.
        using (var bitmap = new Bitmap(imagePath))
        {
            // Retrieve the top-left pixel, which should represent the background color.
            Aspose.Drawing.Color bgPixel = bitmap.GetPixel(0, 0);
            bool bgMatches = bgPixel.Equals(Aspose.Drawing.Color.Yellow);

            // Determine a point near the image center, likely part of a barcode bar.
            int cx = bitmap.Width / 2;
            int cy = bitmap.Height / 2;

            // Retrieve the pixel at the center point, which should be the foreground color.
            Aspose.Drawing.Color fgPixel = bitmap.GetPixel(cx, cy);
            bool fgMatches = fgPixel.Equals(Aspose.Drawing.Color.Blue);

            // Output the results of the color checks.
            Console.WriteLine($"Background pixel at (0,0): {bgPixel} (expected Yellow) - Match: {bgMatches}");
            Console.WriteLine($"Foreground pixel at ({cx},{cy}): {fgPixel} (expected Blue) - Match: {fgMatches}");

            // Report overall verification status.
            if (bgMatches && fgMatches)
            {
                Console.WriteLine("Custom colors confirmed successfully.");
            }
            else
            {
                Console.WriteLine("Color verification failed.");
            }
        }
    }
}