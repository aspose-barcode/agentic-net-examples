// Title: Barcode Generation and Verification Test Suite
// Description: Demonstrates generating barcodes of various symbologies, saving them as PNG, and verifying the encoded text using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeReader to create and validate barcodes. Typical use cases include automated testing, CI pipelines, and cross‑platform validation of barcode output across .NET Framework, .NET Core, and .NET 6. Developers often need to ensure consistent barcode rendering and decoding in different runtime environments.
// Prompt: Create a test suite that validates barcode generation across .NET Framework, .NET Core, and .NET 6 runtimes.
// Tags: barcode generation, barcode verification, aspnet, aspnetcore, .net framework, .net core, .net 6, encode types, decode types, png output, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides a simple test suite that generates barcodes of several symbologies,
/// saves them as PNG files, and verifies that the decoded text matches the original input.
/// This example can be executed on .NET Framework, .NET Core, and .NET 6 runtimes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test suite. Creates a temporary working folder,
    /// runs generation/verification for each test case, reports results,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test run
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases: symbology name, code text, and corresponding EncodeTypes field name
        var testCases = new (string SymbologyName, string CodeText, string EncodeField)[]
        {
            ("Code128", "ABC123456", "Code128"),
            ("QR", "https://example.com", "QR"),
            ("DataMatrix", "DM12345", "DataMatrix"),
            ("Pdf417", "PDF417_SAMPLE", "Pdf417"),
            ("Aztec", "AZTEC123", "Aztec")
        };

        int passed = 0;
        int failed = 0;

        // Iterate over each test case, generate the barcode, and verify it
        foreach (var (symName, codeText, encodeField) in testCases)
        {
            // Resolve EncodeTypes field via reflection
            var fieldInfo = typeof(EncodeTypes).GetField(encodeField);
            if (fieldInfo == null)
            {
                Console.WriteLine($"[WARN] Unknown symbology field: {encodeField}");
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

            // Build a unique file path for the generated image
            string filePath = Path.Combine(tempFolder, $"{symName}_{Guid.NewGuid().ToString("N")}.png");

            // Generate barcode image
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Basic barcode parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Human‑readable text styling
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Save the barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Read and verify the generated barcode
            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    bool matchFound = false;
                    foreach (var result in results)
                    {
                        if (result.CodeText == codeText)
                        {
                            matchFound = true;
                            break;
                        }
                    }

                    if (matchFound)
                    {
                        Console.WriteLine($"[PASS] {symName} - CodeText matched.");
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine($"[FAIL] {symName} - Decoded text does not match. Expected: '{codeText}'.");
                        failed++;
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"[WARN] {symName} - Image could not be loaded: {ex.Message}");
                failed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {symName} - Unexpected exception: {ex.Message}");
                failed++;
            }
        }

        // Output summary of test results
        Console.WriteLine($"Test summary: Passed = {passed}, Failed = {failed}");

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}