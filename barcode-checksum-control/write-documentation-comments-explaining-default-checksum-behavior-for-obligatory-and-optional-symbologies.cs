// Title: Default Checksum Behavior for Obligatory and Optional Barcode Symbologies
// Description: Demonstrates how Aspose.BarCode sets the default checksum flag for symbologies that require a checksum versus those where it is optional.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the default IsChecksumEnabled property for different BaseEncodeType values. It shows obligatory symbologies (e.g., Code128, EAN13) where the checksum is always enabled, and optional symbologies (e.g., Code39, Code93) where the checksum is disabled by default. Developers use these patterns when configuring barcode generation to meet standards and validation requirements.
// Prompt: Write documentation comments explaining default checksum behavior for obligatory and optional symbologies.
// Tags: barcode symbology, checksum, default behavior, aspose.barcode, generation

using System;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that prints the default checksum setting for a set of barcode symbologies
/// and demonstrates the behavior when attempting to change that setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates through a list of symbologies, shows their default
    /// <c>IsChecksumEnabled</c> value, and illustrates the effect of enabling or disabling the checksum.
    /// </summary>
    static void Main()
    {
        // List of symbologies to demonstrate default checksum behavior.
        // Obligatory checksum symbologies (checksum always enabled and cannot be disabled):
        //   Code128, EAN13, UPC-A, ITF14, etc.
        // Optional checksum symbologies (checksum can be enabled or disabled, default is disabled):
        //   Code39, Code93, Codabar, etc.
        var symbologies = new List<BaseEncodeType>
        {
            EncodeTypes.Code128,   // obligatory checksum
            EncodeTypes.EAN13,     // obligatory checksum
            EncodeTypes.UPCA,      // obligatory checksum
            EncodeTypes.ITF14,     // obligatory checksum
            EncodeTypes.Code39,    // optional checksum
            EncodeTypes.Code93,    // optional checksum
            EncodeTypes.Codabar    // optional checksum
        };

        // Iterate through each symbology and display its default checksum setting.
        foreach (var encode in symbologies)
        {
            // Use a simple numeric code text; most symbologies accept it.
            using (var generator = new BarcodeGenerator(encode, "123456"))
            {
                // The default value of IsChecksumEnabled depends on the symbology.
                // For obligatory checksum symbologies the default is EnableChecksum.Yes
                // and attempting to set it to EnableChecksum.No will throw an exception.
                // For optional checksum symbologies the default is EnableChecksum.No.
                EnableChecksum defaultChecksum = generator.Parameters.Barcode.IsChecksumEnabled;

                Console.WriteLine($"{encode}: Default IsChecksumEnabled = {defaultChecksum}");
            }
        }

        // Demonstrate that disabling checksum on an obligatory symbology throws.
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // This line will raise BarCodeException because Code128 requires checksum.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attempt to disable checksum on Code128 failed: {ex.Message}");
        }

        // Demonstrate enabling checksum on an optional symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            // By default checksum is disabled; we can enable it explicitly.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            Console.WriteLine($"Code39 after enabling checksum: IsChecksumEnabled = {generator.Parameters.Barcode.IsChecksumEnabled}");
        }
    }
}