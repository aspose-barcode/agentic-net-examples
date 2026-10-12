// Title: GS1 Composite Barcode Generation and Verification Unit Test
// Description: Demonstrates generating a GS1 Composite barcode, saving it as an image, and verifying its components using Aspose.BarCode's reader.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator with EncodeTypes.GS1CompositeBar, setting linear and 2D component types, and reading the barcode via BarCodeReader with DecodeType.GS1CompositeBar. Developers often need to validate delimiter handling and component extraction when working with GS1 Composite barcodes in inventory and logistics applications.
// Prompt: Create unit test verifying correct delimiter handling when splitting CodeText for GS1 Composite.
// Tags: gs1, composite, barcode, generation, recognition, unit-test, delimiter, split, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 Composite barcode, reads it back,
/// and validates that the linear and 2‑D components are correctly split using the '|' delimiter.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, verification, and cleanup.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "gs1composite.png");

        // --------------------------------------------------------------
        // Define linear and 2‑D components (valid GTIN‑14 values) and
        // concatenate them with the GS1 Composite delimiter '|'
        // --------------------------------------------------------------
        string linearComponent = "(01)01234567890128";
        string twoDComponent = "(01)00123456789012";
        string fullCodeText = $"{linearComponent}|{twoDComponent}";

        // --------------------------------------------------------------
        // Generate the GS1 Composite barcode using Aspose.BarCode
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, fullCodeText))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------
        // Read the generated barcode and verify that the components match
        // --------------------------------------------------------------
        bool testPassed = false;
        string failureReason = "No barcode detected.";

        using (var reader = new BarCodeReader(barcodePath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                var ext = result.Extended.GS1CompositeBar;
                if (ext != null &&
                    ext.OneDCodeText == linearComponent &&
                    ext.TwoDCodeText == twoDComponent)
                {
                    testPassed = true;
                }
                else
                {
                    failureReason = $"Mismatch. Expected Linear='{linearComponent}', Got='{ext?.OneDCodeText}'. Expected 2D='{twoDComponent}', Got='{ext?.TwoDCodeText}'.";
                }
                break; // Only need the first result
            }
        }

        // --------------------------------------------------------------
        // Output test result
        // --------------------------------------------------------------
        if (testPassed)
        {
            Console.WriteLine("TEST PASSED");
        }
        else
        {
            Console.WriteLine("TEST FAILED: " + failureReason);
        }

        // --------------------------------------------------------------
        // Cleanup temporary files and folder
        // --------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}