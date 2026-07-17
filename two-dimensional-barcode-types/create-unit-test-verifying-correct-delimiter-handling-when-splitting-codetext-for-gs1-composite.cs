// Title: GS1 Composite Barcode Delimiter Handling Unit Test
// Description: Demonstrates a simple verification that the GS1 Composite barcode generator correctly splits the CodeText using the '|' delimiter into linear and 2‑D components.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to create a GS1 Composite barcode, configure its linear and 2‑D components, and validate the split CodeText using the BarCodeReader. Developers working with GS1 Composite symbology often need to ensure proper delimiter handling when combining linear and 2‑D data, making this pattern useful for unit testing and automated validation scenarios.
// Prompt: Create unit test verifying correct delimiter handling when splitting CodeText for GS1 Composite.
// Tags: barcode symbology, gs1 composite, generation, recognition, unit test, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a GS1 Composite barcode, reads it back, and verifies that the
/// linear (1D) and 2‑D components are correctly extracted using the '|' delimiter.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, reading, and validation.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare test data: combine linear and 2‑D parts with a delimiter.
        // ------------------------------------------------------------
        string fullCodeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";
        string[] parts = fullCodeText.Split('|');
        string expectedOneD = parts.Length > 0 ? parts[0] : string.Empty;
        string expectedTwoD = parts.Length > 1 ? parts[1] : string.Empty;

        // ------------------------------------------------------------
        // Define a temporary file path for the generated barcode image.
        // ------------------------------------------------------------
        string imagePath = Path.Combine(Path.GetTempPath(), "gs1composite_test.png");

        // ------------------------------------------------------------
        // Generate the GS1 Composite barcode with specific component types.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCodeText))
        {
            // Linear component: GS1 Code 128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            // 2‑D component: CC-A (Composite Component A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual tweaks
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the barcode image to the temporary file.
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image file was created successfully.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode from the image and extract the extended GS1 Composite data.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            var results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("FAILED: No barcode detected.");
                return;
            }

            var result = results[0];
            var extended = result.Extended.GS1CompositeBar;

            // Retrieve the actual linear and 2‑D CodeText values.
            string actualOneD = extended.OneDCodeText ?? string.Empty;
            string actualTwoD = extended.TwoDCodeText ?? string.Empty;

            // Compare expected and actual values.
            bool oneDMatch = string.Equals(expectedOneD, actualOneD, StringComparison.Ordinal);
            bool twoDMatch = string.Equals(expectedTwoD, actualTwoD, StringComparison.Ordinal);

            if (oneDMatch && twoDMatch)
            {
                Console.WriteLine("PASSED: Delimiter handling verified.");
            }
            else
            {
                Console.WriteLine("FAILED: Delimiter handling mismatch.");
                Console.WriteLine($"Expected OneD: '{expectedOneD}', Actual OneD: '{actualOneD}'");
                Console.WriteLine($"Expected TwoD: '{expectedTwoD}', Actual TwoD: '{actualTwoD}'");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file (optional).
        // ------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored: cleanup failures should not affect test outcome.
        }
    }
}