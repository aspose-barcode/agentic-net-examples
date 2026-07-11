// Title: Decode Swiss Post Parcel barcode with checksum validation
// Description: Demonstrates decoding a Swiss Post Parcel international barcode from a BMP image, showing how to enable and disable checksum validation.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, focusing on checksum handling for Swiss Post Parcel symbology. It uses BarCodeReader, BarcodeSettings, and QualitySettings to illustrate typical scenarios where developers need to read barcodes with strict or relaxed checksum validation, useful for parcel tracking and logistics applications.
// Prompt: Decode a Swiss Post Parcel international barcode from a BMP image and verify checksum correction.
// Tags: swisspostparcel, barcode, decoding, checksum, barcodereader, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program demonstrating decoding of a Swiss Post Parcel barcode with checksum validation toggling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the barcode from a BMP file twice: first with checksum validation on, then off with allowance for incorrect checksums.
    /// </summary>
    static void Main()
    {
        // Path to the BMP image containing the Swiss Post Parcel barcode
        string imagePath = "SwissPostParcel.bmp";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // ------------------------------------------------------------
        // First attempt: enable checksum validation (default behavior)
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            // Ensure checksum validation is active
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read all barcodes that satisfy the checksum requirement
            var results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected with checksum validation.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine("=== Checksum Validation ON ===");
                    Console.WriteLine($"Type: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
        }

        // ------------------------------------------------------------
        // Second attempt: disable strict checksum validation and allow incorrect barcodes
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            // Disable strict checksum validation
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Allow the engine to return barcodes even if the checksum is incorrect
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Read all barcodes regardless of checksum correctness
            var results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected even with checksum disabled.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine("=== Checksum Validation OFF (AllowIncorrectBarcodes) ===");
                    Console.WriteLine($"Type: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}