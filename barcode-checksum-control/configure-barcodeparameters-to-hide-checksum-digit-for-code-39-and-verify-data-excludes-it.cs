// Title: Hide Checksum Digit for Code 39 Barcode and Verify Data
// Description: Demonstrates generating a Code 39 barcode with the checksum digit hidden and then reading it back to confirm the encoded data excludes the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to configure BarcodeGenerator parameters such as IsChecksumEnabled and ChecksumAlwaysShow for Code 39 symbology, generate an image, and use BarCodeReader to decode and validate the result. Developers working with barcode creation, customization, and verification commonly use these APIs to control checksum display and ensure data integrity.
// Prompt: Configure BarcodeParameters to hide the checksum digit for Code 39 and verify the data excludes it.
// Tags: code39, checksum, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates configuring BarcodeParameters to hide the checksum digit for Code 39,
/// generating the barcode image, and verifying that the decoded data matches the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates a Code 39 barcode without a visible checksum,
    /// reads the barcode back, validates the data, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code39Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output path for the barcode image and the data to encode
        string barcodePath = Path.Combine(tempFolder, "code39.png");
        string data = "ABC123";

        // Generate Code 39 barcode without checksum display
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, data))
        {
            // Disable checksum calculation and hide it if present
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode and verify the decoded text matches the original data (no checksum)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code39FullASCII))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read CodeText: {result.CodeText}");
                bool matches = string.Equals(result.CodeText, data, StringComparison.Ordinal);
                Console.WriteLine($"Verification {(matches ? "passed" : "failed")}: code text {(matches ? "matches" : "does not match")} original data.");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}