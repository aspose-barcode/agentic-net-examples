// Title: Batch barcode generation and recognition using Aspose.BarCode
// Description: Demonstrates creating multiple barcode images in a temporary folder, then reading each image to extract barcode data.
// Category-Description: This example belongs to the Aspose.BarCode batch processing collection, illustrating how to use BarcodeGenerator for image creation and BarCodeReader for decoding. Typical use cases include bulk barcode creation, automated scanning of generated files, and validation pipelines. Developers often need to loop over file paths, handle various symbologies, and manage temporary resources.
// Prompt: Process a batch of image files in a directory by looping BarCodeReader construction for each file path.
// Tags: barcode, generation, recognition, batch processing, aspose.barcode, csharp, symbology

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch creation of barcode images and subsequent reading of each image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them back, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the sample batch
        // --------------------------------------------------------------------
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // --------------------------------------------------------------------
        // Prepare sample barcode data (symbology, text, and output file name)
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType Encode, string Text, string FileName)>
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE", "code128.png"),
            (EncodeTypes.QR, "QR_SAMPLE", "qr.png"),
            (EncodeTypes.DataMatrix, "DM_SAMPLE", "datamatrix.png"),
            (EncodeTypes.Aztec, "AZTEC_SAMPLE", "aztec.png"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE", "pdf417.png")
        };

        // --------------------------------------------------------------------
        // Generate barcode images and collect their file paths
        // --------------------------------------------------------------------
        var filePaths = new List<string>();
        foreach (var (encode, text, fileName) in samples)
        {
            string filePath = Path.Combine(batchFolder, fileName);
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            filePaths.Add(filePath);
        }

        Console.WriteLine($"Generated {filePaths.Count} barcode images in: {batchFolder}");
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Read each barcode image using BarCodeReader
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        foreach (string path in filePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(path, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcodes detected in file: {Path.GetFileName(path)}");
                    }
                    else
                    {
                        Console.WriteLine($"Barcodes in {Path.GetFileName(path)}:");
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Failed to load image '{Path.GetFileName(path)}': {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(path)}': {ex.Message}");
            }

            Console.WriteLine();
        }

        // --------------------------------------------------------------------
        // Cleanup: delete temporary folder and its contents
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // If deletion fails (e.g., files in use), ignore – the OS will clean up temp files later.
        }
    }
}