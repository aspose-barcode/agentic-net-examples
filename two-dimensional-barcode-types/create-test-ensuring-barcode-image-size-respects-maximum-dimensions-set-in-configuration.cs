// Title: Barcode Image Size Validation Against Configured Maximum Dimensions
// Description: Demonstrates how to generate a barcode image with Aspose.BarCode and verify that its dimensions do not exceed the maximum width and height defined in configuration.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to control image size using the AutoSizeMode and dimension parameters. It illustrates typical use cases such as enforcing size constraints for barcode images in automated tests or UI components, leveraging classes like BarcodeGenerator, BarcodeParameters, and Bitmap. Developers often need to ensure generated barcodes fit within layout limits without manual resizing.
// Prompt: Create a test ensuring barcode image size respects maximum dimensions set in configuration.
// Tags: barcode symbology, generation, image size, validation, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode, saves it as PNG, and validates that the image dimensions stay within configured limits.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the barcode size validation test.
    /// </summary>
    static void Main()
    {
        // Configuration: maximum allowed dimensions (pixels)
        const int maxWidth = 300;
        const int maxHeight = 200;

        // Prepare a unique folder for the test files
        string testFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(testFolder);
        string imagePath = Path.Combine(testFolder, "barcode.png");

        try
        {
            // Create a barcode generator for Code128 with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
            {
                // Enforce the maximum dimensions by setting AutoSizeMode and explicit pixel values
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.ImageWidth.Pixels = maxWidth;
                generator.Parameters.ImageHeight.Pixels = maxHeight;

                // Generate the barcode image as a Bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image to the temporary file
                    bitmap.Save(imagePath, ImageFormat.Png);

                    // Retrieve actual dimensions of the generated image
                    int actualWidth = bitmap.Width;
                    int actualHeight = bitmap.Height;

                    // Determine whether the dimensions respect the configured limits
                    bool widthOk = actualWidth <= maxWidth;
                    bool heightOk = actualHeight <= maxHeight;

                    // Output the results
                    Console.WriteLine($"Generated barcode size: {actualWidth}x{actualHeight} pixels");
                    Console.WriteLine($"Maximum allowed size: {maxWidth}x{maxHeight} pixels");
                    Console.WriteLine(widthOk && heightOk
                        ? "Test passed: Image size respects maximum dimensions."
                        : "Test failed: Image size exceeds maximum dimensions.");
                }
            }
        }
        finally
        {
            // Clean up generated files and directories
            if (File.Exists(imagePath))
            {
                try { File.Delete(imagePath); } catch { }
            }
            try { Directory.Delete(testFolder, true); } catch { }
        }
    }
}