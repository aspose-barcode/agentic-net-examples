using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a GS1 Code128 barcode with FNC characters,
/// then reading it with and without stripping those characters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it twice (once with StripFNC disabled,
    /// once with StripFNC enabled) and prints the results.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Code128 text containing FNC characters.
        // Parentheses denote Application Identifier (AI) sections.
        const string codeText = "(02)04006664241007(37)1(400)7019590754";

        // Create a memory stream to hold the generated barcode image.
        using (var ms = new MemoryStream())
        {
            // Generate the barcode and save it as PNG into the memory stream.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            ms.Position = 0;

            // -----------------------------------------------------------------
            // Read the barcode with StripFNC disabled (default behavior).
            // This provides a baseline comparison where FNC characters are not stripped.
            // -----------------------------------------------------------------
            using (var readerNoStrip = new BarCodeReader(ms, DecodeType.Code128))
            {
                // No need to modify StripFNC; it defaults to false.
                var resultsNoStrip = readerNoStrip.ReadBarCodes();

                foreach (var result in resultsNoStrip)
                {
                    Console.WriteLine($"StripFNC = false => CodeText: {result.CodeText}");
                }
            }

            // Reset the stream again for the second read operation.
            ms.Position = 0;

            // -----------------------------------------------------------------
            // Read the same barcode with StripFNC enabled.
            // This should retain the FNC symbols (parentheses) in the decoded text.
            // -----------------------------------------------------------------
            using (var readerStrip = new BarCodeReader(ms, DecodeType.Code128))
            {
                // Enable stripping of FNC characters.
                readerStrip.BarcodeSettings.StripFNC = true;

                var resultsStrip = readerStrip.ReadBarCodes();

                foreach (var result in resultsStrip)
                {
                    Console.WriteLine($"StripFNC = true => CodeText: {result.CodeText}");

                    // Verify that FNC symbols (parentheses) are still present when StripFNC is true.
                    bool containsFnc = result.CodeText.Contains("(") && result.CodeText.Contains(")");
                    if (containsFnc)
                    {
                        Console.WriteLine("PASS: FNC symbols are retained when StripFNC is true.");
                    }
                    else
                    {
                        Console.WriteLine("FAIL: FNC symbols were stripped when StripFNC is true.");
                    }
                }
            }
        }
    }
}