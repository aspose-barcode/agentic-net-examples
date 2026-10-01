// Title: Enable checksum validation for Code128 barcode reading and detect failures
// Description: Demonstrates generating a Code128 barcode, intentionally corrupting it, and reading it with checksum validation enabled to report verification failures.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the ChecksumValidation setting to ensure data integrity. Developers commonly need to validate barcodes during scanning to detect tampering or read errors, especially for Code128 symbology.
// Prompt: Enable checksum validation for Code128 barcodes and report any verification failures during processing.
// Tags: code128, checksum, validation, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates enabling checksum validation for Code128 barcodes and reporting verification failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, corrupts it, and reads both images with checksum validation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the original and corrupted barcode images
        string originalPath = Path.Combine(tempFolder, "code128_original.png");
        string corruptedPath = Path.Combine(tempFolder, "code128_corrupted.png");

        // Sample Code128 text (checksum is automatically calculated)
        string codeText = "1234567890";

        // -------------------------------------------------
        // Generate a Code128 barcode image
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save as PNG
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Corrupt the image by drawing a black line across it
        // -------------------------------------------------
        using (var bitmap = new Bitmap(originalPath))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var pen = new Pen(Color.Black, 5f))
                {
                    graphics.DrawLine(pen, 0, 0, bitmap.Width, bitmap.Height);
                }
            }
            bitmap.Save(corruptedPath, ImageFormat.Png);
        }

        // -------------------------------------------------
        // Function to read a barcode with checksum validation enabled
        // -------------------------------------------------
        void ReadAndReport(string imagePath, string label)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"{label}: File not found -> {imagePath}");
                return;
            }

            // Use DecodeType.Code128 (BaseDecodeType) for reading
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Enable checksum validation (default is On, but set explicitly)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"{label}: Checksum validation failed or barcode not detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{label}: Successfully read. CodeText = {result.CodeText}");
                    }
                }
            }
        }

        // -------------------------------------------------
        // Read the original (valid) barcode
        // -------------------------------------------------
        ReadAndReport(originalPath, "Original");

        // -------------------------------------------------
        // Read the corrupted barcode (expected to fail checksum)
        // -------------------------------------------------
        ReadAndReport(corruptedPath, "Corrupted");

        // Clean up temporary files (optional)
        try
        {
            File.Delete(originalPath);
            File.Delete(corruptedPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}