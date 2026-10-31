// Title: Evaluate recognition success rate for rotated DataMatrix barcodes
// Description: Generates DataMatrix barcodes rotated at 0°, 45°, and 90°, then measures how often Aspose.BarCode can correctly recognize them.
// Category-Description: This example demonstrates the combined use of Aspose.BarCode.Generation and Aspose.BarCode.BarCodeRecognition APIs to create and read barcodes. It is part of a series showing how to generate barcodes with specific visual properties (e.g., rotation) and evaluate recognition performance—common tasks for developers building automated scanning solutions, quality‑control tests, or image‑processing pipelines.
// Prompt: Evaluate recognition success rate for rotated DataMatrix codes at 0°, 45°, and 90° angles.
// Tags: datamatrix, rotation, recognition, barcode, generation, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate rotated DataMatrix barcodes, recognize them,
/// and calculate the recognition success rate for each rotation angle.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes at multiple angles,
    /// attempts to read them, and reports the success percentages.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixRotated_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define rotation angles to test and number of samples per angle
        float[] angles = { 0f, 45f, 90f };
        int samplesPerAngle = 5;
        var successCounts = new Dictionary<float, int>();

        // Iterate over each rotation angle
        foreach (float angle in angles)
        {
            successCounts[angle] = 0;

            // Generate and test a set of samples for the current angle
            for (int i = 0; i < samplesPerAngle; i++)
            {
                string filePath = Path.Combine(tempFolder, $"dm_{angle}_{i}.png");

                // -------------------- Barcode Generation --------------------
                try
                {
                    using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ASPOSE"))
                    {
                        // Apply the desired rotation to the generated barcode
                        generator.Parameters.RotationAngle = angle;
                        generator.Save(filePath, BarCodeImageFormat.Png);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Generation error (angle {angle}, sample {i}): {ex.Message}");
                    continue; // Skip to next sample if generation fails
                }

                // -------------------- Barcode Recognition --------------------
                try
                {
                    BaseDecodeType decodeType = DecodeType.DataMatrix;
                    using (var reader = new BarCodeReader(filePath, decodeType))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                        {
                            // Increment success count when a valid code is read
                            successCounts[angle]++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Recognition error (angle {angle}, sample {i}): {ex.Message}");
                }
            }
        }

        // -------------------- Output Success Rates --------------------
        Console.WriteLine("Recognition success rates for rotated DataMatrix codes:");
        foreach (float angle in angles)
        {
            int success = successCounts[angle];
            double rate = (double)success / samplesPerAngle * 100.0;
            Console.WriteLine($"Angle {angle}°: {success}/{samplesPerAngle} ({rate:F2}%)");
        }

        // -------------------- Cleanup Temporary Files --------------------
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