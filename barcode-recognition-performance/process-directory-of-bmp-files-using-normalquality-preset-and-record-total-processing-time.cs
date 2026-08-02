// Title: Process BMP Barcodes with NormalQuality Preset
// Description: Demonstrates reading multiple BMP barcode images using the NormalQuality preset and measuring total processing time.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeReader with QualitySettings to efficiently decode all supported symbologies from image files. Developers often need to batch‑process images, adjust quality presets, and benchmark performance; this snippet illustrates those common tasks using BarcodeGenerator, BarCodeReader, and QualitySettings classes.
// Prompt: Process a directory of BMP files using NormalQuality preset and record total processing time.
// Tags: barcode symbology, batch processing, performance measurement, normalquality, bmp, aspose.barcode, barcodegenerator, barcodereader, qualitysettings

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates sample BMP barcode images, reads them using the NormalQuality preset,
/// and reports the total processing time. Demonstrates batch processing and performance measurement with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample BMP barcodes, reads them, and outputs timing information.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and generate sample BMP barcode images.
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Path.GetTempPath(), "BarcodesSample");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Generate a few sample Code128 barcodes as BMP files.
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode{i}.bmp");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                generator.Save(filePath);
            }
        }

        // ---------------------------------------------------------------
        // Start timing the barcode reading process.
        // ---------------------------------------------------------------
        var stopwatch = Stopwatch.StartNew();

        // Process all BMP files in the folder using NormalQuality preset.
        string[] bmpFiles = Directory.GetFiles(folderPath, "*.bmp");
        foreach (string bmpFile in bmpFiles)
        {
            // Verify that the file exists before attempting to read.
            if (!File.Exists(bmpFile))
            {
                Console.WriteLine($"File not found: {bmpFile}");
                continue;
            }

            // Open the image with BarCodeReader and apply the NormalQuality preset.
            using (var reader = new BarCodeReader(bmpFile, DecodeType.AllSupportedTypes))
            {
                reader.QualitySettings = QualitySettings.NormalQuality;

                // Read all barcodes in the image and output their details.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(bmpFile)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // Stop the timer and display the total elapsed time.
        stopwatch.Stop();
        Console.WriteLine($"Total processing time: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}