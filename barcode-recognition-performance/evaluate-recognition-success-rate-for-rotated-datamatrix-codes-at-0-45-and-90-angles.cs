// Title: DataMatrix Rotation Recognition Success Rate Evaluation
// Description: Generates DataMatrix barcodes at 0°, 45°, and 90° rotations, reads them back, and reports the recognition success percentage for each angle.
// Category-Description: This example demonstrates Aspose.BarCode's barcode generation and recognition APIs, focusing on rotation handling for DataMatrix symbology. It showcases the use of BarcodeGenerator for creating images with specific RotationAngle settings and BarCodeReader for decoding. Developers testing barcode robustness, image preprocessing pipelines, or rotation tolerance can use similar patterns to evaluate detection performance across varied orientations.
// Prompt: Evaluate recognition success rate for rotated DataMatrix codes at 0°, 45°, and 90° angles.
// Tags: datamatrix, rotation, recognition, success-rate, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate DataMatrix barcodes at different rotation angles,
/// decode them, and calculate the recognition success rate for each angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them back, and prints success statistics.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixRotationTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample DataMatrix texts to test
        List<string> testTexts = new List<string>
        {
            "ABC1234567",
            "XYZ9876543",
            "HELLO2023",
            "DATA2024",
            "TESTCODE1",
            "CODE2025",
            "SAMPLE001",
            "BARCODE99",
            "DMATRIX88",
            "ROTATE555"
        };

        // Angles to evaluate
        float[] angles = new float[] { 0f, 45f, 90f };

        // Store success counts per angle
        Dictionary<float, int> successCounts = new Dictionary<float, int>();
        Dictionary<float, int> totalCounts = new Dictionary<float, int>();

        foreach (float angle in angles)
        {
            successCounts[angle] = 0;
            totalCounts[angle] = testTexts.Count;

            foreach (string text in testTexts)
            {
                // Generate DataMatrix barcode with the specified rotation
                string filePath = Path.Combine(tempFolder, $"DM_{angle}_{Guid.NewGuid().ToString("N")}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
                {
                    generator.Parameters.RotationAngle = angle;

                    // Optional: set minimal padding around the barcode
                    generator.Parameters.Barcode.Padding.Left.Point = 2f;
                    generator.Parameters.Barcode.Padding.Right.Point = 2f;
                    generator.Parameters.Barcode.Padding.Top.Point = 2f;
                    generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

                    // Save the generated barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Verify that the image file was created before attempting to read it
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Failed to create image: {filePath}");
                    continue;
                }

                // Read the barcode from the saved image
                BaseDecodeType decodeType = DecodeType.DataMatrix;
                using (var reader = new BarCodeReader(filePath, decodeType))
                {
                    try
                    {
                        var results = reader.ReadBarCodes();
                        bool recognized = false;

                        // Check if any decoded result matches the original text
                        foreach (var result in results)
                        {
                            if (result != null && result.CodeText == text)
                            {
                                recognized = true;
                                break;
                            }
                        }

                        if (recognized)
                        {
                            successCounts[angle]++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading barcode from {filePath}: {ex.Message}");
                    }
                }
            }
        }

        // Output success rates for each rotation angle
        Console.WriteLine("DataMatrix Recognition Success Rates:");
        foreach (float angle in angles)
        {
            int success = successCounts[angle];
            int total = totalCounts[angle];
            double rate = total > 0 ? (double)success / total * 100.0 : 0.0;
            Console.WriteLine($"Angle {angle}°: {success}/{total} ({rate:F2}%)");
        }

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – files will be removed by the OS eventually
        }
    }
}