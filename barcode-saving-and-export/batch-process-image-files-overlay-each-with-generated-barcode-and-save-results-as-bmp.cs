using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to overlay a generated barcode onto each image file in a directory
/// and save the resulting image as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Scans the input directory for image files, generates a Code128 barcode from each file name,
    /// draws the barcode onto the image, and saves the result to the output directory.
    /// </summary>
    static void Main()
    {
        // Define input and output directories (fallback to sample folders)
        string inputDir = Path.Combine(Directory.GetCurrentDirectory(), "input");
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the input directory exists
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        // Retrieve all files in the input directory (filter later by extension)
        string[] imageFiles = Directory.GetFiles(inputDir, "*.*", SearchOption.TopDirectoryOnly);
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No image files found to process.");
            return;
        }

        // Process each file individually
        foreach (string filePath in imageFiles)
        {
            // Simple filter for common image extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
                continue;

            // Double‑check that the file still exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File does not exist: {filePath}");
                continue;
            }

            try
            {
                // Load the original image from disk
                using (Image original = Image.FromFile(filePath))
                {
                    // Use the file name (without extension) as the barcode text
                    string codeText = Path.GetFileNameWithoutExtension(filePath);

                    // Initialize the barcode generator for Code128
                    using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                    {
                        // Optional: customize barcode appearance
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                        generator.Parameters.BackColor = Aspose.Drawing.Color.Transparent;

                        // Generate the barcode image as a bitmap
                        using (Bitmap barcodeImg = generator.GenerateBarCodeImage())
                        {
                            // Draw the barcode onto the original image at the bottom‑right corner
                            using (Graphics graphics = Graphics.FromImage(original))
                            {
                                const int padding = 10; // Space from the image edges
                                int x = original.Width - barcodeImg.Width - padding;
                                int y = original.Height - barcodeImg.Height - padding;

                                // Ensure coordinates are not negative
                                if (x < 0) x = 0;
                                if (y < 0) y = 0;

                                graphics.DrawImage(barcodeImg, x, y, barcodeImg.Width, barcodeImg.Height);
                            }

                            // Construct the output file name and path
                            string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_barcode.bmp";
                            string outputPath = Path.Combine(outputDir, outputFileName);

                            // Save the combined image as BMP
                            original.Save(outputPath, ImageFormat.Bmp);
                            Console.WriteLine($"Processed and saved: {outputPath}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of the current file
                Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}