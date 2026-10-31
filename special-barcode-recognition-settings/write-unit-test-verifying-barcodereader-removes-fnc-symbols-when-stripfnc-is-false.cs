// Title: BarCodeReader StripFNC behavior demonstration
// Description: Shows how to generate a Code128 barcode containing FNC symbols and verifies that BarCodeReader respects the StripFNC setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading and generation category, illustrating the use of BarcodeGenerator, BarCodeReader, and BarcodeSettings.StripFNC. Developers often need to control whether function characters (FNC) are retained or stripped during decoding, especially for symbologies like Code128 where FNC symbols carry special meaning. The snippet demonstrates typical setup, encoding, decoding, and validation steps useful for unit testing and integration scenarios.
// Prompt: Write a unit test verifying BarCodeReader removes FNC symbols when StripFNC is false.
// Tags: code128, fnc, stripfnc, barcode, reading, generation, aspose.barcode, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with FNC symbols and validates the StripFNC setting of BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, reads it with different StripFNC settings, and reports the outcome.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "Code128FNC.png");

        // Create barcode text that includes FNC1, FNC2, and FNC3 characters
        string fnc1 = ((char)0xF1).ToString();
        string fnc2 = ((char)0xF2).ToString();
        string fnc3 = ((char)0xF3).ToString();
        string codeText = "Aspose" + fnc1 + fnc2 + fnc3;

        // Generate the barcode image
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with StripFNC = false (FNC symbols should be present)
        bool fncPresentWhenFalse = false;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                fncPresentWhenFalse = result.CodeText.Contains("<FNC");
                Console.WriteLine($"StripFNC=false => CodeText: {result.CodeText}");
            }
        }

        // Read the barcode with StripFNC = true (FNC symbols should be removed)
        bool fncAbsentWhenTrue = true;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                fncAbsentWhenTrue = !result.CodeText.Contains("<FNC");
                Console.WriteLine($"StripFNC=true => CodeText: {result.CodeText}");
            }
        }

        // Report the test results
        if (fncPresentWhenFalse && fncAbsentWhenTrue)
        {
            Console.WriteLine("TEST PASSED: StripFNC behavior is as expected.");
        }
        else
        {
            Console.WriteLine("TEST FAILED:");
            Console.WriteLine($"  Expected FNC symbols present when StripFNC=false: {fncPresentWhenFalse}");
            Console.WriteLine($"  Expected FNC symbols absent when StripFNC=true: {fncAbsentWhenTrue}");
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}