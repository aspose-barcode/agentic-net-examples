// Title: Batch barcode generation and quality preset verification
// Description: Demonstrates generating multiple barcode images, processing them in two batches with different quality presets, and confirming that changing presets does not affect previously obtained results.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and QualitySettings to control recognition accuracy. Typical scenarios include high‑throughput barcode generation, automated quality checks, and validation of recognition settings across large image sets. Developers often need to switch between NormalQuality and HighQuality presets without corrupting earlier decoded data, making this pattern valuable for CI pipelines and batch verification tasks.
// Prompt: Verify that switching presets mid‑batch does not corrupt previously obtained results.
// Tags: barcode symbology, generation, recognition, batch processing, quality settings, aspose.barcode, png, verification

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a batch of barcodes, reads them using different
/// quality presets, and verifies that switching presets does not corrupt earlier results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, reads them with NormalQuality and HighQuality
    /// presets, and validates that results remain consistent after preset changes.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare barcode definitions (type, text, output file name)
        var barcodeInfos = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "CODE128TEST", "code128.png"),
            (EncodeTypes.QR, "QRTEST123", "qr.png"),
            (EncodeTypes.DataMatrix, "DATAMATRIX", "datamatrix.png")
        };

        // Generate barcode images and collect their file paths
        var generatedFiles = new List<string>();
        foreach (var info in barcodeInfos)
        {
            string filePath = Path.Combine(batchFolder, info.fileName);
            using (var generator = new BarcodeGenerator(info.type, info.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Split the generated files into two batches for separate processing
        int splitIndex = generatedFiles.Count / 2;
        var firstBatch = generatedFiles.GetRange(0, splitIndex);
        var secondBatch = generatedFiles.GetRange(splitIndex, generatedFiles.Count - splitIndex);

        // Store decoding results from the first batch for later verification
        var firstBatchResults = new Dictionary<string, string>();

        // Process the first batch using the NormalQuality preset
        foreach (string file in firstBatch)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix))
            {
                reader.QualitySettings = QualitySettings.NormalQuality;
                reader.ReadBarCodes();
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    firstBatchResults[file] = result.CodeText;
                }
            }
        }

        // Process the second batch using the HighQuality preset (no result storage needed)
        foreach (string file in secondBatch)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix))
            {
                reader.QualitySettings = QualitySettings.HighQuality;
                reader.ReadBarCodes();
                // Intentionally ignore results; we only need to ensure no exceptions occur
            }
        }

        // Verify that results from the first batch remain unchanged when read with HighQuality
        bool allMatch = true;
        foreach (var kvp in firstBatchResults)
        {
            string file = kvp.Key;
            string expected = kvp.Value;
            using (var reader = new BarCodeReader(file, DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix))
            {
                reader.QualitySettings = QualitySettings.HighQuality; // switch preset again
                reader.ReadBarCodes();
                string actual = null;
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    actual = result.CodeText;
                    break; // only need the first result
                }
                if (actual != expected)
                {
                    allMatch = false;
                    Console.WriteLine($"Mismatch in file '{Path.GetFileName(file)}': expected '{expected}', got '{actual ?? "null"}'");
                }
            }
        }

        // Output verification summary
        Console.WriteLine(allMatch
            ? "Verification succeeded: switching presets did not corrupt previous results."
            : "Verification failed: some results changed after preset switch.");

        // Cleanup temporary folder (ignore any errors during deletion)
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }
}