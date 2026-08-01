// Title: Enable Checksum Validation for Code39 Barcode Reading
// Description: Demonstrates generating a Code39 barcode, saving it as PNG, and reading it with checksum validation enabled.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with BarcodeSettings to validate checksums, a common requirement when working with optional symbologies like Code 39. Developers often need to ensure data integrity during barcode scanning, and this snippet illustrates the key API classes and typical workflow for such scenarios.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for optional symbologies like Code 39 before reading.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code39 barcode, saves it as a PNG image,
/// and reads it back with checksum validation turned on.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation and reading with checksum validation.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "code39.png";

        // Generate a Code39 barcode containing the text "ABC123"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Ensure the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not found.");
            return;
        }

        // Initialize a reader for Code39 barcodes from the saved image
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Turn on checksum validation for optional symbologies like Code39
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // If extended 1D parameters are available, display the checksum value
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}