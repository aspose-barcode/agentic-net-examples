// Title: Read QR Code version and error correction level from detected barcodes
// Description: Demonstrates generating QR codes with specific version and error correction level, then reading those properties from each detected QR barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create QR codes with custom QRVersion and QRErrorLevel settings, and BarCodeReader to decode QR symbols and retrieve extended QR metadata such as version and error correction level. Developers working with QR code customization, validation, or analytics commonly use these APIs to control QR encoding parameters and extract detailed information during scanning.
// Prompt: Read QR Code version and error correction level from each detected QR barcode.
// Tags: qr code, version, error correction level, barcode generation, barcode recognition, aspose.barcode, c#

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates sample QR codes with specific versions and error correction levels,
/// then reads each code to display its version and error correction level.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary QR images, reads them,
    /// outputs QR metadata, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample QR codes
        string tempFolder = Path.Combine(Path.GetTempPath(), "QrSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample data: text, version, error correction level
        var samples = new List<(string Text, QRVersion Version, QRErrorLevel ErrorLevel)>
        {
            ("Sample QR 1", QRVersion.Version05, QRErrorLevel.LevelL),
            ("Sample QR 2", QRVersion.Version10, QRErrorLevel.LevelM),
            ("Sample QR 3", QRVersion.Version15, QRErrorLevel.LevelQ),
            ("Sample QR 4", QRVersion.Version20, QRErrorLevel.LevelH)
        };

        var filePaths = new List<string>();

        // Generate QR codes with specified version and error correction level
        for (int i = 0; i < samples.Count; i++)
        {
            var (text, version, errorLevel) = samples[i];
            string filePath = Path.Combine(tempFolder, $"qr_{i + 1}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                generator.Parameters.Barcode.QR.Version = version;
                generator.Parameters.Barcode.QR.ErrorLevel = errorLevel;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            filePaths.Add(filePath);
        }

        // Read each QR code and output its version and error correction level
        foreach (string file in filePaths)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    var qrExt = result.Extended.QR;
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Version: {qrExt.Version}");
                    Console.WriteLine($"  ErrorLevel: {qrExt.ErrorLevel}");
                }
            }
        }

        // Clean up temporary files
        foreach (string file in filePaths)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}