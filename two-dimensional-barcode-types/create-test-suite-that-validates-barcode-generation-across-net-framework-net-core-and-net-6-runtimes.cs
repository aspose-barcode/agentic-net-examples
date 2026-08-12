// Title: Barcode Generation and Validation Test Suite
// Description: Demonstrates generating barcodes of various symbologies, saving them as PNG files, and verifying the encoded data by reading the images.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, configure symbology‑specific options, and employ BarCodeReader to validate the output. Typical use cases include automated testing, CI pipelines, and cross‑platform verification of barcode rendering across .NET Framework, .NET Core, and .NET 6.
// Prompt: Create a test suite that validates barcode generation across .NET Framework, .NET Core, and .NET 6 runtimes.
// Tags: barcode generation, barcode recognition, code128, qr, datamatrix, australiapost, aspose.barcode, .net framework, .net core, .net 6, png output

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a console‑based test suite that generates barcodes, saves them as PNG files,
/// and validates the encoded data using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the barcode generation tests, reports results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test run
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define test cases for different symbologies
        var tests = new List<BarcodeTest>
        {
            new BarcodeTest
            {
                Symbology = EncodeTypes.Code128,
                CodeText = "Test123",
                Decode = DecodeType.Code128,
                FileName = "code128.png"
            },
            new BarcodeTest
            {
                Symbology = EncodeTypes.QR,
                CodeText = "https://example.com",
                Decode = DecodeType.QR,
                FileName = "qr.png"
            },
            new BarcodeTest
            {
                Symbology = EncodeTypes.DataMatrix,
                CodeText = "DataMatrixSample",
                Decode = DecodeType.DataMatrix,
                FileName = "datamatrix.png"
            },
            new BarcodeTest
            {
                Symbology = EncodeTypes.AustraliaPost,
                // FCC=59, DPID=12345678, customer info "AB" (CTable, max 5 chars)
                CodeText = "5912345678AB",
                Decode = DecodeType.AustraliaPost,
                FileName = "australiapost.png",
                Configure = generator =>
                {
                    // Use CTable interpreting type for customer information
                    generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.CTable;
                }
            }
        };

        int passed = 0;
        int failed = 0;

        // Iterate through each test case
        foreach (var test in tests)
        {
            string filePath = Path.Combine(tempFolder, test.FileName);
            try
            {
                // ---------- Generate ----------
                using (var generator = new BarcodeGenerator(test.Symbology, test.CodeText))
                {
                    // Apply common barcode settings
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.FilledBars = false;
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                    generator.Parameters.Resolution = 300;
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                    generator.Parameters.RotationAngle = 0f;
                    generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                    // Apply symbology‑specific configuration if provided
                    test.Configure?.Invoke(generator);

                    // Save the generated barcode as a PNG file
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                // ---------- Verify ----------
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"[ERROR] File not created: {filePath}");
                    failed++;
                    continue;
                }

                using (var reader = new BarCodeReader(filePath, test.Decode))
                {
                    bool matchFound = false;
                    foreach (var result in reader.ReadBarCodes())
                    {
                        if (result.CodeText == test.CodeText)
                        {
                            matchFound = true;
                            break;
                        }
                    }

                    if (matchFound)
                    {
                        Console.WriteLine($"[PASS] {test.Symbology} - \"{test.CodeText}\"");
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine($"[FAIL] {test.Symbology} - Expected \"{test.CodeText}\" but not found.");
                        failed++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION] {test.Symbology}: {ex.Message}");
                failed++;
            }
        }

        // Output summary of test results
        Console.WriteLine();
        Console.WriteLine($"Total Passed: {passed}");
        Console.WriteLine($"Total Failed: {failed}");

        // Cleanup temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If deletion fails (e.g., files still in use), ignore – the OS will clean up temp files later.
        }
    }

    // Helper class to hold test data
    class BarcodeTest
    {
        public BaseEncodeType Symbology { get; set; }
        public string CodeText { get; set; }
        public BaseDecodeType Decode { get; set; }
        public string FileName { get; set; }
        public Action<BarcodeGenerator> Configure { get; set; }
    }
}