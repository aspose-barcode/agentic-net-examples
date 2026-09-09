// Title: Validate detected barcode symbologies in a combined image
// Description: Demonstrates generating multiple barcode types, combining them into a single image, and verifying that the barcode reader correctly identifies each symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, Aspose.Drawing to compose images, and BarCodeReader with DecodeType.AllSupportedTypes to detect multiple symbologies in one image. Developers often need to batch‑process or validate mixed barcode scans, making this pattern useful for testing and quality‑control scenarios.
// Prompt: Validate that FoundBarCodes collection contains expected symbology types after processing a mixed barcode image.
// Tags: barcode symbology detection generation composition validation aspose.barcode aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates several barcodes, merges them into one image,
/// and validates that each expected symbology is detected by the reader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the generation, composition, and validation steps.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "MixedBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the barcodes to generate: type, text, and output file name
        var barcodeInfos = new List<(BaseEncodeType Encode, string Text, string FileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.Aztec, "AZTEC", "aztec.png"),
            (EncodeTypes.Code39, "CODE39", "code39.png"),
            (EncodeTypes.Codabar, "A123B", "codabar.png")
        };

        var generatedFiles = new List<string>();

        // Generate individual barcode images and store their file paths
        foreach (var info in barcodeInfos)
        {
            using (var generator = new BarcodeGenerator(info.Encode, info.Text))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                string filePath = Path.Combine(tempDir, info.FileName);
                generator.Save(filePath, BarCodeImageFormat.Png);
                generatedFiles.Add(filePath);
            }
        }

        // Load each generated image into a Bitmap for later composition
        var bitmaps = new List<Bitmap>();
        foreach (var file in generatedFiles)
        {
            if (File.Exists(file))
            {
                var bmp = new Bitmap(file);
                bitmaps.Add(bmp);
            }
        }

        // Compose a single image that places all barcodes side by side with spacing
        int spacing = 10;
        int totalWidth = bitmaps.Sum(b => b.Width) + spacing * (bitmaps.Count - 1);
        int maxHeight = bitmaps.Max(b => b.Height);
        string mixedPath = Path.Combine(tempDir, "mixed.png");

        using (var combined = new Bitmap(totalWidth, maxHeight))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
                int offsetX = 0;
                foreach (var bmp in bitmaps)
                {
                    graphics.DrawImage(bmp, offsetX, 0, bmp.Width, bmp.Height);
                    offsetX += bmp.Width + spacing;
                }
                combined.Save(mixedPath, Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // Release resources held by the individual bitmaps
        foreach (var bmp in bitmaps)
        {
            bmp.Dispose();
        }

        // Verify that the combined image was successfully created
        if (!File.Exists(mixedPath))
        {
            Console.WriteLine("Mixed barcode image not found.");
            return;
        }

        // Read all barcodes from the combined image using the universal decoder
        using (var reader = new BarCodeReader(mixedPath, DecodeType.AllSupportedTypes))
        {
            reader.ReadBarCodes();

            Console.WriteLine($"FoundCount: {reader.FoundCount}");
            var foundTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var result in reader.FoundBarCodes)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                foundTypes.Add(result.CodeTypeName);
            }

            // Define the expected symbology names and verify their presence
            var expectedTypes = new[] { "Code128", "Aztec", "Code39", "Codabar" };
            bool allPresent = expectedTypes.All(t => foundTypes.Contains(t));

            Console.WriteLine(allPresent
                ? "All expected symbologies detected."
                : "Missing one or more expected symbologies.");
        }
    }
}