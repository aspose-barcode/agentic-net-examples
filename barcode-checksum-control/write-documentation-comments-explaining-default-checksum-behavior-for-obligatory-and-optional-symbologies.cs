using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace AsposeBarcodeChecksumDemo
{
    /// <summary>
    /// Demonstrates the default checksum behavior for symbologies that require a checksum
    /// (obligatory) and for symbologies where a checksum is optional.
    /// <para>
    /// The <see cref="BarcodeGenerator.Parameters.Barcode.IsChecksumEnabled"/> property
    /// returns an <see cref="EnableChecksum"/> value. When the value is <c>Default</c>,
    /// Aspose.BarCode automatically attaches a checksum for symbologies that must contain
    /// one, and omits it for symbologies where a checksum is only possible.
    /// </para>
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo application.
        /// </summary>
        static void Main()
        {
            // -----------------------------------------------------------------
            // Example 1: EAN13 (checksum is mandatory)
            // -----------------------------------------------------------------
            // The default setting (EnableChecksum.Default) causes Aspose.BarCode
            // to automatically add the required checksum.
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
            {
                // Output the effective checksum setting for verification.
                Console.WriteLine($"EAN13 default IsChecksumEnabled: {generator.Parameters.Barcode.IsChecksumEnabled}");
            }

            // -----------------------------------------------------------------
            // Example 2: Code39FullASCII (checksum is optional)
            // -----------------------------------------------------------------
            // With the default setting, Aspose.BarCode resolves the value to
            // EnableChecksum.No because the checksum is not required for this symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
            {
                // Output the effective checksum setting for verification.
                Console.WriteLine($"Code39FullASCII default IsChecksumEnabled: {generator.Parameters.Barcode.IsChecksumEnabled}");
            }
        }
    }
}