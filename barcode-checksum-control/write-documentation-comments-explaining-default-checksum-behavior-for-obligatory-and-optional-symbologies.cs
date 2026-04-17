using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    /// <summary>
    /// Demonstrates the default checksum behavior of Aspose.BarCode.
    /// For symbologies that obligatorily contain a checksum (e.g., EAN13, UPC-A),
    /// the generator's <c>IsChecksumEnabled</c> property defaults to <c>EnableChecksum.Yes</c>.
    /// For symbologies where a checksum is optional (e.g., Code39, Interleaved2of5),
    /// the default is <c>EnableChecksum.No</c>. The property can be overridden
    /// explicitly if a different behavior is required.
    /// </summary>
    static void ShowDefaultChecksumBehavior()
    {
        // Example 1: EAN13 – checksum is mandatory.
        using (var ean13Gen = new BarcodeGenerator(EncodeTypes.EAN13, "590123412345"))
        {
            // No explicit setting – default is Yes for mandatory checksum.
            Console.WriteLine($"EAN13 IsChecksumEnabled default: {ean13Gen.Parameters.Barcode.IsChecksumEnabled}");
            // Save to demonstrate that a valid checksum is appended automatically.
            ean13Gen.Save("ean13.png");
        }

        // Example 2: Code39 – checksum is optional.
        using (var code39Gen = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            // No explicit setting – default is No for optional checksum.
            Console.WriteLine($"Code39 IsChecksumEnabled default: {code39Gen.Parameters.Barcode.IsChecksumEnabled}");
            // Save to demonstrate generation without checksum.
            code39Gen.Save("code39.png");
        }

        // Example 3: Force checksum on optional symbology.
        using (var code39WithChecksum = new BarcodeGenerator(EncodeTypes.Code39, "CODE39"))
        {
            code39WithChecksum.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            Console.WriteLine($"Code39 forced IsChecksumEnabled: {code39WithChecksum.Parameters.Barcode.IsChecksumEnabled}");
            code39WithChecksum.Save("code39_checksum.png");
        }
    }

    static void Main()
    {
        ShowDefaultChecksumBehavior();
        Console.WriteLine("Demo completed.");
    }
}