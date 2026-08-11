// Title: BarCodeReader StripFNC behavior verification example
// Description: Demonstrates how to use Aspose.BarCode to read a GS1 Code128 barcode and verify the StripFNC setting retains or removes FNC symbols.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and BarcodeGenerator for GS1 symbologies. It shows how to configure BarcodeSettings.StripFNC to control the handling of Function (FNC) characters, a common requirement when processing GS1 data streams. Developers often need to toggle this setting to preserve AI delimiters or produce clean numeric strings.
// Prompt: Write a unit test verifying BarCodeReader removes FNC symbols when StripFNC is false.
// Tags: barcode, gs1code128, stripfnc, fnc-symbols, barcode-recognition, aspose.barcode, unit-test

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the entry point and verification logic for testing the StripFNC behavior of BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Executes the StripFNC verification routine.
    /// </summary>
    static void Main()
    {
        // Run the verification test
        VerifyStripFncBehavior();
    }

    static void VerifyStripFncBehavior()
    {
        // GS1 Code128 barcode with FNC (parentheses represent AI delimiters)
        const string originalCodeText = "(02)04006664241007(37)1(400)7019590754";

        // Generate the barcode image in memory
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, originalCodeText))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Ensure the stream is ready for reading
            ms.Position = 0;

            // Test 1: StripFNC = false (should retain the original text)
            using (var reader = new BarCodeReader(ms, DecodeType.GS1Code128))
            {
                reader.BarcodeSettings.StripFNC = false;
                var result = reader.ReadBarCodes().FirstOrDefault();
                string readText = result?.CodeText ?? string.Empty;

                bool pass = readText == originalCodeText;
                Console.WriteLine(pass
                    ? "PASS: StripFNC = false retains FNC symbols."
                    : $"FAIL: StripFNC = false altered code text. Expected '{originalCodeText}', got '{readText}'.");
            }

            // Reset stream position for the second read
            ms.Position = 0;

            // Test 2: StripFNC = true (should remove the parentheses)
            using (var reader = new BarCodeReader(ms, DecodeType.GS1Code128))
            {
                reader.BarcodeSettings.StripFNC = true;
                var result = reader.ReadBarCodes().FirstOrDefault();
                string readText = result?.CodeText ?? string.Empty;

                // Expected text without parentheses
                string expectedStripped = originalCodeText.Replace("(", string.Empty).Replace(")", string.Empty);
                bool pass = readText == expectedStripped;
                Console.WriteLine(pass
                    ? "PASS: StripFNC = true correctly strips FNC symbols."
                    : $"FAIL: StripFNC = true did not strip correctly. Expected '{expectedStripped}', got '{readText}'.");
            }
        }
    }
}