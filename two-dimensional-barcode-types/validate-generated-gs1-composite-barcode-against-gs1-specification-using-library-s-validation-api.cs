// Title: Validate GS1 Composite barcode generation and decoding
// Description: Demonstrates generating a GS1 Composite barcode, saving it as PNG, and validating the decoded components against the original data using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a GS1 Composite barcode, BarCodeReader to decode it, and the validation of decoded components. Developers working with GS1 symbologies often need to generate composite barcodes, read them back, and ensure compliance with GS1 specifications. The key API classes illustrated are BarcodeGenerator, BarCodeReader, and related parameter objects.
// Prompt: Validate generated GS1 Composite barcode against GS1 specification using the library's validation API.
// Tags: gs1, composite, barcode, generation, recognition, validation, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 Composite barcode, saves it to a temporary file,
/// reads it back, and validates that the decoded components match the original input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, reading, and validation.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output path for the generated barcode image
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "gs1Composite.png");

        // --------------------------------------------------------------------
        // Define GS1 components:
        //   - Linear component: GTIN-14 (AI 01)
        //   - 2D component: Sample AI 10 (batch/lot number)
        // --------------------------------------------------------------------
        string linearComponent = "(01)01234567890128";
        string twoDComponent = "(10)ABCD1234";
        string codeText = $"{linearComponent}|{twoDComponent}";

        // --------------------------------------------------------------------
        // Generate GS1 Composite barcode using BarcodeGenerator
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set barcode appearance and encoding options
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode generated at: {outputPath}");

        // --------------------------------------------------------------------
        // Read and validate the barcode using BarCodeReader
        // --------------------------------------------------------------------
        bool validationPassed = false;
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Extract decoded linear and 2D components
                string readLinear = result.Extended.GS1CompositeBar.OneDCodeText;
                string readTwoD = result.Extended.GS1CompositeBar.TwoDCodeText;
                string readLinearType = result.Extended.GS1CompositeBar.OneDType.ToString();
                string readTwoDType = result.Extended.GS1CompositeBar.TwoDType.ToString();

                // Output decoded information
                Console.WriteLine($"Detected Linear Component Type: {readLinearType}");
                Console.WriteLine($"Detected Linear Component Text: {readLinear}");
                Console.WriteLine($"Detected 2D Component Type: {readTwoDType}");
                Console.WriteLine($"Detected 2D Component Text: {readTwoD}");

                // Verify that decoded components match the original input
                if (readLinear == linearComponent && readTwoD == twoDComponent)
                {
                    validationPassed = true;
                }
            }
        }

        // Report validation result
        Console.WriteLine(validationPassed
            ? "Validation passed: decoded components match original."
            : "Validation failed: decoded components do not match original.");
    }
}