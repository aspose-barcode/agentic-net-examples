// Title: Code128 barcode generation with checksum disabled handling
// Description: Demonstrates generating a Code128 barcode while attempting to disable its mandatory checksum, and handling the resulting exception.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and error handling. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as IsChecksumEnabled. Developers often need to adjust symbology settings and gracefully handle invalid configurations, especially when working with mandatory checksum symbologies like Code128.
// Prompt: Implement exception handling for disabling checksum on an obligatory‑checksum symbology like Code 128.
// Tags: barcode symbology, checksum, code128, exception handling, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that attempts to generate a Code128 barcode with the checksum disabled,
/// captures the exception thrown by the Aspose.BarCode library, and reports the error.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code128 barcode, disables its mandatory checksum, and handles any resulting exception.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "code128_no_checksum.png");

        // Attempt to generate the barcode with an invalid checksum configuration
        try
        {
            // Initialize the barcode generator for Code128 with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Disable the checksum (which is obligatory for Code128)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Save the generated barcode image to the specified path
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Output a friendly message and the exception details when generation fails
            Console.WriteLine("Failed to generate barcode with checksum disabled:");
            Console.WriteLine(ex.Message);
        }
    }
}