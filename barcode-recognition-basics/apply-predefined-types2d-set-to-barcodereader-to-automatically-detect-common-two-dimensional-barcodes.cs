// Title: Detect 2D Barcodes Using BarCodeReader with Types2D Set
// Description: This example generates QR and DataMatrix barcodes, saves them to temporary files, and then uses BarCodeReader with the Types2D preset to automatically detect common two‑dimensional barcodes.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It showcases the BarcodeGenerator for creating QR and DataMatrix symbols and the BarCodeReader with DecodeType.AllSupportedTypes (which includes the Types2D preset) for automatic detection of 2D symbologies. Developers working with inventory, ticketing, or mobile scanning often need to generate barcodes and later read them without knowing the exact type in advance; this snippet provides a concise reference for such scenarios.
// Prompt: Apply the predefined Types2D set to BarCodeReader to automatically detect common two‑dimensional barcodes.
// Tags: barcode symbology, detection, 2d, types2d, aspose.barcode, generation, recognition, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of QR and DataMatrix barcodes and automatic detection of them using BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them back, and outputs detection details.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate: QR and DataMatrix.
        var samples = new[]
        {
            new { Type = EncodeTypes.QR, Text = "https://example.com/qr", File = Path.Combine(tempFolder, "qr.png") },
            new { Type = EncodeTypes.DataMatrix, Text = "DM_SAMPLE_12345", File = Path.Combine(tempFolder, "datamatrix.png") }
        };

        // -----------------------------------------------------------------
        // Generate barcode images and save them as PNG files.
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            using (var generator = new BarcodeGenerator(sample.Type, sample.Text))
            {
                generator.Save(sample.File, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated {sample.Type} barcode at: {sample.File}");
            }
        }

        // -----------------------------------------------------------------
        // Read each generated image and attempt to detect any supported barcode.
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            if (!File.Exists(sample.File))
            {
                Console.WriteLine($"File not found: {sample.File}");
                continue;
            }

            // Use BarCodeReader with DecodeType.AllSupportedTypes (includes Types2D preset) to auto‑detect 2D barcodes.
            using (var reader = new BarCodeReader(sample.File, DecodeType.AllSupportedTypes))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {sample.File}");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {sample.File}");
                        Console.WriteLine($"  Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"  Code Text   : {result.CodeText}");
                        Console.WriteLine($"  Reading Quality: {result.ReadingQuality}");
                        Console.WriteLine($"  Region Angle: {result.Region.Angle}");
                    }
                }
            }
        }

        // -----------------------------------------------------------------
        // Clean up temporary files and folder.
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup (e.g., files in use).
        }
    }
}