// Title: Validate null or empty CodeText handling for all barcode symbologies
// Description: This example iterates through every supported barcode type in Aspose.BarCode and checks that providing a null or empty CodeText triggers the appropriate exception, ensuring strict validation is enforced.
// Category-Description: Demonstrates how to use Aspose.BarCode's generation API to perform validation across all EncodeTypes. It shows configuring the generator to throw exceptions on invalid CodeText, a common requirement when building unit tests or input validation layers for barcode creation. Developers working with barcode generation, automated testing, or data integrity checks will find this pattern useful.
// Prompt: Create unit test that verifies correct handling of null or empty CodeText for all barcode types.
// Tags: barcode symbology, code text validation, exception handling, aspose.barcode, unit test, c#

using System;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that validates handling of null or empty CodeText for every supported barcode symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the validation loop and reports any failures.
    /// </summary>
    static void Main()
    {
        // Collect failure messages for any symbology that does not behave as expected
        var failures = new List<string>();

        // Retrieve all public static fields from EncodeTypes (each represents a barcode symbology)
        var encodeType = typeof(EncodeTypes);
        var fields = encodeType.GetFields(BindingFlags.Public | BindingFlags.Static);

        // Iterate over each barcode type and test both null and empty CodeText values
        foreach (var field in fields)
        {
            var symName = field.Name;
            var baseEncode = (BaseEncodeType)field.GetValue(null);

            // Test with null CodeText
            if (!TestCodeText(baseEncode, null, out string nullMsg))
                failures.Add($"{symName} (null): {nullMsg}");

            // Test with empty CodeText
            if (!TestCodeText(baseEncode, string.Empty, out string emptyMsg))
                failures.Add($"{symName} (empty): {emptyMsg}");
        }

        // Output overall test result
        if (failures.Count == 0)
        {
            Console.WriteLine("ALL TESTS PASSED");
        }
        else
        {
            Console.WriteLine($"FAILED: {failures.Count} tests failed.");
            foreach (var f in failures)
                Console.WriteLine(f);
        }
    }

    // Returns true if the behavior is as expected (exception thrown)
    static bool TestCodeText(BaseEncodeType encode, string codeText, out string message)
    {
        try
        {
            // Create a generator for the specific barcode type
            using (var generator = new BarcodeGenerator(encode))
            {
                // Enable strict validation so invalid CodeText throws an exception
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;
                generator.CodeText = codeText;

                // Attempt to generate the barcode image; an exception is expected for invalid text
                using (Bitmap bmp = generator.GenerateBarCodeImage())
                {
                    // If we reach this point, no exception was thrown – this is a failure
                    message = "No exception thrown for invalid CodeText.";
                    return false;
                }
            }
        }
        catch (InvalidCodeException)
        {
            // Expected outcome for invalid CodeText
            message = null;
            return true;
        }
        catch (ArgumentException)
        {
            // Expected outcome for null arguments
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            // Unexpected exception type – report details
            message = $"Unexpected exception: {ex.GetType().Name} - {ex.Message}";
            return false;
        }
    }
}