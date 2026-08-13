// Title: Evaluate recognition success rate for rotated DataMatrix barcodes
// Description: Generates DataMatrix barcodes, rotates them at 0°, 45°, and 90°, and measures how often Aspose.BarCode correctly reads the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, demonstrating how to create, manipulate, and read DataMatrix symbols using BarcodeGenerator, BarCodeReader, and related imaging classes. Typical use cases include testing scanner robustness against rotated symbols and validating recognition algorithms. Developers often need to generate test images, apply transformations, and programmatically assess decoding success rates.
// Prompt: Evaluate recognition success rate for rotated DataMatrix codes at 0°, 45°, and 90° angles.
// Tags: datamatrix, recognition, rotation, success-rate, aspose.barcodes, barcode-generation, barcode-reader

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating DataMatrix barcodes, rotating them, and evaluating recognition success rate using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates images at specified angles, reads them back, and prints the success rate.
    /// </summary>
    static void Main()
    {
        // Text to encode in the DataMatrix barcode
        const string codeText = "TestDataMatrix123";

        // Directory to store generated barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "DataMatrixSamples");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Angles (in degrees) at which the barcode will be rotated for testing
        int[] angles = new int[] { 0, 45, 90 };
        var imagePaths = new List<string>();

        // -----------------------------------------------------------------
        // Generate a barcode for each angle and optionally rotate it
        // -----------------------------------------------------------------
        foreach (int angle in angles)
        {
            // Create a DataMatrix barcode generator with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Save the generated barcode to a memory stream as PNG
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Load the PNG into a bitmap for further processing
                    using (var original = new Bitmap(ms))
                    {
                        string filePath = Path.Combine(folder, $"DataMatrix_{angle}.png");

                        if (angle == 0)
                        {
                            // No rotation needed; save the original bitmap directly
                            original.Save(filePath, ImageFormat.Png);
                        }
                        else
                        {
                            // Rotate the bitmap around its center by the specified angle
                            int width = original.Width;
                            int height = original.Height;
                            using (var rotated = new Bitmap(width, height))
                            {
                                using (var g = Graphics.FromImage(rotated))
                                {
                                    g.Clear(Aspose.Drawing.Color.White);
                                    g.TranslateTransform(width / 2f, height / 2f);
                                    g.RotateTransform(angle);
                                    g.TranslateTransform(-width / 2f, -height / 2f);
                                    g.DrawImage(original, 0, 0, width, height);
                                }
                                rotated.Save(filePath, ImageFormat.Png);
                            }
                        }

                        // Keep track of the generated image path for later recognition
                        imagePaths.Add(filePath);
                    }
                }
            }
        }

        // -----------------------------------------------------------------
        // Recognize each generated image and count successful decodings
        // -----------------------------------------------------------------
        int successCount = 0;
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            // Initialize a barcode reader for DataMatrix symbology
            using (var reader = new BarCodeReader(path, DecodeType.DataMatrix))
            {
                var results = reader.ReadBarCodes();
                bool success = false;

                // Check each decoded result for a match with the original text
                foreach (var result in results)
                {
                    if (!string.IsNullOrEmpty(result.CodeText) && result.CodeText == codeText)
                    {
                        success = true;
                        break;
                    }
                }

                if (success)
                {
                    successCount++;
                    Console.WriteLine($"Success: {Path.GetFileName(path)}");
                }
                else
                {
                    Console.WriteLine($"Failure: {Path.GetFileName(path)}");
                }
            }
        }

        // -----------------------------------------------------------------
        // Compute and display the overall recognition success rate
        // -----------------------------------------------------------------
        double successRate = (double)successCount / angles.Length * 100.0;
        Console.WriteLine($"Recognition success rate: {successRate:F2}% ({successCount}/{angles.Length})");
    }
}