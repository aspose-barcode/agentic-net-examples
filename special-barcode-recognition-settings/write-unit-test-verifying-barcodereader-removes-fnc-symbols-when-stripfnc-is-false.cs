// Title: Demonstrate StripFNC behavior in GS1 Code128 barcode reading
// Description: Shows how BarCodeReader handles FNC characters when StripFNC is false versus true, using a GS1 Code128 sample.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and BarcodeSettings to control FNC character stripping. Developers working with GS1 symbologies often need to preserve or remove FNC1 separators depending on downstream processing. The snippet demonstrates typical API usage for reading raw and stripped barcode data, useful for unit testing and integration scenarios.
// Prompt: Write a unit test verifying BarCodeReader removes FNC symbols when StripFNC is false.
// Tags: gs1code128, stripfnc, barcoderecognition, aspose.barcode, unit-test, fnc1

using System;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that reads a GS1 Code128 barcode and demonstrates the effect of the StripFNC setting.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image in memory and reads it twice: once with StripFNC disabled (default) and once with it enabled,
    /// printing verification results to the console.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code128 data containing multiple Application Identifier (AI) groups.
        string sourceCodeText = "(02)04006664241007(37)1(400)7019590754";

        // Create a barcode generator for GS1 Code128 and produce the image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sourceCodeText))
        using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
        {
            // ------------------------------------------------------------
            // Test case 1: StripFNC = false (default behavior)
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
            {
                // Explicitly ensure that FNC characters are NOT stripped.
                reader.BarcodeSettings.StripFNC = false;

                // Read the first barcode result from the image.
                BarCodeResult result = reader.ReadBarCodes().FirstOrDefault();

                // Verify that a result was obtained.
                if (result == null)
                {
                    Console.WriteLine("Failed to read barcode with StripFNC = false.");
                    return;
                }

                // Expected raw text includes FNC1 separators (ASCII 29, represented as \x1D).
                string expectedRaw = "\x1D04006664241007\x1D1\x1D7019590754";

                // Compare the actual CodeText with the expected raw string.
                bool rawMatches = result.CodeText == expectedRaw;
                Console.WriteLine($"StripFNC = false => CodeText matches expected: {rawMatches}");

                // Output detailed mismatch information if the comparison fails.
                if (!rawMatches)
                {
                    Console.WriteLine($"Actual:   [{result.CodeText}]");
                    Console.WriteLine($"Expected: [{expectedRaw}]");
                }
            }

            // ------------------------------------------------------------
            // Test case 2: StripFNC = true
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(barcodeImage, DecodeType.Code128))
            {
                // Enable stripping of FNC characters.
                reader.BarcodeSettings.StripFNC = true;

                // Read the first barcode result from the image.
                BarCodeResult result = reader.ReadBarCodes().FirstOrDefault();

                // Verify that a result was obtained.
                if (result == null)
                {
                    Console.WriteLine("Failed to read barcode with StripFNC = true.");
                    return;
                }

                // Expected text after stripping FNC1 separators: concatenated data without delimiters.
                string expectedStripped = "0400666424100717019590754";

                // Compare the actual CodeText with the expected stripped string.
                bool strippedMatches = result.CodeText == expectedStripped;
                Console.WriteLine($"StripFNC = true => CodeText matches expected: {strippedMatches}");

                // Output detailed mismatch information if the comparison fails.
                if (!strippedMatches)
                {
                    Console.WriteLine($"Actual:   [{result.CodeText}]");
                    Console.WriteLine($"Expected: [{expectedStripped}]");
                }
            }
        }
    }
}