// Title: Verify default checksum setting for Code 39 barcode
// Description: Demonstrates checking that the IsChecksumEnabled property defaults to disabled for Code 39 symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to inspect default barcode parameters using the BarcodeGenerator class. Developers often need to confirm default settings such as checksum behavior before customizing barcode generation for inventory, shipping, or tracking applications.
// Prompt: Write a unit test confirming that the default IsChecksumEnabled for Code 39 is false.
// Tags: barcode, code39, checksum, default-setting, aspose.barcode, generation, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates verifying the default checksum setting for Code 39 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code 39 generator and checks the default IsChecksumEnabled value.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code39 without modifying checksum settings
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Retrieve the default checksum setting from the generator parameters
            EnableChecksum defaultChecksum = generator.Parameters.Barcode.IsChecksumEnabled;

            // Determine whether the default is disabled (EnableChecksum.No)
            bool isDisabled = defaultChecksum == EnableChecksum.No;

            // Output the test result
            if (isDisabled)
            {
                Console.WriteLine("PASSED: Default IsChecksumEnabled for Code39 is disabled.");
            }
            else
            {
                Console.WriteLine($"FAILED: Expected disabled, but got {defaultChecksum}.");
            }
        }
    }
}