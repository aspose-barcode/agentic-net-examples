using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates the default checksum behavior for barcode symbologies that
/// require a checksum (obligatory) and those where a checksum is optional.
/// </summary>
class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // Obligatory checksum symbology (EAN13)
        // ------------------------------------------------------------
        // EAN13 specification mandates a checksum digit. When a BarcodeGenerator
        // is created with EncodeTypes.EAN13, the <c>IsChecksumEnabled</c> property
        // defaults to <c>EnableChecksum.Default</c>, which the library interprets
        // as <c>EnableChecksum.Yes</c> for symbologies that must contain a checksum.
        // Therefore the checksum digit is automatically calculated and appended
        // to the supplied code text (if it is not already present).
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "123456789012"))
        {
            Console.WriteLine("EAN13 - Default IsChecksumEnabled: " + generator.Parameters.Barcode.IsChecksumEnabled);
            // The generated image will include the checksum digit in the human‑readable text.
            generator.Save("ean13.png");
        }

        // ------------------------------------------------------------
        // Optional checksum symbology (Code39)
        // ------------------------------------------------------------
        // Code39 allows a checksum but does not require it. For such symbologies
        // the default value of <c>IsChecksumEnabled</c> is <c>EnableChecksum.Default</c>,
        // which the library treats as <c>EnableChecksum.No</c>. Consequently no checksum
        // is added unless the developer explicitly enables it.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            Console.WriteLine("Code39 - Default IsChecksumEnabled: " + generator.Parameters.Barcode.IsChecksumEnabled);
            // No checksum digit will be added to the barcode image.
            generator.Save("code39.png");
        }
    }
}