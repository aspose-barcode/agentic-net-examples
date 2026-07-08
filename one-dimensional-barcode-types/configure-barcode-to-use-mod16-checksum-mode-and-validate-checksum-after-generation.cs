// Title: Codabar barcode generation with Mod16 checksum and validation
// Description: Demonstrates how to generate a Codabar barcode using Mod16 checksum mode and then read it back while validating the checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating 1D barcodes with specific checksum settings and BarCodeReader for decoding and validating those barcodes. Developers working with inventory, shipping, or point‑of‑sale systems often need to configure checksum modes such as Mod16 for Codabar and ensure data integrity during scanning.
// Prompt: Configure barcode to use Mod16 checksum mode and validate the checksum after generation.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Codabar barcode with Mod16 checksum, saves it as an image,
/// then reads the image back and validates the checksum using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation, saving, and validation.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "codabar_mod16.png");

        // --------------------------------------------------------------------
        // Generate a Codabar barcode with Mod16 checksum enabled
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456B"))
        {
            // Turn on checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Specify Mod16 checksum mode for Codabar symbology
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // --------------------------------------------------------------------
        // Read the generated barcode image and validate its checksum
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.Codabar))
        {
            // Enable checksum validation during the recognition process
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text
                Console.WriteLine($"CodeText: {result.CodeText}");

                // For 1D barcodes, the checksum value is available via Extended.OneD.CheckSum
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
            }
        }
    }
}