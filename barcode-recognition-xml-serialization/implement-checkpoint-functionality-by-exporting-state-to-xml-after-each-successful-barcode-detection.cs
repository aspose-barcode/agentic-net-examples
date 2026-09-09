// Title: Barcode Generation, Detection, and Checkpoint Export to XML
// Description: Demonstrates generating Code128 barcodes, detecting them, and exporting the reader state as XML checkpoints after each successful detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and the ExportToXml method for persisting reader state. Typical use cases include batch processing of barcode images, audit logging, and recovery checkpoints in automated workflows. Developers often need to generate barcodes, read them in various formats, and capture processing state for debugging or resumption purposes.
/// Prompt: Implement checkpoint functionality by exporting the state to XML after each successful barcode detection.
// Tags: barcode, code128, generation, recognition, xml, checkpoint, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, detection, and exporting reader state as XML checkpoints.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates barcodes, reads them, and saves checkpoints.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeCheckpointDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode texts to encode
        string[] codes = { "ABC123", "XYZ789", "HELLO2026" };
        string[] filePaths = new string[codes.Length];

        // Generate barcode images and store their file paths
        for (int i = 0; i < codes.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codes[i]))
            {
                // Set barcode visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2;
                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            filePaths[i] = filePath;
        }

        // Process each barcode image and export reader state after successful detection
        for (int i = 0; i < filePaths.Length; i++)
        {
            string imagePath = filePaths[i];
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Attempt to read barcodes from the image
                var results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                {
                    Console.WriteLine($"Detected {results.Length} barcode(s) in {Path.GetFileName(imagePath)}:");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }

                    // Export reader state to XML as a checkpoint
                    string checkpointPath = Path.Combine(tempFolder, $"checkpoint_{i}.xml");
                    reader.ExportToXml(checkpointPath);
                    Console.WriteLine($"Reader state exported to: {checkpointPath}");
                }
                else
                {
                    Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}.");
                }
            }
        }

        // Cleanup: optionally delete the temporary folder
        // Directory.Delete(tempFolder, true);
    }
}