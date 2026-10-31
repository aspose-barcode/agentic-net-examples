// Title: Merge multiple barcode recognition XML state files into a summary document
// Description: Demonstrates generating QR barcodes, exporting their recognition state to XML, and merging those XML files to produce a consolidated text summary of detected barcodes.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing how to use BarcodeGenerator, BarCodeReader, and related state‑export/import APIs. Typical use cases include batch processing of scanned images, persisting recognition results, and later aggregating them for reporting or analytics. Developers often need to generate barcodes, capture recognition state, and combine multiple states into a single overview.
// Prompt: Implement a feature that merges multiple XML state files into a single document summarizing all detected barcodes.
// Tags: qr, barcode, xml, merge, summary, generation, recognition, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates merging multiple barcode recognition XML state files into a single summary document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR barcodes, exports their recognition state to XML, imports the states, and creates a merged summary.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory for all generated files.
        string workDir = Path.Combine(Path.GetTempPath(), "MergeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Sample data: list of code texts for QR barcodes.
        List<string> codeTexts = new List<string> { "HelloWorld", "Aspose.BarCode", "MergeDemo" };
        List<string> imagePaths = new List<string>();
        List<string> xmlPaths = new List<string>();

        // Step 1: Generate QR barcode images and export their recognition state to XML.
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string imagePath = Path.Combine(workDir, $"barcode_{i}.png");
            string xmlPath = Path.Combine(workDir, $"state_{i}.xml");
            imagePaths.Add(imagePath);
            xmlPaths.Add(xmlPath);

            // Generate QR barcode image.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeTexts[i]))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            // Read the generated image and export its recognition state.
            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                // Perform a read to populate internal state.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Export state to XML (the image itself is not stored in the XML).
                reader.ExportToXml(xmlPath);
            }
        }

        // Step 2: Merge XML state files and summarize detected barcodes.
        List<string> summaryLines = new List<string>();
        summaryLines.Add("Merged Barcode Recognition Summary");
        summaryLines.Add($"Generated on: {DateTime.Now}");
        summaryLines.Add(string.Empty);

        for (int i = 0; i < xmlPaths.Count; i++)
        {
            string xmlPath = xmlPaths[i];
            string imagePath = imagePaths[i];

            // Verify that both the XML state file and the corresponding image exist.
            if (!File.Exists(xmlPath) || !File.Exists(imagePath))
            {
                summaryLines.Add($"[Warning] Missing XML or image for index {i}, skipping.");
                continue;
            }

            // Import reader state from XML.
            using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
            {
                // Set the image source (required because it is not stored in XML).
                importedReader.SetBarCodeImage(imagePath);
                // Ensure the correct decode type is set.
                importedReader.SetBarCodeReadType(DecodeType.QR);

                // Read barcodes using the imported state.
                BarCodeResult[] results = importedReader.ReadBarCodes();

                summaryLines.Add($"File: {Path.GetFileName(imagePath)}");
                if (results.Length == 0)
                {
                    summaryLines.Add("  No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        summaryLines.Add($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
                summaryLines.Add(string.Empty);
            }
        }

        // Write the summary to a text file.
        string summaryPath = Path.Combine(workDir, "Summary.txt");
        File.WriteAllLines(summaryPath, summaryLines);

        // Output summary to console.
        Console.WriteLine("=== Barcode Merge Summary ===");
        foreach (string line in summaryLines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine($"Summary file saved to: {summaryPath}");
    }
}