// Title: Demonstrate checksum validation for optional symbologies (Code 39)
// Description: Shows how to enable BarcodeSettings.ChecksumValidation before reading a Code 39 barcode, ensuring checksum is validated when present.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for optional symbologies like Code 39 before reading.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code 39 barcode, saves it to a file,
/// and then reads it back with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, verifies its existence,
    /// and reads it while validating the checksum.
    /// </summary>
    static void Main()
    {
        // Define file path for the generated barcode image
        string imagePath = "code39.png";

        // Generate a Code39 barcode (checksum is optional for this symbology)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // Save the barcode image to a file
            generator.Save(imagePath);
        }

        // Verify that the image file was created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Create a BarCodeReader for Code39 and enable checksum validation
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Enable checksum validation (On) for optional symbologies like Code39
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Read barcodes from the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                // Extended parameters may contain checksum information for 1D barcodes
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}