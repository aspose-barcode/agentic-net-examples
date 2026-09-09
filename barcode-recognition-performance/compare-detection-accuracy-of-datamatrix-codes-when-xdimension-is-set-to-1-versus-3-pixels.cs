// Title: Compare DataMatrix detection accuracy with different XDimension values
// Description: Demonstrates generating DataMatrix barcodes with XDimension set to 1 and 3 pixels, then reads them back to compare detection success rates.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create DataMatrix symbols, configure the XDimension property, and employ BarCodeReader to decode them. Developers working with high‑density or low‑density DataMatrix codes often need to tune XDimension for optimal scanning performance, making this pattern useful for testing and quality assurance.
// Prompt: Compare detection accuracy of DataMatrix codes when XDimension is set to 1 versus 3 pixels.
// Tags: datamatrix, xdimension, detection, accuracy, barcode generation, barcode recognition, aspnet, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how XDimension affects DataMatrix detection accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Generates DataMatrix barcodes with XDimension 1 and 3 pixels, reads them back, and reports detection success.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixXDimTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare a list of random code texts to encode
        List<string> codeTexts = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            codeTexts.Add("DM" + Guid.NewGuid().ToString("N").Substring(0, 8));
        }

        // Define the XDimension values to test (1 pixel and 3 pixels)
        var xDimensions = new float[] { 1f, 3f };
        var successCounts = new Dictionary<float, int>();

        // Iterate over each XDimension setting
        foreach (float xDim in xDimensions)
        {
            var generatedFiles = new List<string>();
            int index = 0;

            // Generate barcode images for each code text with the current XDimension
            foreach (string text in codeTexts)
            {
                string filePath = Path.Combine(tempFolder, $"DM_X{xDim}_{index}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = xDim;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                generatedFiles.Add(filePath);
                index++;
            }

            // Attempt to read each generated image and count successful detections
            int success = 0;
            foreach (string file in generatedFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                using (var reader = new BarCodeReader(file, DecodeType.DataMatrix))
                {
                    try
                    {
                        var results = reader.ReadBarCodes();
                        if (results != null && results.Length > 0)
                        {
                            success++;
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
                    }
                }
            }

            // Store the detection count for the current XDimension
            successCounts[xDim] = success;
        }

        // Output the comparison results
        Console.WriteLine("Detection accuracy comparison for DataMatrix XDimension:");
        foreach (var kvp in successCounts)
        {
            Console.WriteLine($"XDimension = {kvp.Key} pixels: {kvp.Value} out of {codeTexts.Count} detected");
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}