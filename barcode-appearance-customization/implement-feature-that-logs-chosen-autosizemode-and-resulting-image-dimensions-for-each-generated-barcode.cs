// Title: Barcode generation with AutoSizeMode logging
// Description: Demonstrates generating barcodes with different AutoSizeMode settings and logs the selected mode along with the resulting image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, showcasing how to configure AutoSizeMode, set target image size, and retrieve image dimensions using BarcodeGenerator and related classes. Developers often need to adjust barcode sizing for various output formats and log details for debugging or reporting purposes.
// Prompt: Implement a feature that logs the chosen AutoSizeMode and resulting image dimensions for each generated barcode.
// Tags: barcode symbology, autosizemode, image generation, logging, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with different AutoSizeMode settings and logs details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, logs AutoSizeMode and image size, and saves PNG files.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define sample barcodes together with the desired AutoSizeMode for each
        var samples = new (BaseEncodeType EncodeType, string CodeText, AutoSizeMode Mode)[]
        {
            (EncodeTypes.Code128, "1234567890", AutoSizeMode.None),
            (EncodeTypes.QR, "https://example.com", AutoSizeMode.Interpolation),
            (EncodeTypes.DataMatrix, "DataMatrix Sample", AutoSizeMode.Interpolation)
        };

        // Process each sample
        foreach (var sample in samples)
        {
            // Create a BarcodeGenerator for the specified symbology and code text
            using (var generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                // Apply the chosen AutoSizeMode
                generator.Parameters.AutoSizeMode = sample.Mode;

                // If Interpolation mode is selected, set the target image dimensions
                if (sample.Mode == AutoSizeMode.Interpolation)
                {
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                }

                // Generate the barcode image as a Bitmap
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Log the symbology, selected AutoSizeMode, and resulting image size
                    Console.WriteLine($"Symbology: {sample.EncodeType.TypeName}");
                    Console.WriteLine($"AutoSizeMode: {generator.Parameters.AutoSizeMode}");
                    Console.WriteLine($"Image Width: {bitmap.Width}px, Height: {bitmap.Height}px");

                    // Build the output file path and save the image as PNG
                    string filePath = Path.Combine(outputDir, $"{sample.EncodeType.TypeName}_{sample.Mode}.png");
                    bitmap.Save(filePath, ImageFormat.Png);
                }
            }
        }

        // Indicate that the process has finished
        Console.WriteLine("Barcode generation completed.");
    }
}