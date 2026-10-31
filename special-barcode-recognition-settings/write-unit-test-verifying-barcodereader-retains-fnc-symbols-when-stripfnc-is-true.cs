// Title: Verify StripFNC behavior for Code128 barcode with FNC symbols
// Description: Demonstrates how to generate a Code128 barcode containing FNC1‑FNC3 characters, read it with Aspose.BarCode, and verify that the StripFNC setting correctly retains or removes those symbols.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on FNC symbol handling in Code128 symbology. It showcases the use of BarcodeGenerator, BarCodeReader, and the StripFNC property, which developers commonly need when processing raw barcode data versus human‑readable text. Ideal for unit‑testing scenarios and integration tests that require precise control over FNC character preservation.
// Prompt: Write a unit test verifying BarCodeReader retains FNC symbols when StripFNC is true.
// Tags: code128, fnc, stripfnc, barcode, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and verification of FNC symbol handling in Code128 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode with FNC symbols, reads it with different StripFNC settings, and reports the test result.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder for the generated barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128_fnc.png");

        // ------------------------------------------------------------
        // Build the barcode text containing FNC1, FNC2, and FNC3 characters (ASCII 200‑202)
        // ------------------------------------------------------------
        string fnc1 = ((char)200).ToString();
        string fnc2 = ((char)201).ToString();
        string fnc3 = ((char)202).ToString();
        string codeText = "Aspose" + fnc1 + fnc2 + fnc3;

        // ------------------------------------------------------------
        // Generate the Code128 barcode image
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode with StripFNC = true (FNC symbols should be retained)
        // ------------------------------------------------------------
        bool retainFnc = false;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                string readText = results[0].CodeText;
                retainFnc = readText.Contains("<FNC1>") && readText.Contains("<FNC2>") && readText.Contains("<FNC3>");
                Console.WriteLine("Read with StripFNC = true:");
                Console.WriteLine($"CodeText: {readText}");
                Console.WriteLine($"Contains FNC symbols: {retainFnc}");
            }
        }

        // ------------------------------------------------------------
        // Read the barcode with StripFNC = false (FNC symbols should be stripped)
        // ------------------------------------------------------------
        bool strippedFnc = false;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
            {
                string readText = results[0].CodeText;
                strippedFnc = !readText.Contains("<FNC1>") && !readText.Contains("<FNC2>") && !readText.Contains("<FNC3>");
                Console.WriteLine("Read with StripFNC = false:");
                Console.WriteLine($"CodeText: {readText}");
                Console.WriteLine($"FNC symbols stripped: {strippedFnc}");
            }
        }

        // ------------------------------------------------------------
        // Evaluate the test outcome based on both read scenarios
        // ------------------------------------------------------------
        if (retainFnc && strippedFnc)
        {
            Console.WriteLine("TEST PASSED: StripFNC behavior is as expected.");
        }
        else
        {
            Console.WriteLine("TEST FAILED: StripFNC behavior is not as expected.");
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}