// Title: Barcode XML Serialization Round‑Trip Validation
// Description: Generates barcodes, serializes the generator to XML, deserializes it, and verifies that the resulting image matches the original.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It demonstrates how to use BarcodeGenerator to create various symbologies, export the generator's state to XML, import it back, and compare the output images. Developers often need to persist barcode settings, exchange them between services, or ensure that serialization does not alter the visual result. The key API classes shown are BarcodeGenerator, BaseEncodeType, and BarCodeImageFormat, useful for unit testing and integration scenarios.
// Prompt: Write unit tests that compare generated barcode images before and after XML serialization round‑trip.
// Tags: barcode, serialization, xml, roundtrip, image comparison, aspose.barcode, qrcode, code128, datamatrix, pdf417, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, XML export/import, and image comparison to validate a round‑trip serialization.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs a series of round‑trip tests for different barcode symbologies.
    /// </summary>
    static void Main()
    {
        // Counters for test results
        int totalTests = 0;
        int failedTests = 0;

        // Define test cases: each tuple contains the barcode type and the text to encode
        var testCases = new (BaseEncodeType encodeType, string codeText)[]
        {
            (EncodeTypes.QR, "Hello World"),
            (EncodeTypes.Code128, "1234567890"),
            (EncodeTypes.DataMatrix, "Aspose"),
            (EncodeTypes.Pdf417, "Sample PDF417 Text")
        };

        // Execute each test case
        foreach (var (encodeType, codeText) in testCases)
        {
            totalTests++;
            bool passed = RunRoundTripTest(encodeType, codeText);
            if (!passed)
            {
                failedTests++;
                Console.WriteLine($"FAILED: EncodeType={encodeType}, CodeText=\"{codeText}\"");
            }
            else
            {
                Console.WriteLine($"PASSED: EncodeType={encodeType}, CodeText=\"{codeText}\"");
            }
        }

        // Summary of results
        Console.WriteLine($"TOTAL: {totalTests}, FAILED: {failedTests}");
    }

    /// <summary>
    /// Generates a barcode, exports the generator to XML, re‑imports it, and compares the two PNG images.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>True if the images are identical; otherwise, false.</returns>
    static bool RunRoundTripTest(BaseEncodeType encodeType, string codeText)
    {
        byte[] originalImageBytes;
        byte[] roundTripImageBytes;

        // Generate the original barcode and capture its PNG bytes
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            using (var originalStream = new MemoryStream())
            {
                generator.Save(originalStream, BarCodeImageFormat.Png);
                originalImageBytes = originalStream.ToArray();
            }

            // Export the generator's configuration to an XML stream
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for reading

                // Import a new generator instance from the XML data
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Generate the barcode from the imported configuration
                    using (var roundTripStream = new MemoryStream())
                    {
                        importedGenerator.Save(roundTripStream, BarCodeImageFormat.Png);
                        roundTripImageBytes = roundTripStream.ToArray();
                    }
                }
            }
        }

        // Compare the two image byte arrays for equality
        return CompareByteArrays(originalImageBytes, roundTripImageBytes);
    }

    /// <summary>
    /// Performs a byte‑wise comparison of two arrays.
    /// </summary>
    /// <param name="a">First byte array.</param>
    /// <param name="b">Second byte array.</param>
    /// <returns>True if arrays are non‑null, same length, and contain identical bytes.</returns>
    static bool CompareByteArrays(byte[] a, byte[] b)
    {
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}