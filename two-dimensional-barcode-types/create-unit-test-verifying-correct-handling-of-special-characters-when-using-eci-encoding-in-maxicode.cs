// Title: Unit test for ECI encoding handling in MaxiCode
// Description: Demonstrates generating a MaxiCode barcode with UTF-8 ECI encoding containing special characters and verifies correct decoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on MaxiCode symbology with ECI (Extended Channel Interpretation) support. It showcases using BarcodeGenerator, setting MaxiCode parameters, saving as PNG, and reading back with BarCodeReader. Developers often need to ensure special characters are correctly encoded and decoded in high‑density barcodes.
// Prompt: Create unit test verifying correct handling of special characters when using ECI encoding in MaxiCode.
// Tags: maxicode, eci encoding, png, barcodegenerator, barcodereader, barcoderesult

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and validating a MaxiCode barcode with UTF-8 ECI encoding containing special characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, reads it back, and reports pass/fail.
    /// </summary>
    static void Main()
    {
        // Original text containing special characters (accented and non‑Latin characters)
        string originalText = "Café 漢字";

        // Temporary file path for the generated barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "maxicode_test.png");

        // Generate MaxiCode barcode with ECI UTF-8 encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
        {
            // Configure ECI encoding to UTF-8 and enable ECI mode for MaxiCode
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;
            generator.Parameters.Barcode.MaxiCode.MaxiCodeEncodeMode = MaxiCodeEncodeMode.ECI;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Flag indicating whether the decoded text matches the original
        bool testPassed = false;

        // Read the barcode back from the image and verify the decoded text
        using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string decodedText = result.CodeText;
                if (decodedText == originalText)
                {
                    testPassed = true;
                }
                else
                {
                    Console.WriteLine($"FAIL: Decoded text does not match. Expected '{originalText}', got '{decodedText}'.");
                }
            }
        }

        // Output the overall test result
        if (!testPassed)
        {
            Console.WriteLine("FAIL: No barcode was read or text mismatch.");
        }
        else
        {
            Console.WriteLine("PASS: Decoded text matches original.");
        }

        // Clean up the temporary image file
        try
        {
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}