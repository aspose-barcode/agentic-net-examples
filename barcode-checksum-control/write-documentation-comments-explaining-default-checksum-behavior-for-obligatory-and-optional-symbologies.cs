// Title: Demonstrate Default Checksum Behavior for Various Barcode Symbologies
// Description: Shows how Aspose.BarCode sets the default checksum flag for symbologies where it is required, optional, or not used.
// Prompt: Write documentation comments explaining default checksum behavior for obligatory and optional symbologies.
// Tags: barcode symbology, checksum, default behavior, aspose.barcode, c#

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that prints the default <see cref="EnableChecksum"/> setting for several barcode symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Calls <see cref="ShowDefaultChecksum"/> for a few representative symbologies.
    /// </summary>
    static void Main()
    {
        // EAN13 – checksum is obligatory.
        ShowDefaultChecksum(EncodeTypes.EAN13);

        // Code39FullASCII – checksum is optional.
        ShowDefaultChecksum(EncodeTypes.Code39FullASCII);

        // Codabar – checksum is never used.
        ShowDefaultChecksum(EncodeTypes.Codabar);
    }

    /// <summary>
    /// Creates a <see cref="BarcodeGenerator"/> for the specified symbology and prints the default
    /// <see cref="EnableChecksum"/> value. The default is <c>EnableChecksum.Default</c>, which the
    /// Aspose.BarCode engine interprets as:
    /// <list type="bullet">
    ///   <item><description>Yes – for symbologies that must contain a checksum (e.g., EAN13, UPC-A).</description></item>
    ///   <item><description>No – for symbologies where a checksum is optional (e.g., Code39FullASCII, MSI).</description></item>
    ///   <item><description>No – for symbologies that never use a checksum (e.g., Codabar).</description></item>
    /// </list>
    /// </summary>
    /// <param name="type">The barcode symbology.</param>
    static void ShowDefaultChecksum(BaseEncodeType type)
    {
        // Initialize the generator with the given symbology.
        using (var generator = new BarcodeGenerator(type))
        {
            // Retrieve the default checksum setting (EnableChecksum.Default).
            var defaultSetting = generator.Parameters.Barcode.IsChecksumEnabled;

            // Output the symbology name and its default checksum state.
            Console.WriteLine($"{type.TypeName}: Default IsChecksumEnabled = {defaultSetting}");
        }
    }
}