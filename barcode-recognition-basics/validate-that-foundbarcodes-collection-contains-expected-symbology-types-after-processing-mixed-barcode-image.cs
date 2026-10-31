// Title: Validate barcode symbology detection in a combined image
// Description: Generates Code128, QR, and DataMatrix barcodes, merges them into a single bitmap, and checks that the Aspose.BarCode reader correctly identifies each symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create different barcode types, combine them with Aspose.Drawing, and then employ BarCodeReader with DecodeType.AllSupportedTypes to detect multiple symbologies in one image. Developers often need to process documents or photos containing several barcodes and verify that each expected type is recognized.
// Prompt: Validate that FoundBarCodes collection contains expected symbology types after processing a mixed barcode image.
// Tags: barcode, symbology, generation, recognition, mixed-image, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating multiple barcode types, combining them into a single image,
/// and validating that the Aspose.BarCode reader detects all expected symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, merges them, reads back the combined image,
    /// and verifies that each expected barcode type is found.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for generated images
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeMixed_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for individual barcode images and the combined image
        string code128Path = Path.Combine(workFolder, "code128.png");
        string qrPath = Path.Combine(workFolder, "qr.png");
        string dataMatrixPath = Path.Combine(workFolder, "datamatrix.png");
        string combinedPath = Path.Combine(workFolder, "combined.png");

        // -------------------- Generate Code128 barcode --------------------
        using (var genCode128 = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            genCode128.Parameters.Barcode.XDimension.Pixels = 2f;
            using (Bitmap bmp = genCode128.GenerateBarCodeImage())
            {
                bmp.Save(code128Path, ImageFormat.Png);
            }
        }

        // -------------------- Generate QR barcode --------------------
        using (var genQR = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            genQR.Parameters.Barcode.XDimension.Pixels = 2f;
            using (Bitmap bmp = genQR.GenerateBarCodeImage())
            {
                bmp.Save(qrPath, ImageFormat.Png);
            }
        }

        // -------------------- Generate DataMatrix barcode --------------------
        using (var genDM = new BarcodeGenerator(EncodeTypes.DataMatrix, "DM123"))
        {
            genDM.Parameters.Barcode.XDimension.Pixels = 2f;
            using (Bitmap bmp = genDM.GenerateBarCodeImage())
            {
                bmp.Save(dataMatrixPath, ImageFormat.Png);
            }
        }

        // -------------------- Combine the three barcodes into one image --------------------
        using (Bitmap bmp1 = new Bitmap(code128Path))
        using (Bitmap bmp2 = new Bitmap(qrPath))
        using (Bitmap bmp3 = new Bitmap(dataMatrixPath))
        {
            int combinedWidth = bmp1.Width + bmp2.Width + bmp3.Width;
            int maxHeight = Math.Max(bmp1.Height, Math.Max(bmp2.Height, bmp3.Height));

            using (Bitmap combined = new Bitmap(combinedWidth, maxHeight))
            {
                using (Graphics g = Graphics.FromImage(combined))
                {
                    // Fill background with white
                    g.Clear(Aspose.Drawing.Color.White);
                    int offsetX = 0;

                    // Draw each barcode side by side
                    g.DrawImage(bmp1, offsetX, 0);
                    offsetX += bmp1.Width;
                    g.DrawImage(bmp2, offsetX, 0);
                    offsetX += bmp2.Width;
                    g.DrawImage(bmp3, offsetX, 0);
                }

                // Save the merged image
                combined.Save(combinedPath, ImageFormat.Png);
            }
        }

        // Verify that the combined image was successfully created
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Combined image was not created.");
            return;
        }

        // Define the set of expected symbology names (case‑insensitive)
        var expected = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Code128",
            "QR",
            "DataMatrix"
        };

        // -------------------- Read and validate barcodes from the combined image --------------------
        using (var reader = new BarCodeReader(combinedPath, DecodeType.AllSupportedTypes))
        {
            // Perform detection of all supported barcode types
            reader.ReadBarCodes();
            BarCodeResult[] found = reader.FoundBarCodes;

            Console.WriteLine($"Found barcodes count: {reader.FoundCount}");
            foreach (var result in found)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                // Remove the found symbology from the expected set
                expected.Remove(result.CodeTypeName);
            }

            // Report validation outcome
            if (expected.Count == 0)
            {
                Console.WriteLine("Validation succeeded: all expected symbologies were found.");
            }
            else
            {
                Console.WriteLine("Validation failed: missing symbologies:");
                foreach (var missing in expected)
                {
                    Console.WriteLine(missing);
                }
            }
        }

        // -------------------- Cleanup temporary files (optional) --------------------
        try
        {
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}