// Title: QR Code Reading Quality under Varying Lighting Conditions
// Description: Demonstrates how to generate a QR code, simulate different lighting by adjusting brightness, and evaluate the barcode reading quality using Aspose.BarCode's ReadingQuality metric.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing image preprocessing and quality assessment. It uses BarcodeGenerator, BarCodeReader, and QualitySettings classes to illustrate typical workflows for developers who need to test barcode readability under diverse imaging conditions, such as varying illumination in industrial or retail environments.
// Prompt: Compare recognition quality of QR codes captured under different lighting conditions by analyzing ReadingQuality values.
// Tags: qr code, readingquality, lighting, brightness, barcode recognition, aspose.barcode, image preprocessing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of QR codes, simulation of lighting variations, and evaluation of reading quality using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates QR codes with different brightness levels, reads them, and prints the ReadingQuality values.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrQuality_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define lighting factors: normal (1.0), dark (0.5), bright (1.5)
        var lightingFactors = new Dictionary<string, float>
        {
            { "Normal", 1.0f },
            { "Dark", 0.5f },
            { "Bright", 1.5f }
        };

        // Store generated file paths
        var imageFiles = new List<string>();

        // Generate QR code and apply lighting variations
        foreach (var kvp in lightingFactors)
        {
            string label = kvp.Key;
            float factor = kvp.Value;
            string filePath = Path.Combine(tempFolder, $"qr_{label}.png");

            // Generate QR code image
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode QR Test"))
            {
                // Save to memory stream first
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Load bitmap from stream
                    using (var bitmap = new Bitmap(ms))
                    {
                        // Adjust brightness to simulate lighting condition
                        AdjustBrightness(bitmap, factor);

                        // Save adjusted image to file
                        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            bitmap.Save(fileStream, ImageFormat.Png);
                        }
                    }
                }
            }

            imageFiles.Add(filePath);
        }

        // Read each image and output the ReadingQuality
        Console.WriteLine("QR Code Reading Quality under different lighting conditions:");
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Use QR decode type
            BaseDecodeType decodeType = DecodeType.QR;

            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                // Optional: set high performance quality settings
                reader.QualitySettings = QualitySettings.HighPerformance;

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"{Path.GetFileName(imagePath)}: No barcode detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        double quality = result.ReadingQuality; // 0-100
                        Console.WriteLine($"{Path.GetFileName(imagePath)}: Quality = {quality:F2}");
                    }
                }
            }
        }

        // Cleanup: delete temporary files and folder
        try
        {
            foreach (string file in imageFiles)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not crash the program
        }
    }

    // Adjusts the brightness of a bitmap by the given factor.
    // factor > 1 brightens, factor < 1 darkens.
    private static void AdjustBrightness(Bitmap bitmap, float factor)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Get current pixel
                var color = bitmap.GetPixel(x, y);

                // Apply brightness factor to each channel
                int r = Clamp((int)(color.R * factor));
                int g = Clamp((int)(color.G * factor));
                int b = Clamp((int)(color.B * factor));

                // Set new pixel
                bitmap.SetPixel(x, y, Aspose.Drawing.Color.FromArgb(r, g, b));
            }
        }
    }

    // Ensures the color component stays within 0-255
    private static int Clamp(int value)
    {
        if (value < 0) return 0;
        if (value > 255) return 255;
        return value;
    }
}