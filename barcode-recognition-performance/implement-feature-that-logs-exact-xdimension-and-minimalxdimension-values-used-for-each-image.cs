// Title: Barcode XDimension and MinimalXDimension logging example
// Description: Demonstrates generating Code128 barcodes with varying XDimension values and logging the exact dimensions used during generation and recognition.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to control and inspect XDimension settings via the BarcodeGenerator and BarCodeReader classes. Developers often need to fine‑tune module width (XDimension) for printing quality or scanning reliability, and this snippet illustrates typical usage patterns for setting XDimension in points, configuring QualitySettings to use MinimalXDimension, and retrieving those values for diagnostics.
// Prompt: Implement a feature that logs the exact XDimension and MinimalXDimension values used for each image.
// Tags: barcode, code128, xdimension, minimalxdimension, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates barcode images with different XDimension values,
/// then reads them back while logging the exact XDimension and MinimalXDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directory, generates barcodes,
    /// configures reader quality settings, and logs dimension information.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate three barcode images, each with a distinct XDimension (2pt, 4pt, 6pt)
        for (int i = 1; i <= 3; i++)
        {
            // Calculate XDimension in points for the current iteration
            float xDim = i * 2f; // 2pt, 4pt, 6pt

            // Build the file path for the generated image
            string filePath = Path.Combine(outputDir, $"barcode_{i}.png");

            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Apply the XDimension (module width) in points
                generator.Parameters.Barcode.XDimension.Point = xDim;

                // Save the generated barcode image to disk
                generator.Save(filePath);
            }

            // Open the generated image with a barcode reader to inspect settings
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Instruct the reader to use MinimalXDimension mode for quality assessment
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                // Set MinimalXDimension (in pixels) to match the generation XDimension
                reader.QualitySettings.MinimalXDimension = xDim;

                // Perform barcode recognition (results are not used here)
                reader.ReadBarCodes();

                // Log the dimension values for diagnostic purposes
                Console.WriteLine($"Image: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Generator XDimension (points): {xDim}pt");
                Console.WriteLine($"  Reader QualitySettings XDimension mode: {reader.QualitySettings.XDimension}");
                Console.WriteLine($"  Reader MinimalXDimension (pixels): {reader.QualitySettings.MinimalXDimension}");
                Console.WriteLine();
            }
        }
    }
}