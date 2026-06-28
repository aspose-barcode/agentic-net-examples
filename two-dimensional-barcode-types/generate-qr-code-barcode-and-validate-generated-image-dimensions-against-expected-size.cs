using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with specific dimensions and validating the output image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, saves it to a file, and verifies the image dimensions.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Desired size of the QR code in points (1 point = 1/72 inch).
        float targetSizePoints = 300f;

        // Resolution in dots per inch (dpi) used for converting points to pixels.
        float resolutionDpi = 96f;

        // Calculate the expected pixel dimension based on points and resolution.
        int expectedPixels = (int)Math.Round(targetSizePoints * resolutionDpi / 72.0);

        // Generate the QR code with the specified image dimensions.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Use interpolation mode so that ImageWidth/ImageHeight are respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the image width and height in points.
            generator.Parameters.ImageWidth.Point = targetSizePoints;
            generator.Parameters.ImageHeight.Point = targetSizePoints;

            // Set the image resolution (dpi) for accurate pixel conversion.
            generator.Parameters.Resolution = resolutionDpi;

            // Save the generated QR code image to the specified path.
            generator.Save(outputPath);
        }

        // Load the saved image to validate its actual pixel dimensions.
        using (var image = Image.FromFile(outputPath))
        {
            int actualWidth = image.Width;
            int actualHeight = image.Height;

            // Determine whether the actual dimensions match the expected pixel size.
            bool widthMatch = actualWidth == expectedPixels;
            bool heightMatch = actualHeight == expectedPixels;

            // Output the results to the console.
            Console.WriteLine($"Actual size: {actualWidth}x{actualHeight} pixels");
            Console.WriteLine($"Expected size: {expectedPixels}x{expectedPixels} pixels");
            Console.WriteLine($"Width match: {widthMatch}, Height match: {heightMatch}");
        }
    }
}