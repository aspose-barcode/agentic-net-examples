// Title: Retrieve and verify barcode image dimensions
// Description: Demonstrates how to generate a barcode with specific width and height, then programmatically confirm the resulting image dimensions match the requested size.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image parameter settings to control output size. Developers often need to ensure generated barcode images meet exact dimension requirements for layout or printing, and this snippet shows how to validate those dimensions using Aspose.Drawing.Imaging.
// Prompt: Programmatically retrieve the generated barcode image dimensions to confirm they match the specified ImageWidth and ImageHeight.
// Tags: code128, image-size, png, barcodelibrary, barcodegenerator, parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with specified dimensions
/// and verifies that the resulting image size matches the requested width and height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and checks its dimensions.
    /// </summary>
    static void Main()
    {
        // Desired dimensions in points (1 point = 1/72 inch)
        const float desiredWidth = 300f;
        const float desiredHeight = 150f;
        const string outputFile = "barcode.png";

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure AutoSizeMode to use interpolation so ImageWidth/ImageHeight control the size
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = desiredWidth;
            generator.Parameters.ImageHeight.Point = desiredHeight;

            // Generate the barcode image in memory
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to disk (optional, for visual verification)
                bitmap.Save(outputFile, ImageFormat.Png);

                // Retrieve the actual pixel dimensions of the generated image
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Compare actual dimensions with the expected values (rounded to nearest integer)
                bool widthMatches = actualWidth == (int)Math.Round(desiredWidth);
                bool heightMatches = actualHeight == (int)Math.Round(desiredHeight);

                // Output the comparison results
                Console.WriteLine($"Expected Width: {desiredWidth} pt, Actual Width: {actualWidth} px, Match: {widthMatches}");
                Console.WriteLine($"Expected Height: {desiredHeight} pt, Actual Height: {actualHeight} px, Match: {heightMatches}");
            }
        }
    }
}