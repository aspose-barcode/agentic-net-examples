// Title: Mixed Barcode Generation and Validation Example
// Description: Generates several barcode types, combines them into a single image, then validates that the recognized barcodes match the expected symbology types.
// Prompt: Validate that FoundBarCodes collection contains expected symbology types after processing a mixed barcode image.
// Tags: barcode symbology, generation, recognition, validation, aspnet barcoderecognition, aspnet barcodelibrary

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcode types, combining them into one image, and validating detection of each symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, creates a composite image, reads it back, and validates expected symbology types.
    /// </summary>
    static void Main()
    {
        // Define a set of barcode samples with their encode types and corresponding text.
        var samples = new List<(BaseEncodeType EncodeType, string CodeText)>
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE"),
            (EncodeTypes.QR, "QR_SAMPLE"),
            (EncodeTypes.DataMatrix, "DM_SAMPLE"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE"),
            (EncodeTypes.EAN13, "1234567890128")
        };

        // Lists to hold generated bitmap images and the expected symbology type names.
        var bitmaps = new List<Bitmap>();
        var expectedTypes = new List<string>();

        // Generate each barcode image and record its expected type.
        foreach (var sample in samples)
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.EncodeType, sample.CodeText))
            {
                expectedTypes.Add(generator.BarcodeType.TypeName);
                Bitmap bmp = generator.GenerateBarCodeImage();
                bitmaps.Add(bmp);
            }
        }

        // Determine canvas size needed to place all barcodes vertically with spacing.
        int canvasWidth = 0;
        int canvasHeight = 0;
        int spacing = 10;

        foreach (var bmp in bitmaps)
        {
            if (bmp.Width > canvasWidth) canvasWidth = bmp.Width;
            canvasHeight += bmp.Height + spacing;
        }

        // Create a combined image and draw each barcode bitmap onto it.
        using (Bitmap combined = new Bitmap(canvasWidth, canvasHeight))
        {
            using (Graphics graphics = Graphics.FromImage(combined))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
                int yOffset = 0;
                foreach (var bmp in bitmaps)
                {
                    graphics.DrawImage(bmp, 0, yOffset, bmp.Width, bmp.Height);
                    yOffset += bmp.Height + spacing;
                    bmp.Dispose(); // Release individual bitmap resources.
                }
            }

            // Save the combined image to disk.
            string imagePath = "mixed_barcodes.png";
            combined.Save(imagePath, ImageFormat.Png);
        }

        // Verify that the combined image file was created successfully.
        string combinedImagePath = "mixed_barcodes.png";
        if (!File.Exists(combinedImagePath))
        {
            Console.WriteLine($"Error: Image file '{combinedImagePath}' not found.");
            return;
        }

        // Read all barcodes from the combined image using Aspose.BarCode.
        using (BarCodeReader reader = new BarCodeReader(combinedImagePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            // Collect the types of barcodes that were actually detected.
            var foundTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var result in results)
            {
                foundTypes.Add(result.CodeTypeName);
                Console.WriteLine($"Detected: {result.CodeTypeName} - Text: {result.CodeText}");
            }

            // Output validation results comparing expected vs. found symbology types.
            Console.WriteLine();
            Console.WriteLine("Validation Results:");
            foreach (var expected in expectedTypes)
            {
                if (foundTypes.Contains(expected))
                {
                    Console.WriteLine($"PASS: Expected symbology '{expected}' was found.");
                }
                else
                {
                    Console.WriteLine($"FAIL: Expected symbology '{expected}' was NOT found.");
                }
            }
        }
    }
}