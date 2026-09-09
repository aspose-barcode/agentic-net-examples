// Title: Rotated QR Code Recognition Demo
// Description: Demonstrates generating a QR barcode, creating rotated versions, and verifying that Aspose.BarCode can correctly decode them regardless of orientation.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to work with image rotation and orientation detection. It uses BarcodeGenerator for encoding, BarCodeReader for decoding, and image manipulation classes to rotate images. Developers often need to ensure reliable barcode scanning from images captured at arbitrary angles, such as in mobile scanning or document processing scenarios.
// Prompt: Test recognition of rotated barcodes by loading images with varied orientation and verifying correct decoding.
// Tags: qr, barcode, rotation, recognition, aspose.barcode, image-processing, decode, orientation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR barcode, creates rotated copies,
/// and validates that the Aspose.BarCode reader correctly decodes each orientation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store generated images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "RotatedBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the original barcode image and its rotated variants.
        string originalPath = Path.Combine(tempFolder, "barcode_original.png");
        var rotatedPaths = new List<string>();

        // --------------------------------------------------------------------
        // Generate a QR barcode image and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            generator.Save(originalPath, BarCodeImageFormat.Png);
        }

        // Define the rotation angles to apply (in degrees).
        int[] angles = { 0, 90, 180, 270 };

        // --------------------------------------------------------------------
        // Create rotated images for each specified angle.
        // --------------------------------------------------------------------
        foreach (int angle in angles)
        {
            string rotatedPath = Path.Combine(tempFolder, $"barcode_{angle}.png");
            using (Image img = Image.FromFile(originalPath))
            {
                using (Bitmap bmp = new Bitmap(img))
                {
                    // Map the angle to the corresponding RotateFlipType.
                    RotateFlipType rotateFlip = angle switch
                    {
                        0 => RotateFlipType.RotateNoneFlipNone,
                        90 => RotateFlipType.Rotate90FlipNone,
                        180 => RotateFlipType.Rotate180FlipNone,
                        270 => RotateFlipType.Rotate270FlipNone,
                        _ => RotateFlipType.RotateNoneFlipNone
                    };
                    // Apply rotation.
                    bmp.RotateFlip(rotateFlip);
                    // Save the rotated image.
                    bmp.Save(rotatedPath, ImageFormat.Png);
                }
            }
            rotatedPaths.Add(rotatedPath);
        }

        // --------------------------------------------------------------------
        // Read and verify each rotated barcode image.
        // --------------------------------------------------------------------
        foreach (string filePath in rotatedPaths)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.QR))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  Decoded Text : {results[0].CodeText}");
                        Console.WriteLine($"  Detected Type: {results[0].CodeTypeName}");
                        Console.WriteLine($"  Orientation  : {results[0].Region.Angle} degrees");
                    }
                    else
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} - No barcode detected.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error reading file {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and folder.
        // --------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}