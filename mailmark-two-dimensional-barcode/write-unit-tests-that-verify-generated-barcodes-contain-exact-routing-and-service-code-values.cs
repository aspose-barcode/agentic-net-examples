// Title: Generate and verify UPC-A with GS1 Code128 coupon barcode
// Description: Demonstrates creating a UPC-A barcode combined with a GS1 Code128 coupon, then reading it back to confirm that the routing and service code values are encoded correctly.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showing how to use BarcodeGenerator with EncodeTypes.UpcaGs1Code128Coupon and BarCodeReader to validate encoded data. Typical use cases include testing barcode output for retail coupons where precise routing and service codes are required. Developers often need unit‑testable code that confirms the generated CodeText matches expected values.
// Prompt: Write unit tests that verify generated barcodes contain the exact routing and service code values.
// Tags: upc-a,gs1-code128,coupon,barcode-generation,barcode-recognition,unit-test,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and verification of a UPC‑A with GS1 Code128 coupon barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs a simple verification test for routing and service code values.
    /// </summary>
    static void Main()
    {
        // Counters for total executed tests and failed tests
        int totalTests = 0;
        int failedTests = 0;

        // ------------------------------------------------------------
        // Test 1: UPC-A with GS1 Code128 coupon (routing and service code)
        // ------------------------------------------------------------
        totalTests++;
        try
        {
            // Expected full code text (UPCA part + GS1 Code128 part)
            string expectedCodeText = "514141100906(8102)03";

            // Generate barcode image in memory using the specified symbology and data
            using (var generator = new BarcodeGenerator(EncodeTypes.UpcaGs1Code128Coupon, expectedCodeText))
            {
                using (var ms = new MemoryStream())
                {
                    // Save the generated barcode as PNG into the memory stream
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading

                    // Read barcode back from the memory stream
                    using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                    {
                        var results = reader.ReadBarCodes();

                        // Verify that at least one barcode was detected
                        if (results.Length == 0)
                        {
                            Console.WriteLine("FAILED: No barcode detected.");
                            failedTests++;
                        }
                        else
                        {
                            // Check if any detected barcode matches the expected CodeText
                            bool matchFound = false;
                            foreach (var result in results)
                            {
                                if (result.CodeText == expectedCodeText)
                                {
                                    matchFound = true;
                                    break;
                                }
                            }

                            if (matchFound)
                            {
                                Console.WriteLine("PASSED: Routing and service code values match.");
                            }
                            else
                            {
                                Console.WriteLine($"FAILED: Detected CodeText does not match. Expected '{expectedCodeText}'.");
                                foreach (var result in results)
                                {
                                    Console.WriteLine($"  Detected: '{result.CodeText}'");
                                }
                                failedTests++;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any unexpected exceptions as test failures
            Console.WriteLine($"FAILED: Exception occurred - {ex.Message}");
            failedTests++;
        }

        // ------------------------------------------------------------
        // Summary of test results
        // ------------------------------------------------------------
        if (failedTests == 0)
        {
            Console.WriteLine($"ALL TESTS PASSED: {totalTests} tests executed.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failedTests} out of {totalTests} tests failed.");
        }
    }
}