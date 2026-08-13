// Title: Generate Barcodes with Embedded FNC Symbols and Test StripFNC Behavior
// Description: Creates synthetic barcode images containing FNC symbols for GS1-128, PDF417, and QR code symbologies, then reads them with and without stripping FNC characters to demonstrate the StripFNC setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes with special function characters (FNC1, group separators) and BarCodeReader for decoding them while toggling the StripFNC option. Developers working with GS1, PDF417, or QR codes often need to test how embedded function characters are handled during scanning, making this pattern useful for unit tests and data validation pipelines.
// Prompt: Write a script that generates synthetic barcode images containing embedded FNC symbols for testing StripFNC behavior.
// Tags: barcode generation, barcode recognition, fnc symbols, stripfnc, gs1-128, pdf417, qr code, aspose.barcode, synthetic test images

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;
using Aspose.BarCode.Generation; // for BarCodeImageFormat
using Aspose.BarCode.Generation; // for QrExtCodetextBuilder
using Aspose.BarCode.Generation; // for QREncodeMode

/// <summary>
/// Demonstrates how to generate barcodes that contain FNC symbols and how to read them
/// with the StripFNC option enabled or disabled using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, then reads them to show
    /// the effect of the StripFNC setting.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory for generated barcode images
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // 1. Generate a GS1-128 barcode (FNC1 is inserted automatically for AI format)
        // --------------------------------------------------------------------
        string gs1Path = Path.Combine(outputDir, "gs1code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, "(02)04006664241007(37)1(400)7019590754"))
        {
            generator.Save(gs1Path, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 2. Generate a PDF417 barcode with Code128 emulation (FNC1 encoded as Group Separator \u001D)
        // --------------------------------------------------------------------
        string pdf417Path = Path.Combine(outputDir, "pdf417.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "a\u001d1222322323"))
        {
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;
            generator.Save(pdf417Path, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 3. Generate a QR code using Extended mode with FNC1 in the first position
        // --------------------------------------------------------------------
        string qrPath = Path.Combine(outputDir, "qr_fnc1.png");
        var qrBuilder = new QrExtCodetextBuilder();
        qrBuilder.AddFNC1FirstPosition();               // <FNC1> at first position
        qrBuilder.AddPlainCodetext("12345");            // data segment
        qrBuilder.AddFNC1GroupSeparator();             // group separator (GS)
        qrBuilder.AddPlainCodetext("67890");            // second data segment
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = qrBuilder.GetExtendedCodetext();
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "QR with FNC1";
            generator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Local function: reads a barcode image with StripFNC false and true,
        // then prints the decoded CodeText values.
        // --------------------------------------------------------------------
        void ReadAndDisplay(string imagePath, BaseDecodeType decodeType)
        {
            Console.WriteLine($"Reading '{Path.GetFileName(imagePath)}' without stripping FNC:");
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                reader.BarcodeSettings.StripFNC = false;
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                }
            }

            Console.WriteLine($"Reading '{Path.GetFileName(imagePath)}' with StripFNC enabled:");
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                reader.BarcodeSettings.StripFNC = true;
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                }
            }

            Console.WriteLine();
        }

        // --------------------------------------------------------------------
        // Execute reading tests for each generated barcode
        // --------------------------------------------------------------------
        ReadAndDisplay(gs1Path, DecodeType.GS1Code128);
        ReadAndDisplay(pdf417Path, DecodeType.Pdf417);
        ReadAndDisplay(qrPath, DecodeType.QR);

        Console.WriteLine("Barcode generation and StripFNC testing completed.");
    }
}