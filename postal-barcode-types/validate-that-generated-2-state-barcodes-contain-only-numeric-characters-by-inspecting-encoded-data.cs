using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and validation of RM4SCC (2‑state) barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, reads them back,
    /// validates that the decoded text contains only numeric characters, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Sample numeric texts for 2‑state (RM4SCC) barcodes
        string[] samples = { "1234567890", "987654321", "0011223344" };
        // Use the system temporary directory for generated images
        string tempDir = Path.GetTempPath();

        // Process each sample text
        foreach (var text in samples)
        {
            // Build a unique file name for the barcode image
            string filePath = Path.Combine(tempDir, $"rm4scc_{text}.png");

            // -------------------------------------------------
            // Generate the barcode image and save it to disk
            // -------------------------------------------------
            using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, text))
            {
                generator.Save(filePath);
            }

            // Verify that the image file was successfully created
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Failed to create barcode for '{text}'.");
                continue;
            }

            // -------------------------------------------------
            // Read the barcode from the image and validate its content
            // -------------------------------------------------
            using (var reader = new BarCodeReader(filePath, DecodeType.RM4SCC))
            {
                var results = reader.ReadBarCodes();

                // No barcode detected
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in image for '{text}'.");
                }
                else
                {
                    // Iterate over all detected barcodes (should be one per image)
                    foreach (var result in results)
                    {
                        // Check if the decoded text consists only of digits
                        bool isNumeric = IsStringNumeric(result.CodeText);
                        Console.WriteLine($"Input '{text}' => Decoded '{result.CodeText}' is {(isNumeric ? "valid" : "invalid")}.");
                    }
                }
            }

            // -------------------------------------------------
            // Clean up the temporary barcode image file
            // -------------------------------------------------
            try
            {
                File.Delete(filePath);
            }
            catch
            {
                // Suppress any exceptions during cleanup
            }
        }
    }

    /// <summary>
    /// Determines whether the specified string consists solely of numeric digits.
    /// </summary>
    /// <param name="s">The string to evaluate.</param>
    /// <returns>True if the string is non‑empty and contains only digits; otherwise, false.</returns>
    static bool IsStringNumeric(string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;

        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }

        return true;
    }
}