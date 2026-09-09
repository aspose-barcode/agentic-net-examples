// Title: Verify StripFNC behavior of BarCodeReader
// Description: Demonstrates reading a Code128 barcode containing FNC symbols and checks how the StripFNC setting affects the output.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and its BarcodeSettings.StripFNC property. Developers often need to control whether function characters (FNC) are retained or removed during decoding, especially when processing Code128 barcodes that embed control symbols. The snippet shows generation, reading, and validation of FNC handling, useful for unit testing and integration scenarios.
// Prompt: Write a unit test verifying BarCodeReader removes FNC symbols when StripFNC is false.
// Tags: code128, fnc, stripfnc, barcodereader, barcodegeneration, unit-test, aspnet, aspnet-core

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to verify the StripFNC setting of BarCodeReader by generating a Code128 barcode with FNC symbols,
/// reading it with different StripFNC values, and validating the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary barcode image, reads it with StripFNC set to false and true,
    /// and prints verification results to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128fnc.png");

        // Prepare code text that includes FNC symbols (character codes 241‑243)
        string codeText = "Aspose" + ((char)241) + ((char)242) + ((char)243);

        // Generate a Code128 barcode image from the prepared text
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            gen.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with StripFNC = false (expected: FNC symbols are removed)
        string resultWithoutFnc = null;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
                resultWithoutFnc = results[0].CodeText;
        }

        // Read the barcode with StripFNC = true (expected: FNC symbols are retained)
        string resultWithFnc = null;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
                resultWithFnc = results[0].CodeText;
        }

        // Verify the behavior of StripFNC
        bool pass = true;
        if (resultWithoutFnc == null || resultWithFnc == null)
        {
            Console.WriteLine("FAILED: Unable to read barcode results.");
            pass = false;
        }
        else
        {
            // When StripFNC is false, the result should not contain any FNC placeholders
            if (resultWithoutFnc.Contains("<FNC"))
            {
                Console.WriteLine("FAILED: StripFNC = false did not remove FNC symbols.");
                pass = false;
            }

            // When StripFNC is true, the result should contain FNC placeholders
            if (!resultWithFnc.Contains("<FNC"))
            {
                Console.WriteLine("FAILED: StripFNC = true did not retain FNC symbols.");
                pass = false;
            }
        }

        // Output the overall test result
        Console.WriteLine(pass ? "PASS: StripFNC behavior verified." : "FAIL: StripFNC behavior verification failed.");

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect the test outcome
        }
    }
}