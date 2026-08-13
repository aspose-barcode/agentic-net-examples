// Title: Enable checksum validation for Code128 barcode
// Description: Demonstrates generating a Code128 barcode, then reading it with checksum validation to detect any verification failures.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with checksum validation for verifying Code128 symbology. Developers commonly need to ensure data integrity when scanning barcodes, and this pattern illustrates how to enable and handle checksum checks using Aspose.BarCode APIs.
// Prompt: Enable checksum validation for Code128 barcodes and report any verification failures during processing.
// Tags: code128, checksum validation, barcode generation, barcode recognition, aspose.barcode, symbology

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it back with checksum validation,
/// and reports any verification failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, validates it, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "code128.png");

        // Generate a Code128 barcode; the checksum is added automatically by the generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize a reader for Code128 barcodes and enable checksum validation
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Turn on checksum validation during the recognition process
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Attempt to read barcodes from the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no results are returned, checksum validation has failed
            if (results.Length == 0)
            {
                Console.WriteLine("Checksum validation failed: no valid barcode detected.");
            }
            else
            {
                // Output the decoded text and checksum information for each detected barcode
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // Attempt to delete the temporary barcode image; ignore any errors during cleanup
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // No action needed if cleanup fails
        }
    }
}