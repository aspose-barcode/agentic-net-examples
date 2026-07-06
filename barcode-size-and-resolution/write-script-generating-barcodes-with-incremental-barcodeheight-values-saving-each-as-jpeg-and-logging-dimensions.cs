// Title: Generating Barcodes with Incremental BarHeight and Saving as JPEG
// Description: The example creates a series of Code128 barcodes, each with a different BarHeight, saves them as JPEG files, and logs their pixel dimensions.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, demonstrating how to control barcode dimensions using the BarcodeGenerator class and its Parameters.Barcode properties. Typical use cases include creating barcodes with custom visual appearance for packaging, labeling, or printing workflows. Developers often need to adjust BarHeight, AutoSizeMode, and output formats, making this example a useful reference for similar tasks.
// Prompt: Write script generating barcodes with incremental BarCodeHeight values, saving each as JPEG and logging dimensions.
// Tags: barcode, code128, barheight, jpeg, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple Code128 barcodes with varying BarHeight values,
/// saving each image as a JPEG file, and outputting the resulting dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output directory, generates barcodes,
    /// saves them, and writes dimension information to the console.
    /// </summary>
    static void Main()
    {
        // Determine the directory where barcode images will be stored
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Generate 5 barcodes, each with an increased BarHeight
        for (int i = 0; i < 5; i++)
        {
            // Calculate BarHeight: start at 20 points and increase by 10 points per iteration
            float barHeight = 20f + i * 10f;

            // Initialize a barcode generator for the Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Assign the text to be encoded in the barcode
                generator.CodeText = $"Sample{i + 1}";

                // Disable automatic sizing so we can set BarHeight manually
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Apply the calculated BarHeight using the Point unit
                generator.Parameters.Barcode.BarHeight.Point = barHeight;

                // Generate the barcode image as a Bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Construct a descriptive file name that includes the index and BarHeight
                    string fileName = $"barcode_{i + 1}_{barHeight}pt.jpg";
                    string filePath = Path.Combine(outputDir, fileName);

                    // Save the bitmap as a JPEG file using Aspose.Drawing.Imaging.ImageFormat
                    bitmap.Save(filePath, ImageFormat.Jpeg);

                    // Output the saved file name and its pixel dimensions to the console
                    Console.WriteLine($"Saved {fileName}: {bitmap.Width}x{bitmap.Height} pixels (BarHeight={barHeight}pt)");
                }
            }
        }
    }
}