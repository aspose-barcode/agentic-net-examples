// Title: Barcode Detection with XML Checkpoint Export
// Description: Demonstrates generating barcodes, detecting them, and exporting the reader state to XML after each successful detection.
// Category-Description: Shows Aspose.BarCode generation and recognition workflow, covering BarcodeGenerator, BarCodeReader, and ExportToXml for checkpointing. Useful for developers implementing step‑by‑step processing, error recovery, or audit trails in barcode scanning applications.
// Prompt: Implement checkpoint functionality by exporting the state to XML after each successful barcode detection.
// Tags: barcode generation, barcode recognition, checkpoint, xml export, code128, qr, datamatrix, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates sample barcodes, reads them, and exports detection checkpoints to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, reads each image, prints detection results, and saves a checkpoint XML file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes and checkpoints
        string tempFolder = Path.Combine(Path.GetTempPath(), "Checkpoint_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        var barcodeFiles = new List<string>();
        GenerateBarcode(EncodeTypes.Code128, "CODE128-12345", Path.Combine(tempFolder, "code128.png"));
        barcodeFiles.Add(Path.Combine(tempFolder, "code128.png"));
        GenerateBarcode(EncodeTypes.QR, "https://example.com", Path.Combine(tempFolder, "qr.png"));
        barcodeFiles.Add(Path.Combine(tempFolder, "qr.png"));
        GenerateBarcode(EncodeTypes.DataMatrix, "DM-98765", Path.Combine(tempFolder, "datamatrix.png"));
        barcodeFiles.Add(Path.Combine(tempFolder, "datamatrix.png"));

        // Process each barcode image and export a checkpoint after successful detection
        int index = 0;
        foreach (string filePath in barcodeFiles)
        {
            // Verify that the image file exists before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize the barcode reader for the current image
                using (BarCodeReader reader = new BarCodeReader(filePath))
                {
                    // Attempt to read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If any barcodes were detected, output details and export a checkpoint
                    if (results != null && results.Length > 0)
                    {
                        Console.WriteLine($"Detected {results.Length} barcode(s) in {Path.GetFileName(filePath)}:");
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }

                        // Export the reader's internal state to an XML file for later recovery or auditing
                        string checkpointPath = Path.Combine(tempFolder, $"checkpoint_{index}.xml");
                        reader.ExportToXml(checkpointPath);
                        Console.WriteLine($"Checkpoint exported to: {checkpointPath}");
                    }
                    else
                    {
                        Console.WriteLine($"No barcodes detected in {Path.GetFileName(filePath)}.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the image cannot be processed (e.g., unsupported format)
                Console.WriteLine($"Failed to read image '{filePath}': {ex.Message}");
            }

            index++;
        }

        // Cleanup temporary folder (optional)
        // Directory.Delete(tempFolder, true);
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text, then saves it to the given path.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The data to encode in the barcode.</param>
    /// <param name="outputPath">File system path where the PNG image will be saved.</param>
    static void GenerateBarcode(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        // Create a barcode generator with the desired type and content
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Optional: adjust visual appearance (e.g., module size)
            generator.Parameters.Barcode.XDimension.Point = 0.8f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated barcode: {outputPath}");
        }
    }
}