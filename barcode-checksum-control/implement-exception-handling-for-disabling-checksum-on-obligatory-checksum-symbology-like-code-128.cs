// Title: Demonstrate exception handling when disabling checksum on Code 128 barcode
// Description: Shows how to generate a Code 128 barcode with default checksum, then attempts to disable the checksum, which is not allowed, and captures the resulting exception.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on checksum management for symbologies. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to illustrate typical use cases such as creating barcodes, configuring parameters, and handling errors when unsupported settings are applied. Developers often need to understand which symbologies require mandatory checksums and how to gracefully handle related exceptions.
// Prompt: Implement exception handling for disabling checksum on an obligatory‑checksum symbology like Code 128.
// Tags: barcode symbology, checksum, exception handling, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code 128 barcode, demonstrates default checksum behavior,
/// and handles the exception thrown when attempting to disable the mandatory checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output directories, generates barcodes,
    /// and captures errors related to checksum configuration.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "ChecksumDemo");
        Directory.CreateDirectory(outputDir);

        // Define the file path for the barcode with default checksum enabled
        string barcodePath = Path.Combine(outputDir, "Code128_DefaultChecksum.png");

        // Generate a Code 128 barcode using default settings (checksum enabled)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved with default checksum: {barcodePath}");
        }

        // Define the file path for the barcode where checksum disabling is attempted
        string disabledPath = Path.Combine(outputDir, "Code128_DisabledChecksum.png");

        // Attempt to disable the checksum for Code 128, which requires a checksum,
        // and handle the expected exception gracefully
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            try
            {
                // This assignment triggers an exception because Code 128 mandates a checksum
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                generator.Save(disabledPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved with checksum disabled (unexpected): {disabledPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught while disabling checksum for Code128:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}