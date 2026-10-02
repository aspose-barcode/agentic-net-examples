// Title: Generate barcodes with incremental height and save as JPEG
// Description: Demonstrates how to create Code128 barcodes with varying BarCodeHeight values, save each image as a JPEG file, and log the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. Typical use cases include dynamic barcode sizing for different printing requirements, where developers need to adjust bar height programmatically and export images in common formats like JPEG. The snippet illustrates common patterns for creating, configuring, and persisting barcodes using Aspose.BarCode APIs.
// Prompt: Write script generating barcodes with incremental BarCodeHeight values, saving each as JPEG and logging dimensions.
// Tags: barcode, code128, generation, barheight, jpeg, aspose.barcode, aspnet, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program that generates Code128 barcodes with incremental heights, saves them as JPEG files,
/// and writes the image dimensions to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, iterates over height values, generates barcodes,
    /// logs dimensions, and saves each image as JPEG.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Loop to generate barcodes with incremental BarCodeHeight values.
        for (int i = 1; i <= 5; i++)
        {
            // Calculate the bar height (10 points per iteration).
            float barHeight = i * 10f;

            // Prepare the text to encode in the barcode.
            string codeText = $"Sample{i}";

            // Build the full file path for the JPEG image.
            string filePath = Path.Combine(outputDir, $"Barcode_Height_{barHeight}.jpg");

            // Initialize the barcode generator with Code128 symbology and the sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the barcode's bar height in points.
                generator.Parameters.Barcode.BarHeight.Point = barHeight;

                // Generate the barcode image as a bitmap.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Retrieve image dimensions for logging.
                    int width = bitmap.Width;
                    int height = bitmap.Height;
                    Console.WriteLine($"Generated barcode with BarHeight {barHeight} -> Image size {width}x{height}");

                    // Save the bitmap to a JPEG file.
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fileStream, ImageFormat.Jpeg);
                    }
                }
            }
        }

        // Indicate that the barcode generation process has finished.
        Console.WriteLine("Barcode generation completed.");
    }
}