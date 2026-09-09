// Title: Limit barcode reading to a maximum of three per image
// Description: Demonstrates generating multiple Code128 barcodes, combining them into a single image, and reading up to three barcodes from the combined image to reduce processing overhead.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, Bitmap and Graphics for image composition, and BarCodeReader for decoding. Typical scenarios include batch barcode creation, image stitching, and controlled barcode scanning where developers need to limit the number of decoded symbols to improve performance.
// Prompt: Set maximum number of barcodes per image to three to limit processing overhead.
// Tags: barcode symbology, generation, recognition, code128, limit, image, combine, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates multiple Code128 barcodes, combines them into a single image,
/// and reads up to three barcodes from the combined image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, merges them,
    /// reads a limited number of barcodes, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate individual barcode images and store their file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"code{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"CODE{i}"))
            {
                // Adjust X-dimension for better visual size
                generator.Parameters.Barcode.XDimension.Point = 0.8f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Combine the generated barcode images into a single wide image
        string combinedPath = Path.Combine(tempFolder, "combined.png");
        CombineImages(barcodeFiles, combinedPath);

        // Verify that the combined image was created successfully
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        // Read barcodes from the combined image, processing at most three results
        using (var reader = new BarCodeReader(combinedPath, DecodeType.Code128))
        {
            int count = 0;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                count++;
                if (count >= 3)
                    break; // Stop after three barcodes to limit overhead
            }
            Console.WriteLine($"Processed {count} barcode(s) (maximum 3).");
        }

        // Attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors (e.g., file locks)
        }
    }

    /// <summary>
    /// Combines a list of image files horizontally into a single output image.
    /// </summary>
    /// <param name="imagePaths">File paths of the source images.</param>
    /// <param name="outputPath">File path where the combined image will be saved.</param>
    static void CombineImages(List<string> imagePaths, string outputPath)
    {
        if (imagePaths.Count == 0)
            throw new ArgumentException("No images to combine.");

        // Load the first image to obtain width and height of individual barcodes
        using (var first = new Bitmap(imagePaths[0]))
        {
            int singleWidth = first.Width;
            int singleHeight = first.Height;
            int totalWidth = singleWidth * imagePaths.Count;
            int maxHeight = singleHeight;

            // Create a new bitmap large enough to hold all images side by side
            using (var combined = new Bitmap(totalWidth, maxHeight))
            {
                using (var graphics = Graphics.FromImage(combined))
                {
                    // Fill background with white for a clean look
                    graphics.Clear(Color.White);

                    // Draw each barcode image at the appropriate horizontal offset
                    for (int i = 0; i < imagePaths.Count; i++)
                    {
                        using (var img = new Bitmap(imagePaths[i]))
                        {
                            int x = i * singleWidth;
                            graphics.DrawImage(img, x, 0, img.Width, img.Height);
                        }
                    }
                }

                // Save the combined image as PNG
                combined.Save(outputPath, ImageFormat.Png);
            }
        }
    }
}