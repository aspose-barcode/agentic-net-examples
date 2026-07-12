// Title: Verify BarCodeReader retains FNC symbols when StripFNC is true
// Description: Demonstrates a unit‑test‑style verification that the BarCodeReader keeps the FNC1 character when StripFNC is disabled and removes it when enabled.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading category, focusing on GS1‑128 symbology and the StripFNC setting. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings to control FNC character handling—common tasks for developers integrating barcode validation or data extraction.
// Prompt: Write a unit test verifying BarCodeReader retains FNC symbols when StripFNC is true.
// Tags: barcode symbology, gs1-128, stripfnc, unit test, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the example that validates the StripFNC behavior of <see cref="BarCodeReader"/> for GS1‑128 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a GS1‑128 barcode, reads it twice (with StripFNC disabled and enabled),
    /// and verifies that the FNC1 character is retained or removed as expected.
    /// </summary>
    static void Main()
    {
        // Sample GS1‑128 code text containing Application Identifier (AI) sections.
        const string sampleCodeText = "(02)04006664241007(37)1(400)7019590754";

        // Generate the barcode image into a memory stream to avoid file I/O.
        using (var imageStream = new MemoryStream())
        {
            // Create a generator for GS1‑128 and save the image as PNG.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sampleCodeText))
            {
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position so it can be read from the beginning.
            imageStream.Position = 0;

            // Test with StripFNC disabled (the FNC1 character should be retained).
            bool stripFncDisabledResult = TestStripFnc(imageStream, false, out string decodedWithoutStrip);
            // Reset stream position for the next read.
            imageStream.Position = 0;

            // Test with StripFNC enabled (the FNC1 character should be removed).
            bool stripFncEnabledResult = TestStripFnc(imageStream, true, out string decodedWithStrip);
            // Reset stream position for any further use.
            imageStream.Position = 0;

            int passed = 0, failed = 0;

            // The FNC1 character is represented by ASCII 29 (Group Separator).
            const char fnc1Char = '\u001D';

            // Verify that the result without stripping contains the FNC1 character.
            if (decodedWithoutStrip != null && decodedWithoutStrip.IndexOf(fnc1Char) >= 0)
                passed++;
            else
                failed++;

            // Verify that the result with stripping does NOT contain the FNC1 character.
            if (decodedWithStrip != null && decodedWithStrip.IndexOf(fnc1Char) < 0)
                passed++;
            else
                failed++;

            // Output the test summary and the decoded strings.
            Console.WriteLine($"Test results: {passed} passed, {failed} failed.");
            Console.WriteLine($"Decoded without StripFNC: \"{decodedWithoutStrip}\"");
            Console.WriteLine($"Decoded with StripFNC:    \"{decodedWithStrip}\"");
        }
    }

    /// <summary>
    /// Reads a barcode from the provided stream using the specified <c>StripFNC</c> setting.
    /// </summary>
    /// <param name="imageStream">Stream containing the barcode image.</param>
    /// <param name="stripFnc">If true, the reader will remove FNC characters from the result.</param>
    /// <param name="codeText">Outputs the decoded barcode text when reading succeeds.</param>
    /// <returns>True if a barcode was successfully read; otherwise, false.</returns>
    static bool TestStripFnc(Stream imageStream, bool stripFnc, out string codeText)
    {
        codeText = null;
        // Initialize the reader for GS1‑128 symbology.
        using (var reader = new BarCodeReader(imageStream, DecodeType.GS1Code128))
        {
            // Apply the StripFNC setting.
            reader.BarcodeSettings.StripFNC = stripFnc;

            // Iterate over detected barcodes (there should be only one in this example).
            foreach (var result in reader.ReadBarCodes())
            {
                codeText = result.CodeText;
                return true;
            }
        }
        return false;
    }
}