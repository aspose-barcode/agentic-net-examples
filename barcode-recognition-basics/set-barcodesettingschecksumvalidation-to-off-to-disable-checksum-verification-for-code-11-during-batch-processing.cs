// Title: Code11 barcode generation and checksum‑disabled batch reading
// Description: Demonstrates generating Code11 barcodes, saving them as PNG, and reading them back with checksum validation turned off.
// Prompt: Set BarcodeSettings.ChecksumValidation to Off to disable checksum verification for Code 11 during batch processing.
// Tags: code11, barcode, generation, recognition, checksumvalidation, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates Code11 barcodes, saves them as PNG files, and reads them back with checksum validation disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Creates sample Code11 barcodes, writes PNG files, then reads them while turning off checksum verification.
    /// </summary>
    static void Main()
    {
        // Define sample Code11 values to encode
        string[] codeTexts = { "12345", "67890", "112233" };

        // Prepare output directory for generated barcode images
        string outputFolder = "Barcodes";
        Directory.CreateDirectory(outputFolder);

        // ------------------------------------------------------------
        // Generate Code11 barcodes and save each as a PNG file
        // ------------------------------------------------------------
        for (int i = 0; i < codeTexts.Length; i++)
        {
            // Build full file path for the current barcode image
            string filePath = Path.Combine(outputFolder, $"code11_{i}.png");

            // Create a barcode generator for Code11 with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code11, codeTexts[i]))
            {
                // Save the generated barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Read each generated barcode image with checksum validation disabled
        // ------------------------------------------------------------
        foreach (string file in Directory.GetFiles(outputFolder, "*.png"))
        {
            // Verify that the file actually exists before attempting to read
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Initialize a barcode reader for Code11
            using (var reader = new BarCodeReader(file, DecodeType.Code11))
            {
                // Disable checksum verification for Code11 as required by the prompt
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Output basic barcode information
                    Console.WriteLine($"File: {Path.GetFileName(file)}  CodeText: {result.CodeText}");

                    // If extended 1D information is available, display the checksum value
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
        }
    }
}