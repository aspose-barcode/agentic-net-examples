// Title: Barcode generation and recognition with mid‑batch preset switching
// Description: Demonstrates creating Code128 barcodes, then reading them while changing quality presets partway through the batch to ensure earlier results remain unaffected.
// Prompt: Verify that switching presets mid‑batch does not corrupt previously obtained results.
// Tags: barcode, code128, generation, recognition, qualitysettings, batch, preset

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a set of Code128 barcodes, saves them as PNG files,
/// and then reads them back while switching the quality preset mid‑batch to verify
/// that earlier decoded results are not affected.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for barcode images
        string folder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // Sample code texts to encode
        string[] codeTexts = { "123456789012", "ABCDEF", "9876543210", "Test123", "HelloWorld" };
        string[] filePaths = new string[codeTexts.Length];

        // -------------------------------------------------
        // Generate barcode images and store their file paths
        // -------------------------------------------------
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string filePath = Path.Combine(folder, $"barcode_{i}.png");
            filePaths[i] = filePath;

            // Create a barcode generator for Code128 and save the image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                generator.Save(filePath);
            }
        }

        // -------------------------------------------------
        // Read barcodes back, switching quality presets mid‑batch
        // -------------------------------------------------
        for (int i = 0; i < filePaths.Length; i++)
        {
            // Initialize a reader for the current file
            using (var reader = new BarCodeReader(filePaths[i], DecodeType.Code128))
            {
                // Switch preset after the second barcode (mid‑batch)
                if (i >= 2)
                {
                    reader.QualitySettings = QualitySettings.HighQuality;
                }
                else
                {
                    reader.QualitySettings = QualitySettings.NormalQuality;
                }

                bool matched = false;

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    if (result.CodeText == codeTexts[i])
                    {
                        matched = true;
                        Console.WriteLine($"File {i}: Success - decoded '{result.CodeText}'.");
                    }
                    else
                    {
                        Console.WriteLine($"File {i}: Mismatch - expected '{codeTexts[i]}', got '{result.CodeText}'.");
                    }
                }

                // If no barcode was detected, report it
                if (!matched)
                {
                    Console.WriteLine($"File {i}: No barcode detected.");
                }
            }
        }

        // Cleanup (optional)
        // foreach (var path in filePaths) { File.Delete(path); }
        // Directory.Delete(folder);
    }
}