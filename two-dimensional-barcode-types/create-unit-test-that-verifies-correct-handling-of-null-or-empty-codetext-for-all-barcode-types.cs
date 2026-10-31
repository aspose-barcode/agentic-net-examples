// Title: Unit test for handling null or empty CodeText across all barcode types
// Description: Demonstrates how to verify that Aspose.BarCode throws an exception when CodeText is null or empty for each supported barcode symbology.
// Category-Description: This example belongs to the Aspose.BarCode validation suite, illustrating the use of EncodeTypes, BarcodeGenerator, and exception handling to ensure correct input validation. Developers working with barcode generation often need to test edge cases such as missing or empty data to guarantee robust applications. The snippet shows how to iterate over all encode types and programmatically assert expected failures.
// Prompt: Create unit test that verifies correct handling of null or empty CodeText for all barcode types.
// Tags: barcode symbology, validation, exception handling, encode types, aspose.barcode, unit test

using System;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains a simple console‑based test that iterates over every supported barcode symbology
/// and verifies that generating a barcode with a null or empty <c>CodeText</c> throws an exception.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the validation tests and reports results to the console.
    /// </summary>
    static void Main()
    {
        // Collect any test failures for reporting at the end
        var failures = new List<string>();

        // Retrieve all public static fields of EncodeTypes (each represents a barcode symbology)
        var encodeTypeFields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);

        // Iterate through each barcode type and run tests for null and empty CodeText
        foreach (var field in encodeTypeFields)
        {
            var encodeType = field.GetValue(null) as BaseEncodeType;
            if (encodeType == null) continue;

            // Test with null CodeText
            TestCodeText(encodeType, null, failures);

            // Test with empty CodeText
            TestCodeText(encodeType, string.Empty, failures);
        }

        // Output summary of test execution
        Console.WriteLine($"Total tests run: {encodeTypeFields.Length * 2}");
        if (failures.Count == 0)
        {
            Console.WriteLine("All tests passed.");
        }
        else
        {
            Console.WriteLine($"FAILED: {failures.Count} tests failed.");
            foreach (var msg in failures)
            {
                Console.WriteLine(msg);
            }
        }
    }

    /// <summary>
    /// Attempts to generate a barcode with the specified <paramref name="codeText"/> and records a failure
    /// if no exception is thrown. Expected behavior is an exception when <paramref name="codeText"/> is null or empty.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to test.</param>
    /// <param name="codeText">The CodeText value to use (null or empty).</param>
    /// <param name="failures">Collection to which failure messages are added.</param>
    static void TestCodeText(BaseEncodeType encodeType, string codeText, List<string> failures)
    {
        string testName = $"{encodeType.GetType().Name}.{encodeType} with {(codeText == null ? "null" : "empty")} CodeText";
        try
        {
            // Initialize generator with the current encode type and test CodeText
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure generator to throw on invalid CodeText
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Attempt to generate the barcode image; should throw
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // If we reach this point, no exception was thrown – record as failure
                    failures.Add($"{testName}: Expected exception but none was thrown.");
                }
            }
        }
        catch (Exception ex)
        {
            // Expected path: an exception should be thrown; log for visibility
            Console.WriteLine($"{testName}: Caught expected exception: {ex.Message}");
        }
    }
}