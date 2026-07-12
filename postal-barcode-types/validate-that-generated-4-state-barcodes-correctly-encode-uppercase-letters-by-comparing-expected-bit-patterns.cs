// Title: Validate 4‑State Barcode Encoding of Uppercase Letters
// Description: Generates Code4State barcodes for each uppercase letter, decodes them, and verifies the decoded text matches the original.
// Category-Description: Demonstrates Aspose.BarCode 4‑state symbology handling, covering barcode generation with BarcodeGenerator, image saving, and recognition using BarCodeReader. Useful for developers needing to validate encoding/decoding of Code4State barcodes, compare expected patterns, and ensure deterministic image parameters for reliable recognition.
// Prompt: Validate that generated 4‑state barcodes correctly encode uppercase letters by comparing expected bit patterns.
// Tags: barcode symbology, validation, png, encode, decode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that validates the encoding and decoding of uppercase letters
/// using the 4‑state Code4State barcode symbology provided by Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode for each letter A‑Z, decodes it, and reports pass/fail.
    /// </summary>
    static void Main()
    {
        // Resolve the 4‑state symbology at runtime via reflection.
        const string symbologyName = "Code4State";
        var encodeField = typeof(EncodeTypes).GetField(symbologyName);
        var decodeField = typeof(DecodeType).GetField(symbologyName);

        // Verify that the symbology is supported in the current Aspose.BarCode version.
        if (encodeField == null || decodeField == null)
        {
            Console.WriteLine($"Symbology '{symbologyName}' is not supported by this version of Aspose.BarCode.");
            return;
        }

        // Cast the reflected fields to the appropriate enum types.
        var encodeType = (BaseEncodeType)encodeField.GetValue(null);
        var decodeType = (SingleDecodeType)decodeField.GetValue(null);

        bool allPassed = true;

        // Iterate over all uppercase ASCII letters.
        for (char ch = 'A'; ch <= 'Z'; ch++)
        {
            string text = ch.ToString();

            // Generate the barcode image in memory for the current letter.
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Set visual parameters for deterministic recognition.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.Barcode.XDimension.Point = 2f;

                using (var ms = new MemoryStream())
                {
                    // Save the barcode as PNG into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Recognize the barcode from the generated image.
                    using (var reader = new BarCodeReader(ms, decodeType))
                    {
                        bool matched = false;

                        // Read all detected barcodes and compare the decoded text.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            if (result.CodeText == text)
                            {
                                matched = true;
                                break;
                            }
                        }

                        // Output the validation result for the current letter.
                        if (matched)
                        {
                            Console.WriteLine($"[PASS] Letter '{text}' encoded and decoded correctly.");
                        }
                        else
                        {
                            Console.WriteLine($"[FAIL] Letter '{text}' did not decode as expected.");
                            allPassed = false;
                        }
                    }
                }
            }
        }

        // Summarize overall validation outcome.
        Console.WriteLine(allPassed
            ? "All uppercase letters validated successfully."
            : "Some letters failed validation.");
    }
}