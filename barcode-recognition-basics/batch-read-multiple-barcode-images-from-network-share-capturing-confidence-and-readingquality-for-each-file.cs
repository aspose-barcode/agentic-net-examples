// Title: Batch barcode generation and reading with confidence metrics
// Description: Demonstrates generating multiple barcode images, storing them in a temporary folder, and reading each image to retrieve barcode type, text, confidence and reading quality.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting information, including confidence and reading quality, from images. Typical scenarios include bulk processing of barcode assets, quality assessment, and automated verification in inventory or logistics systems. Developers often need to handle multiple symbologies, manage temporary storage, and evaluate read reliability using these core API classes.
// Prompt: Batch read multiple barcode images from a network share, capturing Confidence and ReadingQuality for each file.
// Tags: barcode, generation, recognition, confidence, readingquality, batch, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a set of barcode images, reads them back, and displays confidence and reading quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates temporary barcode files, reads them, and outputs detailed results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the sample barcodes
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define sample barcodes to generate (symbology, text, file name)
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "SampleCode128", "code128.png"),
            (EncodeTypes.QR, "SampleQR", "qr.png"),
            (EncodeTypes.DataMatrix, "SampleDM", "datamatrix.png"),
            (EncodeTypes.Aztec, "SampleAztec", "aztec.png"),
            (EncodeTypes.Pdf417, "SamplePdf417", "pdf417.png")
        };

        var generatedFiles = new List<string>();

        // --------------------------------------------------------------------
        // Generate barcode images and collect their file paths
        // --------------------------------------------------------------------
        foreach (var (encodeType, codeText, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Batch barcode reading results:");
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Read each generated barcode and output Confidence and ReadingQuality
        // --------------------------------------------------------------------
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
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)}");
                            Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                            Console.WriteLine($"  Confidence: {result.Confidence}");
                            Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handles image loading failures or unsupported formats
                Console.WriteLine($"Failed to read file '{Path.GetFileName(file)}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup: delete temporary folder and its contents
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – folder may be in use or permission restricted
        }
    }
}