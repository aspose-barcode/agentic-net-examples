// Title: Validate Detected Barcode Symbologies in a Mixed Image
// Description: This example generates multiple barcode types, merges them into a single image, reads the combined image, and checks that each expected symbology is present.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create barcodes with BarcodeGenerator, compose them using Aspose.Drawing, and extract information with BarCodeReader. Typical for scenarios where developers need to batch‑process or validate mixed barcode images, such as inventory scanning or document verification.
// Prompt: Validate that FoundBarCodes collection contains expected symbology types after processing a mixed barcode image.
// Tags: barcode, symbology, validation, generation, recognition, mixed image, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating several barcodes, combining them into one image,
/// reading the combined image, and verifying that all expected symbologies are detected.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, composition, recognition,
    /// and validation steps for a mixed barcode image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all intermediate files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeMix_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode types, their data, and output file names.
        var specs = new List<(BaseEncodeType EncodeType, string CodeText, string FileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Aztec, "AZTEC", "aztec.png")
        };

        // Generate individual barcode images and store their paths.
        var barcodePaths = new List<string>();
        foreach (var spec in specs)
        {
            string path = Path.Combine(tempFolder, spec.FileName);
            using (var generator = new BarcodeGenerator(spec.EncodeType, spec.CodeText))
            {
                generator.Save(path, BarCodeImageFormat.Png);
            }
            barcodePaths.Add(path);
        }

        // Prepare to combine the generated barcodes into a single horizontal layout.
        int spacing = 20;                     // Space between barcodes and margins.
        int totalWidth = spacing;             // Initial left margin.
        int maxHeight = 0;                    // Track the tallest barcode.
        var bitmaps = new List<Bitmap>();

        // Load each barcode image, calculate combined dimensions, and collect bitmaps.
        foreach (string path in barcodePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Missing barcode image: {path}");
                return;
            }

            var bmp = new Bitmap(path);
            bitmaps.Add(bmp);
            totalWidth += bmp.Width + spacing;
            if (bmp.Height > maxHeight) maxHeight = bmp.Height;
        }

        totalWidth += spacing; // Right margin.
        int totalHeight = maxHeight + spacing * 2; // Top and bottom margins.

        // Create the combined bitmap and draw each barcode onto it.
        string combinedPath = Path.Combine(tempFolder, "combined.png");
        using (var combinedBitmap = new Bitmap(totalWidth, totalHeight))
        {
            using (var graphics = Graphics.FromImage(combinedBitmap))
            {
                graphics.Clear(Color.White);
                int x = spacing;
                int y = spacing;
                foreach (var bmp in bitmaps)
                {
                    graphics.DrawImage(bmp, x, y, bmp.Width, bmp.Height);
                    x += bmp.Width + spacing;
                }
            }
            combinedBitmap.Save(combinedPath, ImageFormat.Png);
        }

        // Release resources held by individual barcode bitmaps.
        foreach (var bmp in bitmaps) bmp.Dispose();

        // Verify that the combined image was successfully created.
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Failed to create combined barcode image.");
            return;
        }

        // Define the set of symbologies we expect to find in the combined image.
        var expectedSymbologies = new HashSet<string> { "Code128", "QR", "DataMatrix", "Aztec" };
        var foundSymbologies = new HashSet<string>();

        // Read all barcodes from the combined image.
        using (var reader = new BarCodeReader(combinedPath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                if (!string.IsNullOrEmpty(result.CodeTypeName))
                {
                    foundSymbologies.Add(result.CodeTypeName);
                    Console.WriteLine($"Detected: {result.CodeTypeName} - Text: {result.CodeText}");
                }
            }
        }

        // Validate that each expected symbology was detected.
        bool allFound = true;
        foreach (var expected in expectedSymbologies)
        {
            if (!foundSymbologies.Contains(expected))
            {
                Console.WriteLine($"Missing expected symbology: {expected}");
                allFound = false;
            }
        }

        // Output the overall validation result.
        if (allFound)
        {
            Console.WriteLine("Validation succeeded: all expected symbologies were detected.");
        }
        else
        {
            Console.WriteLine("Validation failed: some expected symbologies were not detected.");
        }

        // Clean up temporary files and folders (optional).
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the validation outcome.
        }
    }
}