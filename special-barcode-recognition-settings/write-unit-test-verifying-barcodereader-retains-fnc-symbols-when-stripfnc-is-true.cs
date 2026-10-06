// Title: BarCodeReader StripFNC Demonstration
// Description: This example generates a Code128 barcode containing FNC1, FNC2, and FNC3 symbols, then reads it twice to show how the StripFNC setting controls whether those symbols are retained in the decoded text.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, focusing on BarcodeGenerator, BarCodeReader, and BarcodeSettings. Typical scenarios include validating barcode data integrity when FNC symbols are required or need to be removed, a common need in inventory and shipping applications. This example belongs to the “Barcode reading options” collection, useful for developers searching for how to configure StripFNC behavior.
// Prompt: Write a unit test verifying BarCodeReader retains FNC symbols when StripFNC is true.
// Tags: barcode, code128, fnc, stripfnc, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with FNC symbols and verifying
/// BarCodeReader's StripFNC behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it with StripFNC false and true,
    /// and reports whether the FNC symbols are correctly retained or stripped.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code128_fnc.png");

        // Code128 text that includes FNC1, FNC2, and FNC3 characters (char values 200‑202)
        string codeText = "Aspose" + (char)200 + (char)201 + (char)202;

        // -----------------------------------------------------------------
        // Generate the barcode image using BarcodeGenerator
        // -----------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set a small X‑dimension for a compact image
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // -----------------------------------------------------------------
        // Read the barcode with StripFNC = false (FNC symbols should be kept)
        // -----------------------------------------------------------------
        bool retainFnc = false;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length > 0)
            {
                string text = results[0].CodeText;
                retainFnc = text.Contains("<FNC1>") && text.Contains("<FNC2>") && text.Contains("<FNC3>");
                Console.WriteLine($"StripFNC false, CodeText: {text}");
            }
        }

        // -----------------------------------------------------------------
        // Read the barcode with StripFNC = true (FNC symbols should be removed)
        // -----------------------------------------------------------------
        bool stripFnc = false;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length > 0)
            {
                string text = results[0].CodeText;
                stripFnc = !text.Contains("<FNC1>") && !text.Contains("<FNC2>") && !text.Contains("<FNC3>");
                Console.WriteLine($"StripFNC true, CodeText: {text}");
            }
        }

        // -----------------------------------------------------------------
        // Report the overall test outcome
        // -----------------------------------------------------------------
        if (retainFnc && stripFnc)
        {
            Console.WriteLine("PASSED: FNC symbols retained when StripFNC is false and stripped when true.");
        }
        else
        {
            Console.WriteLine("FAILED: FNC symbol handling did not meet expectations.");
        }

        // -----------------------------------------------------------------
        // Cleanup temporary files and folder
        // -----------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the test result
        }
    }
}