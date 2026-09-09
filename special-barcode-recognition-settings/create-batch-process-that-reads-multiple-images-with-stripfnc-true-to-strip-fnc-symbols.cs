// Title: Batch barcode reading with StripFNC enabled
// Description: Demonstrates how to generate multiple Code128 barcode images, then read them in a batch while stripping FNC symbols.
// Category-Description: This example belongs to the Aspose.BarCode batch processing and decoding category. It showcases the use of BarcodeGenerator for image creation and BarCodeReader with the StripFNC setting to remove Function Code (FNC) characters during recognition. Developers often need to process many barcode images automatically and control FNC handling, making this pattern useful for bulk scanning scenarios.
// Prompt: Create a batch process that reads multiple images with StripFNC true to strip FNC symbols.
// Tags: code128, batch processing, stripfnc, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a set of Code128 barcode images,
/// then reads them back in a batch with the StripFNC option enabled
/// to remove any Function Code (FNC) symbols from the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images,
    /// reads them with StripFNC = true, outputs results, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the sample files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate sample barcode images (Code128) and collect their paths
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Set X-dimension to control barcode size
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        Console.WriteLine("Batch reading with StripFNC = true");

        // --------------------------------------------------------------------
        // Read each generated image with StripFNC set to true
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
                    // Enable stripping of FNC symbols during decoding
                    reader.BarcodeSettings.StripFNC = true;

                    // Iterate through all detected barcodes in the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error reading {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup: delete temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            foreach (string file in imageFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup error: {ex.Message}");
        }
    }
}