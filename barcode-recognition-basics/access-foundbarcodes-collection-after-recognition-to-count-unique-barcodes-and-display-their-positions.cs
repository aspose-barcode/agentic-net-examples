// Title: Combine Multiple Barcodes into One Image and Recognize Unique Barcodes
// Description: Generates several barcodes, merges them onto a single canvas, then reads the combined image to count unique barcodes and display their positions.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, combine them with Aspose.Drawing, and employ BarCodeReader to detect and enumerate found barcodes via the FoundBarCodes collection. Typical use cases include batch processing of barcode images, creating composite scans for inventory, and extracting barcode metadata such as location and type. Developers often need to count distinct codes and retrieve their geometric regions for downstream processing.
// Prompt: Access FoundBarCodes collection after recognition to count unique barcodes and display their positions.
// Tags: barcode generation, barcode recognition, combined image, unique barcodes, positions, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate multiple barcodes, combine them into a single image,
/// and then recognize the barcodes to count unique entries and output their positions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, creates a composite image,
    /// reads the barcodes back, and prints detection details to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Prepare barcode data (type and text)
        var barcodeInfos = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DM12345")
        };

        // Generate individual barcode bitmaps and store them in a list
        var bitmaps = new List<Bitmap>();
        foreach (var info in barcodeInfos)
        {
            using (var generator = new BarcodeGenerator(info.type, info.text))
            {
                Bitmap bmp = generator.GenerateBarCodeImage();
                bitmaps.Add(bmp);
            }
        }

        // Calculate canvas size based on individual barcode dimensions and spacing
        int spacing = 20;
        int totalWidth = spacing;
        int maxHeight = 0;
        foreach (var bmp in bitmaps)
        {
            totalWidth += bmp.Width + spacing;
            if (bmp.Height > maxHeight) maxHeight = bmp.Height;
        }
        totalWidth += spacing;
        maxHeight += spacing * 2;

        // Create a blank canvas and draw each barcode bitmap onto it
        using (Bitmap canvas = new Bitmap(totalWidth, maxHeight, PixelFormat.Format32bppArgb))
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
                int xOffset = spacing;
                foreach (var bmp in bitmaps)
                {
                    g.DrawImage(bmp, xOffset, spacing, bmp.Width, bmp.Height);
                    xOffset += bmp.Width + spacing;
                }
            }

            // Save the combined image to the temporary folder
            string combinedPath = Path.Combine(tempFolder, "combined.png");
            canvas.Save(combinedPath, ImageFormat.Png);

            // Initialize a reader that supports all barcode types
            BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;
            using (BarCodeReader reader = new BarCodeReader(combinedPath, decodeAll))
            {
                // Perform recognition on the combined image
                reader.ReadBarCodes();
                Console.WriteLine($"Total barcodes detected: {reader.FoundCount}");

                // Use a HashSet to track unique barcode texts
                var uniqueTexts = new HashSet<string>();
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    uniqueTexts.Add(result.CodeText);
                    var rect = result.Region.Rectangle;
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    Console.WriteLine($"  Position - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                }

                // Output the count of distinct barcode texts
                Console.WriteLine($"Unique barcode count: {uniqueTexts.Count}");
            }

            // Dispose of generated bitmap resources
            foreach (var bmp in bitmaps)
            {
                bmp.Dispose();
            }
        }

        // Optionally delete temporary folder (commented out to keep files for inspection)
        // Directory.Delete(tempFolder, true);
    }
}