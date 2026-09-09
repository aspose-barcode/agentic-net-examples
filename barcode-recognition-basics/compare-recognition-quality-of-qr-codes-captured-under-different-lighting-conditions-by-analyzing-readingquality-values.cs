// Title: Compare QR code reading quality under varying lighting
// Description: Demonstrates generating a QR code, creating darker and brighter variants, and measuring the ReadingQuality of each image to assess how lighting affects recognition.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to use BarcodeGenerator to create barcodes, manipulate images with Aspose.Drawing, and employ BarCodeReader to decode QR codes and retrieve the ReadingQuality metric. Developers often need to evaluate barcode readability under different conditions, such as lighting or contrast, to optimize scanning performance in real‑world applications.
// Prompt: Compare recognition quality of QR codes captured under different lighting conditions by analyzing ReadingQuality values.
// Tags: qr, barcode, readingquality, lighting, recognition, aspose.barcode, generation, image-processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR code generation, lighting variation, and reading quality analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR images with normal, dark, and bright lighting, reads them,
    /// and reports the ReadingQuality values and average.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrQuality_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the three lighting conditions
        string basePath = Path.Combine(tempFolder, "qr_normal.png");
        string darkPath = Path.Combine(tempFolder, "qr_dark.png");
        string brightPath = Path.Combine(tempFolder, "qr_bright.png");

        // Generate a base QR code image with default lighting
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            generator.Save(basePath, BarCodeImageFormat.Png);
        }

        // Create a darker version by overlaying a semi‑transparent black rectangle
        using (var bitmap = new Bitmap(basePath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var brush = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
                {
                    graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
                }
                bitmap.Save(darkPath, ImageFormat.Png);
            }
        }

        // Create a brighter version by overlaying a semi‑transparent white rectangle
        using (var bitmap = new Bitmap(basePath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var brush = new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                {
                    graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
                }
                bitmap.Save(brightPath, ImageFormat.Png);
            }
        }

        // Collect all generated image file paths
        var imageFiles = new[] { basePath, darkPath, brightPath };

        double totalQuality = 0;
        int count = 0;

        // Iterate through each image and evaluate its QR code reading quality
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Initialize a reader for QR codes in the current image
            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                // Process each detected barcode result
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    double quality = result.ReadingQuality;
                    Console.WriteLine($"{Path.GetFileName(imagePath)} - CodeType: {result.CodeTypeName}, Quality: {quality}");
                    totalQuality += quality;
                    count++;
                }
            }
        }

        // Output the average reading quality across all samples, if any were processed
        if (count > 0)
        {
            double averageQuality = totalQuality / count;
            Console.WriteLine($"Average ReadingQuality across samples: {averageQuality:F2}");
        }

        // Cleanup temporary folder (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}