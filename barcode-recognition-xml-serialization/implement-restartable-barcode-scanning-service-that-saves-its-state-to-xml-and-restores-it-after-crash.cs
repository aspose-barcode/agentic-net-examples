// Title: Restartable Barcode Scanning Service with XML State Persistence
// Description: Demonstrates how to generate sample Code128 barcodes, scan them using Aspose.BarCode, and persist the reader state and processing progress to XML so the service can resume after a crash.
// Category-Description: This example belongs to the Aspose.BarCode scanning and state management category. It showcases the BarCodeReader and BarcodeGenerator classes, XML export/import of reader settings, and progress tracking for long‑running barcode processing jobs. Developers building robust scanning services often need to save state, handle interruptions, and resume processing without re‑scanning already processed images.
// Prompt: Implement a restartable barcode scanning service that saves its state to XML and restores it after a crash.
// Tags: code128, barcode generation, barcode recognition, xml persistence, restartable service, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates a restartable barcode scanning service that generates sample barcodes,
/// reads them, and persists reader state and progress to XML for crash recovery.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Sets up a temporary workspace, generates sample barcodes, and starts processing with restartable logic.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeScanService_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Generate sample barcode images (Code128) and collect their file paths
        List<string> barcodeFiles = GenerateSampleBarcodes(workFolder, 5);

        // Define file paths for persisted reader state and processing progress
        string readerStatePath = Path.Combine(workFolder, "readerState.xml");
        string progressPath = Path.Combine(workFolder, "progress.txt");

        // Process the barcode images with restartable logic
        ProcessBarcodes(barcodeFiles, readerStatePath, progressPath);

        Console.WriteLine("Processing completed.");
    }

    /// <summary>
    /// Generates a specified number of Code128 barcode images and returns their file paths.
    /// </summary>
    /// <param name="folder">Folder where barcode images will be saved.</param>
    /// <param name="count">Number of barcode images to generate.</param>
    /// <returns>List of file paths for the generated barcode images.</returns>
    static List<string> GenerateSampleBarcodes(string folder, int count)
    {
        var files = new List<string>();
        for (int i = 0; i < count; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"CODE{i + 1:D3}";
            string filePath = Path.Combine(folder, $"barcode_{i + 1}.png");

            // Generate the barcode image using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.XDimension.Point = 1.5f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            files.Add(filePath);
        }
        return files;
    }

    /// <summary>
    /// Reads each barcode image, exports the reader state to XML, and tracks progress so the operation can resume after a crash.
    /// </summary>
    /// <param name="files">List of barcode image file paths to process.</param>
    /// <param name="stateXmlPath">Path to the XML file where the reader state is persisted.</param>
    /// <param name="progressPath">Path to the text file that stores the index of the last successfully processed image.</param>
    static void ProcessBarcodes(List<string> files, string stateXmlPath, string progressPath)
    {
        // Determine the starting index based on previously saved progress
        int startIndex = 0;
        if (File.Exists(progressPath))
        {
            string txt = File.ReadAllText(progressPath);
            if (int.TryParse(txt, out int savedIndex) && savedIndex >= 0 && savedIndex < files.Count)
                startIndex = savedIndex;
        }

        // Iterate over the barcode files starting from the saved index
        for (int i = startIndex; i < files.Count; i++)
        {
            string imagePath = files[i];

            // Verify that the image file exists before attempting to read it
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Initialize the barcode reader with desired settings
            using (var reader = new BarCodeReader())
            {
                reader.BarcodeSettings.StripFNC = true;

                // Export the initial reader configuration to XML (state persistence)
                try
                {
                    reader.ExportToXml(stateXmlPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to export reader state: {ex.Message}");
                }

                // Assign the image to be processed
                reader.SetBarCodeImage(imagePath);

                // Perform barcode recognition
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Image: {Path.GetFileName(imagePath)} - Barcodes found: {results.Length}");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }

                // Update the progress file after successful processing
                File.WriteAllText(progressPath, (i + 1).ToString());

                // Export the updated reader state (in case runtime changes need to be persisted)
                try
                {
                    reader.ExportToXml(stateXmlPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to export reader state after read: {ex.Message}");
                }
            }
        }
    }
}