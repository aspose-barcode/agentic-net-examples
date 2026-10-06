// Title: Merge Barcode Reader XML States and Summarize Detected Barcodes
// Description: Demonstrates how to generate sample barcodes, export each reader's state to XML, import those states, and produce a consolidated text summary of all detected barcodes.
// Category-Description: This example belongs to the Aspose.BarCode processing suite, illustrating state export/import, barcode generation, and recognition workflows. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and methods like ExportToXml and ImportFromXml. Developers often need to persist reader configurations, merge results from multiple scans, or create audit reports, making this pattern useful for batch processing and reporting scenarios.
// Prompt: Implement a feature that merges multiple XML state files into a single document summarizing all detected barcodes.
// Tags: barcode, merge, xml, state, summary, generation, recognition, aspose.barcode, png, txt

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates merging multiple barcode reader XML state files into a single summary document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes, creates XML state files, imports them, and writes a summary of detected barcodes.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for generated images and state files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeMergeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Step 1: Generate sample barcode images and collect their file paths
        List<string> imageFiles = new List<string>();
        GenerateSampleBarcodes(workDir, imageFiles);

        // Step 2: For each image, read barcodes and export the reader state to an XML file
        List<string> stateFiles = new List<string>();
        CreateReaderStateFiles(workDir, imageFiles, stateFiles);

        // Step 3: Import each XML state, reassign the original image, read barcodes again, and collect summary lines
        List<string> summaryLines = new List<string>();
        for (int i = 0; i < stateFiles.Count; i++)
        {
            string statePath = stateFiles[i];
            string imagePath = imageFiles[i];

            using (BarCodeReader reader = BarCodeReader.ImportFromXml(statePath))
            {
                // The image source is not stored in the XML; set it explicitly before reading
                reader.SetBarCodeImage(imagePath);

                // Read barcodes using the imported state
                BarCodeResult[] results = reader.ReadBarCodes();

                // Build a human‑readable line for each detected barcode
                foreach (BarCodeResult result in results)
                {
                    string line = $"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}";
                    summaryLines.Add(line);
                }
            }
        }

        // Step 4: Write the summary lines to the console and to a text file in the working directory
        string summaryPath = Path.Combine(workDir, "BarcodeSummary.txt");
        using (StreamWriter writer = new StreamWriter(summaryPath, false))
        {
            foreach (string line in summaryLines)
            {
                Console.WriteLine(line);
                writer.WriteLine(line);
            }
        }

        // Optional cleanup: keep the work directory for inspection or delete it
        // Directory.Delete(workDir, true);
    }

    /// <summary>
    /// Generates sample barcode images (Code128, QR, PDF417) and stores their file paths.
    /// </summary>
    /// <param name="folder">The folder where images will be saved.</param>
    /// <param name="imagePaths">A list to receive the generated image file paths.</param>
    private static void GenerateSampleBarcodes(string folder, List<string> imagePaths)
    {
        // Code128 barcode
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            string path = Path.Combine(folder, "code128.png");
            gen.Save(path, BarCodeImageFormat.Png);
            imagePaths.Add(path);
        }

        // QR Code barcode
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            string path = Path.Combine(folder, "qr.png");
            gen.Save(path, BarCodeImageFormat.Png);
            imagePaths.Add(path);
        }

        // PDF417 barcode
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Pdf417, "PDF417 Sample Text"))
        {
            string path = Path.Combine(folder, "pdf417.png");
            gen.Save(path, BarCodeImageFormat.Png);
            imagePaths.Add(path);
        }
    }

    /// <summary>
    /// Reads each barcode image, exports the reader's internal state to an XML file, and records the state file paths.
    /// </summary>
    /// <param name="folder">The folder where state files will be saved.</param>
    /// <param name="imagePaths">List of barcode image file paths.</param>
    /// <param name="statePaths">A list to receive the generated XML state file paths.</param>
    private static void CreateReaderStateFiles(string folder, List<string> imagePaths, List<string> statePaths)
    {
        for (int i = 0; i < imagePaths.Count; i++)
        {
            string imagePath = imagePaths[i];
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Perform an initial read to populate the reader's internal state
                reader.ReadBarCodes();

                // Export the populated state to an XML file
                string statePath = Path.Combine(folder, $"state_{i}.xml");
                reader.ExportToXml(statePath);
                statePaths.Add(statePath);
            }
        }
    }
}