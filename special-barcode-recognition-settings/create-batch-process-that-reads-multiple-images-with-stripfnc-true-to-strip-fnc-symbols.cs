// Title: Batch barcode processing with StripFNC to remove FNC symbols
// Description: Demonstrates generating multiple Code128 barcodes containing FNC1/FNC2 symbols, then reading them with StripFNC enabled to strip those symbols from the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create barcodes, configure decoding settings, and perform batch processing. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and BarCodeResult, which developers commonly use for automated barcode workflows, bulk image handling, and data sanitization in inventory or logistics systems.
// Prompt: Create a batch process that reads multiple images with StripFNC true to strip FNC symbols.
// Tags: barcode, code128, stripfnc, batch, generation, recognition, aspose.barcode, image, fnc

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and reading of Code128 barcodes with FNC symbols,
/// using Aspose.BarCode to strip FNC characters during recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them with StripFNC enabled,
    /// outputs results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "BatchFNC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample barcode data with FNC symbols
        char FNC1 = (char)241; // FNC1 control character
        char FNC2 = (char)242; // FNC2 control character
        string baseText = "Sample";
        string codeText = baseText + FNC1 + FNC2;

        // List to hold generated file paths
        List<string> barcodeFiles = new List<string>();

        // Generate a few barcode images
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(batchFolder, $"Code128FNC_{i}.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                gen.Parameters.Barcode.XDimension.Pixels = 2f;
                gen.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        Console.WriteLine("Batch processing of barcodes with StripFNC = true");

        // Read each barcode image with StripFNC set to true
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
                    // Enable stripping of FNC characters during decoding
                    reader.BarcodeSettings.StripFNC = true;

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
                Console.WriteLine($"Failed to read '{file}': {ex.Message}");
            }
        }

        // Cleanup: delete temporary files and folder
        try
        {
            foreach (string file in barcodeFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(batchFolder))
                Directory.Delete(batchFolder);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}