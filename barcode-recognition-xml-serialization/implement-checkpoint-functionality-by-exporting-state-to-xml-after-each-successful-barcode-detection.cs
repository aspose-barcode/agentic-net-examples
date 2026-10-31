// Title: Barcode generation, detection, and checkpoint export to XML
// Description: Demonstrates creating several barcode images, recognizing them, and exporting the reader state as XML checkpoints after each successful detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for detecting them, and the ExportToXml method for persisting the reader state. Typical scenarios include batch processing of barcode images, audit logging, and recovery checkpoints in automated workflows. Developers often need to generate barcodes, read them from files, and store intermediate results for later analysis.
// Prompt: Implement checkpoint functionality by exporting the state to XML after each successful barcode detection.
// Tags: barcode generation, barcode recognition, checkpoint, xml export, code128, qr, pdf417, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode creation, detection, and checkpoint export using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them, and exports checkpoints.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for generated images and checkpoints
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeCheckpoint_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // List to hold paths of generated barcode image files
        List<string> barcodeFiles = new List<string>();

        // Generate sample barcodes of different symbologies
        GenerateBarcode(EncodeTypes.Code128, "1234567890", Path.Combine(workDir, "code128.png"));
        GenerateBarcode(EncodeTypes.QR, "Hello World", Path.Combine(workDir, "qr.png"));
        GenerateBarcode(EncodeTypes.Pdf417, "Sample PDF417 Data", Path.Combine(workDir, "pdf417.png"));

        // Add generated file paths to the collection for processing
        barcodeFiles.Add(Path.Combine(workDir, "code128.png"));
        barcodeFiles.Add(Path.Combine(workDir, "qr.png"));
        barcodeFiles.Add(Path.Combine(workDir, "pdf417.png"));

        // Process each barcode image and export a checkpoint after successful detection
        int index = 1;
        foreach (string filePath in barcodeFiles)
        {
            // Verify that the image file exists before attempting recognition
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize BarCodeReader for the current image
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Load the image to be recognized
                reader.SetBarCodeImage(filePath);

                // Perform barcode detection
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results != null && results.Length > 0)
                {
                    Console.WriteLine($"Detected {results.Length} barcode(s) in '{Path.GetFileName(filePath)}':");
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }

                    // Export the reader's internal state to an XML checkpoint file
                    string checkpointPath = Path.Combine(workDir, $"checkpoint_{index}.xml");
                    reader.ExportToXml(checkpointPath);
                    Console.WriteLine($"Checkpoint exported to: {checkpointPath}");
                }
                else
                {
                    Console.WriteLine($"No barcodes detected in '{Path.GetFileName(filePath)}'.");
                }
            }

            index++;
        }

        Console.WriteLine("Processing completed.");
    }

    /// <summary>
    /// Generates a barcode image using the specified symbology and text.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the barcode image will be saved.</param>
    static void GenerateBarcode(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        // Create a BarcodeGenerator with the desired type and content
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set simple appearance: black bars on white background
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Generated barcode saved to: {outputPath}");
        }
    }
}