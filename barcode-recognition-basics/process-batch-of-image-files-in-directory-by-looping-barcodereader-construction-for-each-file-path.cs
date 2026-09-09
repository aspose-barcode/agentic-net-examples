// Title: Batch barcode generation and reading using Aspose.BarCode
// Description: This example generates multiple barcode images, saves them to a temporary directory, and then reads each image to extract and display the barcode information.
// Category-Description: Demonstrates Aspose.BarCode's generation and recognition APIs for batch processing scenarios. It uses BarcodeGenerator to create images of various symbologies and BarCodeReader to decode them, a common workflow for applications that need to validate or process large sets of barcode files. Ideal for developers implementing automated scanning, inventory management, or document processing pipelines.
// Prompt: Process a batch of image files in a directory by looping BarCodeReader construction for each file path.
// Tags: barcode generation, barcode recognition, batch processing, csharp, aspose.barcode, png, decode, encode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch creation of barcode images and subsequent reading of each file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes, reads them back, and writes results to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define sample barcode data: symbology, text, and output file name
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE", "code128.png"),
            (EncodeTypes.QR, "QR_SAMPLE_TEXT", "qr.png"),
            (EncodeTypes.DataMatrix, "DM_SAMPLE", "datamatrix.png"),
            (EncodeTypes.Aztec, "AZTEC_SAMPLE", "aztec.png"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE", "pdf417.png")
        };

        var generatedFiles = new List<string>();

        // Generate barcode images and collect their file paths
        foreach (var (encodeType, codeText, fileName) in samples)
        {
            string filePath = Path.Combine(batchFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Generated barcode images:");
        foreach (var file in generatedFiles)
        {
            Console.WriteLine(file);
        }

        Console.WriteLine();
        Console.WriteLine("Reading barcodes from generated images:");

        // Process each image file with BarCodeReader
        foreach (string filePath in generatedFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: No barcodes detected.");
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"{Path.GetFileName(filePath)} - {result.CodeTypeName}: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Skipping file {Path.GetFileName(filePath)} due to loading error: {ex.Message}");
            }
        }

        // Cleanup: delete temporary folder and its contents
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore - folder will be removed by OS temp cleanup
        }
    }
}