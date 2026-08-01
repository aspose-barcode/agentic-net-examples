// Title: Enforce default checksum validation when reading mixed‑symbology barcodes
// Description: Demonstrates generating Code128 and EAN13 barcodes, then reading them with ChecksumValidation.Default to ensure proper checksum handling.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure checksum validation using the BarCodeReader and BarcodeSettings classes. Developers often need to read mixed‑symbology images while applying default checksum rules to filter out invalid codes. Typical use cases include inventory systems, point‑of‑sale scanners, and batch processing of barcode images.
// Prompt: Apply ChecksumValidation.Default to enforce default checksum handling when reading mixed‑symbology images.
// Tags: barcode, checksumvalidation, default, mixed-symbology, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of generating barcodes and reading them with default checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, saves them, and reads them applying default checksum validation.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory for generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Generate a Code128 barcode and save it as PNG
        string code128Path = Path.Combine(outputDir, "code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(code128Path);
        }

        // Generate an EAN13 barcode (including checksum digit) and save it as PNG
        string ean13Path = Path.Combine(outputDir, "ean13.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(ean13Path);
        }

        // Iterate over all generated PNG files and read them with default checksum validation
        foreach (string filePath in Directory.GetFiles(outputDir, "*.png"))
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Enforce default checksum handling for each read operation
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Output each detected barcode's type and text
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                }
            }
        }
    }
}