// Title: Verify zero‑suppressed GTIN‑12 handling in GS1 Composite barcode
// Description: Demonstrates generating a GS1 Composite barcode with a zero‑suppressed GTIN‑12 in the 2D component and validates it using Aspose.BarCode reader.
// Category-Description: This example belongs to the Aspose.BarCode GS1 Composite operations collection. It shows how to configure linear and 2D components, generate the barcode, and read back the GS1‑Composite data using BarcodeGenerator, BarCodeReader, and related parameters. Developers working with GS1‑Composite symbology often need to ensure correct encoding and decoding of zero‑suppressed identifiers.
// Prompt: Create a unit test verifying correct handling of zero‑suppressed GTIN‑12 in the 2D component of GS1 Composite.
// Tags: gs1 composite, zero-suppressed, gtin-12, barcode generation, barcode recognition, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Composite barcode containing a zero‑suppressed GTIN‑12
/// in the 2D component, then reads it back to verify correct handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, saves it to a temporary file, reads it back,
    /// and reports whether the 2D component matches the expected zero‑suppressed GTIN‑12.
    /// </summary>
    static void Main()
    {
        // Prepare the linear and 2D components of the GS1 Composite barcode.
        // The linear part uses a full GTIN‑14, while the 2D part uses a zero‑suppressed GTIN‑12
        // padded to 14 digits as required by the GS1 Composite specification.
        string linearComponent = "(01)12345678901231"; // valid GTIN‑14 for linear part
        string twoDComponent = "(01)00123456789012"; // zero‑suppressed GTIN‑12 (padded to 14 digits)
        string codeText = $"{linearComponent}|{twoDComponent}";

        // Define a temporary file path for the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "GS1CompositeZeroSuppressed.png");

        // Generate the GS1 Composite barcode with the specified components.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Configure the linear and 2D component types.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.UPCA;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Save the barcode image as PNG.
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was created successfully.
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        bool testPassed = false;

        // Read the generated barcode and compare the 2D component text.
        try
        {
            using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.GS1CompositeBar))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    string readTwoD = result.Extended.GS1CompositeBar.TwoDCodeText;
                    if (readTwoD == twoDComponent)
                    {
                        testPassed = true;
                    }
                    else
                    {
                        Console.WriteLine($"FAILED: Expected 2D component '{twoDComponent}', but got '{readTwoD}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception during reading - {ex.Message}");
            return;
        }

        // Output the test result.
        if (testPassed)
        {
            Console.WriteLine("PASSED: Zero‑suppressed GTIN‑12 correctly handled in 2D component.");
        }
        else
        {
            Console.WriteLine("FAILED: No barcode result was read.");
        }

        // Clean up the temporary file.
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}