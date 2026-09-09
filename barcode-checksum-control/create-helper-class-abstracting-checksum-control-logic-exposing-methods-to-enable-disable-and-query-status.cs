// Title: Checksum Control Helper for Aspose.BarCode
// Description: Demonstrates enabling, disabling, and querying the checksum setting of a barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to manipulate checksum behavior for barcodes. It uses the BarcodeGenerator class and its Parameters.Barcode.IsChecksumEnabled property. Typical use cases include ensuring data integrity for symbologies that support checksums, such as Code39, and providing developers with a reusable helper for checksum management.
// Prompt: Create a helper class abstracting checksum control logic, exposing methods to enable, disable, and query status.
// Tags: barcode, checksum, code39, aspose.barcode, generation, helper, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides static helper methods to control checksum settings on a <see cref="BarcodeGenerator"/>.
/// </summary>
public static class ChecksumHelper
{
    /// <summary>
    /// Enables checksum calculation for the specified barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance to modify.</param>
    public static void SetChecksumOn(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        // Set the checksum flag to Yes
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
    }

    /// <summary>
    /// Disables checksum calculation for the specified barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance to modify.</param>
    public static void SetChecksumOff(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        // Set the checksum flag to No
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
    }

    /// <summary>
    /// Retrieves the current checksum status of the specified barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> instance to query.</param>
    /// <returns>The <see cref="EnableChecksum"/> value indicating whether checksum is enabled.</returns>
    public static EnableChecksum GetChecksumStatus(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        // Return the current checksum setting
        return generator.Parameters.Barcode.IsChecksumEnabled;
    }
}

class Program
{
    /// <summary>
    /// Demonstrates usage of <see cref="ChecksumHelper"/> to enable/disable checksum and save barcodes.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the two barcode images
        string pathWithChecksum = Path.Combine(outputDir, "Code39_WithChecksum.png");
        string pathWithoutChecksum = Path.Combine(outputDir, "Code39_WithoutChecksum.png");

        // Initialize a barcode generator for Code39 with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Enable checksum and save the barcode image
            ChecksumHelper.SetChecksumOn(generator);
            generator.Save(pathWithChecksum, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode with checksum to: {pathWithChecksum}");

            // Disable checksum and save the barcode image
            ChecksumHelper.SetChecksumOff(generator);
            generator.Save(pathWithoutChecksum, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved barcode without checksum to: {pathWithoutChecksum}");

            // Query and display the current checksum status
            var status = ChecksumHelper.GetChecksumStatus(generator);
            Console.WriteLine($"Current checksum status after operations: {status}");
        }

        // Clean up: optionally delete the temporary files (commented out to keep results)
        // File.Delete(pathWithChecksum);
        // File.Delete(pathWithoutChecksum);
        // Directory.Delete(outputDir);
    }
}