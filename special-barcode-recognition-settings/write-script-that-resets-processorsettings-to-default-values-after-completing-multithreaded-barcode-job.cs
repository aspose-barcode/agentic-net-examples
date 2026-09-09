// Title: Reset ProcessorSettings after multithreaded barcode processing
// Description: Demonstrates generating barcode images, configuring multithreaded processor settings for reading, and then restoring the default settings.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and the static ProcessorSettings class to control CPU core usage during multithreaded barcode decoding. Developers often need to tune these settings for performance‑critical applications and then reset them to defaults to avoid side effects in subsequent operations.
// Prompt: Write a script that resets ProcessorSettings to default values after completing a multithreaded barcode job.
// Tags: barcode symbology, multithreaded processing, processor settings, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates barcodes, reads them using custom multithreaded processor settings,
/// and finally resets those settings to their default values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = $"CODE{i}";
            string filePath = Path.Combine(tempFolder, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Save each barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Configure custom multithreaded processor settings (e.g., force single‑core mode)
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Perform barcode reading using the configured settings
        BaseDecodeType decodeType = DecodeType.Code128;
        foreach (string file in barcodeFiles)
        {
            using (var reader = new BarCodeReader(file, decodeType))
            {
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Reset processor settings to their default values to avoid affecting other code
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – the OS will clean up temp files later
        }
    }
}