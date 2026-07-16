// Title: Unit Test for Null or Empty CodeText Across All Barcode Types
// Description: Demonstrates how to verify that Aspose.BarCode correctly handles null or empty CodeText for each supported barcode symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of EncodeTypes, BarcodeGenerator, and related parameters to validate input handling. Developers working with barcode creation often need to ensure that invalid or missing data (such as null or empty strings) triggers appropriate exceptions or fallback behavior. The snippet shows a systematic approach to testing all symbologies, useful for unit testing and CI pipelines.
// Prompt: Create unit test that verifies correct handling of null or empty CodeText for all barcode types.
// Tags: barcode symbology, validation, null handling, unit test, aspose.barcode, generation

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Contains a simple console‑based test that iterates over every barcode symbology
/// defined in <see cref="EncodeTypes"/> and verifies the behavior when <c>CodeText</c>
/// is null or empty.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loops through all <see cref="EncodeTypes"/> fields and calls
    /// <see cref="TestBarcode(string, BaseEncodeType, string)"/> for null and empty
    /// <c>CodeText</c> values.
    /// </summary>
    static void Main()
    {
        // Retrieve all public static fields of EncodeTypes (each represents a barcode symbology)
        var encodeFields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);

        // Iterate over each symbology and test both null and empty CodeText scenarios
        foreach (var field in encodeFields)
        {
            var encodeName = field.Name;
            var encodeValue = (BaseEncodeType)field.GetValue(null);

            // Test with null CodeText
            TestBarcode(encodeName, encodeValue, null);

            // Test with empty CodeText
            TestBarcode(encodeName, encodeValue, string.Empty);
        }

        Console.WriteLine("All tests completed.");
    }

    /// <summary>
    /// Generates a barcode of the specified type with the supplied <c>codeText</c>
    /// and saves the image. Any exception is caught and reported.
    /// </summary>
    /// <param name="encodeName">Name of the barcode symbology (field name).</param>
    /// <param name="encodeType">Corresponding <see cref="BaseEncodeType"/> instance.</param>
    /// <param name="codeText">The text to encode; may be null or empty.</param>
    static void TestBarcode(string encodeName, BaseEncodeType encodeType, string codeText)
    {
        // Determine a suffix for the output file based on the test case
        var suffix = codeText == null ? "null" : "empty";
        var fileName = $"{encodeName}_{suffix}.png";

        try
        {
            // Initialize the generator for the current barcode type
            using (var generator = new BarcodeGenerator(encodeType))
            {
                // Enforce exception when CodeText is invalid for 1D barcodes
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Assign the test CodeText (null or empty)
                generator.CodeText = codeText;

                // Attempt to generate and save the barcode image
                generator.Save(fileName);
            }

            Console.WriteLine($"{encodeName}: CodeText {(codeText == null ? "null" : "empty")} - succeeded, image saved as {fileName}");
        }
        catch (Exception ex)
        {
            // Expected for many 1D symbologies when CodeText is invalid
            Console.WriteLine($"{encodeName}: CodeText {(codeText == null ? "null" : "empty")} - threw {ex.GetType().Name}: {ex.Message}");
        }
    }
}