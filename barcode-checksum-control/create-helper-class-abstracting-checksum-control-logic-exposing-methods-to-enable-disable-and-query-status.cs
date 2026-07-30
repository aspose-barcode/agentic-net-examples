// Title: Checksum Control Helper for Aspose.BarCode
// Description: Demonstrates how to enable, disable, and query the checksum setting of a barcode generator using a reusable helper class.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode parameter manipulation. It showcases the use of BarcodeGenerator, EncodeTypes, and EnableChecksum classes to control checksum behavior, a common requirement when generating symbologies that support optional checksums such as Code39. Developers often need a simple abstraction to toggle checksum settings across multiple generators.
// Prompt: Create a helper class abstracting checksum control logic, exposing methods to enable, disable, and query status.
// Tags: code39, checksum, png, aspose.barcode, generation, helper

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides static methods to control the checksum setting of a <see cref="BarcodeGenerator"/>.
/// </summary>
public static class ChecksumHelper
{
    /// <summary>
    /// Enables checksum for the specified barcode generator.
    /// </summary>
    /// <param name="generator">The barcode generator whose checksum will be enabled.</param>
    public static void SetChecksumOn(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
    }

    /// <summary>
    /// Disables checksum for the specified barcode generator.
    /// </summary>
    /// <param name="generator">The barcode generator whose checksum will be disabled.</param>
    public static void SetChecksumOff(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
    }

    /// <summary>
    /// Retrieves the current checksum status of the specified barcode generator.
    /// </summary>
    /// <param name="generator">The barcode generator to query.</param>
    /// <returns>The <see cref="EnableChecksum"/> value indicating whether checksum is enabled.</returns>
    public static EnableChecksum GetChecksumStatus(BarcodeGenerator generator)
    {
        if (generator == null) throw new ArgumentNullException(nameof(generator));
        return generator.Parameters.Barcode.IsChecksumEnabled;
    }
}

class Program
{
    /// <summary>
    /// Entry point of the example. Generates two Code39 barcodes—one with checksum enabled and one with it disabled—using the <see cref="ChecksumHelper"/>.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the enabled and disabled checksum images.
        string enabledPath = Path.Combine(outputDir, "code39_enabled.png");
        string disabledPath = Path.Combine(outputDir, "code39_disabled.png");

        // Generate a barcode with checksum enabled (Code39 supports optional checksum).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "12345"))
        {
            ChecksumHelper.SetChecksumOn(generator);
            generator.Save(enabledPath);
            Console.WriteLine($"Checksum enabled status: {ChecksumHelper.GetChecksumStatus(generator)}");
            Console.WriteLine($"Saved barcode with checksum enabled to: {enabledPath}");
        }

        // Generate a barcode with checksum disabled.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "12345"))
        {
            ChecksumHelper.SetChecksumOff(generator);
            generator.Save(disabledPath);
            Console.WriteLine($"Checksum disabled status: {ChecksumHelper.GetChecksumStatus(generator)}");
            Console.WriteLine($"Saved barcode with checksum disabled to: {disabledPath}");
        }
    }
}