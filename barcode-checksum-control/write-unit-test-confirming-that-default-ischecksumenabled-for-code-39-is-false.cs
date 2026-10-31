// Title: Verify default checksum setting for Code 39 barcode
// Description: Demonstrates how to check that the IsChecksumEnabled property defaults to No for Code 39 full ASCII barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating default parameter values for barcode symbologies. It uses BarcodeGenerator and its Parameters.Barcode.IsChecksumEnabled property to validate default behavior, a common task when configuring barcode generation in automated tests or CI pipelines.
// Prompt: Write a unit test confirming that the default IsChecksumEnabled for Code 39 is false.
// Tags: barcode, code39, checksum, default, unit-test, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains the entry point for the example that verifies the default checksum setting for Code 39 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Checks whether the default IsChecksumEnabled value for Code 39 full ASCII is set to <see cref="EnableChecksum.No"/>.
    /// Writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Flag indicating whether the test succeeded
        bool testPassed = false;

        // Create a barcode generator for Code39FullASCII with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "TEST"))
        {
            // Retrieve the current checksum setting
            EnableChecksum checksumSetting = generator.Parameters.Barcode.IsChecksumEnabled;

            // Verify that the default is No (i.e., checksum disabled)
            testPassed = checksumSetting == EnableChecksum.No;
        }

        // Output the test result
        if (testPassed)
        {
            Console.WriteLine("PASSED: Default IsChecksumEnabled for Code39 is No.");
        }
        else
        {
            Console.WriteLine("FAILED: Default IsChecksumEnabled for Code39 is not No.");
        }
    }
}