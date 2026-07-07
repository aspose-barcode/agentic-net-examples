// Title: Verify MaxiCode Mode 2 Codetext Generation
// Description: Demonstrates a unit‑test‑style verification that the MaxiCode Mode 2 codetext produced by Aspose.BarCode matches the expected formatted string.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It shows how to use MaxiCodeCodetextMode2, MaxiCodeStandardSecondMessage, and ComplexBarcodeGenerator to construct and validate codetext without rendering an image. Developers working with shipping or logistics barcode solutions often need to ensure the encoded data follows the required format before creating the barcode image.
// Prompt: Create a unit test that verifies the generated MaxiCode Mode 2 codetext matches the expected formatted string.
// Tags: maxicode, mode2, codetext, unit-test, aspose.barcode, complexbarcodegenerator

using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that builds a MaxiCode Mode 2 codetext, generates a barcode generator instance,
/// and validates that the constructed codetext matches the expected formatted string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares expected values, constructs the codetext object,
    /// instantiates the generator, and verifies the resulting codetext.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare expected values for the MaxiCode components
        // ------------------------------------------------------------
        string expectedPostalCode = "524032140";
        int expectedCountryCode = 56; // will be formatted as three digits "056"
        int expectedServiceCategory = 999;
        string expectedMessage = "Test message";

        // Build the expected formatted codetext string according to MaxiCode Mode 2 rules
        string expectedCodetext = expectedPostalCode +
                                 expectedCountryCode.ToString("D3") +
                                 expectedServiceCategory.ToString("D3") +
                                 expectedMessage;

        // ------------------------------------------------------------
        // Create and populate the MaxiCode Mode 2 codetext object
        // ------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = expectedPostalCode,
            CountryCode = expectedCountryCode,
            ServiceCategory = expectedServiceCategory
        };

        // Attach the standard second message to the codetext
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = expectedMessage
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // Initialize the ComplexBarcodeGenerator (required lifecycle)
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Image generation is unnecessary for this test, but the generator must be instantiated.
            generator.GenerateBarCodeImage();
        }

        // ------------------------------------------------------------
        // Retrieve the constructed codetext from the object for verification
        // ------------------------------------------------------------
        string actualCodetext = maxiCodeCodetext.GetConstructedCodetext();

        // ------------------------------------------------------------
        // Verify that the generated codetext matches the expected format
        // ------------------------------------------------------------
        if (actualCodetext == expectedCodetext)
        {
            Console.WriteLine("Test Passed: Generated codetext matches expected.");
        }
        else
        {
            Console.WriteLine("Test Failed:");
            Console.WriteLine($"Expected: \"{expectedCodetext}\"");
            Console.WriteLine($"Actual:   \"{actualCodetext}\"");
        }
    }
}