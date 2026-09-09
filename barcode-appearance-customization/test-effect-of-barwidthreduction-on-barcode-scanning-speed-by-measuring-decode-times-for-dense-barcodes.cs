// Title: BarWidthReduction Impact on Barcode Decode Speed
// Description: Demonstrates how varying BarWidthReduction affects the decoding time of dense Code128 barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for high‑performance decoding. Developers often need to benchmark barcode readability under different rendering parameters, such as BarWidthReduction, to optimize scanning speed in high‑density scenarios.
// Prompt: Test the effect of BarWidthReduction on barcode scanning speed by measuring decode times for dense barcodes.
// Tags: code128, barwidthreduction, performance, decoding, aspose.barcode, barcode generation, barcode recognition, qualitysettings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates Code128 barcodes with varying BarWidthReduction values,
/// measures their decode times, and outputs the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcodes, decodes them,
    /// reports performance metrics, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarWidthReductionTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test parameters
        string codeText = new string('A', 50); // dense Code128 text
        float xDimensionPixels = 2f;
        List<float> barWidthReductions = new List<float> { 0f, 2f, 4f };
        List<string> generatedFiles = new List<string>();

        // Generate barcodes with different BarWidthReduction values
        foreach (float reduction in barWidthReductions)
        {
            string filePath = Path.Combine(tempFolder, $"Code128_BWR_{reduction}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set barcode dimensions and reduction
                generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;
                generator.Parameters.Barcode.BarWidthReduction.Pixels = reduction;

                // Save the barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Measure decoding time for each generated barcode
        Console.WriteLine("BarWidthReduction\tDecodeTimeMs\tDecodedText");
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Use high‑performance preset for speed measurement
                reader.QualitySettings = QualitySettings.HighPerformance;

                Stopwatch sw = Stopwatch.StartNew();
                BarCodeResult[] results = reader.ReadBarCodes();
                sw.Stop();

                string decodedText = results.Length > 0 ? results[0].CodeText : "None";

                // Extract reduction value from filename
                string reductionValue = Path.GetFileNameWithoutExtension(file).Split('_')[2];
                Console.WriteLine($"{reductionValue}\t\t{sw.ElapsedMilliseconds}\t\t{decodedText}");
            }
        }

        // Cleanup temporary files
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