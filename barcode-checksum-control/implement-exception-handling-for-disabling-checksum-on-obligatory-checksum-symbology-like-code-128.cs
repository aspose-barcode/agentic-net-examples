// Title: Demonstrate handling exception when disabling checksum on mandatory-checksum symbology (Code 128)
// Description: This example generates a Code 128 barcode, shows the default checksum behavior, then attempts to disable the checksum, capturing the resulting exception.
// Category-Description: Example of Aspose.BarCode barcode generation focusing on checksum control. It illustrates using BarcodeGenerator, setting IsChecksumEnabled, and handling errors for symbologies where checksum is mandatory. Developers working with barcode validation, custom rendering, or troubleshooting checksum settings will find this pattern useful.
// Prompt: Implement exception handling for disabling checksum on an obligatory‑checksum symbology like Code 128.
// Tags: barcode symbology, checksum, code128, exception handling, aspnet.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to handle an exception when attempting to disable the checksum
/// on a symbology (Code 128) where the checksum is required.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code 128 barcode with the default
    /// checksum, then tries to turn the checksum off and catches the expected exception.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Generate a Code128 barcode (checksum is obligatory)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save barcode with default (checksum enabled)
            string defaultPath = Path.Combine(outputDir, "Code128_Default.png");
            generator.Save(defaultPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with default checksum: {defaultPath}");

            // Attempt to disable checksum and handle the expected exception
            try
            {
                // Disable checksum – this operation is not allowed for Code128 and will throw
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // Regenerate the image after changing the setting
                generator.GenerateBarCodeImage();
                // Save the (unexpected) result if no exception occurs
                string disabledPath = Path.Combine(outputDir, "Code128_NoChecksum.png");
                generator.Save(disabledPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved barcode with checksum disabled: {disabledPath}");
            }
            catch (Exception ex)
            {
                // Output the exception message indicating why disabling failed
                Console.WriteLine($"Exception when disabling checksum for Code128: {ex.Message}");
            }
        }
    }
}