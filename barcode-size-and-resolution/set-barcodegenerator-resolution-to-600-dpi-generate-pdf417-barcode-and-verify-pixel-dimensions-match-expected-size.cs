using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode with specific dimensions and verifying the output image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PDF417 barcode, saves it as a PNG,
    /// and checks that the image dimensions match the expected size based on resolution and point measurements.
    /// </summary>
    static void Main()
    {
        // Define the temporary output file path for the generated barcode image.
        string outputPath = Path.Combine(Path.GetTempPath(), "pdf417.png");

        // Create a BarcodeGenerator for PDF417 encoding with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Set the image resolution to 600 dots per inch.
            generator.Parameters.Resolution = 600f;

            // Use interpolation mode so that ImageWidth/ImageHeight values control the final image size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Specify the desired image size in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 300f;   // 300 pt ≈ 4.166 in
            generator.Parameters.ImageHeight.Point = 150f;  // 150 pt ≈ 2.083 in

            // Save the generated barcode as a PNG file at the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to verify its actual pixel dimensions.
        using (var image = Image.FromFile(outputPath))
        {
            int actualWidth = image.Width;   // Width in pixels
            int actualHeight = image.Height; // Height in pixels

            // Calculate the expected pixel dimensions based on points and resolution.
            int expectedWidth = (int)Math.Round(300f * 600f / 72f);
            int expectedHeight = (int)Math.Round(150f * 600f / 72f);

            // Output actual and expected dimensions to the console.
            Console.WriteLine($"Actual dimensions: {actualWidth}x{actualHeight} pixels");
            Console.WriteLine($"Expected dimensions: {expectedWidth}x{expectedHeight} pixels");

            // Determine whether the actual dimensions match the expected values.
            bool widthMatch = actualWidth == expectedWidth;
            bool heightMatch = actualHeight == expectedHeight;

            // Report the comparison results.
            Console.WriteLine($"Width match: {widthMatch}");
            Console.WriteLine($"Height match: {heightMatch}");
        }
    }
}