// Title: Barcode Generation and Validation Test Suite
// Description: Generates barcodes for multiple symbologies, saves them as PNG files, and validates their readability using Aspose.BarCode's recognition engine.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to create barcodes with BarcodeGenerator, configure common and symbology‑specific parameters, save images, and verify them with BarCodeReader. Typical use cases include automated testing of barcode output across .NET Framework, .NET Core, and .NET 6 environments, ensuring consistent encoding and decoding behavior for developers building cross‑platform barcode solutions.
// Prompt: Create a test suite that validates barcode generation across .NET Framework, .NET Core, and .NET 6 runtimes.
// Tags: barcode, symbology, generation, recognition, testing, aspose.barcode, png, .net, .netframework, .netcore, .net6

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.BarCode;

/// <summary>
/// Demonstrates a cross‑platform test suite that generates barcodes, saves them as PNG images,
/// and validates them using Aspose.BarCode's recognition capabilities.
/// </summary>
class Program
{
    /// <summary>
    /// Retrieves the <see cref="BaseEncodeType"/> corresponding to a symbology name.
    /// Returns <c>null</c> if the symbology is not supported.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <returns>The matching <see cref="BaseEncodeType"/> or <c>null</c>.</returns>
    static BaseEncodeType GetEncodeType(string symbologyName)
    {
        // Use reflection to map the string name to the EncodeTypes field.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return null;
        }
        return (BaseEncodeType)field.GetValue(null);
    }

    /// <summary>
    /// Entry point of the test suite. Generates barcodes for a set of test cases,
    /// saves them, reads them back for validation, and reports the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases: each tuple contains a symbology name and the data to encode.
        var testCases = new List<(string Symbology, string CodeText)>
        {
            ("Code128", "Test123"),
            ("QR", "https://example.com"),
            ("DataMatrix", "DMTest"),
            ("DatabarExpanded", "(01)01234567890123"),
            ("AustraliaPost", "1100000000"),
            ("GS1CompositeBar", "(01)01234567890123|(21)A12345678")
        };

        int passed = 0;
        int failed = 0;
        int index = 0;

        // Iterate over each test case, generate the barcode, and validate it.
        foreach (var (symbology, codeText) in testCases)
        {
            index++;
            BaseEncodeType encodeType = GetEncodeType(symbology);
            if (encodeType == null)
            {
                Console.WriteLine($"Skipping test {index}: unsupported symbology {symbology}");
                failed++;
                continue;
            }

            // Build the output file path for the generated PNG image.
            string filePath = Path.Combine(tempFolder, $"{symbology}_{index}.png");

            try
            {
                // Generate the barcode with common and symbology‑specific settings.
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Common parameters
                    generator.Parameters.Resolution = 300f;
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
                    generator.Parameters.ImageWidth.Pixels = 300f;

                    // Symbology‑specific settings
                    switch (symbology)
                    {
                        case "QR":
                            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                            break;
                        case "DataMatrix":
                            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
                            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
                            break;
                        case "AustraliaPost":
                            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                            break;
                        case "GS1CompositeBar":
                            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                            generator.Parameters.Barcode.Pdf417.Columns = 30;
                            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
                            break;
                        default:
                            // No extra configuration needed for other symbologies.
                            break;
                    }

                    // Save the generated barcode as a PNG file.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // Read and validate the generated barcode.
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    var results = reader.ReadBarCodes();

                    bool success = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                    if (success)
                    {
                        passed++;
                        Console.WriteLine($"Test {index} [{symbology}] succeeded. Detected: {results[0].CodeText} ({results[0].CodeTypeName})");
                    }
                    else
                    {
                        failed++;
                        Console.WriteLine($"Test {index} [{symbology}] failed: no readable result.");
                    }
                }
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"Test {index} [{symbology}] exception: {ex.Message}");
            }
        }

        // Output a summary of the test run.
        Console.WriteLine($"Summary: {passed} passed, {failed} failed.");

        // Cleanup the temporary folder; ignore any errors during deletion.
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress cleanup exceptions.
        }
    }
}