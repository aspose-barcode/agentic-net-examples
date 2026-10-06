// Title: Validate Detected Barcode Symbologies in a Combined Image
// Description: Generates several different barcode types, merges them into a single image, then reads the image to confirm each expected symbology is detected.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator to create barcodes, combining images with Aspose.Drawing, and employing BarCodeReader with DecodeType.AllSupportedTypes to recognize multiple symbologies. Developers often need to batch‑process mixed barcode images, verify content, or build validation pipelines; this snippet shows the key API classes (BarcodeGenerator, BarCodeReader, DecodeType) and typical workflow for such scenarios.
// Prompt: Validate that FoundBarCodes collection contains expected symbology types after processing a mixed barcode image.
// Tags: barcode, symbology, validation, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcode types, combining them into one image,
/// and validating that the recognized symbologies match the expected set.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates barcodes, merges them, reads back the combined image,
    /// and prints validation results to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary working directory for intermediate files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeMixed_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the barcodes to generate: type, data, and the expected name used for validation
        var barcodeInfos = new List<(BaseEncodeType Encode, string Text, string ExpectedName)>
        {
            (EncodeTypes.Code128, "CODE128TEST", "Code128"),
            (EncodeTypes.QR, "https://example.com", "QR"),
            (EncodeTypes.Pdf417, "PDF417DATA", "PDF417")
        };

        // Generate individual barcode images and store them in a list
        var bitmaps = new List<Bitmap>();
        foreach (var info in barcodeInfos)
        {
            using (var generator = new BarcodeGenerator(info.Encode, info.Text))
            {
                // Optional visual parameters for better readability
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 30f;

                Bitmap bmp = generator.GenerateBarCodeImage();
                bitmaps.Add(bmp);
            }
        }

        // Calculate the size of the combined image (max width, total height with spacing)
        int maxWidth = 0;
        int totalHeight = 0;
        int spacing = 10; // pixels between barcodes
        foreach (var bmp in bitmaps)
        {
            if (bmp.Width > maxWidth) maxWidth = bmp.Width;
            totalHeight += bmp.Height + spacing;
        }
        totalHeight -= spacing; // remove extra spacing after the last image

        // Create the combined bitmap and draw each barcode onto it sequentially
        string combinedPath = Path.Combine(tempDir, "combined.png");
        using (var combined = new Bitmap(maxWidth, totalHeight, PixelFormat.Format32bppArgb))
        {
            using (var graphics = Graphics.FromImage(combined))
            {
                graphics.Clear(Color.White);
                int yOffset = 0;
                foreach (var bmp in bitmaps)
                {
                    graphics.DrawImage(bmp, 0, yOffset, bmp.Width, bmp.Height);
                    yOffset += bmp.Height + spacing;
                    bmp.Dispose(); // release individual bitmap resources after drawing
                }
            }
            combined.Save(combinedPath, ImageFormat.Png);
        }

        // Verify that the combined image file was created successfully
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Failed to create combined barcode image.");
            return;
        }

        // Read and recognize all barcodes from the combined image
        var foundTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        using (var reader = new BarCodeReader(combinedPath, DecodeType.AllSupportedTypes))
        {
            reader.ReadBarCodes();
            foreach (var result in reader.FoundBarCodes)
            {
                foundTypes.Add(result.CodeTypeName);
                Console.WriteLine($"Detected: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // Validate that each expected symbology is present in the recognized set
        Console.WriteLine("\nValidation Results:");
        foreach (var info in barcodeInfos)
        {
            bool present = foundTypes.Contains(info.ExpectedName);
            Console.WriteLine($"{info.ExpectedName}: {(present ? "PASS" : "FAIL")}");
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(combinedPath))
                File.Delete(combinedPath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit
        }
    }
}