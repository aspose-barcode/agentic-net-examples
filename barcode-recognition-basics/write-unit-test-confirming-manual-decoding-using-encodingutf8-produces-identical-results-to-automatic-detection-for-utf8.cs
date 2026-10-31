// Title: Unit test for UTF-8 barcode decoding consistency
// Description: Demonstrates generating a QR code with UTF‑8 text, then decoding it automatically and manually to verify both approaches yield identical results.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create QR codes, BarCodeReader with DetectEncoding settings, and Encoding.UTF8 for manual text extraction—common tasks when validating barcode data integrity in automated tests.
// Prompt: Write a unit test confirming manual decoding using Encoding.UTF8 produces identical results to automatic detection for UTF8 barcodes.
// Tags: qr,utf-8,encoding,barcode,recognition,generation,unit-test,aspose.barcode

using System;
using System.IO;
using System.Text;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code containing UTF‑8 text,
/// then reads the barcode using both automatic encoding detection and
/// manual UTF‑8 decoding to confirm the results are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, decoding,
    /// comparison, and cleanup steps.
    /// </summary>
    static void Main()
    {
        // Prepare test data
        string testText = "Aspose常に先を行";
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "qr.png");

        // Generate QR code with manual UTF‑8 encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 8;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
            generator.SetCodeText(testText, Encoding.UTF8);
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // Read with automatic encoding detection
        string autoDecoded = null;
        using (var readerAuto = new BarCodeReader(imagePath, DecodeType.QR))
        {
            readerAuto.BarcodeSettings.DetectEncoding = true;
            var result = readerAuto.ReadBarCodes().FirstOrDefault();
            if (result != null)
                autoDecoded = result.CodeText;
        }

        // Read with manual decoding (DetectEncoding = false)
        string manualDecoded = null;
        using (var readerManual = new BarCodeReader(imagePath, DecodeType.QR))
        {
            readerManual.BarcodeSettings.DetectEncoding = false;
            var result = readerManual.ReadBarCodes().FirstOrDefault();
            if (result != null)
                manualDecoded = result.GetCodeText(Encoding.UTF8);
        }

        // Compare the two decoding results
        if (autoDecoded == null || manualDecoded == null)
        {
            Console.WriteLine("FAILED: One of the decodings returned null.");
        }
        else if (autoDecoded == manualDecoded)
        {
            Console.WriteLine("PASSED: Automatic and manual UTF8 decoding produce identical results.");
        }
        else
        {
            Console.WriteLine("FAILED: Decoding results differ.");
            Console.WriteLine($"Automatic: {autoDecoded}");
            Console.WriteLine($"Manual   : {manualDecoded}");
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }
    }
}