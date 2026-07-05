// Title: Verify default checksum setting for Code 39 barcode
// Description: Demonstrates how to confirm that the IsChecksumEnabled property for Code 39 barcodes defaults to disabled, useful for ensuring correct checksum handling in barcode generation.
// Prompt: Write a unit test confirming that the default IsChecksumEnabled for Code 39 is false.
// Tags: barcode symbology, checksum, unit test, code39, aspose.barcode

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that checks the default checksum configuration for a Code 39 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Creates a barcode generator and verifies the default checksum setting.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code39FullASCII with sample text "ABC".
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC"))
        {
            // Retrieve the current default checksum setting from the generator's parameters.
            EnableChecksum defaultChecksum = generator.Parameters.Barcode.IsChecksumEnabled;

            // Compare the retrieved setting with the expected default (No) and output the result.
            if (defaultChecksum == EnableChecksum.No)
            {
                Console.WriteLine("PASSED: Default IsChecksumEnabled for Code39 is No.");
            }
            else
            {
                Console.WriteLine($"FAILED: Expected No, but got {defaultChecksum}.");
            }
        }
    }
}