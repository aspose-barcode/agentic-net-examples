// Title: Batch overlay of barcodes onto images and save as BMP
// Description: Demonstrates how to batch‑process image files, overlay each with a Code128 barcode, and store the results in BMP format.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, showcasing the use of BarcodeGenerator, Bitmap, and Graphics classes to embed barcodes onto existing images. Typical use cases include batch labeling, document stamping, and automated asset tagging where developers need to programmatically add barcodes to multiple images.
// Prompt: Batch process image files, overlay each with a generated barcode, and save the results as BMP.
// Tags: code128, overlay, bmp, barcodegenerator, bitmap, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates batch processing of images by overlaying a generated barcode onto each image and saving the result as BMP.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates sample images, generates a barcode, overlays it, and writes the output files.
    /// </summary>
    static void Main()
    {
        // Create temporary input and output folders for the batch operation
        string inputFolder = Path.Combine(Path.GetTempPath(), "BatchInput_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Generate a few sample PNG images to act as input files
        List<string> inputFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"sample{i}.png");
            using (Bitmap bmp = new Bitmap(300, 200))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Fill the image with a white background
                    g.Clear(Aspose.Drawing.Color.White);
                }

                // Save the bitmap as a PNG file
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    bmp.Save(fs, ImageFormat.Png);
                }
            }
            inputFiles.Add(filePath);
        }

        // Process each image: overlay a barcode and save the result as BMP
        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the base image onto which the barcode will be drawn
                using (Bitmap baseImage = new Bitmap(inputPath))
                {
                    // Create a barcode generator for Code128 with sample data
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                    {
                        // Optional: adjust barcode appearance (e.g., X-dimension)
                        generator.Parameters.Barcode.XDimension.Point = 2f;

                        // Generate the barcode as a bitmap
                        using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                        {
                            // Calculate bottom‑right position with a 10‑pixel margin
                            int x = baseImage.Width - barcodeImage.Width - 10;
                            int y = baseImage.Height - barcodeImage.Height - 10;
                            if (x < 0) x = 0;
                            if (y < 0) y = 0;

                            // Draw the barcode onto the base image
                            using (Graphics graphics = Graphics.FromImage(baseImage))
                            {
                                graphics.DrawImage(barcodeImage, x, y);
                            }
                        }
                    }

                    // Build the output file path and save the combined image as BMP
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(inputPath) + "_with_barcode.bmp");
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        baseImage.Save(outStream, ImageFormat.Bmp);
                    }

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the input file cannot be loaded as an image
                Console.WriteLine($"Skipping file due to load error: {inputPath}. Message: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling for unexpected issues
                Console.WriteLine($"Error processing file {inputPath}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}