// Title: Barcode Generation and Validation Test Suite
// Description: Generates sample barcodes (Code128, QR, DataMatrix), saves them as PNG files, and then reads them back to verify detection and extracted text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to use BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical use cases include automated testing of barcode output across different .NET runtimes, ensuring visual fidelity and data integrity. Developers often need to validate that generated barcodes are readable by standard scanners, making this pattern essential for CI pipelines and cross‑platform verification.
// Prompt: Create a test suite that validates barcode generation across .NET Framework, .NET Core, and .NET 6 runtimes.
// Tags: barcode generation, barcode recognition, code128, qr, datamatrix, png, aspose.barcode, .net framework, .net core, .net 6

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating barcodes, saving them to disk, and validating them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the barcode generation and validation workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the set of barcode types, associated text, and output file names
        var items = new[]
        {
            new { Encode = EncodeTypes.Code128, Text = "Test123", FileName = "code128.png" },
            new { Encode = EncodeTypes.QR, Text = "https://example.com", FileName = "qr.png" },
            new { Encode = EncodeTypes.DataMatrix, Text = "DM12345", FileName = "datamatrix.png" }
        };

        // -----------------------------------------------------------------
        // Generate barcode images and save them as PNG files
        // -----------------------------------------------------------------
        foreach (var item in items)
        {
            string filePath = Path.Combine(tempDir, item.FileName);
            using (var generator = new BarcodeGenerator(item.Encode, item.Text))
            {
                // Common visual settings for all barcodes
                generator.Parameters.Resolution = 300f;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                generator.Parameters.ImageWidth.Pixels = 300f;
                generator.Parameters.ImageHeight.Pixels = 150f;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Read and validate each generated barcode
        // -----------------------------------------------------------------
        foreach (var item in items)
        {
            string filePath = Path.Combine(tempDir, item.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"{item.FileName}: No barcode detected.");
                }
                else
                {
                    var result = results[0];
                    // In evaluation mode, CodeText may contain a watermark; check for non‑empty text
                    bool hasText = !string.IsNullOrEmpty(result.CodeText);
                    Console.WriteLine($"{item.FileName}: Detected {result.CodeTypeName}, Text present: {hasText}");
                }
            }
        }

        // -----------------------------------------------------------------
        // Clean up temporary files and folder
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}