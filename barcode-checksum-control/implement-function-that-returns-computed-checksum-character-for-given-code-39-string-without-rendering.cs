// Title: Compute Code 39 checksum character without rendering
// Description: Demonstrates how to calculate the checksum character for a Code 39 barcode by generating a temporary image and reading it back, without displaying the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to enable checksum, save to a stream, and BarCodeReader to extract the computed checksum. Developers working with barcode validation, data integrity checks, or custom encoding often need to obtain checksum values programmatically without rendering the final image.
// Prompt: Implement a function that returns the computed checksum character for a given Code 39 string without rendering.
// Tags: code39, checksum, barcode, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates computing a Code 39 checksum character without rendering the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Computes and prints the checksum for a sample string.
    /// </summary>
    static void Main()
    {
        // Sample text for which we want the checksum
        string sample = "CODE39";

        // Compute the checksum using the helper method
        char checksum = ComputeCode39Checksum(sample);

        // Output the result to the console
        Console.WriteLine($"Checksum for \"{sample}\" is '{checksum}'");
    }

    /// <summary>
    /// Generates a temporary Code 39 barcode with checksum enabled, reads it back,
    /// and returns the checksum character without rendering the image to the user.
    /// </summary>
    /// <param name="codeText">The input string for which to compute the checksum.</param>
    /// <returns>The checksum character appended by the barcode generator.</returns>
    static char ComputeCode39Checksum(string codeText)
    {
        // Validate input
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("codeText cannot be null or empty.", nameof(codeText));

        // Use a memory stream to avoid writing the image to disk initially
        using (MemoryStream ms = new MemoryStream())
        {
            // Create a barcode generator for Code 39 Full ASCII with the provided text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
            {
                // Enable checksum calculation and make it visible in the human‑readable text
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;

                // Save the generated barcode to the memory stream in PNG format
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Write the stream to a temporary file because BarCodeReader works with file paths
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".png");
            try
            {
                File.WriteAllBytes(tempPath, ms.ToArray());

                // Initialize a reader for Code 39 Full ASCII to decode the temporary image
                using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.Code39FullASCII))
                {
                    // Iterate over all detected barcodes (expecting one)
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // The decoded text includes the checksum as the last character
                        string fullText = result.CodeText;

                        // Ensure we have a valid result with at least two characters
                        if (string.IsNullOrEmpty(fullText) || fullText.Length < 2)
                            throw new InvalidOperationException("Unable to retrieve checksum from barcode.");

                        // Return the last character, which is the checksum
                        return fullText[fullText.Length - 1];
                    }
                }
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempPath))
                {
                    try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
                }
            }
        }

        // If we reach this point, something went wrong
        throw new InvalidOperationException("Checksum could not be computed.");
    }
}