// Title: Checksum Control Example for Aspose.BarCode
// Description: Demonstrates enabling and disabling checksum calculation for a Code39 barcode and saving the resulting images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to manipulate checksum settings via the BarcodeGenerator.Parameters.Barcode.IsChecksumEnabled property. It showcases the EnableChecksum enum, typical use cases such as data validation and error detection, and helps developers quickly learn how to control checksum behavior when creating barcodes.
// Prompt: Create a helper class abstracting checksum control logic, exposing methods to enable, disable, and query status.
// Tags: barcode, checksum, generation, aspose.barcode, code39, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Helper class that abstracts checksum control logic for a <see cref="BarcodeGenerator"/>.
/// Provides methods to enable, disable, and query the checksum status.
/// </summary>
class ChecksumHelper
{
    /// <summary>
    /// Enables checksum calculation for the specified barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> to modify.</param>
    public static void SetChecksumOn(BarcodeGenerator generator)
    {
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
    }

    /// <summary>
    /// Disables checksum calculation for the specified barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> to modify.</param>
    public static void SetChecksumOff(BarcodeGenerator generator)
    {
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
    }

    /// <summary>
    /// Retrieves the current checksum status of the specified barcode generator.
    /// </summary>
    /// <param name="generator">The <see cref="BarcodeGenerator"/> to query.</param>
    /// <returns>The <see cref="EnableChecksum"/> value indicating whether checksum is enabled.</returns>
    public static EnableChecksum GetChecksumStatus(BarcodeGenerator generator)
    {
        return generator.Parameters.Barcode.IsChecksumEnabled;
    }
}

class Program
{
    /// <summary>
    /// Generates two Code39 barcodes—one with checksum enabled and one without—saving them as PNG files.
    /// </summary>
    static void Main()
    {
        // Create a temporary output directory with a unique name.
        string outputDir = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the enabled and disabled checksum images.
        string pathEnabled = Path.Combine(outputDir, "Code39_WithChecksum.png");
        string pathDisabled = Path.Combine(outputDir, "Code39_WithoutChecksum.png");

        // Generate barcode with checksum enabled.
        using (BarcodeGenerator genEnabled = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            ChecksumHelper.SetChecksumOn(genEnabled);
            genEnabled.Save(pathEnabled, BarCodeImageFormat.Png);
            Console.WriteLine($"Enabled checksum status: {ChecksumHelper.GetChecksumStatus(genEnabled)}");
        }

        // Generate barcode with checksum disabled.
        using (BarcodeGenerator genDisabled = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            ChecksumHelper.SetChecksumOff(genDisabled);
            genDisabled.Save(pathDisabled, BarCodeImageFormat.Png);
            Console.WriteLine($"Disabled checksum status: {ChecksumHelper.GetChecksumStatus(genDisabled)}");
        }

        // Inform the user where the barcode images have been saved.
        Console.WriteLine($"Barcodes saved to: {outputDir}");
    }
}