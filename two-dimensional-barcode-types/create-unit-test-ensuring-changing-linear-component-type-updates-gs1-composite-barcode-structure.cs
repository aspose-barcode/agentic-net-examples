// Title: GS1 Composite Barcode Linear Component Type Update Test
// Description: Demonstrates a unit‑style test that generates a GS1 Composite barcode, changes the linear component type to EAN13, and verifies the resulting barcode metadata.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on GS1 Composite barcodes. It showcases the use of BarcodeGenerator to create a composite barcode, the configuration of linear and 2D component types, and BarCodeReader to inspect extended GS1 Composite parameters. Developers working with product identification, supply‑chain labeling, or any scenario requiring GS1 Composite symbology will find this pattern useful for validating barcode structure changes.
// Prompt: Create unit test ensuring changing linear component type updates GS1 Composite barcode structure.
// Tags: gs1 composite barcode, linear component type, unit test, aspose.barcode, barcode generation, barcode recognition, ean13, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains a simple test that verifies changing the linear component type of a GS1 Composite barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the test and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Run the test and output the result
        bool testPassed = TestLinearComponentTypeChange();
        Console.WriteLine(testPassed ? "PASSED: Linear component type updated correctly." : "FAILED: Linear component type did not update as expected.");
    }

    /// <summary>
    /// Generates a GS1 Composite barcode with a specific linear component type,
    /// reads it back, and validates that the type and code text are reported correctly.
    /// </summary>
    /// <returns>True if the barcode metadata matches the expectations; otherwise false.</returns>
    static bool TestLinearComponentTypeChange()
    {
        // Prepare a temporary folder for generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // GS1 Composite codetext: linear part | 2D part
        // Linear part uses AI (01) with 14 digits (GTIN‑14)
        string linearComponent = "(01)00123456789012"; // 14 digits
        string twoDComponent = "(01)00123456789012";   // same format for simplicity
        string codeText = $"{linearComponent}|{twoDComponent}";

        string imagePath = Path.Combine(tempFolder, "gs1composite.png");

        // Generate the barcode with LinearComponentType set to EAN13
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure linear component to EAN13 and 2D component to CC_A
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.EAN13;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Save the generated image to file
            generator.Save(imagePath);
        }

        // Verify that the generated barcode reports the correct linear component type
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Access GS1 Composite extended parameters
                var gs1Ext = result.Extended.GS1CompositeBar;
                if (gs1Ext == null)
                {
                    Console.WriteLine("Extended GS1CompositeBar parameters are missing.");
                    return false;
                }

                // OneDType should correspond to the DecodeType for EAN13
                if (gs1Ext.OneDType == DecodeType.EAN13)
                {
                    // Also verify that the 1D code text matches the linear component we supplied
                    if (gs1Ext.OneDCodeText == linearComponent)
                    {
                        return true; // Test succeeded
                    }
                    else
                    {
                        Console.WriteLine($"Unexpected 1D code text. Expected: {linearComponent}, Got: {gs1Ext.OneDCodeText}");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine($"Unexpected 1D type. Expected: {DecodeType.EAN13}, Got: {gs1Ext.OneDType}");
                    return false;
                }
            }
        }

        // If no barcode was read, the test fails
        Console.WriteLine("No barcode was detected in the generated image.");
        return false;
    }
}