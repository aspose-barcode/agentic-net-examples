// Title: Barcode Generation Round‑Trip XML Serialization Test
// Description: Demonstrates generating a barcode, exporting its configuration to XML, re‑importing it, and verifying that the resulting image matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the BarcodeGenerator class, XML export/import methods, and image comparison techniques. Developers often need to persist barcode settings, transfer them between services, or validate serialization integrity, making this pattern useful for unit testing and CI pipelines.
// Prompt: Write unit tests that compare generated barcode images before and after XML serialization round‑trip.
// Tags: barcode, symbology, xml-serialization, roundtrip, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the entry point and helper methods for running barcode round‑trip serialization tests.
/// </summary>
class Program
{
    /// <summary>
    /// Executes a series of barcode generation tests, comparing original and deserialized images.
    /// </summary>
    static void Main()
    {
        // Define test cases: each tuple contains the barcode symbology and the text to encode.
        var tests = new List<(BaseEncodeType Encode, string Text)>
        {
            (EncodeTypes.Code128, "Test123"),
            (EncodeTypes.QR, "https://example.com")
        };

        // Counters for passed and failed tests.
        int passed = 0;
        int failed = 0;

        // Iterate over each test case and run the round‑trip comparison.
        foreach (var (encode, text) in tests)
        {
            bool result = RunRoundTripTest(encode, text);
            if (result)
            {
                passed++;
                Console.WriteLine($"PASS: {encode.GetType().Name}.{encode} with text \"{text}\"");
            }
            else
            {
                failed++;
                Console.WriteLine($"FAIL: {encode.GetType().Name}.{encode} with text \"{text}\"");
            }
        }

        // Output a summary of the test results.
        Console.WriteLine($"Summary: {passed} passed, {failed} failed.");
    }

    /// <summary>
    /// Generates a barcode, serializes its configuration to XML, re‑imports it, and compares the resulting images.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>True if the original and re‑generated images are identical; otherwise, false.</returns>
    static bool RunRoundTripTest(BaseEncodeType encodeType, string codeText)
    {
        // Generate original barcode and capture image bytes.
        byte[] originalBytes;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            using (var msOriginal = new MemoryStream())
            {
                generator.Save(msOriginal, BarCodeImageFormat.Png);
                originalBytes = msOriginal.ToArray();
            }

            // Export generator state to XML.
            using (var msXml = new MemoryStream())
            {
                generator.ExportToXml(msXml);
                msXml.Position = 0;

                // Import generator from XML.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(msXml))
                {
                    // Generate barcode from imported generator and capture image bytes.
                    using (var msImported = new MemoryStream())
                    {
                        importedGenerator.Save(msImported, BarCodeImageFormat.Png);
                        byte[] importedBytes = msImported.ToArray();

                        // Compare image bytes for equality.
                        return originalBytes.SequenceEqual(importedBytes);
                    }
                }
            }
        }
    }
}