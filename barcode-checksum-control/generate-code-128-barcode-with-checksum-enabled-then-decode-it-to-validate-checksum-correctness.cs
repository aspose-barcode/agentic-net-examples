// Title: Generate and Validate Code 128 Barcode with Checksum
// Description: This example creates a Code 128 barcode with checksum enabled, saves it as a PNG image, then reads the image back and validates the checksum during decoding.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for Code 128 symbology. It showcases the use of BarcodeGenerator to produce barcodes, BarCodeReader for decoding, and checksum handling via IsChecksumEnabled and ChecksumValidation. Ideal for developers needing reliable barcode creation and verification in .NET applications.
// Prompt: Generate a Code 128 barcode with checksum enabled, then decode it to validate checksum correctness.
// Tags: code128, checksum, barcode generation, barcode recognition, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a Code 128 barcode with checksum enabled,
/// save it to a file, and then decode it while validating the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, decoding, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code128Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output image path and the text to encode
        string barcodePath = Path.Combine(tempFolder, "code128.png");
        string codeText = "Aspose1234";

        // Generate a Code 128 barcode with checksum enabled and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {barcodePath}");

        // Prepare to decode the barcode; specify the expected symbology
        BaseDecodeType decodeType = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Ensure checksum validation is active (default for mandatory checksum)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool found = false;
            // Iterate through all detected barcodes (should be only one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
                // If execution reaches this point, checksum validation succeeded
                Console.WriteLine("Checksum validation passed.");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any errors during cleanup
        }
    }
}