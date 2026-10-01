// Title: Code128 Barcode Generation and Recognition with Variable DPI and XDimension
// Description: Demonstrates generating Code128 barcodes with a 2‑pixel XDimension at multiple DPI settings, then recognizing them to verify detection quality.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, setting module width via XDimension, adjusting image resolution, and employing BarCodeReader to decode Code128 symbols. Developers often need to test how resolution and module size affect readability across different scanning scenarios, making this pattern useful for quality assurance and automated testing.
// Prompt: Test recognition of Code128 barcodes with XDimension set to 2 pixels across multiple image resolutions.
// Tags: code128, barcode generation, barcode recognition, xdimension, dpi, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates Code128 barcodes at various DPI settings with a fixed XDimension,
/// then reads them back to verify successful recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, reads them,
    /// outputs detection results, and cleans up all generated files.
    /// </summary>
    static void Main()
    {
        // Sample Code128 text to encode
        const string codeText = "1234567890";

        // DPI values to test (low, medium, high resolution)
        float[] resolutions = { 72f, 150f, 300f };

        // Create a unique temporary folder for the generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128Test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Keep track of generated file paths for later cleanup
        var generatedFiles = new System.Collections.Generic.List<string>();

        // -----------------------------------------------------------------
        // Barcode generation loop – one image per DPI setting
        // -----------------------------------------------------------------
        foreach (float dpi in resolutions)
        {
            string filePath = Path.Combine(tempFolder, $"code128_{dpi}dpi.png");

            // Generate a Code128 barcode with XDimension = 2 pixels and the current DPI
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f; // module width in pixels
                generator.Parameters.Resolution = dpi;               // image resolution (DPI)
                generator.Save(filePath, BarCodeImageFormat.Png);   // save as PNG
            }

            generatedFiles.Add(filePath);
        }

        // -----------------------------------------------------------------
        // Barcode recognition loop – read each generated image
        // -----------------------------------------------------------------
        foreach (float dpi in resolutions)
        {
            string filePath = Path.Combine(tempFolder, $"code128_{dpi}dpi.png");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found for resolution {dpi} DPI: {filePath}");
                continue;
            }

            // Initialize reader for Code128 symbology
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                // Read all barcodes in the image (default quality settings)
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length > 0)
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Resolution {dpi} DPI: Detected CodeText = '{result.CodeText}', Symbology = {result.CodeTypeName}, Quality = {result.ReadingQuality}");
                    }
                }
                else
                {
                    Console.WriteLine($"Resolution {dpi} DPI: No barcode detected.");
                }
            }
        }

        // -----------------------------------------------------------------
        // Cleanup: delete generated files and temporary folder
        // -----------------------------------------------------------------
        foreach (string file in generatedFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Suppress any errors during file deletion
            }
        }

        try
        {
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any errors during folder deletion
        }
    }
}