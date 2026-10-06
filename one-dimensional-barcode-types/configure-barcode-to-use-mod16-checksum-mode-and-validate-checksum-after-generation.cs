// Title: Codabar Barcode Generation with Mod16 Checksum and Validation
// Description: Demonstrates how to generate a Codabar barcode using the Mod16 checksum mode, save it as PNG, and then read it back to verify the checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating the use of BarcodeGenerator and BarCodeReader classes to configure checksum settings, generate images, and validate data integrity. Developers working with 1D symbologies such as Codabar often need to enable specific checksum algorithms (e.g., Mod16) to meet industry standards and ensure reliable scanning. The snippet shows typical steps for setting checksum mode, saving the barcode, and reading it back with checksum validation.
// Prompt: Configure barcode to use Mod16 checksum mode and validate the checksum after generation.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Codabar barcode with Mod16 checksum, saves it as an image,
/// then reads the image back to validate the checksum using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, saving, reading, and cleanup.
    /// </summary>
    static void Main()
    {
        // Define temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "codabar_mod16.png");

        // ------------------------------------------------------------
        // Generate Codabar barcode with Mod16 checksum
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Enable checksum (default for Codabar is Mod16, but set explicitly for clarity)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Explicitly set Mod16 checksum mode
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the generated barcode as a PNG image
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify checksum by reading the generated barcode image
        // ------------------------------------------------------------
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        using (var reader = new BarCodeReader(tempPath, DecodeType.Codabar))
        {
            // Enable checksum validation (default is On, but set explicitly for clarity)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;

            // Iterate through all detected barcodes (should be only one in this case)
            foreach (var result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Extended data contains checksum value for 1D barcodes
                Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
            }

            if (!anyResult)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}