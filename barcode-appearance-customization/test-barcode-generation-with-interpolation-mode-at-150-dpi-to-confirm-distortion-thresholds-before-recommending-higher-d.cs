using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with interpolation mode and DPI settings,
/// then reads back the image to display its dimensions and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, saves it as PNG, and reports image properties.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "interpolation_barcode.png";

        // Create a barcode generator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Configure the generator to use interpolation for auto-sizing.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired resolution (dots per inch) for the output image.
            generator.Parameters.Resolution = 150f; // DPI

            // Specify the target image width in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 300f;

            // Specify the target image height in points.
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode to a PNG file using the defined settings.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to retrieve and display its pixel dimensions and DPI.
        using (var image = Image.FromFile(outputPath))
        {
            // Output the width and height in pixels.
            Console.WriteLine($"Image size: {image.Width}x{image.Height} pixels");

            // Output the horizontal resolution (DPI).
            Console.WriteLine($"Horizontal DPI: {image.HorizontalResolution}");

            // Output the vertical resolution (DPI).
            Console.WriteLine($"Vertical DPI: {image.VerticalResolution}");
        }

        // Inform the user that the process has completed successfully.
        Console.WriteLine("Barcode generation with interpolation at 150 DPI completed.");
    }
}