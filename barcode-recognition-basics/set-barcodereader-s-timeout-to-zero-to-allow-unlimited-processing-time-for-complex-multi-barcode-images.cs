// Title: Unlimited Timeout for BarCodeReader with Multi-Barcode Image
// Description: Demonstrates how to set BarCodeReader.Timeout to zero, allowing unlimited processing time when decoding a complex image that contains multiple barcodes.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It showcases the use of BarcodeGenerator to create individual barcodes, combines them into a single image with Aspose.Drawing, and then employs BarCodeReader (DecodeType.AllSupportedTypes) to recognize all barcodes. Developers often need to adjust the reader's timeout for large or dense images to prevent premature termination of the recognition process.
// Prompt: Set BarCodeReader's TimeOut to zero to allow unlimited processing time for complex multi‑barcode images.
// Tags: barcode, generation, reading, timeout, multibarcode, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates two different barcodes, merges them into a single image,
/// and reads them back using an unlimited timeout setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files, generates barcodes, combines them,
    /// reads them with unlimited timeout, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MultiBarcode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the individual and combined barcode images
        string code128Path = Path.Combine(tempFolder, "code128.png");
        string qrPath = Path.Combine(tempFolder, "qr.png");
        string multiPath = Path.Combine(tempFolder, "multi.png");

        // Generate a Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // Generate a QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // Combine the two barcode images side by side into a single image
        if (File.Exists(code128Path) && File.Exists(qrPath))
        {
            using (Bitmap bmp1 = new Bitmap(code128Path))
            using (Bitmap bmp2 = new Bitmap(qrPath))
            {
                int combinedWidth = bmp1.Width + bmp2.Width;
                int combinedHeight = Math.Max(bmp1.Height, bmp2.Height);
                using (Bitmap combined = new Bitmap(combinedWidth, combinedHeight))
                using (Graphics g = Graphics.FromImage(combined))
                {
                    g.DrawImage(bmp1, 0, 0, bmp1.Width, bmp1.Height);
                    g.DrawImage(bmp2, bmp1.Width, 0, bmp2.Width, bmp2.Height);
                    combined.Save(multiPath, ImageFormat.Png);
                }
            }
        }

        // Verify that the combined image was created successfully
        if (!File.Exists(multiPath))
        {
            Console.WriteLine("Failed to create combined barcode image.");
            return;
        }

        // Read all barcodes from the combined image with an unlimited timeout
        using (BarCodeReader reader = new BarCodeReader(multiPath, DecodeType.AllSupportedTypes))
        {
            // Setting Timeout to zero disables the time limit for recognition
            reader.Timeout = 0;

            try
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Barcodes found: {results.Length}");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException ex)
            {
                Console.WriteLine($"Recognition aborted: {ex.Message}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(code128Path);
            File.Delete(qrPath);
            File.Delete(multiPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program outcome
        }
    }
}