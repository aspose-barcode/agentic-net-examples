// Title: Compare recognition speed of 1D vs 2D barcodes
// Description: Demonstrates measuring the time required to recognize a 1D Code128 barcode and a 2D QR code using identical QualitySettings.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing how to use BarCodeReader with QualitySettings to evaluate performance. It highlights key API classes such as BarcodeGenerator, BarCodeReader, QualitySettings, and DecodeType, which developers commonly use for generating barcodes, configuring recognition parameters, and benchmarking speed in real‑world applications.
// Prompt: Compare recognition speed of 1D barcodes versus 2D barcodes under identical QualitySettings.
// Tags: barcode, code128, qr, recognition, speed, qualitysettings, aspose.barcode, generation, decoding

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a 1D Code128 barcode and a 2D QR code,
/// then measures and compares their recognition times using identical
/// <see cref="QualitySettings"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes if needed,
    /// configures identical quality settings, and reports recognition
    /// durations for both 1D and 2D symbologies.
    /// </summary>
    static void Main()
    {
        // Paths for generated barcode images
        string code128Path = "code128.png";
        string qrPath = "qr.png";

        // Generate a 1D barcode (Code128) if it does not already exist
        if (!File.Exists(code128Path))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(code128Path, BarCodeImageFormat.Png);
            }
        }

        // Generate a 2D barcode (QR) if it does not already exist
        if (!File.Exists(qrPath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "1234567890"))
            {
                generator.Save(qrPath, BarCodeImageFormat.Png);
            }
        }

        // Define identical quality settings for both recognitions
        QualitySettings commonSettings = QualitySettings.NormalQuality;

        // Measure recognition time for the 1D barcode
        using (var reader1D = new BarCodeReader(code128Path, DecodeType.Code128))
        {
            reader1D.QualitySettings = commonSettings; // Apply common quality settings
            Stopwatch sw1D = Stopwatch.StartNew();    // Start timing
            var results1D = reader1D.ReadBarCodes();  // Perform recognition
            sw1D.Stop();                               // Stop timing

            Console.WriteLine($"1D (Code128) recognition time: {sw1D.ElapsedMilliseconds} ms, detected {results1D.Length} barcode(s).");
        }

        // Measure recognition time for the 2D barcode
        using (var reader2D = new BarCodeReader(qrPath, DecodeType.QR))
        {
            reader2D.QualitySettings = commonSettings; // Apply common quality settings
            Stopwatch sw2D = Stopwatch.StartNew();    // Start timing
            var results2D = reader2D.ReadBarCodes();  // Perform recognition
            sw2D.Stop();                               // Stop timing

            Console.WriteLine($"2D (QR) recognition time: {sw2D.ElapsedMilliseconds} ms, detected {results2D.Length} barcode(s).");
        }
    }
}