// Title: Batch barcode reading with StripFNC disabled to retain FNC symbols
// Description: Demonstrates generating several Code128 barcode images and then reading them in a batch while preserving FNC characters by setting StripFNC to false.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and how to configure BarcodeSettings (StripFNC) for scenarios where FNC symbols must be kept. Typical use cases include bulk processing of scanned documents, inventory systems, and any workflow that requires accurate preservation of special barcode characters.
// Prompt: Create a batch process that reads multiple images with StripFNC false to keep FNC symbols.
// Tags: code128, batch, stripfnc, barcode-generation, barcode-recognition, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of Code128 barcodes and subsequent reading with StripFNC disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcode images, reads them while preserving FNC symbols, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a dedicated temporary folder for the batch process
        // --------------------------------------------------------------------
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // --------------------------------------------------------------------
        // Prepare sample barcode data and a list to hold generated file paths
        // --------------------------------------------------------------------
        List<string> texts = new List<string> { "Sample1", "Sample2", "Sample3" };
        List<string> imageFiles = new List<string>();

        // --------------------------------------------------------------------
        // Generate barcode images (PNG) for each sample text
        // --------------------------------------------------------------------
        foreach (string text in texts)
        {
            string filePath = Path.Combine(batchFolder, text + ".png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set X-dimension to 2 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        Console.WriteLine("Batch reading barcodes with StripFNC = false");

        // --------------------------------------------------------------------
        // Read each barcode image with StripFNC set to false (keep FNC symbols)
        // --------------------------------------------------------------------
        foreach (string file in imageFiles)
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
                    // Disable stripping of FNC characters
                    reader.BarcodeSettings.StripFNC = false;

                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode
                Console.WriteLine($"Warning: Unable to read '{Path.GetFileName(file)}' - {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary folder (optional)
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files still in use)
        }
    }
}