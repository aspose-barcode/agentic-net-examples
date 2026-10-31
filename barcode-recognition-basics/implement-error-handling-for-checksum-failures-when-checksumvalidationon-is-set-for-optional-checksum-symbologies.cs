// Title: Demonstrate checksum validation handling for Code39 barcodes
// Description: This example generates a Code39 barcode with a checksum, then tampers the image to cause a checksum mismatch, showing how to detect failures using ChecksumValidation.On.
// Category-Description: Shows Aspose.BarCode checksum validation for optional checksum symbologies. It uses BarcodeGenerator, BarCodeReader, and related settings to illustrate generating a barcode, modifying it, and handling validation results. Developers working with 1D barcodes often need to verify data integrity and handle checksum errors gracefully.
// Prompt: Implement error handling for checksum failures when ChecksumValidation.On is set for optional checksum symbologies.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, error handling, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program demonstrating checksum validation handling for Code39 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, tampers it, and reads both images with different checksum validation settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and tampered barcode images
        string originalPath = Path.Combine(tempFolder, "code39.png");
        string tamperedPath = Path.Combine(tempFolder, "code39_tampered.png");

        // Generate Code39 barcode with checksum enabled
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Tamper the image to cause a checksum mismatch
        if (File.Exists(originalPath))
        {
            using (Bitmap bmp = (Bitmap)Image.FromFile(originalPath))
            {
                // Change a single pixel to corrupt the barcode
                bmp.SetPixel(0, 0, Color.Red);
                bmp.Save(tamperedPath, ImageFormat.Png);
            }
        }
        else
        {
            Console.WriteLine("Generated barcode image not found.");
            return;
        }

        // Read original barcode with default checksum validation (should succeed)
        if (File.Exists(originalPath))
        {
            using (BarCodeReader reader = new BarCodeReader(originalPath, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("[Default] CodeType: " + result.CodeTypeName);
                    Console.WriteLine("[Default] CodeText: " + result.CodeText);
                    Console.WriteLine("[Default] 1D CheckSum: " + result.Extended.OneD.CheckSum);
                }
            }
        }

        // Read tampered barcode with checksum validation ON (should fail)
        if (File.Exists(tamperedPath))
        {
            using (BarCodeReader reader = new BarCodeReader(tamperedPath, DecodeType.Code39))
            {
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                BarCodeResult[] results = reader.ReadBarCodes();

                // Handle possible checksum failure
                if (results.Length == 0)
                {
                    Console.WriteLine("Checksum validation failed: no barcode detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine("[On] CodeType: " + result.CodeTypeName);
                        Console.WriteLine("[On] CodeText: " + result.CodeText);
                        Console.WriteLine("[On] 1D CheckSum: " + result.Extended.OneD.CheckSum);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Tampered barcode image not found.");
        }
    }
}