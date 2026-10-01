// Title: Batch overlay barcode onto images and save as BMP
// Description: Demonstrates generating a Code128 barcode for each input image, overlaying it onto the image, and saving the combined result as a BMP file.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing how to use BarcodeGenerator (Aspose.BarCode.Generation) together with Aspose.Drawing to create barcodes, overlay them on existing images, and export the final composition. Typical use cases include batch labeling of product photos, adding QR or linear barcodes to documents, and preparing assets for printing or archival. Developers often need to automate barcode generation, manipulate graphics, and handle multiple file formats in bulk.
// Prompt: Batch process image files, overlay each with a generated barcode, and save the results as BMP.
// Tags: barcode, code128, overlay, batch processing, bmp, aspose.barcode, aspose.drawing, image processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a console application that batch processes images, adds a generated barcode to each,
/// and saves the resulting composition as BMP files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates temporary input/output folders, generates sample images,
    /// overlays each with a Code128 barcode derived from the file name, and saves the results as BMP.
    /// </summary>
    static void Main()
    {
        // Create dedicated temporary input and output folders
        string inputFolder = Path.Combine(Path.GetTempPath(), "BatchInput_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Generate sample input images (PNG) for demonstration purposes
        List<string> inputFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"SampleImage{i}.png");
            using (Bitmap bmp = new Bitmap(300, 200))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Fill background with a distinct color for each image
                    g.Clear(i % 2 == 0 ? Color.LightBlue : Color.LightGreen);
                }
                bmp.Save(filePath, ImageFormat.Png);
            }
            inputFiles.Add(filePath);
        }

        // Process each image: overlay with a barcode and save as BMP
        foreach (string imagePath in inputFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            try
            {
                // Load the original image from file
                using (Bitmap baseImage = (Bitmap)Image.FromFile(imagePath))
                {
                    // Prepare barcode text based on the image file name (without extension)
                    string codeText = Path.GetFileNameWithoutExtension(imagePath);

                    // Generate a Code128 barcode in memory
                    using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                    {
                        // Optional: adjust barcode dimensions
                        generator.Parameters.Barcode.BarHeight.Point = 30f;
                        generator.Parameters.Barcode.XDimension.Point = 1f;

                        // Save barcode to a memory stream as PNG
                        using (MemoryStream barcodeStream = new MemoryStream())
                        {
                            generator.Save(barcodeStream, BarCodeImageFormat.Png);
                            barcodeStream.Position = 0;

                            // Load the barcode image from the stream
                            using (Bitmap barcodeBmp = (Bitmap)Image.FromStream(barcodeStream))
                            {
                                // Overlay the barcode onto the base image (bottom‑right corner with margin)
                                using (Graphics graphics = Graphics.FromImage(baseImage))
                                {
                                    int x = baseImage.Width - barcodeBmp.Width - 10; // 10 px horizontal margin
                                    int y = baseImage.Height - barcodeBmp.Height - 10; // 10 px vertical margin
                                    graphics.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                                }
                            }
                        }
                    }

                    // Save the combined image as BMP in the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imagePath) + ".bmp");
                    baseImage.Save(outputPath, ImageFormat.Bmp);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (ArgumentException ex)
            {
                // Handles image loading failures or other argument issues
                Console.WriteLine($"Skipping file due to error: {imagePath}. Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error processing {imagePath}: {ex.Message}");
            }
        }

        // Cleanup: (optional) delete temporary folders if desired
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}