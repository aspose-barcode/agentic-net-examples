// Title: Rotated Barcode Generation and Recognition Example
// Description: Demonstrates generating Code128 barcodes at multiple rotation angles, saving them as PNG images, and then recognizing each image to verify correct decoding.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes with specific rotation angles and BarCodeReader for detecting and decoding them. Developers often need to handle barcodes that appear rotated in images, and this pattern illustrates typical API usage for such scenarios, making it searchable for solutions involving rotated barcode processing.
// Prompt: Test recognition of rotated barcodes by loading images with varied orientation and verifying correct decoding.
// Tags: barcode symbology, generation, recognition, rotation, code128, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates barcode images at various rotation angles and verifies their recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates rotated barcode images, reads them back, and outputs verification results.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "RotatedBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string barcodeText = "Test123";
        float[] angles = new float[] { 0f, 90f, 180f, 270f };
        List<string> imageFiles = new List<string>();

        // ------------------------------------------------------------
        // Generate barcode images rotated at the specified angles
        // ------------------------------------------------------------
        foreach (float angle in angles)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{angle}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
            {
                // Set the rotation angle for the barcode image
                generator.Parameters.RotationAngle = angle;
                // Save the generated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // ------------------------------------------------------------
        // Read each generated image and verify the decoded barcode
        // ------------------------------------------------------------
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Explicitly set the image source (required by some API versions)
                    reader.SetBarCodeImage(file);

                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(file)}");
                        continue;
                    }

                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Orientation Angle: {result.Region.Angle}");
                        Console.WriteLine($"Match Expected: {result.CodeText == barcodeText}");
                        Console.WriteLine();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error reading {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and folder
        // ------------------------------------------------------------
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
            // Ignore any errors during cleanup
        }
    }
}