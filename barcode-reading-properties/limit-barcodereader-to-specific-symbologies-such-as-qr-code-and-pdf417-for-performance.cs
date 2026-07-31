// Title: Limit BarCodeReader to specific symbologies for faster decoding
// Description: Demonstrates generating QR, PDF417, and Code128 barcodes, then reading only QR and PDF417 types to improve performance.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarcodeGenerator to create images and BarCodeReader with selective DecodeType parameters. Developers often need to limit symbology detection to reduce processing time when only certain barcode types are expected, such as QR Code and PDF417 in mobile scanning or document processing scenarios.
// Prompt: Limit BarCodeReader to specific symbologies such as QR Code and PDF417 for performance.
// Tags: barcode symbology, read, png, barcodegenerator, barcodereader, qr, pdf417

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating sample barcodes and reading only selected symbologies (QR and PDF417) to improve performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcode images, then reads them while limiting detection to QR and PDF417.
    /// </summary>
    static void Main()
    {
        // Define file paths for the generated barcode images
        string qrPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");
        string pdf417Path = Path.Combine(Directory.GetCurrentDirectory(), "pdf417.png");
        string code128Path = Path.Combine(Directory.GetCurrentDirectory(), "code128.png");

        // -------------------------------------------------
        // Generate a QR Code image
        // -------------------------------------------------
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Generate a PDF417 image
        // -------------------------------------------------
        using (var pdf417Generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            pdf417Generator.Save(pdf417Path, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Generate a Code128 image (will be ignored during reading)
        // -------------------------------------------------
        using (var code128Generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            code128Generator.Save(code128Path, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Read barcodes, limiting detection to QR and PDF417 only
        // -------------------------------------------------
        string[] filesToRead = { qrPath, pdf417Path, code128Path };
        foreach (string file in filesToRead)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Construct BarCodeReader with the desired decode types.
            // Only QR and PDF417 symbologies will be processed, improving performance.
            using (var reader = new BarCodeReader(file, DecodeType.QR, DecodeType.Pdf417))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Detected Type : {result.CodeTypeName}");
                    Console.WriteLine($"  Code Text     : {result.CodeText}");
                }
            }
        }

        // End of program
    }
}