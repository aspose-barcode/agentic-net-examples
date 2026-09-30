// Title: Verify default checksum setting for Code 39 barcode
// Description: Demonstrates how to check the default IsChecksumEnabled property for a Code 39 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to inspect default barcode parameters such as checksum settings. It uses the BarcodeGenerator class with EncodeTypes.Code39FullASCII and the EnableChecksum enum. Developers often need to confirm default configurations before customizing barcode generation in unit tests or applications.
// Prompt: Write a unit test confirming that the default IsChecksumEnabled for Code 39 is false.
// Tags: barcode, code39, checksum, unit-test, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates checking the default checksum setting for a Code 39 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode generator and verifies the default checksum flag.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code39FullASCII symbology with sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC"))
        {
            // Retrieve the current checksum configuration (default value).
            EnableChecksum defaultChecksum = generator.Parameters.Barcode.IsChecksumEnabled;

            // Evaluate whether the default is disabled (EnableChecksum.No) and output the result.
            if (defaultChecksum == EnableChecksum.No)
            {
                Console.WriteLine("PASSED: Default IsChecksumEnabled is No (false).");
            }
            else
            {
                Console.WriteLine($"FAILED: Default IsChecksumEnabled is {defaultChecksum}, expected No.");
            }
        }
    }
}