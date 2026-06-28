using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode image with size constraints using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, verifies its dimensions, and cleans up the file.
    /// </summary>
    static void Main()
    {
        // Configuration: maximum allowed dimensions (in points)
        float maxWidth = 200f;
        float maxHeight = 100f;

        // Output file path for the generated barcode image
        string outputPath = "barcode.png";

        // Generate barcode with AutoSizeMode.Nearest so actual size will not exceed the targets
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set auto-size mode to ensure the image fits within the specified dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Define the maximum width and height in points
            generator.Parameters.ImageWidth.Point = maxWidth;
            generator.Parameters.ImageHeight.Point = maxHeight;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Verify that the saved image respects the maximum dimensions
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image to inspect its actual pixel dimensions
        using (var image = Image.FromFile(outputPath))
        {
            int actualWidth = image.Width;
            int actualHeight = image.Height;

            // Determine whether the actual dimensions are within the configured limits
            bool widthOk = actualWidth <= (int)maxWidth;
            bool heightOk = actualHeight <= (int)maxHeight;

            // Output the results to the console
            Console.WriteLine($"Actual size: {actualWidth}x{actualHeight} pixels");
            Console.WriteLine($"Maximum allowed: {maxWidth}x{maxHeight} points");
            Console.WriteLine($"Width within limit: {widthOk}");
            Console.WriteLine($"Height within limit: {heightOk}");
        }

        // Optional cleanup: delete the generated image file
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Ignore any cleanup errors (e.g., file in use)
        }
    }
}