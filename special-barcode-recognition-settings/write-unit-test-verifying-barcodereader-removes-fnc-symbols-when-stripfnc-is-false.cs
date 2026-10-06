// Title: Verify StripFNC behavior of BarCodeReader with FNC symbols
// Description: Demonstrates generating a Code128 barcode containing FNC characters, reading it with StripFNC set to false and true, and confirming the FNC symbols are retained or removed accordingly.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to embed FNC symbols, BarCodeReader to decode barcodes, and the BarcodeSettings.StripFNC property to control whether FNC characters are stripped. Developers working with barcode symbology often need to validate FNC handling for compliance with standards or custom processing pipelines.
// Prompt: Write a unit test verifying BarCodeReader removes FNC symbols when StripFNC is false.
// Tags: barcode, fnc, stripfnc, code128, aspose.barcode, generation, recognition, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates verification of the StripFNC setting in BarCodeReader by generating a Code128 barcode
/// with FNC symbols and reading it with different StripFNC configurations.
/// </summary>
class Program
{
    // Approximate FNC character codes used by Aspose.BarCode
    private const char FNC1 = (char)200;
    private const char FNC2 = (char)201;
    private const char FNC3 = (char)202;

    /// <summary>
    /// Executes the demonstration and validation logic.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarCodeFncTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "Code128FNC.png");

        // Generate a Code128 barcode that includes FNC symbols
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, "Aspose" + FNC1 + FNC2 + FNC3))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            gen.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Read the barcode with StripFNC = false (FNC symbols should be present in the result)
        string codeTextStripFalse;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("FAILED: No barcode detected with StripFNC = false.");
                return;
            }
            codeTextStripFalse = results[0].CodeText;
        }

        // Read the same barcode with StripFNC = true (FNC symbols should be removed)
        string codeTextStripTrue;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("FAILED: No barcode detected with StripFNC = true.");
                return;
            }
            codeTextStripTrue = results[0].CodeText;
        }

        // Determine whether the false setting retained any FNC placeholders
        bool containsFnc = codeTextStripFalse.Contains("<FNC1>") ||
                           codeTextStripFalse.Contains("<FNC2>") ||
                           codeTextStripFalse.Contains("<FNC3>");

        // Determine whether the true setting stripped all FNC placeholders
        bool stripped = !codeTextStripTrue.Contains("<FNC");

        // Output the verification result
        if (containsFnc && stripped)
        {
            Console.WriteLine("PASSED: StripFNC behavior verified.");
        }
        else
        {
            Console.WriteLine("FAILED: StripFNC behavior not as expected.");
            Console.WriteLine($"StripFNC false result: {codeTextStripFalse}");
            Console.WriteLine($"StripFNC true result: {codeTextStripTrue}");
        }

        // Cleanup temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}