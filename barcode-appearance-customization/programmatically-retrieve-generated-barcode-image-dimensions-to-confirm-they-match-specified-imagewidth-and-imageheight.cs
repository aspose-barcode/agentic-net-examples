// Title: Retrieve Generated Barcode Image Dimensions
// Description: Demonstrates how to generate a barcode with specific image dimensions and programmatically verify the resulting image size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, AutoSizeMode, and image sizing properties (ImageWidth, ImageHeight). Developers often need to create barcodes that fit exact layout constraints, such as printing labels or embedding images in documents, and must confirm that the generated bitmap matches those constraints.
// Prompt: Programmatically retrieve the generated barcode image dimensions to confirm they match the specified ImageWidth and ImageHeight.
// Tags: barcode, generation, image dimensions, autosizemode, code128, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with predefined image dimensions
/// and validates that the produced bitmap matches those dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, checks its size, and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Desired dimensions for the barcode image (in pixels)
        float desiredWidth = 300f;
        float desiredHeight = 150f;

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the generator to respect the explicit ImageWidth and ImageHeight values
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Pixels = desiredWidth;
            generator.Parameters.ImageHeight.Pixels = desiredHeight;

            // Generate the barcode image as a bitmap
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve actual dimensions of the generated bitmap
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Output the dimensions to the console for verification
                Console.WriteLine($"Generated image size: {actualWidth}x{actualHeight}");
                bool matches = actualWidth == (int)desiredWidth && actualHeight == (int)desiredHeight;
                Console.WriteLine($"Matches specified dimensions: {matches}");

                // Save the bitmap to a PNG file for visual inspection
                using (var fileStream = new FileStream("barcode.png", FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }
            }
        }
    }
}