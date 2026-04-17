using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        var failures = new List<string>();
        var successes = new List<string>();

        // Get all barcode symbology types defined in EncodeTypes
        var encodeTypeFields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in encodeTypeFields)
        {
            if (!typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
                continue;

            var symbologyName = field.Name;
            var symbology = (BaseEncodeType)field.GetValue(null);

            // Test null CodeText
            try
            {
                using (var generator = new BarcodeGenerator(symbology))
                {
                    generator.CodeText = null;
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                    }
                }
                failures.Add($"{symbologyName}: No exception for null CodeText");
            }
            catch (Exception ex) when (ex is InvalidCodeException || ex is ArgumentException)
            {
                successes.Add($"{symbologyName}: Correctly threw for null CodeText");
            }
            catch (Exception ex)
            {
                failures.Add($"{symbologyName}: Unexpected exception for null CodeText - {ex.GetType().Name}");
            }

            // Test empty CodeText
            try
            {
                using (var generator = new BarcodeGenerator(symbology))
                {
                    generator.CodeText = string.Empty;
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                    }
                }
                failures.Add($"{symbologyName}: No exception for empty CodeText");
            }
            catch (Exception ex) when (ex is InvalidCodeException || ex is ArgumentException)
            {
                successes.Add($"{symbologyName}: Correctly threw for empty CodeText");
            }
            catch (Exception ex)
            {
                failures.Add($"{symbologyName}: Unexpected exception for empty CodeText - {ex.GetType().Name}");
            }
        }

        Console.WriteLine("=== Test Summary ===");
        Console.WriteLine($"Total symbologies tested: {successes.Count + failures.Count}");
        Console.WriteLine($"Passed: {successes.Count}");
        Console.WriteLine($"Failed: {failures.Count}");
        if (failures.Count > 0)
        {
            Console.WriteLine("\nFailures:");
            foreach (var f in failures)
                Console.WriteLine(f);
        }
    }
}