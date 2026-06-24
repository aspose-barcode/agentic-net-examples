using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a set of barcode images with varying XDimension values.
    /// </summary>
    static void Main()
    {
        // Determine the output folder path relative to the current working directory.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        // Number of barcode samples to generate.
        // Use a smaller count for quick execution; set to 100 for full batch.
        int sampleCount = 5;

        // Loop to create each barcode with a distinct XDimension.
        for (int i = 0; i < sampleCount; i++)
        {
            // Calculate XDimension in millimeters (e.g., 0.5mm, 0.6mm, ...).
            float xDimensionMm = 0.5f + i * 0.1f;

            // Initialize a barcode generator for Code128 with unique text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i + 1}"))
            {
                // Apply the calculated XDimension (in millimeters) to the barcode parameters.
                generator.Parameters.Barcode.XDimension.Millimeters = xDimensionMm;

                // Construct the full file path for the output TIFF image.
                string filePath = Path.Combine(outputFolder, $"barcode_{i + 1}.tiff");

                // Save the generated barcode as a TIFF file.
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
        }

        // Inform the user about the generation result.
        Console.WriteLine($"Generated {sampleCount} barcode images in '{outputFolder}'.");
    }
}