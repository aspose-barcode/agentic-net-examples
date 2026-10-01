// Title: Rotated Barcode Generation and Recognition Example
// Description: Demonstrates generating Code128 barcodes at multiple rotation angles, saving them as PNG images, and then recognizing them to verify correct decoding regardless of orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes with specific rotation angles and BarCodeReader for detecting and decoding barcodes in images. Typical scenarios include processing scanned documents where barcodes may appear rotated, requiring robust detection across all orientations. Developers often need to combine these APIs to ensure reliable barcode reading in varied image conditions.
// Prompt: Test recognition of rotated barcodes by loading images with varied orientation and verifying correct decoding.
// Tags: barcode symbology, generation, recognition, rotation, code128, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates barcode images at different rotation angles and verifies that they can be correctly recognized.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates rotated barcode images, reads them back, and outputs verification results.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode
        const string codeText = "1234567890";

        // Rotation angles (in degrees) to apply to each generated barcode
        float[] angles = new float[] { 0f, 90f, 180f, 270f };

        // Create a unique temporary folder for storing generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "RotatedBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Collect file paths of generated barcode images for later processing
        List<string> barcodeFiles = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images with the specified rotation angles
        // -----------------------------------------------------------------
        foreach (float angle in angles)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{angle}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Apply rotation to the barcode image
                generator.Parameters.RotationAngle = angle;

                // Save the rotated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // -----------------------------------------------------------------
        // Read each generated image and verify that the barcode is detected
        // -----------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();

                    // If no barcodes were detected, report and continue
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                        continue;
                    }

                    // Output detection details for each found barcode
                    foreach (var result in results)
                    {
                        bool isMatch = string.Equals(result.CodeText, codeText, StringComparison.Ordinal);
                        double detectedAngle = result.Region.Angle; // Orientation reported by the reader
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Detected Angle: {detectedAngle}° | Text Match: {isMatch}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Failed to load image '{Path.GetFileName(file)}': {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // Cleanup: delete the temporary folder and its contents
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – the folder resides in a temporary location.
        }
    }
}