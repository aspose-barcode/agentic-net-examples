// Title: Barcode checksum validation for multiple symbologies in a single read
// Description: This example generates Code11 (mandatory checksum) and Code39 (optional checksum) barcodes, merges them into one image, and reads both barcodes in a single operation with checksum validation turned on.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, focusing on BarcodeGenerator, BarCodeReader, and the ChecksumValidation setting. Useful for developers who need to validate mandatory and optional checksum symbologies during batch scanning or combined image processing. Typical scenarios include inventory systems, document automation, and quality control where multiple barcode types are read together.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for both obligatory and optional checksum symbologies in a single read operation.
// Tags: barcode symbology, checksum validation, read operation, generation, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating barcodes with different checksum requirements,
/// combining them into a single image, and reading them with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates, combines, reads, and cleans up barcode images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the individual and combined barcode images
        string code11Path = Path.Combine(tempFolder, "code11.png");
        string code39Path = Path.Combine(tempFolder, "code39.png");
        string combinedPath = Path.Combine(tempFolder, "combined.png");

        // -------------------------------------------------
        // Generate Code11 barcode (checksum is obligatory)
        // -------------------------------------------------
        using (var gen11 = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            gen11.Parameters.Barcode.XDimension.Pixels = 2f; // Set module size
            gen11.Save(code11Path, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Generate Code39 barcode (checksum is optional) and enable it
        // -------------------------------------------------
        using (var gen39 = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            gen39.Parameters.Barcode.XDimension.Pixels = 2f; // Set module size
            gen39.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // Enable optional checksum
            gen39.Save(code39Path, BarCodeImageFormat.Png);
        }

        // Verify that both barcode images were created successfully
        if (!File.Exists(code11Path) || !File.Exists(code39Path))
        {
            Console.WriteLine("Failed to generate barcode images.");
            return;
        }

        // -------------------------------------------------
        // Combine the two barcode images side by side
        // -------------------------------------------------
        using (var bmp11 = new Bitmap(code11Path))
        using (var bmp39 = new Bitmap(code39Path))
        {
            int spacing = 20; // Space between images
            int combinedWidth = bmp11.Width + bmp39.Width + spacing;
            int combinedHeight = Math.Max(bmp11.Height, bmp39.Height);

            using (var combinedBmp = new Bitmap(combinedWidth, combinedHeight))
            using (var graphics = Graphics.FromImage(combinedBmp))
            {
                graphics.Clear(Aspose.Drawing.Color.White);
                graphics.DrawImage(bmp11, 0, 0, bmp11.Width, bmp11.Height);
                graphics.DrawImage(bmp39, bmp11.Width + spacing, 0, bmp39.Width, bmp39.Height);
                combinedBmp.Save(combinedPath, ImageFormat.Png);
            }
        }

        // -------------------------------------------------
        // Read both barcodes from the combined image with checksum validation turned on
        // -------------------------------------------------
        if (!File.Exists(combinedPath))
        {
            Console.WriteLine("Combined image not found.");
            return;
        }

        using (var reader = new BarCodeReader(combinedPath, DecodeType.AllSupportedTypes))
        {
            // Enable checksum validation for all supported symbologies
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine();
            }
        }

        // -------------------------------------------------
        // Cleanup temporary files and folder (optional)
        // -------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}