// Title: Disabling checksum on Code128 barcode and handling exception
// Description: Demonstrates how to attempt disabling the checksum for a Code128 barcode, which requires a checksum, and captures the resulting exception.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode parameter configuration and error handling. It showcases the use of BarcodeGenerator, EncodeTypes, and checksum settings (EnableChecksum) to illustrate typical scenarios where developers need to validate symbology constraints and gracefully handle invalid configurations. Ideal for developers working with barcode creation, validation, and image output in .NET applications.
// Prompt: Implement exception handling for disabling checksum on an obligatory‑checksum symbology like Code 128.
// Tags: barcode symbology, checksum, exception handling, code128, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows exception handling when disabling checksum on a mandatory‑checksum symbology (Code128).
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with checksum enabled, then attempts to disable the checksum,
    /// captures the resulting exception, and saves the valid barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the barcode with checksum enabled
        string outputPath = Path.Combine(Path.GetTempPath(), "ChecksumDemo.png");

        // Create a Code128 barcode generator with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            // Enable checksum (default for Code128) and save the image
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode with checksum saved to: {outputPath}");

            // Attempt to disable checksum on an obligatory‑checksum symbology
            try
            {
                // Set checksum to disabled; this is not allowed for Code128
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Generate the barcode image to trigger validation logic
                generator.GenerateBarCodeImage();

                // If no exception occurs (unexpected), save the invalid image
                string disabledPath = Path.Combine(Path.GetTempPath(), "ChecksumDisabled.png");
                generator.Save(disabledPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode with checksum disabled saved to: {disabledPath}");
            }
            catch (Exception ex)
            {
                // Output the caught exception details
                Console.WriteLine("Exception caught while disabling checksum on Code128:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}