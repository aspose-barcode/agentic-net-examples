// Title: DataMatrix detection accuracy comparison for different XDimension values
// Description: Demonstrates generating DataMatrix barcodes with XDimension set to 1 and 3 pixels, then measuring detection success using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating DataMatrix symbols and BarCodeReader for decoding them. Typical scenarios include evaluating barcode readability under varying rendering parameters, such as XDimension, which developers often adjust to meet printing or scanning requirements. The code serves as a reference for batch processing and accuracy testing of barcode images.
/// Prompt: Compare detection accuracy of DataMatrix codes when XDimension is set to 1 versus 3 pixels.
// Tags: datamatrix, detection, accuracy, xdimension, barcode-generation, barcode-recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that compares detection accuracy of DataMatrix barcodes
/// generated with XDimension of 1 pixel versus 3 pixels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, evaluates detection, outputs results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DMCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample texts to encode into DataMatrix barcodes
        string[] sampleTexts = new[] { "ABC123", "HELLO", "1234567890", "DATA", "XYZ" };

        // Dictionary to hold file paths grouped by XDimension value
        var filesByXDim = new Dictionary<int, List<string>>
        {
            { 1, new List<string>() },
            { 3, new List<string>() }
        };

        // Generate DataMatrix barcodes with XDimension = 1 and 3 pixels
        foreach (int xDim in new[] { 1, 3 })
        {
            for (int i = 0; i < sampleTexts.Length; i++)
            {
                string text = sampleTexts[i];
                string filePath = Path.Combine(tempFolder, $"DM_{xDim}_{i}.png");

                // Create barcode generator, set XDimension, and save as PNG
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = xDim;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                filesByXDim[xDim].Add(filePath);
            }
        }

        // Local function to evaluate detection accuracy for a given XDimension
        int Evaluate(int xDim, List<string> files)
        {
            int detected = 0;

            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];
                string expectedText = sampleTexts[i];

                try
                {
                    // Read barcode from image file
                    using (var reader = new BarCodeReader(file, DecodeType.DataMatrix))
                    {
                        var results = reader.ReadBarCodes();

                        // Count as detected if a result matches the expected text
                        if (results.Length > 0 && results[0].CodeText == expectedText)
                        {
                            detected++;
                        }
                    }
                }
                catch (ArgumentException)
                {
                    // Image loading failed; skip this file
                }
            }

            return detected;
        }

        // Perform detection evaluation for both XDimension settings
        int detected1 = Evaluate(1, filesByXDim[1]);
        int detected3 = Evaluate(3, filesByXDim[3]);

        // Output detection results to the console
        Console.WriteLine($"DataMatrix detection with XDimension = 1 pixel: {detected1}/{sampleTexts.Length} detected");
        Console.WriteLine($"DataMatrix detection with XDimension = 3 pixels: {detected3}/{sampleTexts.Length} detected");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}