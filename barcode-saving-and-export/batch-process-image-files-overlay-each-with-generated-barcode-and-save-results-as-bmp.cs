// Title: Batch barcode overlay on images
// Description: Demonstrates loading up to five images, generating a Code128 barcode, overlaying it on each image, and saving the result as BMP files.
// Prompt: Batch process image files, overlay each with a generated barcode, and save the results as BMP.
// Tags: barcode, code128, overlay, batch, bmp, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that processes a set of image files, adds a generated barcode to each,
/// and saves the combined image as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs batch processing of images with barcode overlay.
    /// </summary>
    static void Main()
    {
        // Define input and output directories (adjust paths as needed)
        string inputDir = "input_images";
        string outputDir = "output_images";

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Verify that the input directory exists before proceeding
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Retrieve up to five image files with supported extensions from the input directory
        string[] files = Directory.GetFiles(inputDir, "*.*")
            .Where(f => f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            .Take(5)
            .ToArray();

        // If no images were found, inform the user and exit
        if (files.Length == 0)
        {
            Console.WriteLine("No image files found to process.");
            return;
        }

        // Process each image file individually
        foreach (string filePath in files)
        {
            // Derive the output file name by appending "_barcode" and changing the extension to BMP
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string outputPath = Path.Combine(outputDir, $"{fileName}_barcode.bmp");

            // Load the original image from disk
            using (Image baseImage = Image.FromFile(filePath))
            {
                // Create a barcode generator for Code128 with sample text
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    // Optional: configure barcode size and scaling mode
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                    generator.Parameters.ImageWidth.Point = 200f;   // Desired barcode width
                    generator.Parameters.ImageHeight.Point = 80f;   // Desired barcode height

                    // Generate the barcode as a bitmap image
                    using (Bitmap barcodeBitmap = generator.GenerateBarCodeImage())
                    {
                        // Draw the barcode onto the base image using graphics context
                        using (Graphics graphics = Graphics.FromImage(baseImage))
                        {
                            // Position the barcode at the bottom‑right corner with a 10‑pixel margin
                            int x = baseImage.Width - barcodeBitmap.Width - 10;
                            int y = baseImage.Height - barcodeBitmap.Height - 10;
                            graphics.DrawImage(barcodeBitmap, x, y, barcodeBitmap.Width, barcodeBitmap.Height);
                        }

                        // Save the combined image as a BMP file
                        baseImage.Save(outputPath, ImageFormat.Bmp);
                        Console.WriteLine($"Processed and saved: {outputPath}");
                    }
                }
            }
        }
    }
}