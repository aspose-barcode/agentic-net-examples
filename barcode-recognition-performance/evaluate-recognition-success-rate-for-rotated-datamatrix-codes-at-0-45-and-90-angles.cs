// Title: Evaluate recognition success rate for rotated DataMatrix barcodes
// Description: Generates DataMatrix barcodes rotated at 0°, 45°, and 90°, then measures how often they are correctly recognized.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to create DataMatrix symbols with specific rotation angles and BarCodeReader to decode them. Typical use cases include testing scanner robustness, validating image preprocessing pipelines, and benchmarking recognition performance across different orientations. Developers often need to generate test images, read them back, and compute success metrics using the EncodeTypes, DecodeType, and BarCodeImageFormat classes.
// Prompt: Evaluate recognition success rate for rotated DataMatrix codes at 0°, 45°, and 90° angles.
// Tags: datamatrix, rotation, recognition, success-rate, generation, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating rotated DataMatrix barcodes and evaluating their recognition success rate.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes at multiple rotation angles, reads them back, and reports success percentages.
    /// </summary>
    static void Main()
    {
        // Number of barcode images to generate per angle
        const int sampleCount = 5;

        // Rotation angles to test (in degrees)
        string[] angles = { "0", "45", "90" };

        // Create a temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixRotated_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Text to encode in each barcode
        string barcodeText = "TEST12345";

        // Dictionary to store success rate per angle
        var successRates = new Dictionary<int, double>();

        // Iterate over each rotation angle
        foreach (string angleStr in angles)
        {
            int angle = int.Parse(angleStr);
            int successes = 0;

            // Generate and test a set of barcodes for the current angle
            for (int i = 0; i < sampleCount; i++)
            {
                // Build file path for the generated image
                string filePath = Path.Combine(tempFolder, $"dm_{angle}_{i}.png");

                // Generate a DataMatrix barcode with the specified rotation
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, barcodeText))
                {
                    generator.Parameters.RotationAngle = (float)angle;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Attempt to read the generated barcode
                using (var reader = new BarCodeReader(filePath, DecodeType.DataMatrix))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        // Count as success if the decoded text matches the original
                        if (result.CodeText == barcodeText)
                        {
                            successes++;
                            break;
                        }
                    }
                }
            }

            // Calculate success percentage for the current angle
            double rate = (double)successes / sampleCount * 100.0;
            successRates[angle] = rate;
        }

        // Output the recognition success rates
        Console.WriteLine("Recognition success rates for rotated DataMatrix codes:");
        foreach (var kvp in successRates)
        {
            Console.WriteLine($"Angle {kvp.Key}°: {kvp.Value}% ({kvp.Value / 100 * sampleCount} of {sampleCount} samples)");
        }

        // Cleanup temporary files (optional)
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