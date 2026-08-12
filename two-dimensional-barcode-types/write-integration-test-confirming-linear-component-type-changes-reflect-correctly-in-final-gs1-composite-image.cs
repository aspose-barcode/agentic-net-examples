// Title: GS1 Composite Linear Component Type Integration Test
// Description: Demonstrates how to generate GS1 Composite barcodes with different linear component types and verifies that the selected type is correctly reflected in the decoded image.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite operations collection. It shows how to use BarcodeGenerator to set the LinearComponentType and TwoDComponentType, and how to read back the barcode with BarCodeReader to inspect extended GS1 Composite data. Developers working with GS1‑128, EAN13, or other linear symbologies in composite barcodes can use this pattern for automated testing or validation of barcode generation settings.
// Prompt: Write integration test confirming linear component type changes reflect correctly in the final GS1 Composite image.
// Tags: gs1 composite, linear component, barcode generation, barcode recognition, aspose.barcode, c#, integration test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Contains the entry point and helper methods for the GS1 Composite linear component type integration test.
/// </summary>
class Program
{
    /// <summary>
    /// Generates temporary barcode images with different linear component types, then verifies that the decoded
    /// linear component matches the expected type. This method serves as an integration test.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Common GS1 Composite codetext: linear part (AI 01, 14 digits) and 2D part (AI 21)
        string linearPart = "(01)00123456789012"; // 14‑digit GTIN
        string twoDPart = "(21)A12345678";
        string codeText = $"{linearPart}|{twoDPart}";

        // First test: Linear component type = GS1Code128
        string fileGs1Code128 = Path.Combine(tempFolder, "gs1code128.png");
        GenerateGs1Composite(fileGs1Code128, codeText, EncodeTypes.GS1Code128);
        // Verify the linear component type in the decoded result
        VerifyLinearComponent(fileGs1Code128, DecodeType.GS1Code128, "GS1Code128");

        // Second test: Linear component type = EAN13
        string fileEan13 = Path.Combine(tempFolder, "ean13.png");
        GenerateGs1Composite(fileEan13, codeText, EncodeTypes.EAN13);
        // Verify the linear component type in the decoded result
        VerifyLinearComponent(fileEan13, DecodeType.EAN13, "EAN13");

        Console.WriteLine("Integration test completed.");
    }

    // Generates a GS1 Composite barcode image with the specified linear component type
    static void GenerateGs1Composite(string outputPath, string codeText, BaseEncodeType linearComponentType)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set linear component type
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearComponentType;

            // Choose a 2D component type (required for GS1 Composite)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional visual settings
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the image
            generator.Save(outputPath);
        }
    }

    // Reads the generated barcode and checks that the decoded linear component type matches the expected one
    static void VerifyLinearComponent(string imagePath, BaseDecodeType expectedDecodeType, string expectedName)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: File not found - {imagePath}");
            return;
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.GS1CompositeBar))
        {
            var results = reader.ReadBarCodes();
            if (results == null || results.Length == 0)
            {
                Console.WriteLine($"Failed to read barcode from {Path.GetFileName(imagePath)}");
                return;
            }

            foreach (var result in results)
            {
                var extended = result.Extended;
                if (extended?.GS1CompositeBar == null)
                {
                    Console.WriteLine($"No GS1 Composite extended data for {Path.GetFileName(imagePath)}");
                    continue;
                }

                var oneDType = extended.GS1CompositeBar.OneDType;
                string status = oneDType == expectedDecodeType ? "PASS" : "FAIL";

                Console.WriteLine($"{status}: Linear component type in '{Path.GetFileName(imagePath)}' is '{oneDType}'. Expected '{expectedName}'.");
                Console.WriteLine($"  Decoded linear code text: {extended.GS1CompositeBar.OneDCodeText}");
                Console.WriteLine($"  Decoded 2D code text: {extended.GS1CompositeBar.TwoDCodeText}");
            }
        }
    }
}