// Title: Verify StripFNC behavior for QR codes with FNC1 symbols
// Description: Demonstrates generating a QR code containing FNC1 characters, then reading it with and without stripping FNC symbols to confirm the StripFNC setting works.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the use of BarcodeGenerator, QrExtCodetextBuilder, and BarCodeReader. It illustrates typical scenarios where developers need to preserve or remove function characters (FNC) in QR codes, such as GS1 data handling, and how to validate the StripFNC property during decoding.
// Prompt: Write a unit test verifying BarCodeReader retains FNC symbols when StripFNC is true.
// Tags: qr, fnc1, stripfnc, barcode generation, barcode recognition, aspose.barcode, unit test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code with FNC1 characters and verifying the StripFNC setting of BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode, reads it with different StripFNC settings, and validates the results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest");
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "qr_fnc.png");

        // Build QR code text containing FNC1 characters using the builder
        QrExtCodetextBuilder builder = new QrExtCodetextBuilder();
        builder.AddFNC1FirstPosition();                     // FNC1 at first position
        builder.AddPlainCodetext("DATA");                   // regular data
        builder.AddFNC1SecondPosition("12");                // FNC1 with value "12"
        builder.AddPlainCodetext("MORE");                   // more data
        string extendedText = builder.GetExtendedCodetext();

        // Generate QR barcode with Extended mode (supports FNC1)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = extendedText;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Extended;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Read barcode without stripping FNC characters (StripFNC = false)
        string codeTextWithoutStrip;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.StripFNC = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("FAILED: No barcode detected (StripFNC = false).");
                return;
            }
            codeTextWithoutStrip = results[0].CodeText;
        }

        // Read barcode with stripping FNC characters (StripFNC = true)
        string codeTextWithStrip;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.StripFNC = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("FAILED: No barcode detected (StripFNC = true).");
                return;
            }
            codeTextWithStrip = results[0].CodeText;
        }

        // Simple verification: the texts should differ and the stripped version should be shorter
        bool testPassed = !string.Equals(codeTextWithoutStrip, codeTextWithStrip) &&
                          codeTextWithStrip.Length < codeTextWithoutStrip.Length;

        if (testPassed)
        {
            Console.WriteLine("PASSED: StripFNC works as expected.");
            Console.WriteLine($"Original CodeText: {codeTextWithoutStrip}");
            Console.WriteLine($"Stripped CodeText: {codeTextWithStrip}");
        }
        else
        {
            Console.WriteLine("FAILED: StripFNC did not modify the CodeText as expected.");
            Console.WriteLine($"Original CodeText: {codeTextWithoutStrip}");
            Console.WriteLine($"Stripped CodeText: {codeTextWithStrip}");
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup is best‑effort
        }
    }
}