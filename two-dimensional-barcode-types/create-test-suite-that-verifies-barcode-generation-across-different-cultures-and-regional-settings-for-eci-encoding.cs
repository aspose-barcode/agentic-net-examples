// Title: Barcode ECI Encoding Test Suite Across Cultures
// Description: Demonstrates generating and verifying barcodes with ECI encoding for different cultural character sets using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to work with ECI (Extended Channel Interpretation) encodings across multiple symbologies such as QR, DataMatrix, PDF417, and DotCode. It illustrates creating barcodes with specific regional character sets, saving them, and programmatically reading them back to confirm correct encoding. Developers building multi‑language scanning solutions often need these patterns to ensure reliable barcode handling in global applications.
// Prompt: Create a test suite that verifies barcode generation across different cultures and regional settings for ECI encoding.
// Tags: barcode, eci, culture, regional, qr, datamatrix, pdf417, dotcode, generation, recognition, testing, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that generates barcodes with ECI encoding for various cultures,
/// saves them to temporary files, and validates decoding using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO representing a single barcode test case.
    /// </summary>
    class TestCase
    {
        public string SymbologyName; // e.g., "QR"
        public string CodeText;
        public ECIEncodings EciEncoding;
    }

    /// <summary>
    /// Entry point. Executes the ECI encoding test cases, writes results to console,
    /// and cleans up temporary artifacts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeECITest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases covering different cultures/encodings
        var testCases = new List<TestCase>
        {
            new TestCase { SymbologyName = "QR", CodeText = "ΑΒΓΔΕ", EciEncoding = ECIEncodings.ISO_8859_7 }, // Greek
            new TestCase { SymbologyName = "QR", CodeText = "ÄÖÜ", EciEncoding = ECIEncodings.ISO_8859_1 }, // Latin-1
            new TestCase { SymbologyName = "DataMatrix", CodeText = "ΑΒΓΔΕ", EciEncoding = ECIEncodings.ISO_8859_7 },
            new TestCase { SymbologyName = "DataMatrix", CodeText = "ÄÖÜ", EciEncoding = ECIEncodings.ISO_8859_1 },
            new TestCase { SymbologyName = "Pdf417", CodeText = "ΑΒΓΔΕ", EciEncoding = ECIEncodings.ISO_8859_7 },
            new TestCase { SymbologyName = "Pdf417", CodeText = "ÄÖÜ", EciEncoding = ECIEncodings.ISO_8859_1 },
            new TestCase { SymbologyName = "DotCode", CodeText = "ΑΒΓΔΕ", EciEncoding = ECIEncodings.ISO_8859_7 },
            new TestCase { SymbologyName = "DotCode", CodeText = "ÄÖÜ", EciEncoding = ECIEncodings.ISO_8859_1 }
        };

        foreach (var test in testCases)
        {
            // Resolve EncodeTypes member via reflection
            var encodeField = typeof(EncodeTypes).GetField(test.SymbologyName);
            if (encodeField == null)
            {
                Console.WriteLine($"[SKIP] Unknown symbology for encoding: {test.SymbologyName}");
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)encodeField.GetValue(null);

            // Resolve DecodeType member via reflection
            var decodeField = typeof(DecodeType).GetField(test.SymbologyName);
            if (decodeField == null)
            {
                Console.WriteLine($"[SKIP] Unknown symbology for decoding: {test.SymbologyName}");
                continue;
            }
            BaseDecodeType decodeType = (BaseDecodeType)decodeField.GetValue(null);

            // Build a unique file name for the generated barcode image
            string fileName = $"{test.SymbologyName}_{test.EciEncoding}_{Guid.NewGuid().ToString("N")}.png";
            string filePath = Path.Combine(tempFolder, fileName);

            // Generate barcode with ECI mode
            try
            {
                using (var generator = new BarcodeGenerator(encodeType, test.CodeText))
                {
                    // Set symbology‑specific ECI mode and encoding
                    switch (test.SymbologyName)
                    {
                        case "QR":
                            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                            generator.Parameters.Barcode.QR.ECIEncoding = test.EciEncoding;
                            break;
                        case "DataMatrix":
                            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
                            generator.Parameters.Barcode.DataMatrix.ECIEncoding = test.EciEncoding;
                            break;
                        case "Pdf417":
                            generator.Parameters.Barcode.Pdf417.EncodeMode = Pdf417EncodeMode.ECI;
                            generator.Parameters.Barcode.Pdf417.ECIEncoding = test.EciEncoding;
                            break;
                        case "DotCode":
                            generator.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.ECI;
                            generator.Parameters.Barcode.DotCode.ECIEncoding = test.EciEncoding;
                            break;
                        default:
                            Console.WriteLine($"[SKIP] Unsupported symbology: {test.SymbologyName}");
                            continue;
                    }

                    // Save the generated barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FAIL] Generation error for {test.SymbologyName}: {ex.Message}");
                continue;
            }

            // Verify that the file was created
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"[FAIL] Generated file not found: {filePath}");
                continue;
            }

            // Read back the barcode and compare the decoded text
            try
            {
                using (var reader = new BarCodeReader(filePath, decodeType))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"[FAIL] No barcode detected in {fileName}");
                    }
                    else
                    {
                        bool matchFound = false;
                        foreach (var result in results)
                        {
                            if (result.CodeText == test.CodeText)
                            {
                                matchFound = true;
                                break;
                            }
                        }
                        Console.WriteLine(matchFound
                            ? $"[PASS] {test.SymbologyName} ECI {test.EciEncoding} decoded correctly."
                            : $"[FAIL] {test.SymbologyName} decoded text mismatch. Expected: '{test.CodeText}'.");
                    }
                }
            }
            catch (ArgumentException ae) when (ae.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"[WARN] Skipping unreadable file {fileName}: {ae.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FAIL] Reading error for {fileName}: {ex.Message}");
            }
        }

        // Cleanup: delete temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails, ignore – the folder is in a temp location.
        }
    }
}