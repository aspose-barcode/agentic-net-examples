using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates verification of the default checksum setting for Code39 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a Code39 barcode generator and checks the default checksum flag.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code39FullASCII with sample data "ABC123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC123"))
        {
            // Verify that the default IsChecksumEnabled property is not set to Yes.
            if (generator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.Yes)
            {
                // If the checksum is incorrectly enabled, throw an exception.
                throw new InvalidOperationException("Default IsChecksumEnabled for Code39 is not false.");
            }

            // If the check passes, output a confirmation message.
            Console.WriteLine("Test passed: Default IsChecksumEnabled for Code39 is false.");
        }
    }
}