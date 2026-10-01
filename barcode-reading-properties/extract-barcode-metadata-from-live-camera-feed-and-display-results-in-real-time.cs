// Title: Real-time barcode metadata extraction from simulated camera feed
// Description: Demonstrates generating barcode images, treating them as camera frames, and extracting metadata such as symbology, QR version, and DataMatrix info in real time.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to recognize them, and how to access extended metadata like QR version and error level. Typical use cases include scanning live video streams, processing captured images, and retrieving detailed barcode information for inventory or tracking systems. Developers often need to generate test images, read multiple symbologies, and handle extended properties.
// Prompt: Extract barcode metadata from live camera feed and display results in real time.
// Tags: barcode, metadata, realtime, camera, qrcode, datamatrix, code128, aspose.barcode, generation, recognition, extendedinfo

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that simulates a live camera feed by generating barcode images,
/// then reads each image to extract barcode metadata and displays the results in real time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcode frames, reads them,
    /// outputs detection details, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder to simulate frames captured from a live camera.
        string tempFolder = Path.Combine(Path.GetTempPath(), "CameraSim_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold generated image file paths (simulated frames).
        List<string> frameFiles = new List<string>();

        // Define sample barcodes to generate (each will act as a camera frame).
        var samples = new (BaseEncodeType encodeType, string codeText)[]
        {
            (EncodeTypes.QR, "(01)12345678901234(21)ABC123"),
            (EncodeTypes.Code128, "Sample12345"),
            (EncodeTypes.DataMatrix, "DM123456")
        };

        // Generate barcode images and store their file paths.
        foreach (var (encodeType, codeText) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set common visual parameters.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save the generated barcode image to the temporary folder.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            frameFiles.Add(filePath);
        }

        // Process each simulated "frame" and extract barcode metadata.
        foreach (string framePath in frameFiles)
        {
            if (!File.Exists(framePath))
            {
                Console.WriteLine($"File not found: {framePath}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(framePath))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("=== Detected Barcode ===");
                        Console.WriteLine($"File: {Path.GetFileName(framePath)}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");

                        // Show extended information for QR codes if available.
                        if (result.Extended.QR != null)
                        {
                            Console.WriteLine($"QR Version: {result.Extended.QR.Version}");
                            Console.WriteLine($"QR Error Level: {result.Extended.QR.ErrorLevel}");
                        }

                        // Show extended information for DataMatrix if available.
                        if (result.Extended.DataMatrix != null)
                        {
                            Console.WriteLine("DataMatrix barcode detected.");
                        }

                        Console.WriteLine();
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unsupported file: {framePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {framePath}: {ex.Message}");
            }
        }

        // Clean up temporary files and folder.
        try
        {
            foreach (string file in frameFiles)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            Directory.Delete(tempFolder, true);
        }
        catch (Exception cleanupEx)
        {
            Console.WriteLine($"Cleanup warning: {cleanupEx.Message}");
        }
    }
}