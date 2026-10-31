// Title: QR Code Reading Quality Comparison Under Varying Lighting
// Description: Demonstrates how to generate a QR code, create a darkened version to simulate low lighting, and compare the ReadingQuality values returned by Aspose.BarCode's recognition engine.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarcodeGenerator, BarCodeReader, and related classes to evaluate scan quality. Developers often need to assess how environmental factors such as lighting affect barcode readability, and this snippet shows how to programmatically measure reading quality using the ReadingQuality property of BarCodeResult.
// Prompt: Compare recognition quality of QR codes captured under different lighting conditions by analyzing ReadingQuality values.
// Tags: qr code, readingquality, barcode recognition, lighting conditions, aspose.barcode, generation, decoding, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a QR code, creation of a low‑lighting variant,
/// and comparison of the ReadingQuality values obtained from Aspose.BarCode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR images, reads them, and outputs quality comparison.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrQualityDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // QR code content to encode
        string qrText = "https://example.com";

        // Define file paths for the normal and darkened images
        string normalPath = Path.Combine(tempFolder, "qr_normal.png");
        string darkPath = Path.Combine(tempFolder, "qr_dark.png");

        // Generate a normal QR code image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            generator.Save(normalPath, BarCodeImageFormat.Png);
        }

        // Create a darkened version of the QR code to simulate low lighting conditions
        using (var original = new Bitmap(normalPath))
        {
            using (var darkened = new Bitmap(original.Width, original.Height))
            {
                using (var graphics = Graphics.FromImage(darkened))
                {
                    // Draw the original QR code onto the new bitmap
                    graphics.DrawImage(original, 0, 0, original.Width, original.Height);
                    // Overlay a semi‑transparent black rectangle to darken the image
                    using (var brush = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
                    {
                        graphics.FillRectangle(brush, 0, 0, darkened.Width, darkened.Height);
                    }
                }
                // Save the darkened image
                darkened.Save(darkPath, ImageFormat.Png);
            }
        }

        // Local function that reads a QR code image and returns its ReadingQuality value
        double ReadQuality(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return -1;
            }

            BaseDecodeType decodeType = DecodeType.QR;
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                    return result.ReadingQuality;
                }
            }

            Console.WriteLine($"No barcode detected in {imagePath}");
            return -1;
        }

        // Read and store the quality metrics for both images
        double normalQuality = ReadQuality(normalPath);
        double darkQuality = ReadQuality(darkPath);

        // Compare the ReadingQuality values and output the results
        if (normalQuality >= 0 && darkQuality >= 0)
        {
            Console.WriteLine();
            Console.WriteLine("Comparison of ReadingQuality values:");
            Console.WriteLine($"  Normal lighting: {normalQuality}");
            Console.WriteLine($"  Low lighting (darkened): {darkQuality}");
            double diff = normalQuality - darkQuality;
            Console.WriteLine($"  Difference (Normal - Dark): {diff}");
        }

        // Cleanup temporary files and folder (optional)
        try
        {
            File.Delete(normalPath);
            File.Delete(darkPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}