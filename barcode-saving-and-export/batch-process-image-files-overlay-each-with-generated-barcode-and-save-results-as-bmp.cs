// Title: Batch overlay barcode onto images and save as BMP
// Description: Demonstrates generating a Code128 barcode, overlaying it onto multiple image files, and saving the combined results as BMP files.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing how to use BarcodeGenerator (Aspose.BarCode.Generation) together with Aspose.Drawing to create barcodes, draw them onto existing images, and export the result. Typical use cases include batch labeling, product packaging, and document augmentation where each image needs a unique or common barcode overlay. Developers often need to combine barcode generation with graphics manipulation to automate bulk image preparation.
/// Prompt: Batch process image files, overlay each with a generated barcode, and save the results as BMP.
/// Tags: barcode, code128, overlay, batch, bmp, aspose.barcode, image-processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch processing of images by overlaying a generated barcode onto each image
/// and saving the result as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary input images, generates a barcode,
    /// draws it onto each image, and saves the combined images as BMP files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for input images
        string inputFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        // Create a unique temporary folder for output images
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Generate sample input images (PNG format)
        List<string> inputFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"sample{i}.png");
            using (Bitmap bmp = new Bitmap(300, 200))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Fill background with light gray color
                    g.Clear(Color.LightGray);
                }
                // Save the placeholder image as PNG
                bmp.Save(filePath, ImageFormat.Png);
            }
            inputFiles.Add(filePath);
        }

        // Process each image: overlay barcode and save as BMP
        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the original image
                using (Bitmap image = new Bitmap(inputPath))
                {
                    // Generate barcode image using Code128 symbology
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                    {
                        // Optional: set barcode color to black
                        generator.Parameters.Barcode.BarColor = Color.Black;

                        // Create the barcode bitmap
                        using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                        {
                            // Draw the barcode onto the original image at the top-left corner
                            using (Graphics graphics = Graphics.FromImage(image))
                            {
                                graphics.DrawImage(barcodeBmp, 0, 0, barcodeBmp.Width, barcodeBmp.Height);
                            }
                        }
                    }

                    // Build output file path with "_barcode.bmp" suffix
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPath) + "_barcode.bmp");
                    // Save the combined image as BMP
                    image.Save(outputPath, ImageFormat.Bmp);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {inputPath}: {ex.Message}");
            }
        }

        // Cleanup: (optional) delete temporary folders
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}