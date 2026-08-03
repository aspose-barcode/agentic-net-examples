// Title: Batch barcode overlay on images
// Description: Demonstrates how to batch‑process image files, overlay each with a generated Code128 barcode derived from the file name, and save the result as BMP.
// Category-Description: This example belongs to the Aspose.BarCode image manipulation category, illustrating the use of BarcodeGenerator, BarCodeImageFormat, and Aspose.Drawing classes to create barcodes, render them to streams, and composite them onto existing images. Typical use cases include watermarking product photos with SKU barcodes or adding machine‑readable identifiers to documents. Developers often need to automate such batch operations for inventory, labeling, or archival workflows.
// Prompt: Batch process image files, overlay each with a generated barcode, and save the results as BMP.
// Tags: barcode, code128, overlay, batch, bmp, aspose.barcode, aspose.drawing, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that batch processes images, overlays each with a generated barcode,
/// and saves the combined result as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs folder setup, sample image creation,
    /// barcode generation, image compositing, and output saving.
    /// </summary>
    static void Main()
    {
        // Define input and output folders relative to the current directory
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "input_images");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "output_images");

        // Ensure the input and output directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Prepare sample images if the input folder is empty (self‑contained example)
        string[] samplePatterns = new[] { "*.png", "*.jpg", "*.bmp" };
        bool anyImageExists = false;
        foreach (var pattern in samplePatterns)
        {
            if (Directory.GetFiles(inputFolder, pattern).Length > 0)
            {
                anyImageExists = true;
                break;
            }
        }

        if (!anyImageExists)
        {
            // Create 5 simple placeholder images
            for (int i = 1; i <= 5; i++)
            {
                string samplePath = Path.Combine(inputFolder, $"sample{i}.png");
                using (var bmp = new Bitmap(300, 200))
                {
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.LightGray);
                        g.DrawString($"Sample {i}", new Font("Arial", 24f), new SolidBrush(Color.Black), new PointF(50f, 80f));
                    }
                    bmp.Save(samplePath, ImageFormat.Png);
                }
            }
        }

        // Iterate over each supported image pattern
        foreach (var pattern in samplePatterns)
        {
            // Process every file that matches the current pattern
            foreach (var imagePath in Directory.GetFiles(inputFolder, pattern))
            {
                try
                {
                    // Load the original image from disk
                    using (var original = (Bitmap)Image.FromFile(imagePath))
                    {
                        // Use the file name (without extension) as the barcode text
                        string codeText = Path.GetFileNameWithoutExtension(imagePath);

                        // Create a Code128 barcode generator
                        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                        {
                            // Optional: adjust the module size for better readability
                            generator.Parameters.Barcode.XDimension.Point = 2f;

                            // Render the barcode to a memory stream in PNG format
                            using (var barcodeStream = new MemoryStream())
                            {
                                generator.Save(barcodeStream, BarCodeImageFormat.Png);
                                barcodeStream.Position = 0;

                                // Load the rendered barcode image from the stream
                                using (var barcodeImage = (Bitmap)Image.FromStream(barcodeStream))
                                {
                                    // Calculate bottom‑right position with a 10‑pixel margin
                                    int margin = 10;
                                    int xPos = original.Width - barcodeImage.Width - margin;
                                    int yPos = original.Height - barcodeImage.Height - margin;
                                    if (xPos < 0) xPos = 0;
                                    if (yPos < 0) yPos = 0;

                                    // Draw the barcode onto the original image
                                    using (var graphics = Graphics.FromImage(original))
                                    {
                                        graphics.DrawImage(barcodeImage, xPos, yPos, barcodeImage.Width, barcodeImage.Height);
                                    }
                                }
                            }
                        }

                        // Build the output file name and path
                        string outputFileName = Path.GetFileNameWithoutExtension(imagePath) + "_with_barcode.bmp";
                        string outputPath = Path.Combine(outputFolder, outputFileName);

                        // Save the combined image as BMP
                        original.Save(outputPath, ImageFormat.Bmp);
                        Console.WriteLine($"Processed and saved: {outputPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Log any errors that occur during processing of a single file
                    Console.WriteLine($"Error processing '{imagePath}': {ex.Message}");
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}