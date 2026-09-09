// Title: ECI Encoding Barcode Generation Test Across Cultures
// Description: Demonstrates generating barcodes with ECI encoding for different languages and verifying them under specific culture settings.
// Category-Description: This example belongs to the Aspose.BarCode culture‑aware barcode generation category. It shows how to configure ECI encoding for various symbologies (QR, DataMatrix, PDF417, DotCode) using the BarcodeGenerator class, save the image, and validate it with BarCodeReader. Developers often need to ensure correct character set handling when generating barcodes for international applications.
// Prompt: Create a test suite that verifies barcode generation across different cultures and regional settings for ECI encoding.
// Tags: barcode, eci, culture, localization, qrcode, datamatrix, pdf417, dotcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Text;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point and helper methods for running barcode generation tests with ECI encoding across multiple cultures.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Creates a temporary directory and runs a series of barcode generation tests for different symbologies and cultures.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test output files
        string tempDir = Path.Combine(Path.GetTempPath(), "ECITest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Run QR code test with Greek characters under Greek culture
        RunTest(
            "QR_Greek",
            EncodeTypes.QR,
            "ΑΒΓΔΕ", // Greek letters
            ECIEncodings.ISO_8859_7,
            (gen) =>
            {
                gen.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                gen.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.ISO_8859_7;
            },
            new CultureInfo("el-GR")
        );

        // Run DataMatrix test with Cyrillic characters under Russian culture
        RunTest(
            "DataMatrix_Cyrillic",
            EncodeTypes.DataMatrix,
            "Привет", // Cyrillic
            ECIEncodings.Win1251,
            (gen) =>
            {
                gen.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.ECI;
                gen.Parameters.Barcode.DataMatrix.ECIEncoding = ECIEncodings.Win1251;
            },
            new CultureInfo("ru-RU")
        );

        // Run PDF417 test with Greek characters under Greek culture
        RunTest(
            "Pdf417_Greek",
            EncodeTypes.Pdf417,
            "ΑΒΓΔΕ", // Greek letters
            ECIEncodings.ISO_8859_7,
            (gen) =>
            {
                gen.Parameters.Barcode.Pdf417.EncodeMode = Pdf417EncodeMode.ECI;
                gen.Parameters.Barcode.Pdf417.ECIEncoding = ECIEncodings.ISO_8859_7;
            },
            new CultureInfo("el-GR")
        );

        // Run DotCode test with Greek characters under Greek culture
        RunTest(
            "DotCode_Greek",
            EncodeTypes.DotCode,
            "ΑΒΓΔΕ", // Greek letters
            ECIEncodings.ISO_8859_7,
            (gen) =>
            {
                gen.Parameters.Barcode.DotCode.EncodeMode = DotCodeEncodeMode.ECI;
                gen.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.ISO_8859_7;
            },
            new CultureInfo("el-GR")
        );

        Console.WriteLine("All tests completed.");
    }

    /// <summary>
    /// Executes a single barcode generation and verification test.
    /// </summary>
    /// <param name="testName">Unique name for the test, used for file naming and logging.</param>
    /// <param name="encodeType">The barcode symbology to generate.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="eciEncoding">The ECI encoding to apply.</param>
    /// <param name="configure">Action that applies additional generator settings (e.g., enabling ECI mode).</param>
    /// <param name="culture">CultureInfo to set during the test to simulate regional settings.</param>
    static void RunTest(string testName, BaseEncodeType encodeType, string codeText, ECIEncodings eciEncoding, Action<BarcodeGenerator> configure, CultureInfo culture)
    {
        Console.WriteLine($"--- Running {testName} ---");

        // Preserve the original culture to restore later
        CultureInfo originalCulture = CultureInfo.CurrentCulture;

        try
        {
            // Apply the test-specific culture
            CultureInfo.CurrentCulture = culture;

            // Determine output file path for the generated barcode image
            string filePath = Path.Combine(Path.GetTempPath(), $"{testName}.png");

            // Generate the barcode with the specified settings
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                configure(generator);
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read and verify the generated barcode
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                reader.BarcodeSettings.DetectEncoding = true;
                var results = reader.ReadBarCodes();

                if (results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                {
                    Console.WriteLine($"Success: Detected symbology {results[0].CodeTypeName}, text length {results[0].CodeText.Length}");
                }
                else
                {
                    Console.WriteLine("Failure: No barcode detected or empty result.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during {testName}: {ex.Message}");
        }
        finally
        {
            // Restore the original culture regardless of test outcome
            CultureInfo.CurrentCulture = originalCulture;
        }
    }
}