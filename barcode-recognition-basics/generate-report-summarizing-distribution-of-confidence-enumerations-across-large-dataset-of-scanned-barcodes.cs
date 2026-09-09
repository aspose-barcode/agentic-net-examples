// Title: Barcode Confidence Distribution Report
// Description: Generates a set of barcode images, reads them back using Aspose.BarCode, and reports the count of each confidence level detected.
// Category-Description: This example demonstrates the combined use of Aspose.BarCode generation and recognition APIs. It showcases BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and the BarCodeConfidence enumeration for assessing read quality. Typical scenarios include batch processing of scanned barcodes, quality analysis, and reporting confidence metrics in inventory or logistics systems. Developers often need to generate test data, evaluate recognition reliability, and produce summary reports.
// Prompt: Generate a report summarizing the distribution of Confidence enumerations across a large dataset of scanned barcodes.
// Tags: barcode symbology, generation, recognition, confidence, report, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate barcodes, read them back, and summarize the distribution of confidence levels.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them, and prints a confidence distribution report.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode specifications to be generated (type, text, output file name)
        var specs = new List<(BaseEncodeType encode, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Sample128", "code128.png"),
            (EncodeTypes.QR, "SampleQR", "qr.png"),
            (EncodeTypes.DataMatrix, "SampleDM", "datamatrix.png"),
            (EncodeTypes.Aztec, "SampleAztec", "aztec.png"),
            (EncodeTypes.Pdf417, "SamplePdf417", "pdf417.png")
        };

        // Store full paths of generated files for later processing
        var generatedFiles = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images using BarcodeGenerator and save as PNG
        // -----------------------------------------------------------------
        foreach (var spec in specs)
        {
            string filePath = Path.Combine(tempFolder, spec.fileName);
            using (var generator = new BarcodeGenerator(spec.encode, spec.text))
            {
                // No additional parameters needed for this demonstration
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Initialize counters for each possible confidence level
        var confidenceCounts = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 0 },
            { BarCodeConfidence.Strong, 0 }
        };

        // ---------------------------------------------------------------
        // Read each generated barcode and tally the confidence enumeration
        // ---------------------------------------------------------------
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        BarCodeConfidence conf = result.Confidence;
                        if (confidenceCounts.ContainsKey(conf))
                        {
                            confidenceCounts[conf]++;
                        }
                        else
                        {
                            confidenceCounts[conf] = 1;
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Skipping file due to load error: {file}. Message: {ex.Message}");
            }
        }

        // ------------------------------
        // Output the confidence report
        // ------------------------------
        Console.WriteLine("Confidence Distribution Report:");
        foreach (var kvp in confidenceCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // -------------------------------------------------
        // Clean up temporary files and folder (optional)
        // -------------------------------------------------
        try
        {
            foreach (string file in generatedFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}