// Title: Checksum Validation with Mixed Symbology Barcodes
// Description: Demonstrates reading mixed‑symbology barcode images using ChecksumValidation.Default to enforce default checksum handling.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure checksum validation when decoding various symbologies. It uses BarCodeReader, BarcodeGenerator, and related settings—common tasks for developers who need reliable barcode data extraction across different barcode types.
// Prompt: Apply ChecksumValidation.Default to enforce default checksum handling when reading mixed‑symbology images.
// Tags: barcode symbology, checksum validation, mixed symbology, read, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates applying ChecksumValidation.Default when reading mixed‑symbology barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates sample Code11 and Code39 barcodes, reads them with default checksum validation, and outputs results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "MixedChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the sample barcode images
        string code11Path = Path.Combine(tempFolder, "Code11.png");
        string code39Path = Path.Combine(tempFolder, "Code39.png");

        // Generate a Code11 barcode (checksum is mandatory for this symbology)
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Save(code11Path, BarCodeImageFormat.Png);
        }

        // Generate a Code39 barcode (checksum is optional; enable it for demonstration)
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            gen.Save(code39Path, BarCodeImageFormat.Png);
        }

        // Collect the generated barcode file paths
        var barcodeFiles = new[] { code11Path, code39Path };

        Console.WriteLine("Reading barcodes with ChecksumValidation.Default:");
        foreach (string file in barcodeFiles)
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Initialize the reader for all supported barcode types
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Apply the default checksum validation policy
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");

                    // For 1D barcodes, additional extended information may be available
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"  1D Value: {result.Extended.OneD.Value}");
                        Console.WriteLine($"  1D CheckSum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
        }

        // Optional cleanup of the temporary folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors that occur during cleanup
        }
    }
}