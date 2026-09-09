// Title: Compare XDimension Small mode vs default detection for small Code128 barcodes
// Description: Demonstrates generating small Code128 barcodes and measuring detection success using default settings, XDimension Small mode, and UseMinimalXDimension mode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure QualitySettings.XDimension for improved detection of small barcodes. It uses BarcodeGenerator, BarCodeReader, and related classes, typical for developers needing to evaluate detection rates across different XDimension modes.
// Prompt: Compare detection rates of small barcodes using XDimension mode versus default detection.
// Tags: barcode symbology, detection, xdimension, code128, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the XDimension detection comparison example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates small Code128 barcodes, reads them with various XDimension settings, and reports detection counts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "XDimCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define a set of short code texts to produce small barcodes
        List<string> codeTexts = new List<string> { "A", "AB", "ABC", "12345", "XYZ" };
        List<string> generatedFiles = new List<string>();

        // Generate barcode images using default generator settings
        for (int i = 0; i < codeTexts.Count; i++)
        {
            string text = codeTexts[i];
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // No explicit XDimension is set; generator uses default values
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        int defaultDetected = 0;
        int smallModeDetected = 0;
        int useMinimalDetected = 0;

        // Iterate over each generated image and attempt detection with different XDimension configurations
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
                continue;

            // 1. Default detection (no XDimension mode applied)
            using (var readerDefault = new BarCodeReader(file, DecodeType.Code128))
            {
                BarCodeResult[] results = readerDefault.ReadBarCodes();
                if (results.Length > 0)
                    defaultDetected++;
            }

            // 2. Detection with XDimension Small mode
            using (var readerSmall = new BarCodeReader(file, DecodeType.Code128))
            {
                readerSmall.QualitySettings.XDimension = XDimensionMode.Small;
                BarCodeResult[] results = readerSmall.ReadBarCodes();
                if (results.Length > 0)
                    smallModeDetected++;
            }

            // 3. Detection with UseMinimalXDimension mode (MinimalXDimension set to 1)
            using (var readerMinimal = new BarCodeReader(file, DecodeType.Code128))
            {
                readerMinimal.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                readerMinimal.QualitySettings.MinimalXDimension = 1;
                BarCodeResult[] results = readerMinimal.ReadBarCodes();
                if (results.Length > 0)
                    useMinimalDetected++;
            }
        }

        // Output the detection statistics
        Console.WriteLine($"Total barcodes generated: {generatedFiles.Count}");
        Console.WriteLine($"Detected with default settings: {defaultDetected}");
        Console.WriteLine($"Detected with XDimension Small mode: {smallModeDetected}");
        Console.WriteLine($"Detected with UseMinimalXDimension mode: {useMinimalDetected}");

        // Clean up temporary files and folder
        try
        {
            foreach (string file in generatedFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors to avoid interrupting the example flow
        }
    }
}