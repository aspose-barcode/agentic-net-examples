// Title: Batch TIFF barcode generation with contrast variations and accuracy measurement
// Description: Demonstrates creating multiple TIFF barcode images with different foreground/background colors, then reading them to calculate average recognition accuracy.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing barcode generation (BarcodeGenerator) and recognition (BarCodeReader) on TIFF files. It illustrates typical use cases such as testing scanner robustness under varying contrast conditions, useful for developers needing to evaluate detection reliability across image qualities.
// Prompt: Process a batch of TIFF images with varying contrast levels and record average recognition accuracy.
// Tags: barcode, code128, generation, recognition, tiff, contrast, accuracy, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a set of TIFF barcode images with varying contrast levels,
/// reads them back, and reports the average recognition accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates temporary TIFF files, varies their contrast,
    /// attempts recognition, and prints a summary of results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample TIFF images
        string tempFolder = Path.Combine(Path.GetTempPath(), "TiffBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data (barcode texts)
        int sampleCount = 5;
        string[] codeTexts = new string[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            codeTexts[i] = $"Sample{i + 1}";
        }

        // Generate TIFF images with varying contrast (foreground/background colors)
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.tif");

            // Use Code128 for simplicity
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                // Vary contrast by changing foreground/background colors
                switch (i)
                {
                    case 0: // High contrast (black on white)
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                        generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                        break;
                    case 1: // Medium contrast (dark gray on white)
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.FromArgb(64, 64, 64);
                        generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                        break;
                    case 2: // Low contrast (black on light gray)
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                        generator.Parameters.BackColor = Aspose.Drawing.Color.FromArgb(200, 200, 200);
                        break;
                    case 3: // Inverted colors (white on black)
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.White;
                        generator.Parameters.BackColor = Aspose.Drawing.Color.Black;
                        break;
                    case 4: // Very low contrast (dark gray on light gray)
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.FromArgb(80, 80, 80);
                        generator.Parameters.BackColor = Aspose.Drawing.Color.FromArgb(180, 180, 180);
                        break;
                }

                // Save as TIFF
                generator.Save(filePath, BarCodeImageFormat.Tiff);
            }
        }

        // Process the generated TIFF images and evaluate recognition accuracy
        int successCount = 0;
        int totalCount = 0;

        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.tif");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            totalCount++;

            // Create a reader for the image, detecting all supported barcode types
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes in the image (default settings are sufficient)
                BarCodeResult[] results = reader.ReadBarCodes();

                // Success is determined by the presence of a recognized barcode with non‑empty text
                bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                if (success)
                {
                    successCount++;
                    Console.WriteLine($"File '{Path.GetFileName(filePath)}' recognized: {results[0].CodeText}");
                }
                else
                {
                    Console.WriteLine($"File '{Path.GetFileName(filePath)}' NOT recognized.");
                }
            }
        }

        // Calculate and display average recognition accuracy
        double accuracy = totalCount > 0 ? (double)successCount / totalCount * 100.0 : 0.0;
        Console.WriteLine($"Processed {totalCount} TIFF images.");
        Console.WriteLine($"Recognition success: {successCount}/{totalCount}");
        Console.WriteLine($"Average recognition accuracy: {accuracy:F2}%");

        // Cleanup: optionally delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails (e.g., files in use), ignore – the OS will clean up temp files later.
        }
    }
}