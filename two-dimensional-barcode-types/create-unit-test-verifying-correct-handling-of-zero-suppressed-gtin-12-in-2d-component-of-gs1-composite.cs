// Title: GS1 Composite barcode zero‑suppressed GTIN‑12 verification
// Description: Demonstrates generating a GS1 Composite barcode with a zero‑suppressed GTIN‑12 in the 2D component and validates it by reading the barcode back.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite operations collection. It shows how to use BarcodeGenerator and BarCodeReader to create and recognize GS1 Composite symbols, configure linear and 2D component types, and verify GS1 data elements such as zero‑suppressed GTIN‑12. Developers working with product identification, inventory, and retail scanning often need to generate and validate GS1 Composite barcodes using the EncodeTypes, TwoDComponentType, and GS1CompositeBar parameters.
// Prompt: Create a unit test verifying correct handling of zero‑suppressed GTIN‑12 in the 2D component of GS1 Composite.
// Tags: gs1 composite, zero-suppressed gtin-12, barcode generation, barcode recognition, unit test, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Contains the entry point for the GS1 Composite zero‑suppressed GTIN‑12 verification example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a GS1 Composite barcode with a zero‑suppressed GTIN‑12 in the 2D component,
    /// reads it back, and reports whether the decoded value matches the expected data.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test artifacts.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Gs1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image.
        string barcodePath = Path.Combine(tempFolder, "gs1composite.png");

        // Linear component (UPCA) and 2D component (zero‑suppressed GTIN‑12 padded to 14 digits).
        string linearComponent = "001234567895"; // UPCA (GTIN-12)
        string twoDComponent = "(01)00123456789012"; // Zero‑suppressed GTIN‑12
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Generate the GS1 Composite barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure component types.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.UPCA;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Save the barcode image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Flag indicating whether the decoded 2D component matches the expected value.
        bool testPassed = false;

        // Verify the barcode by reading it back.
        if (File.Exists(barcodePath))
        {
            BaseDecodeType decodeType = DecodeType.GS1CompositeBar;
            using (var reader = new BarCodeReader(barcodePath, decodeType))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Extract the decoded 2D component text.
                    string decodedTwoD = result.Extended?.GS1CompositeBar?.TwoDCodeText;
                    if (decodedTwoD == twoDComponent)
                    {
                        testPassed = true;
                    }
                    else
                    {
                        Console.WriteLine($"Decoded 2D component mismatch. Expected: {twoDComponent}, Got: {decodedTwoD}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }

        // Output the test result.
        Console.WriteLine(testPassed
            ? "PASS: Zero‑suppressed GTIN‑12 correctly handled in 2D component."
            : "FAIL: Zero‑suppressed GTIN‑12 handling verification failed.");

        // Clean up temporary files and folder.
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result.
        }
    }
}