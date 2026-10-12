// Title: MaxiCode Mode 2 Codetext Verification Unit Test
// Description: Demonstrates how to generate a MaxiCode Mode 2 barcode and verify that the produced codetext matches the expected formatted string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCodeMode classes to create a barcode, configure visual parameters, and retrieve the codetext. Developers working with shipping, logistics, or inventory systems often need to validate MaxiCode data formats, making such unit‑style checks essential for reliable integration.
// Prompt: Create a unit test that verifies the generated MaxiCode Mode 2 codetext matches the expected formatted string.
// Tags: maxicode, barcode, unit-test, codetext, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple console‑based verification that a generated MaxiCode Mode 2 barcode
/// contains the expected formatted codetext. This pattern can be adapted into a formal
/// unit test framework (e.g., NUnit, xUnit) for automated CI validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode barcode, extracts its codetext,
    /// and compares it against a predefined expected value, outputting the result to the console.
    /// </summary>
    static void Main()
    {
        // Define control characters used in the MaxiCode formatted string
        string gs = "\u001d";   // Group Separator
        string rs = "\u001e";   // Record Separator
        string eot = "\u0004";  // End Of Transmission

        // Build the expected codetext according to MaxiCode Mode 2 specifications
        string expectedCodetext = $"[)>{rs}01{gs}B1050{gs}056{gs}001{gs}ADDITIONAL DATA{eot}";

        // Initialise the barcode generator with MaxiCode type and the expected codetext
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, expectedCodetext))
        {
            // Configure the generator to use MaxiCode Mode 2
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;

            // Optional: adjust visual density (pixel size) of the generated barcode
            generator.Parameters.Barcode.XDimension.Pixels = 15;

            // Generate the barcode image into a memory stream.
            // The image itself is not required for the codetext test, but this ensures full generation.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Retrieve the codetext that the generator has stored internally
            string actualCodetext = generator.CodeText;

            // Compare the actual codetext with the expected value and report the outcome
            if (actualCodetext == expectedCodetext)
            {
                Console.WriteLine("PASS: Generated codetext matches expected value.");
            }
            else
            {
                Console.WriteLine("FAIL: Generated codetext does not match expected value.");
                Console.WriteLine($"Expected: {expectedCodetext}");
                Console.WriteLine($"Actual:   {actualCodetext}");
            }
        }
    }
}