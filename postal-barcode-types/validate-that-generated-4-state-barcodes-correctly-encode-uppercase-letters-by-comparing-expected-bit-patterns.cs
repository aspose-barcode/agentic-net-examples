using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and recognition of RM4SCC barcodes for uppercase letters A‑Z using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode for each letter, saves it to a memory stream, and verifies recognition.
    /// </summary>
    static void Main()
    {
        // Loop through uppercase alphabet characters
        for (char ch = 'A'; ch <= 'Z'; ch++)
        {
            // Convert character to string for barcode text
            string codeText = ch.ToString();

            // Create a barcode generator for RM4SCC type with the current character
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
            {
                // Save generated barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Reset stream position to beginning for reading
                    ms.Position = 0;

                    // Initialize a barcode reader for RM4SCC decoding
                    using (var reader = new BarCodeReader(ms, DecodeType.RM4SCC))
                    {
                        // Read all barcodes found in the image
                        var results = reader.ReadBarCodes();

                        // If no barcode was detected, report and continue to next character
                        if (results.Length == 0)
                        {
                            Console.WriteLine($"[{codeText}] No barcode detected.");
                            continue;
                        }

                        // Take the first detection result
                        var result = results[0];

                        // Compare the recognized text with the original input
                        if (result.CodeText == codeText)
                        {
                            Console.WriteLine($"[{codeText}] Success: recognized correctly.");
                        }
                        else
                        {
                            Console.WriteLine($"[{codeText}] Failure: expected '{codeText}', got '{result.CodeText}'.");
                        }
                    }
                }
            }
        }
    }
}