using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how each barcode symbology handles null or empty CodeText values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates through all supported barcode symbologies,
    /// attempts to generate barcodes with empty and null CodeText, and records the outcomes.
    /// </summary>
    static void Main()
    {
        // Retrieve a dictionary of all barcode symbologies defined in EncodeTypes.
        var symbologies = GetAllEncodeTypes();

        // Store test results for later display.
        var results = new List<string>();

        // Iterate over each symbology.
        foreach (var sym in symbologies)
        {
            string name = sym.Key;               // Human‑readable name of the symbology.
            BaseEncodeType type = sym.Value;     // Corresponding EncodeType value.

            // ------------------------------------------------------------
            // Test case 1: Empty string as CodeText.
            // ------------------------------------------------------------
            try
            {
                // Create a generator with an empty string.
                using (var generator = new BarcodeGenerator(type, ""))
                {
                    // Configure generator to throw an exception when CodeText is invalid.
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Attempt to save the barcode to a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                    }
                }

                // If no exception is thrown, record an unexpected outcome.
                results.Add($"{name}: Empty string - No exception (unexpected)");
            }
            catch (Exception ex)
            {
                // Record the type of exception that was caught.
                results.Add($"{name}: Empty string - Caught exception: {ex.GetType().Name}");
            }

            // ------------------------------------------------------------
            // Test case 2: Null CodeText.
            // ------------------------------------------------------------
            try
            {
                // Create a generator without specifying CodeText (defaults to null).
                using (var generator = new BarcodeGenerator(type))
                {
                    // Ensure the generator throws on invalid CodeText.
                    generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                    // Explicitly set CodeText to null.
                    generator.CodeText = null;

                    // Attempt to save the barcode to a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                    }
                }

                // If no exception is thrown, record an unexpected outcome.
                results.Add($"{name}: Null codetext - No exception (unexpected)");
            }
            catch (Exception ex)
            {
                // Record the type of exception that was caught.
                results.Add($"{name}: Null codetext - Caught exception: {ex.GetType().Name}");
            }
        }

        // Output a summary of all test results.
        Console.WriteLine("Barcode CodeText null/empty handling test results:");
        foreach (var line in results)
        {
            Console.WriteLine(line);
        }
    }

    /// <summary>
    /// Retrieves all public static fields from <see cref="EncodeTypes"/> that are of type <see cref="BaseEncodeType"/>.
    /// </summary>
    /// <returns>
    /// A dictionary mapping field names to their corresponding <see cref="BaseEncodeType"/> values.
    /// </returns>
    private static Dictionary<string, BaseEncodeType> GetAllEncodeTypes()
    {
        var dict = new Dictionary<string, BaseEncodeType>();

        // Get all public static fields defined in EncodeTypes.
        var fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);

        // Filter fields that are of type BaseEncodeType and add them to the dictionary.
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(BaseEncodeType))
            {
                var value = (BaseEncodeType)field.GetValue(null);
                dict[field.Name] = value;
            }
        }

        return dict;
    }
}