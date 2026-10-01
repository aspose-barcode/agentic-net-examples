// Title: DataMatrix XDimension Detection Accuracy Comparison
// Description: Generates DataMatrix barcodes with XDimension set to 1 and 3 pixels, then measures successful recognition counts to compare detection accuracy.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs for DataMatrix symbology. It shows how to configure barcode size via XDimension, create PNG images, and use BarCodeReader to decode them. Developers working with barcode quality testing, image processing pipelines, or automated scanning solutions often need to evaluate how visual parameters affect read rates, making this a useful reference for performance benchmarking scenarios.
// Prompt: Compare detection accuracy of DataMatrix codes when XDimension is set to 1 versus 3 pixels.
// Tags: datamatrix, xdimension, detection, accuracy, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how XDimension influences DataMatrix detection accuracy by generating
/// barcodes with two different pixel sizes and counting successful reads.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them back, and reports detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data strings to encode
        List<string> dataTexts = new List<string>
        {
            "ABC123",
            "XYZ7890",
            "DATA2023",
            "TEST4567",
            "HELLO9"
        };

        // Store file paths for each XDimension setting
        List<string> filesX1 = new List<string>();
        List<string> filesX3 = new List<string>();

        // Generate barcodes with XDimension = 1 pixel and 3 pixels
        foreach (string text in dataTexts)
        {
            // XDimension = 1 pixel
            string pathX1 = Path.Combine(tempFolder, $"dm_x1_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                generator.Parameters.Barcode.XDimension.Point = 1f; // set module size to 1 pixel
                generator.Save(pathX1, BarCodeImageFormat.Png);
            }
            filesX1.Add(pathX1);

            // XDimension = 3 pixels
            string pathX3 = Path.Combine(tempFolder, $"dm_x3_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
            {
                generator.Parameters.Barcode.XDimension.Point = 3f; // set module size to 3 pixels
                generator.Save(pathX3, BarCodeImageFormat.Png);
            }
            filesX3.Add(pathX3);
        }

        // Local function to read barcodes and count successful detections
        int CountSuccessfulReads(List<string> filePaths)
        {
            int successCount = 0;
            foreach (string file in filePaths)
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
                            successCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading {Path.GetFileName(file)}: {ex.Message}");
                    }
                }
            }
            return successCount;
        }

        // Count successful reads for each XDimension setting
        int successX1 = CountSuccessfulReads(filesX1);
        int successX3 = CountSuccessfulReads(filesX3);

        // Output summary of detection results
        Console.WriteLine($"Total samples: {dataTexts.Count}");
        Console.WriteLine($"Successful reads with XDimension = 1 pixel: {successX1}");
        Console.WriteLine($"Successful reads with XDimension = 3 pixels: {successX3}");

        // Cleanup temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – files will be removed by the OS temp cleanup
        }
    }
}