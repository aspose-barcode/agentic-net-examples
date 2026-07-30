// Title: Verify checksum visibility toggles rendering in Code128 barcodes
// Description: Demonstrates generating Code128 barcodes with checksum shown and hidden, then validates that the rendering changes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating how to enable checksum calculation, control its visual display, and verify results using the BarcodeGenerator and BarCodeReader classes. Developers often need to toggle checksum visibility for compliance or aesthetic reasons, and this snippet shows typical API usage for such scenarios.
// Prompt: Write a unit test confirming the checksum visibility property correctly toggles rendering of the checksum digit.
// Tags: code128, checksum, visibility, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Code128 barcodes with and without visible checksum
/// and validates that the checksum visibility property affects the rendered image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images, compares their file sizes,
    /// and reads back the checksum values using <see cref="BarCodeReader"/>.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for output images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeChecksumTest");
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Define file paths for the images with checksum shown and hidden
        string pathShow = Path.Combine(outputDir, "code128_show.png");
        string pathHide = Path.Combine(outputDir, "code128_hide.png");

        // Barcode data to encode
        const string codeText = "12345";

        // Generate barcode with checksum always shown
        using (var generatorShow = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable checksum calculation
            generatorShow.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Force visual display of the checksum digit
            generatorShow.Parameters.Barcode.ChecksumAlwaysShow = true;
            // Save the generated image
            generatorShow.Save(pathShow);
        }

        // Generate barcode with checksum hidden
        using (var generatorHide = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Enable checksum calculation
            generatorHide.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Do not display the checksum digit
            generatorHide.Parameters.Barcode.ChecksumAlwaysShow = false;
            // Save the generated image
            generatorHide.Save(pathHide);
        }

        // Verify that both image files were successfully created
        if (!File.Exists(pathShow) || !File.Exists(pathHide))
        {
            Console.WriteLine("FAILED: One or both barcode images were not created.");
            return;
        }

        // Simple visual‑rendering check: file sizes should differ because the checksum text length changes
        long sizeShow = new FileInfo(pathShow).Length;
        long sizeHide = new FileInfo(pathHide).Length;

        if (sizeShow == sizeHide)
        {
            Console.WriteLine("FAILED: Image sizes are identical; checksum visibility may not have affected rendering.");
        }
        else
        {
            Console.WriteLine("PASSED: Checksum visibility toggles rendering (file sizes differ).");
            Console.WriteLine($"Size with checksum shown: {sizeShow} bytes");
            Console.WriteLine($"Size with checksum hidden: {sizeHide} bytes");
        }

        // Optional: read the checksum value using BarCodeReader to ensure it is present in both images
        using (var readerShow = new BarCodeReader(pathShow, DecodeType.Code128))
        {
            foreach (BarCodeResult result in readerShow.ReadBarCodes())
            {
                Console.WriteLine($"Show - Detected checksum: {result.Extended.OneD.CheckSum}");
            }
        }

        using (var readerHide = new BarCodeReader(pathHide, DecodeType.Code128))
        {
            foreach (BarCodeResult result in readerHide.ReadBarCodes())
            {
                Console.WriteLine($"Hide - Detected checksum: {result.Extended.OneD.CheckSum}");
            }
        }
    }
}