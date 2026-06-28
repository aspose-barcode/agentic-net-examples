using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Postnet barcode with custom dimensions and padding,
/// saving it as a PNG file, and verifying the resulting image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it, and checks the image dimensions.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "postal.png");

        // Create a Postnet barcode generator with the sample code "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set custom margins (padding) in points for each side of the barcode.
            generator.Parameters.Barcode.Padding.Left.Point   = 10f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Disable automatic sizing so that explicit image dimensions are used.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Specify the desired image size in points.
            generator.Parameters.ImageWidth.Point  = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Load the saved image to inspect its actual pixel dimensions.
        using (var image = Image.FromFile(outputPath))
        {
            int actualWidth  = image.Width;
            int actualHeight = image.Height;

            Console.WriteLine($"Generated image size: {actualWidth}x{actualHeight} pixels.");

            // Calculate expected pixel dimensions.
            // Points are converted to pixels based on the default resolution (96 DPI):
            // 1 point = 1/72 inch, therefore 1 point ≈ 96/72 = 1.3333 pixels.
            int expectedWidth  = (int)Math.Round(300.0 * 96.0 / 72.0);
            int expectedHeight = (int)Math.Round(150.0 * 96.0 / 72.0);

            // Determine whether the actual dimensions match the expectations.
            bool sizeMatches = actualWidth == expectedWidth && actualHeight == expectedHeight;

            Console.WriteLine(sizeMatches
                ? "Image dimensions match expectations."
                : $"Image dimensions do NOT match expectations (expected {expectedWidth}x{expectedHeight}).");
        }
    }
}