// Title: GS1 Composite Barcode Delimiter Handling Unit Test
// Description: This example generates a GS1 Composite barcode, saves it as an image, and reads it back to verify that the linear and 2D components are correctly split using the '|' delimiter.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for GS1 Composite symbology. It uses BarcodeGenerator with EncodeTypes.GS1CompositeBar, configures linear and 2D component types, and employs BarCodeReader with DecodeType.GS1CompositeBar to extract the extended GS1CompositeBar data. Developers often need to validate delimiter handling when parsing composite code texts in unit tests or automated pipelines.
// Prompt: Create unit test verifying correct delimiter handling when splitting CodeText for GS1 Composite.
// Tags: gs1, composite, barcode, generation, recognition, unit-test, delimiter, split

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a simple unit‑test‑style verification of delimiter handling for GS1 Composite barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a GS1 Composite barcode, reads it back, and checks that the linear and 2D parts are correctly split.
    /// </summary>
    static void Main()
    {
        // Prepare test data: linear (1D) and 2D components separated by the '|' delimiter.
        string linearPart = "(01)01234567890128"; // valid GTIN‑14
        string twoDPart = "(21)ABC12345";
        string codeText = $"{linearPart}|{twoDPart}";

        // Create a temporary directory to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "GS1CompositeTest");
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "barcode.png");

        // Generate the GS1 Composite barcode with specific component types and settings.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Read the barcode and validate that the delimiter correctly split the code text.
        bool testPassed = false;
        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                var extended = result.Extended.GS1CompositeBar;

                // Compare the extracted linear and 2D parts with the original values.
                bool linearMatch = string.Equals(extended.OneDCodeText, linearPart, StringComparison.Ordinal);
                bool twoDMatch = string.Equals(extended.TwoDCodeText, twoDPart, StringComparison.Ordinal);

                if (linearMatch && twoDMatch)
                {
                    testPassed = true;
                    Console.WriteLine("PASS: Linear and 2D parts correctly split.");
                }
                else
                {
                    Console.WriteLine("FAILED: Split parts do not match expected values.");
                    Console.WriteLine($"Expected Linear: {linearPart}, Got: {extended.OneDCodeText}");
                    Console.WriteLine($"Expected 2D: {twoDPart}, Got: {extended.TwoDCodeText}");
                }
            }
        }

        // Report failure if no barcode result was read.
        if (!testPassed)
        {
            Console.WriteLine("FAILED: No barcode result was read.");
        }

        // Cleanup temporary files and directory.
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}