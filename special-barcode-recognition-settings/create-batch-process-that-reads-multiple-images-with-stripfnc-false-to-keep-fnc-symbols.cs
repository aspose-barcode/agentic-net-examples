// Title: Batch barcode reading with StripFNC disabled to retain FNC symbols
// Description: Demonstrates generating multiple Code128 barcodes containing FNC symbols and reading them in a batch while preserving those symbols by setting StripFNC to false.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create barcodes with special Function (FNC) characters and process them in bulk. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and BarCodeResult, which developers commonly use for batch scanning, inventory management, and data capture scenarios where FNC symbols must be retained.
// Prompt: Create a batch process that reads multiple images with StripFNC false to keep FNC symbols.
// Tags: barcode symbology, batch processing, fnc symbols, stripfnc, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and reading of Code128 barcodes containing FNC symbols,
/// preserving those symbols by disabling StripFNC during recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images with FNC symbols, then reads them back
    /// in a batch while keeping the FNC characters.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "BatchFNC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define FNC symbols (using Unicode private use area as per Aspose examples)
        const string FNC1 = "\u00F1";
        const string FNC2 = "\u00F2";
        const string FNC3 = "\u00F3";

        // Generate sample barcode images with FNC symbols
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            string filePath = Path.Combine(batchFolder, $"barcode{i + 1}.png");
            string codeText = $"Sample{i + 1}{FNC1}{FNC2}{FNC3}";

            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set barcode module size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save as PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            barcodeFiles.Add(filePath);
        }

        // Batch read the generated images with StripFNC set to false
        Console.WriteLine("Batch reading with StripFNC = false (keep FNC symbols):");
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Preserve FNC symbols during decoding
                    reader.BarcodeSettings.StripFNC = false;
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error reading '{file}': {ex.Message}");
            }
        }

        // Cleanup (optional)
        // Directory.Delete(batchFolder, true);
    }
}