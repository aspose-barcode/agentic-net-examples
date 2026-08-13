// Title: Validate 4‑State Barcode Encoding of Uppercase Letters
// Description: Demonstrates how to generate 4‑state (or fallback Code128) barcodes for each uppercase alphabet character and verify that they decode back to the original text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showing how to use BarcodeGenerator, BarCodeReader, EncodeTypes, and DecodeType classes. Typical use cases include automated barcode validation, quality assurance, and unit testing of barcode symbologies. Developers often need to programmatically confirm that a barcode encodes the expected data before deployment.
// Prompt: Validate that generated 4‑state barcodes correctly encode uppercase letters by comparing expected bit patterns.
// Tags: barcode, symbology, fourstate, code128, generation, recognition, validation, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Program that validates 4‑state barcode encoding of uppercase letters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates barcodes for letters A‑Z, decodes them, and reports pass/fail results.
    /// </summary>
    static void Main()
    {
        // Symbology names to try: FourState (if available) otherwise Code128.
        const string primarySymbology = "FourState";
        const string fallbackSymbology = "Code128";

        // Resolve encode type via reflection; fall back if primary is unavailable.
        BaseEncodeType encodeType = ResolveEncodeType(primarySymbology) ?? ResolveEncodeType(fallbackSymbology);
        if (encodeType == null)
        {
            Console.WriteLine("Unable to resolve a suitable encode type.");
            return;
        }

        // Resolve matching decode type based on the selected encode type.
        BaseDecodeType decodeType = ResolveDecodeType(encodeType);
        if (decodeType == null)
        {
            Console.WriteLine("Unable to resolve a matching decode type.");
            return;
        }

        // Prepare test data: uppercase letters A‑Z.
        string[] letters = GetUppercaseLetters();
        int passed = 0;
        int failed = 0;

        // Iterate over each letter, generate and validate the barcode.
        foreach (string letter in letters)
        {
            // Generate barcode image in memory for the current letter.
            using (var generator = new BarcodeGenerator(encodeType, letter))
            {
                // Do not throw if the code text is considered incorrect; we only need to test decoding.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                using (var ms = new MemoryStream())
                {
                    // Save the generated bitmap to a memory stream in PNG format.
                    bitmap.Save(ms, Aspose.Drawing.Imaging.ImageFormat.Png);
                    ms.Position = 0;

                    // Read the barcode from the memory stream.
                    using (var reader = new BarCodeReader(ms, decodeType))
                    {
                        bool matchFound = false;
                        foreach (var result in reader.ReadBarCodes())
                        {
                            if (result.CodeText == letter)
                            {
                                matchFound = true;
                                break;
                            }
                        }

                        if (matchFound)
                        {
                            Console.WriteLine($"[PASS] Letter '{letter}' correctly encoded.");
                            passed++;
                        }
                        else
                        {
                            Console.WriteLine($"[FAIL] Letter '{letter}' did not decode correctly.");
                            failed++;
                        }
                    }
                }
            }
        }

        // Output summary of validation results.
        Console.WriteLine();
        Console.WriteLine($"Validation complete. Passed: {passed}, Failed: {failed}");
    }

    // Returns an array of strings "A".."Z".
    static string[] GetUppercaseLetters()
    {
        var letters = new string[26];
        for (int i = 0; i < 26; i++)
        {
            letters[i] = ((char)('A' + i)).ToString();
        }
        return letters;
    }

    // Resolve an EncodeTypes field name to BaseEncodeType via reflection.
    static BaseEncodeType ResolveEncodeType(string name)
    {
        var field = typeof(EncodeTypes).GetField(name);
        if (field == null) return null;
        return (BaseEncodeType)field.GetValue(null);
    }

    // Resolve a DecodeType field that matches the given encode type name.
    static BaseDecodeType ResolveDecodeType(BaseEncodeType encodeType)
    {
        // EncodeTypes and DecodeType share the same field names.
        string name = encodeType.GetType().GetField(encodeType.ToString())?.Name;
        if (string.IsNullOrEmpty(name)) return null;
        var field = typeof(DecodeType).GetField(name);
        if (field == null) return null;
        return (BaseDecodeType)field.GetValue(null);
    }
}