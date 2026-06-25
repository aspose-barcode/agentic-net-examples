using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and verification of a Codabar barcode with checksum using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Codabar barcode, saves it as PNG, and then reads it back to verify the checksum.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "codabar.png");

        // -------------------------------------------------
        // Barcode Generation
        // -------------------------------------------------
        // Create a BarcodeGenerator for Codabar with the specified data string.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set the checksum calculation mode to Mod10 (specific to Codabar).
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod10;

            // Save the generated barcode image to the defined path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Barcode Verification (Checksum Validation)
        // -------------------------------------------------
        // Initialize a BarCodeReader to read the saved image, specifying Codabar as the decode type.
        using (var reader = new BarCodeReader(outputPath, DecodeType.Codabar))
        {
            // Turn on checksum validation so the reader will verify the checksum during recognition.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all recognized barcodes (should be one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the decoded text of the barcode.
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Output the checksum value obtained from the extended OneD parameters.
                Console.WriteLine($"Checksum (from recognition): {result.Extended.OneD.CheckSum}");
            }
        }
    }
}