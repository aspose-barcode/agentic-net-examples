// Title: Generate Code128 barcodes with incremental heights and save as JPEG
// Description: Demonstrates how to create Code128 barcodes, vary the BarCodeHeight property, save each image as a JPEG file, and log the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image handling classes (Bitmap, ImageFormat). Developers often need to produce barcodes with custom visual dimensions for printing or UI display, and this snippet shows how to adjust height, export to common image formats, and retrieve image size information.
// Prompt: Write script generating barcodes with incremental BarCodeHeight values, saving each as JPEG and logging dimensions.
// Tags: barcode, code128, barcodeheight, jpeg, image, aspose.barcode, generation, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates Code128 barcodes with varying heights, saves them as JPEG files, and writes image dimensions to the console.
    /// </summary>
    static void Main()
    {
        // Determine and create the output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Define a set of barcode heights (in points) to iterate over.
        float[] heights = new float[] { 10f, 20f, 30f, 40f, 50f };

        // Loop through each height, generate a barcode, and save it.
        foreach (float h in heights)
        {
            // Initialize the barcode generator for Code128 with sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample"))
            {
                // Apply the current height to the barcode parameters.
                generator.Parameters.Barcode.BarHeight.Point = h;

                // Generate the barcode image as a bitmap.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Build the file path using the height value in the filename.
                    string filePath = Path.Combine(outputDir, $"barcode_{h}.jpg");

                    // Save the bitmap to a JPEG file.
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fileStream, ImageFormat.Jpeg);
                    }

                    // Output the barcode height and resulting image dimensions to the console.
                    Console.WriteLine($"BarHeight {h}pt => Image dimensions: {bitmap.Width}x{bitmap.Height}");
                }
            }
        }
    }
}