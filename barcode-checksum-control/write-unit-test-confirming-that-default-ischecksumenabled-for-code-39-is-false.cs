// Title: Verify default checksum setting for Code 39 barcode
// Description: Demonstrates checking the default IsChecksumEnabled property for a Code 39 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to inspect barcode parameters such as checksum settings. It uses BarcodeGenerator and its Parameters.Barcode API to retrieve default values, a common task when configuring barcodes for validation or compliance. Developers often need to confirm default configurations before applying custom settings.
// Prompt: Write a unit test confirming that the default IsChecksumEnabled for Code 39 is false.
// Tags: barcode symbology,checksum,default,unit-test,aspose.barcode,generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that checks the default checksum setting for a Code 39 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator for Code 39,
    /// reads the default IsChecksumEnabled value, and reports the result.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code 39 with sample text "TEST"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "TEST"))
        {
            // Retrieve the default checksum setting from the generator's parameters
            EnableChecksum defaultChecksum = generator.Parameters.Barcode.IsChecksumEnabled;

            // Verify that the default is 'No' (i.e., checksum disabled) and output the test result
            if (defaultChecksum == EnableChecksum.No)
            {
                Console.WriteLine("PASSED: Default IsChecksumEnabled for Code39 is No.");
            }
            else
            {
                Console.WriteLine($"FAILED: Default IsChecksumEnabled for Code39 is {defaultChecksum}, expected No.");
            }
        }
    }
}