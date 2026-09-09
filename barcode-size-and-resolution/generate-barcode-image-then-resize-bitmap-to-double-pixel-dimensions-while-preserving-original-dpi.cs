// Title: Generate and Resize Barcode Image with Preserved DPI
// Description: Creates a Code128 barcode, saves it as PNG, then doubles its pixel dimensions while keeping the original DPI.
// Category-Description: This example belongs to the Aspose.BarCode generation and image processing category. It demonstrates using BarcodeGenerator (Aspose.BarCode.Generation) to create a barcode, saving it with Aspose.Drawing.Bitmap, and then resizing the bitmap using Aspose.Drawing.Graphics while preserving the original resolution. Developers often need to generate barcodes for labeling and then adjust image size for different display or print requirements without altering DPI.
// Prompt: Generate barcode image, then resize bitmap to double pixel dimensions while preserving original DPI.
// Tags: barcode, code128, generate, resize, dpi, bitmap, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode image and resizing it while preserving DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, saves original and resized images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the original and resized barcode images
        string originalPath = Path.Combine(outputDir, "barcode_original.png");
        string resizedPath = Path.Combine(outputDir, "barcode_resized.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution (DPI) for the generated barcode
            generator.Parameters.Resolution = 300f;

            // Generate the barcode as a bitmap
            using (Bitmap original = generator.GenerateBarCodeImage())
            {
                // Save the original barcode image to PNG
                original.Save(originalPath, ImageFormat.Png);

                // Calculate new dimensions (double the width and height)
                int newWidth = original.Width * 2;
                int newHeight = original.Height * 2;

                // Create a new bitmap with the enlarged dimensions
                using (Bitmap resized = new Bitmap(newWidth, newHeight))
                {
                    // Preserve the original DPI settings
                    resized.SetResolution(original.HorizontalResolution, original.VerticalResolution);

                    // Draw the original image onto the resized bitmap, scaling it
                    using (Graphics graphics = Graphics.FromImage(resized))
                    {
                        graphics.DrawImage(original, 0, 0, newWidth, newHeight);
                    }

                    // Save the resized barcode image to PNG
                    resized.Save(resizedPath, ImageFormat.Png);
                }
            }
        }

        // Output the locations of the saved images
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Resized barcode saved to: {resizedPath}");
    }
}