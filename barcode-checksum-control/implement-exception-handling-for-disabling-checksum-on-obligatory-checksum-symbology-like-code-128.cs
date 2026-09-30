// Title: Exception handling when disabling checksum on mandatory‑checksum symbology (Code128)
// Description: Demonstrates how Aspose.BarCode throws an exception if you try to disable the checksum for a symbology that requires it, and shows the correct way to generate a Code128 barcode with checksum enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on checksum management. It uses the BarcodeGenerator, EncodeTypes, and related parameter classes to illustrate typical use cases such as validating symbology constraints and handling errors. Developers working with barcode creation often need to ensure required features like checksums are correctly configured, and this snippet provides a searchable reference for handling exceptions in those scenarios.
// Prompt: Implement exception handling for disabling checksum on an obligatory‑checksum symbology like Code 128.
// Tags: barcode symbology, checksum, exception handling, code128, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exception handling when attempting to disable checksum on a mandatory‑checksum symbology (Code128) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Shows both the failing attempt to disable checksum and the correct generation with checksum enabled.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string filePath = Path.Combine(outputDir, "code128.png");

        // --------------------------------------------------------------------
        // Attempt to disable checksum for Code128 (which requires a checksum)
        // --------------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // This setting is invalid for Code128 and will throw BarCodeException
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
        catch (BarCodeException ex)
        {
            Console.WriteLine("Caught expected exception when disabling checksum for Code128:");
            Console.WriteLine(ex.Message);
        }

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with checksum enabled (default behavior)
        // --------------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Ensure checksum is enabled explicitly
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine("Generated Code128 barcode with checksum enabled at:");
                Console.WriteLine(filePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error during barcode generation:");
            Console.WriteLine(ex.Message);
        }
    }
}