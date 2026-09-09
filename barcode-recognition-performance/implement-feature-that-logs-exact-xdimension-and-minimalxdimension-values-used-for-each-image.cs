// Title: Demonstrate logging of XDimension and MinimalXDimension for generated barcodes
// Description: This example generates Code128 barcodes with different XDimension values, reads them back while applying MinimalXDimension settings, and logs the exact dimensions used during generation and recognition.
// Category-Description: Shows how to work with Aspose.BarCode generation and recognition APIs, focusing on XDimension handling. It covers BarcodeGenerator for creating barcodes, BarCodeReader with QualitySettings for fine‑tuning recognition, and typical use cases such as validating dimension parameters across the generation‑recognition pipeline. Developers looking for barcode dimension control, quality settings, or sample code for Code128 PNG output will find this example useful.
// Prompt: Implement a feature that logs the exact XDimension and MinimalXDimension values used for each image.
// Tags: barcode, code128, generation, recognition, xdimension, minimalxdimension, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes with specific XDimension values,
/// reading them back with MinimalXDimension settings, and logging the dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates barcode images, reads them, logs dimension information,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXDimDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define XDimension values to use (in points)
        List<float> xDimensions = new List<float> { 2f, 3f, 4f };
        // Store file paths together with the XDimension used for generation
        List<(string FilePath, float XDim)> generatedFiles = new List<(string, float)>();

        // Generate barcode images with the specified XDimension values
        int index = 1;
        foreach (float xDim in xDimensions)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{index}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Test{index}"))
            {
                // Set the XDimension for barcode generation (points)
                generator.Parameters.Barcode.XDimension.Point = xDim;
                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add((filePath, xDim));
            index++;
        }

        // Read each generated image and log both generation and recognition dimensions
        foreach (var (filePath, xDim) in generatedFiles)
        {
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Configure recognition to use MinimalXDimension mode
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // Apply the same value as used during generation for demonstration purposes
                reader.QualitySettings.MinimalXDimension = xDim;

                // Perform barcode reading (results are not further processed)
                BarCodeResult[] results = reader.ReadBarCodes();

                // Log dimension information and detection results
                Console.WriteLine($"Image: {Path.GetFileName(filePath)}");
                Console.WriteLine($"  Generation XDimension (points): {xDim}");
                Console.WriteLine($"  Recognition MinimalXDimension (pixels): {reader.QualitySettings.MinimalXDimension}");
                Console.WriteLine($"  Barcodes detected: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"    Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Cleanup: delete the temporary folder and all its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – not critical for the demo
        }
    }
}