using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a low‑resolution barcode image using Aspose.BarCode
/// and verifies its DPI settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode at 72 DPI, saves it to a file,
    /// and validates the image resolution.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "lowres_barcode.png";

        // Create a barcode generator for Code128 with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set low resolution (72 DPI) to meet low‑resolution display requirements.
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Load the saved image to inspect its resolution properties.
        using (var image = Image.FromFile(outputPath))
        {
            // Aspose.Drawing.Image provides HorizontalResolution and VerticalResolution properties.
            float horizDpi = image.HorizontalResolution;
            float vertDpi = image.VerticalResolution;

            Console.WriteLine($"Barcode image saved to '{outputPath}'.");
            Console.WriteLine($"Image resolution: {horizDpi} DPI (horizontal), {vertDpi} DPI (vertical).");

            // Simple validation: both dimensions should be approximately 72 DPI.
            if (Math.Abs(horizDpi - 72f) < 0.01f && Math.Abs(vertDpi - 72f) < 0.01f)
            {
                Console.WriteLine("Resolution verification passed: image is 72 DPI.");
            }
            else
            {
                Console.WriteLine("Resolution verification failed: image DPI does not match 72.");
            }
        }
    }
}