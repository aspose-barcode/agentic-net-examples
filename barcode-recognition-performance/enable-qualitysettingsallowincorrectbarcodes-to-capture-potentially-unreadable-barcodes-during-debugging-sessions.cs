// Title: Demonstrate AllowIncorrectBarcodes setting in Aspose.BarCode
// Description: Shows how to enable QualitySettings.AllowIncorrectBarcodes to read potentially corrupted QR codes during debugging.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It illustrates creating a QR code with BarcodeGenerator, intentionally corrupting the image, and then using BarCodeReader with different QualitySettings to observe decoding behavior. Developers working with barcode validation, error handling, or debugging unreadable barcodes will find this pattern useful.
// Prompt: Enable QualitySettings.AllowIncorrectBarcodes to capture potentially unreadable barcodes during debugging sessions.
// Tags: qr, barcode, allowincorrectbarcodes, debugging, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that demonstrates the effect of the
/// <c>QualitySettings.AllowIncorrectBarcodes</c> property when reading a
/// partially corrupted QR code image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, corrupts it,
    /// and reads it twice – once with <c>AllowIncorrectBarcodes</c> disabled
    /// and once enabled – printing the decoding results to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder for the demo files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the original and corrupted images
        string originalPath = Path.Combine(tempFolder, "original.png");
        string corruptedPath = Path.Combine(tempFolder, "corrupted.png");

        // --------------------------------------------------------------------
        // Generate a QR code image and save it as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Corrupt the image by drawing a black rectangle over part of it
        // --------------------------------------------------------------------
        using (var bitmap = new Bitmap(originalPath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var brush = new SolidBrush(Color.Black))
                {
                    // Draw a small black rectangle at the top-left corner
                    graphics.FillRectangle(brush, new Rectangle(0, 0, 20, 20));
                }
            }
            bitmap.Save(corruptedPath, ImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Attempt to read the corrupted QR code without allowing incorrect barcodes
        // --------------------------------------------------------------------
        Console.WriteLine("AllowIncorrectBarcodes: false");
        using (var reader = new BarCodeReader(corruptedPath, DecodeType.QR))
        {
            // Disallow reading of potentially incorrect barcodes
            reader.QualitySettings.AllowIncorrectBarcodes = false;
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Attempt to read the same corrupted QR code while allowing incorrect barcodes
        // --------------------------------------------------------------------
        Console.WriteLine("AllowIncorrectBarcodes: true");
        using (var reader = new BarCodeReader(corruptedPath, DecodeType.QR))
        {
            // Permit reading of barcodes that may be partially corrupted
            reader.QualitySettings.AllowIncorrectBarcodes = true;
            var results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(originalPath)) File.Delete(originalPath);
            if (File.Exists(corruptedPath)) File.Delete(corruptedPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}