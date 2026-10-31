// Title: Rotated QR Code Generation and Recognition Example
// Description: Demonstrates generating QR code images at multiple rotation angles and verifying that Aspose.BarCode can correctly decode them regardless of orientation.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator to create barcodes with specific rotation settings and BarCodeReader to detect and decode them. Typical use cases include testing barcode robustness in imaging pipelines, handling scanned documents with varied orientations, and ensuring reliable decoding across different rotation angles. Developers often need to generate test images, configure rotation parameters, and validate decoding results using these core API classes.
// Prompt: Test recognition of rotated barcodes by loading images with varied orientation and verifying correct decoding.
// Tags: barcode, qr, rotation, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates QR code images rotated at various angles and validates their decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates rotated QR codes, reads them back, and prints decoding results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample images
        string tempFolder = Path.Combine(Path.GetTempPath(), "RotatedBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define rotation angles to test
        float[] angles = new float[] { 0f, 90f, 180f, 270f };
        string codeText = "Test123";

        // Store generated image file paths
        List<string> imageFiles = new List<string>();

        // Generate QR code images with different rotations
        foreach (float angle in angles)
        {
            string filePath = Path.Combine(tempFolder, $"qr_{angle}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Apply rotation to the barcode
                generator.Parameters.RotationAngle = angle;
                // Set module size (X dimension) for better readability
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save the generated barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Prepare decode type for reading QR codes
        BaseDecodeType decodeType = DecodeType.QR;

        // Read each image and verify decoding
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, decodeType))
            {
                // Attempt to read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();
                bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                string fileName = Path.GetFileName(file);
                string anglePart = fileName.Split('_')[1].Replace(".png", "");

                // Output decoding summary
                Console.WriteLine($"File: {fileName} | Rotation: {anglePart}° | Decoded: {(success ? "Success" : "Failure")}");

                if (success)
                {
                    // Output detailed information for each detected barcode
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        Console.WriteLine($"  Detected Angle: {result.Region.Angle}");
                    }
                }
            }
        }

        // Optional cleanup: uncomment to delete temporary files
        // Directory.Delete(tempFolder, true);
    }
}