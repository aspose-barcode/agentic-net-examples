// Title: Check 1D barcode checksum status for Code128 in BMP image
// Description: Demonstrates how to read a BMP file, detect Code128 barcodes, and retrieve checksum information using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use BarCodeReader with a specific DecodeType, access extended barcode data, and obtain checksum values for 1D symbologies such as Code128. Developers often need to validate barcode integrity after scanning images, and this snippet illustrates typical API usage for checksum verification.
// Prompt: Check 1D barcode checksum status for Code128 barcodes detected in a BMP file.
// Tags: code128, checksum, barcode, recognition, bmp, aspose.barcode, 1d, extended data

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates detection of Code128 barcodes in a BMP file and retrieval of checksum status.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that ensures a sample BMP exists, reads Code128 barcodes, and outputs checksum details.
    /// </summary>
    static void Main()
    {
        // Path to the BMP file that will be processed.
        string bmpPath = Path.Combine(Path.GetTempPath(), "sample_code128.bmp");

        // Ensure the BMP file exists. If not, create a simple Code128 barcode image.
        if (!File.Exists(bmpPath))
        {
            CreateSampleCode128Barcode(bmpPath);
        }

        // Verify the file exists before attempting to read.
        if (!File.Exists(bmpPath))
        {
            Console.WriteLine($"File not found: {bmpPath}");
            return;
        }

        // Read barcodes from the BMP file, limiting to Code128 symbology.
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(bmpPath, decodeType))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Filter only Code128 results (extra safety).
                if (result.CodeType != DecodeType.Code128)
                    continue;

                anyFound = true;
                Console.WriteLine($"Detected Code128 barcode. CodeText: {result.CodeText}");

                // Attempt to obtain checksum information via reflection.
                // The extended data for Code128 is available under result.Extended.Code128.
                var extended = result.Extended;
                var code128Ext = extended?.Code128;
                if (code128Ext != null)
                {
                    var checksumProp = code128Ext.GetType().GetProperty("Checksum");
                    var checksumValidProp = code128Ext.GetType().GetProperty("ChecksumValid");

                    if (checksumProp != null)
                    {
                        var checksumValue = checksumProp.GetValue(code128Ext);
                        Console.WriteLine($"Checksum value: {checksumValue}");
                    }

                    if (checksumValidProp != null)
                    {
                        var isValid = checksumValidProp.GetValue(code128Ext);
                        Console.WriteLine($"Checksum valid: {isValid}");
                    }

                    if (checksumProp == null && checksumValidProp == null)
                    {
                        Console.WriteLine("Checksum information not available in the extended data.");
                    }
                }
                else
                {
                    Console.WriteLine("Extended Code128 data not available.");
                }
            }

            if (!anyFound)
            {
                Console.WriteLine("No Code128 barcodes were detected in the image.");
            }
        }
    }

    // Helper method to generate a simple Code128 barcode and save it as BMP.
    private static void CreateSampleCode128Barcode(string outputPath)
    {
        // Sample Code128 text (includes checksum automatically).
        string sampleText = "ABC123";

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sampleText))
        {
            // Ensure checksum is enabled (default for Code128).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save as BMP.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        Console.WriteLine($"Sample Code128 barcode created at: {outputPath}");
    }
}