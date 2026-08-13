// Title: Generate a Postnet postal barcode with custom margins and verify image size
// Description: Demonstrates how to create a Postnet barcode, apply custom padding, set image dimensions, and confirm the saved PNG matches expected pixel size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing barcode creation, layout customization, and image export. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure symbology, margins, and output format—common tasks for developers integrating barcode printing or validation into applications.
// Prompt: Generate a postal barcode with custom margin settings and verify image dimensions match expectations.
// Tags: postnet, margin, image, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Postnet postal barcode with custom margins,
/// saves it as a PNG file, and verifies the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saves the image,
    /// and checks that the image size matches the expected dimensions.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "postal_barcode.png";

        // Remove any existing file to ensure a clean run
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a BarcodeGenerator for the Postnet symbology with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345678"))
        {
            // Configure custom margins (padding) in points
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 15f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 15f;

            // Set the desired image size (including margins) in points
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the barcode image to the specified path in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to verify its dimensions
        using (Bitmap bitmap = new Bitmap(outputPath))
        {
            // Calculate expected pixel dimensions.
            // Aspose.Drawing uses pixels; points are converted using the default DPI (96).
            const float dpi = 96f;
            int expectedWidth = (int)Math.Round(300f * dpi / 72f);
            int expectedHeight = (int)Math.Round(150f * dpi / 72f);

            // Output actual vs. expected dimensions for diagnostic purposes
            Console.WriteLine($"Actual Width: {bitmap.Width}px, Expected Width: {expectedWidth}px");
            Console.WriteLine($"Actual Height: {bitmap.Height}px, Expected Height: {expectedHeight}px");

            // Determine whether the dimensions match the expectations
            bool sizeMatches = bitmap.Width == expectedWidth && bitmap.Height == expectedHeight;
            Console.WriteLine(sizeMatches
                ? "Image dimensions match the expectations."
                : "Image dimensions do NOT match the expectations.");
        }
    }
}