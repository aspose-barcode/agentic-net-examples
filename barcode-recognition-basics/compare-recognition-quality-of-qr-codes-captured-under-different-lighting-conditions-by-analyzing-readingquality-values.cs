// Title: QR Code Reading Quality Comparison under Varying Lighting
// Description: Demonstrates how to generate a QR code, create a darkened version to simulate low lighting, and compare the ReadingQuality values returned by Aspose.BarCode's recognition engine.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on quality assessment of decoded symbols. It uses BarcodeGenerator to create barcodes, BarCodeReader with QualitySettings, and examines the ReadingQuality property of BarCodeResult. Developers often need to evaluate how environmental factors such as lighting affect scan reliability, and this snippet shows a quick way to benchmark QR code readability across different image conditions.
// Prompt: Compare recognition quality of QR codes captured under different lighting conditions by analyzing ReadingQuality values.
// Tags: qr code, readingquality, barcode recognition, quality settings, aspose.barcode, image processing, lighting conditions

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a QR code, creation of a darkened variant,
/// and comparison of the ReadingQuality values obtained from Aspose.BarCode
/// when recognizing each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates sample images, runs recognition,
    /// prints results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrQualityDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample QR code text
        string qrText = "Aspose.BarCode Demo";

        // Paths for original and darkened images
        string originalPath = Path.Combine(tempFolder, "qr_original.png");
        string darkPath = Path.Combine(tempFolder, "qr_dark.png");

        // Generate original QR code image
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Create a darkened version of the QR code to simulate low lighting
        using (var originalBmp = new Bitmap(originalPath))
        {
            int width = originalBmp.Width;
            int height = originalBmp.Height;
            using (var darkBmp = new Bitmap(width, height, originalBmp.PixelFormat))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color srcColor = originalBmp.GetPixel(x, y);
                        // Simple darkening: halve the RGB components, keep alpha unchanged
                        int r = srcColor.R / 2;
                        int g = srcColor.G / 2;
                        int b = srcColor.B / 2;
                        Color darkColor = Color.FromArgb(srcColor.A, r, g, b);
                        darkBmp.SetPixel(x, y, darkColor);
                    }
                }
                darkBmp.Save(darkPath, Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }

        // List of images to evaluate
        List<string> images = new List<string> { originalPath, darkPath };

        // Decode type for QR codes
        BaseDecodeType decodeType = DecodeType.QR;

        Console.WriteLine("QR Code Reading Quality Comparison:");
        foreach (string imgPath in images)
        {
            if (!File.Exists(imgPath))
            {
                Console.WriteLine($"File not found: {imgPath}");
                continue;
            }

            // Initialize reader for the current image with QR decode type
            using (var reader = new BarCodeReader(imgPath, decodeType))
            {
                // Use high quality preset for better assessment
                reader.QualitySettings = QualitySettings.HighQuality;

                bool anyFound = false;
                // Iterate through all detected barcodes (should be one per image)
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    anyFound = true;
                    Console.WriteLine($"Image: {Path.GetFileName(imgPath)}");
                    Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }

                if (!anyFound)
                {
                    Console.WriteLine($"Image: {Path.GetFileName(imgPath)} - No barcode detected.");
                }
            }
        }

        // Cleanup temporary files
        try
        {
            foreach (string file in Directory.GetFiles(tempFolder))
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}