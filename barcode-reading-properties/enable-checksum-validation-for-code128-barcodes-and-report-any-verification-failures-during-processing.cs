// Title: Enable checksum validation for Code128 barcode generation and recognition
// Description: Demonstrates how to generate a Code128 barcode with checksum enabled, read it back with checksum validation, and report verification results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on checksum validation—a common requirement for ensuring data integrity in barcode scanning applications. Developers often need to enable checksum verification to detect corrupted or tampered barcodes during processing.
// Prompt: Enable checksum validation for Code128 barcodes and report any verification failures during processing.
// Tags: code128, checksum, barcode, generation, recognition, validation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates enabling checksum validation for Code128 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with checksum enabled, reads it back with checksum validation,
    /// and outputs the verification results to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for storing the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // ------------------------------
        // Generate a Code128 barcode
        // ------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Ensure checksum is enabled (default for Code128)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ----------------------------------------------
        // Read the barcode with checksum validation enabled
        // ----------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Turn on checksum validation for the reader
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;

            // Iterate through all detected barcodes
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine("Barcode read successfully with checksum validation.");
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Checksum Value: {result.Extended.OneD.CheckSum}");
            }

            // If no barcode was read, report a validation failure
            if (!anyResult)
            {
                Console.WriteLine("Checksum validation failed: no valid barcode detected.");
            }
        }

        // ------------------------------
        // Clean up temporary files
        // ------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}