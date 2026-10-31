// Title: Batch barcode overlay on images and save as BMP
// Description: Demonstrates how to generate a Code128 barcode, overlay it onto multiple image files, and save the combined images as BMP files.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing the use of BarcodeGenerator (Aspose.BarCode.Generation) together with Aspose.Drawing to create barcodes, draw them onto existing images, and export the results. Typical scenarios include batch labeling, product packaging, or any workflow that requires programmatic addition of barcodes to graphics. Developers often need to combine barcode generation with image manipulation APIs to automate such tasks.
// Prompt: Batch process image files, overlay each with a generated barcode, and save the results as BMP.
// Tags: barcode, code128, overlay, batch, bmp, aspose.barcode, aspose.drawing, image-processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates sample images, generates a Code128 barcode for each,
/// overlays the barcode onto the image, and saves the result as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs batch processing of images with barcode overlay.
    /// </summary>
    static void Main()
    {
        // Create a unique working directory for input and output files
        string baseFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        string inputFolder = Path.Combine(baseFolder, "input");
        string outputFolder = Path.Combine(baseFolder, "output");
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Generate sample input PNG images
        List<string> inputFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(inputFolder, $"image{i}.png");
            using (Bitmap bmp = new Bitmap(200, 200))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Fill the background with a light gray color
                    g.Clear(Aspose.Drawing.Color.LightGray);
                }
                // Save the placeholder image as PNG
                bmp.Save(filePath, ImageFormat.Png);
            }
            inputFiles.Add(filePath);
        }

        // Process each image: generate a barcode, overlay it, and save as BMP
        int index = 1;
        foreach (string imgPath in inputFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.WriteLine($"File not found: {imgPath}");
                continue;
            }

            using (Bitmap original = new Bitmap(imgPath))
            {
                // Generate a Code128 barcode image with a unique value
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{index}"))
                {
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    using (Bitmap barcodeBmp = generator.GenerateBarCodeImage())
                    {
                        // Overlay the barcode onto the bottom‑right corner of the original image
                        using (Graphics graphics = Graphics.FromImage(original))
                        {
                            int x = original.Width - barcodeBmp.Width - 10;
                            int y = original.Height - barcodeBmp.Height - 10;
                            graphics.DrawImage(barcodeBmp, x, y, barcodeBmp.Width, barcodeBmp.Height);
                        }
                    }
                }

                // Save the combined image as BMP in the output folder
                string outPath = Path.Combine(outputFolder, $"result{index}.bmp");
                original.Save(outPath, ImageFormat.Bmp);
                Console.WriteLine($"Saved: {outPath}");
            }

            index++;
        }

        Console.WriteLine("Batch processing completed.");
    }
}