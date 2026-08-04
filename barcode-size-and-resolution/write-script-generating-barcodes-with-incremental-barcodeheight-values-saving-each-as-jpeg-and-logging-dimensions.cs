// Title: Generating Code128 barcodes with varying heights
// Description: Demonstrates creating Code128 barcodes with incremental BarCodeHeight values, saving each as a JPEG, and logging the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to control barcode size using the BarHeight property via the BarcodeGenerator and its Parameters. Typical use cases include producing barcodes of different visual sizes for printing or UI display. Developers often need to adjust dimensions, set AutoSizeMode, and export to common image formats using classes like BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing.Image.
// Prompt: Write script generating barcodes with incremental BarCodeHeight values, saving each as JPEG and logging dimensions.
// Tags: barcode symbology, generation, jpeg, barheight, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that generates Code128 barcodes with varying heights, saves them as JPEG files,
/// and outputs the image dimensions to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output folder, iterates over predefined heights, generates barcodes,
    /// saves them, and logs their pixel dimensions.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images
        string outputDir = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define incremental BarCodeHeight values (in points)
        float[] heights = new float[] { 20f, 40f, 60f, 80f, 100f };

        // Process each height value
        foreach (float height in heights)
        {
            // Create a barcode generator for Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Assign a simple codetext that includes the height value
                generator.CodeText = $"Sample{height}";

                // Disable automatic sizing to allow manual BarHeight setting
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set the barcode's bar height (in points)
                generator.Parameters.Barcode.BarHeight.Point = height;

                // Build the file path for the JPEG image
                string filePath = Path.Combine(outputDir, $"barcode_{height}.jpeg");

                // Save the generated barcode as a JPEG file
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }

            // Load the saved JPEG to retrieve its pixel dimensions
            using (var image = Image.FromFile(Path.Combine(outputDir, $"barcode_{height}.jpeg")))
            {
                // Log the file name and its width/height in pixels
                Console.WriteLine($"Saved barcode_{height}.jpeg - Width: {image.Width}px, Height: {image.Height}px");
            }
        }
    }
}