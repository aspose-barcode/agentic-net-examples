// Title: Unit test for handling null or empty CodeText across all barcode types
// Description: Demonstrates how to verify that Aspose.BarCode throws exceptions when CodeText is null or empty for each supported barcode symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation validation suite, illustrating the use of EncodeTypes enumeration, BarcodeGenerator, and exception handling to ensure input validation. Developers testing barcode creation often need to confirm that invalid inputs are rejected, making this pattern useful for unit testing and CI pipelines.
// Prompt: Create unit test that verifies correct handling of null or empty CodeText for all barcode types.
// Tags: barcode symbology, validation, unit test, null handling, empty string, aspose.barcode, encode types, exception handling

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains a simple console‑based test that iterates over all barcode symbologies
/// and verifies that providing a null or empty CodeText results in an exception.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, runs the tests for each EncodeTypes value,
    /// and reports the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for any generated files (none are actually saved here)
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        int totalTests = 0;
        int failedTests = 0;

        // Retrieve all public static fields of the EncodeTypes enum (each represents a barcode type)
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            // Ensure the field value is a BaseEncodeType instance before testing
            if (field.GetValue(null) is BaseEncodeType encodeType)
            {
                // Test with empty string
                totalTests++;
                if (!TestCodeText(encodeType, "", tempFolder))
                {
                    failedTests++;
                }

                // Test with null
                totalTests++;
                if (!TestCodeText(encodeType, null, tempFolder))
                {
                    failedTests++;
                }
            }
        }

        // Output a summary of the test run
        Console.WriteLine($"Total tests run: {totalTests}");
        Console.WriteLine($"Tests failed: {failedTests}");
        Console.WriteLine($"Tests passed: {totalTests - failedTests}");
    }

    /// <summary>
    /// Attempts to generate a barcode with the specified <paramref name="codeText"/> and verifies that an exception is thrown.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to test.</param>
    /// <param name="codeText">The CodeText value (null or empty) to validate.</param>
    /// <param name="folder">The folder path used for temporary storage (not used in this test).</param>
    /// <returns>True if the expected exception is thrown; otherwise, false.</returns>
    static bool TestCodeText(BaseEncodeType encodeType, string codeText, string folder)
    {
        // Determine a readable name for the test case (null vs empty)
        string testName = codeText == null ? "null" : "empty";

        try
        {
            // Initialize the generator with the current encode type and the test CodeText
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure the generator to throw on invalid CodeText
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Trigger barcode generation; this should raise an exception for null/empty CodeText
                generator.GenerateBarCodeImage();

                // If we reach this point, the generator incorrectly accepted the invalid input
                Console.WriteLine($"FAIL: {encodeType.GetType().Name}.{encodeType} accepted {testName} CodeText.");
                return false;
            }
        }
        catch (Exception ex)
        {
            // Expected outcome: an exception indicates proper validation
            Console.WriteLine($"PASS: {encodeType.GetType().Name}.{encodeType} threw exception for {testName} CodeText -> {ex.Message}");
            return true;
        }
    }
}