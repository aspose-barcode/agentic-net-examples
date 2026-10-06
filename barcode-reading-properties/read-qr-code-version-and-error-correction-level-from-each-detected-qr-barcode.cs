// Title: Read QR Code version and error correction level from detected barcodes
// Description: Demonstrates how to generate QR codes with specific versions and error correction levels, then read those images to extract the version and error correction information.
// Category-Description: This example belongs to the Aspose.BarCode QR code generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes with custom QRVersion and QRErrorLevel settings, and BarCodeReader for decoding QR symbols and accessing extended QR metadata such as version and error correction level. Developers working with QR code customization, validation, or analytics will find these APIs essential for controlling QR code parameters and retrieving detailed decoding information.
// Prompt: Read QR Code version and error correction level from each detected QR barcode.
// Tags: qr code, version, error correction, barcode generation, barcode recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates QR code images with various versions and error correction levels,
/// then reads each image to display the detected QR version and error correction level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary QR images, reads them,
    /// outputs QR metadata, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated QR images
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample QR versions to generate
        var versions = new List<QRVersion>
        {
            QRVersion.Version05,
            QRVersion.Version10,
            QRVersion.Version20
        };

        // Define sample error correction levels to generate
        var errorLevels = new List<QRErrorLevel>
        {
            QRErrorLevel.LevelL,
            QRErrorLevel.LevelM,
            QRErrorLevel.LevelQ,
            QRErrorLevel.LevelH
        };

        // Generate QR code images for each combination of version and error level
        var generatedFiles = new List<string>();
        foreach (var version in versions)
        {
            foreach (var errorLevel in errorLevels)
            {
                string filePath = Path.Combine(tempFolder, $"QR_V{version}_E{errorLevel}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
                {
                    generator.Parameters.Barcode.QR.Version = version;
                    generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                generatedFiles.Add(filePath);
            }
        }

        // Read each generated QR code and output version and error correction level
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Detected Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"  Code Text: {result.CodeText}");

                    // Extended QR information contains version and error correction level
                    if (result.Extended?.QR != null)
                    {
                        Console.WriteLine($"  QR Version: {result.Extended.QR.Version}");
                        Console.WriteLine($"  Error Correction Level: {result.Extended.QR.ErrorLevel}");
                    }
                    else
                    {
                        Console.WriteLine("  No extended QR information available.");
                    }
                }
            }
        }

        // Clean up generated files
        foreach (string file in generatedFiles)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
                // Ignored – best effort cleanup
            }
        }

        // Remove the temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – best effort cleanup
        }
    }
}