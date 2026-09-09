// Title: Generate multiple Code128 barcodes with varying heights
// Description: Demonstrates how to create a series of Code128 barcodes, each with an incrementally increased BarCodeHeight, and save them as JPEG images while logging their dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, BarCodeParameters, and image handling classes. Developers often need to produce barcodes with custom sizing for printing or UI display, and this snippet shows how to adjust BarHeight, generate bitmap images, and persist them in common formats.
// Prompt: Write script generating barcodes with incremental BarCodeHeight values, saving each as JPEG and logging dimensions.
// Tags: code128, barcodeheight, jpeg, aspose.barcode, generation, image, dimensions

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a set of Code128 barcodes with increasing BarCodeHeight,
/// saves each barcode as a JPEG file, and writes the image dimensions to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, iterates to generate barcodes,
    /// and logs the saved file paths and dimensions.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Configuration for the barcode series.
        int count = 5;                 // Number of barcodes to generate.
        float startHeight = 10f;       // Initial BarCodeHeight in points.
        float increment = 10f;         // Increment to apply to BarCodeHeight for each subsequent barcode.

        // Loop to generate each barcode with an increased height.
        for (int i = 0; i < count; i++)
        {
            // Calculate the current BarCodeHeight.
            float height = startHeight + i * increment;
            // Define the text to encode in the barcode.
            string codeText = $"Sample{i + 1}";

            // Initialize the barcode generator with Code128 symbology and the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply the calculated BarCodeHeight.
                generator.Parameters.Barcode.BarHeight.Point = height;

                // Generate the barcode image as a bitmap.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Build the file path for the JPEG output.
                    string filePath = Path.Combine(outputDir, $"barcode_{i + 1}.jpg");
                    // Save the bitmap as a JPEG file.
                    bitmap.Save(filePath, ImageFormat.Jpeg);

                    // Log the saved file location and image dimensions.
                    Console.WriteLine($"Saved: {filePath}");
                    Console.WriteLine($"Dimensions: {bitmap.Width}x{bitmap.Height} (BarHeight={height})");
                }
            }
        }

        // Final summary of the output location.
        Console.WriteLine($"All barcodes saved to: {outputDir}");
    }
}