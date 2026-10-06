// Title: Detect rotated barcode orientation
// Description: Demonstrates generating barcodes at various rotation angles, reading them back, and verifying the detected orientation matches the expected rotation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create images with specific rotation angles and BarCodeReader to detect barcodes and retrieve their region angle. Developers often need to handle rotated barcodes in scanned documents, verify orientation for downstream processing, or correct image alignment. The example highlights key classes such as BarcodeGenerator, BarCodeReader, and BarCodeResult.
// Prompt: Detect barcodes in rotated images and verify orientation angle matches expected rotation.
// Tags: barcode, rotation, orientation, code128, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates barcode images at different rotation angles, reads them back,
/// and verifies that the detected orientation matches the expected rotation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, reads them,
    /// prints detection results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "RotatedBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode content and symbology
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Rotation angles (in degrees) to test
        float[] rotations = new float[] { 0f, 90f, 180f, 270f };

        // List to hold file paths of generated images
        List<string> imageFiles = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images with the specified rotations
        // -----------------------------------------------------------------
        foreach (float angle in rotations)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{angle}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply rotation to the generated barcode
                generator.Parameters.RotationAngle = angle;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // -----------------------------------------------------------------
        // Read each image, detect the barcode, and compare detected angle
        // -----------------------------------------------------------------
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Extract expected rotation angle from the file name (after last underscore)
            string fileName = Path.GetFileNameWithoutExtension(file);
            string anglePart = fileName.Substring(fileName.LastIndexOf('_') + 1);
            if (!float.TryParse(anglePart, out float expectedAngle))
            {
                Console.WriteLine($"Unable to parse expected angle from file name: {fileName}");
                continue;
            }

            try
            {
                // Initialize reader for Code128 barcodes
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    bool found = false;
                    // Iterate through all detected barcodes (should be one per image)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        double detectedAngle = result.Region.Angle;
                        // Consider a match if the difference is less than 0.5 degrees
                        bool match = Math.Abs(detectedAngle - expectedAngle) < 0.5;
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Detected: {detectedAngle}° | Expected: {expectedAngle}° | Match: {match}");
                        found = true;
                    }
                    if (!found)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(file)}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Failed to load image {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // Cleanup: delete generated images and temporary folder
        // -----------------------------------------------------------------
        try
        {
            foreach (string file in imageFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors to avoid breaking the example flow
        }
    }
}