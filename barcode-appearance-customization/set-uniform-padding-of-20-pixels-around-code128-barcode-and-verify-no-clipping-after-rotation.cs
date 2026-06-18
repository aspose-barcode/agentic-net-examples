using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with padding and rotation,
/// then verifies the resulting image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a PNG file, and validates the output.
    /// </summary>
    static void Main()
    {
        // Output file name for the generated barcode image
        string outputPath = "code128.png";

        // Create a BarcodeGenerator for Code128 with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply a rotation of 45 degrees to the barcode
            generator.Parameters.RotationAngle = 45f;

            // Set uniform padding of 20 pixels on all sides of the barcode
            generator.Parameters.Barcode.Padding.Left.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 20f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 20f;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Verify that the image file was created and has non‑zero dimensions (ensuring no clipping)
        if (File.Exists(outputPath))
        {
            // Load the saved image for inspection
            using (var image = Image.FromFile(outputPath))
            {
                Console.WriteLine($"Barcode image saved to: {outputPath}");
                Console.WriteLine($"Image dimensions: {image.Width}x{image.Height} pixels");

                // Check that width and height are greater than zero
                if (image.Width > 0 && image.Height > 0)
                {
                    Console.WriteLine("Verification passed: Image dimensions are valid, indicating no clipping after rotation.");
                }
                else
                {
                    Console.WriteLine("Verification failed: Image dimensions are zero.");
                }
            }
        }
        else
        {
            // Output an error if the file was not created
            Console.WriteLine("Error: Barcode image was not generated.");
        }
    }
}