// Title: Unit test for handling null or empty CodeText across all barcode types
// Description: Demonstrates how to iterate through all supported barcode symbologies and verify that the generator throws an exception when CodeText is null or empty.
// Category-Description: This example belongs to the Aspose.BarCode generation validation category. It shows how to use EncodeTypes, BaseEncodeType, and BarcodeGenerator to programmatically test input validation across every barcode symbology. Developers often need to ensure that invalid or missing CodeText values are correctly rejected, especially when building libraries or services that generate barcodes on demand. The pattern illustrated here is useful for creating automated unit tests that cover the full range of supported barcode types.
// Prompt: Create unit test that verifies correct handling of null or empty CodeText for all barcode types.
// Tags: barcode symbology, validation, null handling, empty string, aspose.barcode, generation, unit-test

using System;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Executes a simple validation suite that checks whether the Aspose.BarCode
/// <see cref="BarcodeGenerator"/> throws an exception when the <c>CodeText</c> is
/// null or an empty string for every supported barcode symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test application. Iterates over all <see cref="EncodeTypes"/>
    /// fields, creates a <see cref="BarcodeGenerator"/> for each, and verifies that
    /// an exception is raised for null or empty <c>CodeText</c>.
    /// </summary>
    static void Main()
    {
        // Collect any failures to report at the end of the run
        List<string> failures = new List<string>();
        int totalTests = 0;

        // Retrieve all public static fields of EncodeTypes (each represents a barcode symbology)
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo field in fields)
        {
            // Skip fields that are not barcode type definitions
            if (!typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
                continue;

            // Resolve the actual EncodeType instance and its name for reporting
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string symbologyName = field.Name;

            // ------------------------------------------------------------
            // Test case 1: Empty string as CodeText
            // ------------------------------------------------------------
            totalTests++;
            try
            {
                // Initialise generator with empty CodeText
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, ""))
                {
                    // Configure generator to throw on invalid CodeText
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Attempt to generate the barcode image; should throw
                    using (Bitmap img = generator.GenerateBarCodeImage())
                    {
                        // If we reach this point, the generator did not throw as expected
                        failures.Add($"{symbologyName} - Empty string did not throw.");
                    }
                }
            }
            catch (Exception)
            {
                // Expected path: an exception indicates correct handling
            }

            // ------------------------------------------------------------
            // Test case 2: Null string as CodeText
            // ------------------------------------------------------------
            totalTests++;
            try
            {
                // Initialise generator with null CodeText
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, null))
                {
                    // Configure generator to throw on invalid CodeText
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Attempt to generate the barcode image; should throw
                    using (Bitmap img = generator.GenerateBarCodeImage())
                    {
                        // If we reach this point, the generator did not throw as expected
                        failures.Add($"{symbologyName} - Null string did not throw.");
                    }
                }
            }
            catch (Exception)
            {
                // Expected path: an exception indicates correct handling
            }
        }

        // Summarise test results
        int passed = totalTests - failures.Count;
        Console.WriteLine($"Total tests: {totalTests}");
        Console.WriteLine($"Passed: {passed}");
        Console.WriteLine($"Failed: {failures.Count}");

        // Output details of any failures
        if (failures.Count > 0)
        {
            Console.WriteLine("Failures:");
            foreach (string f in failures)
            {
                Console.WriteLine(f);
            }
        }
    }
}