// Title: Verify default checksum setting for Code 39 barcode
// Description: Demonstrates checking the default IsChecksumEnabled property for a Code 39 barcode using Aspose.BarCode. The test confirms that the checksum is disabled by default.
// Category-Description: This example belongs to the Aspose.BarCode generation API category, illustrating how to inspect barcode parameters such as checksum settings. It shows usage of BarcodeGenerator, EncodeTypes, and the Parameters.Barcode.IsChecksumEnabled property, which developers commonly need when configuring barcode symbologies for validation or compliance.
// Prompt: Write a unit test confirming that the default IsChecksumEnabled for Code 39 is false.
// Tags: barcode, code39, checksum, generation, aspose.barcode, unit-test, default-setting

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that verifies the default checksum setting for a Code 39 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the verification and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Initialize variables to capture test outcome and message.
        bool testPassed = false;
        string resultMessage;

        // Create a BarcodeGenerator for Code 39 Full ASCII without modifying any settings.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "TEST"))
        {
            // Retrieve the default checksum configuration.
            EnableChecksum defaultChecksum = generator.Parameters.Barcode.IsChecksumEnabled;

            // Determine if the default matches the expected value (No).
            if (defaultChecksum == EnableChecksum.No)
            {
                testPassed = true;
                resultMessage = "PASSED: Default IsChecksumEnabled for Code39 is No.";
            }
            else
            {
                resultMessage = $"FAILED: Default IsChecksumEnabled for Code39 is {defaultChecksum}, expected No.";
            }
        }

        // Output the test result.
        Console.WriteLine(resultMessage);
    }
}