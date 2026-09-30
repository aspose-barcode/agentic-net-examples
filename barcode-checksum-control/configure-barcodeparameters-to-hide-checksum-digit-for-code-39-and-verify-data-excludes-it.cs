// Title: Hide Code 39 checksum digit and verify barcode data
// Description: Demonstrates how to generate a Code 39 barcode without calculating or displaying the checksum digit, then reads the barcode to confirm the decoded text matches the original data.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator and BarCodeReader with the EncodeTypes.Code39FullASCII symbology, configuring BarcodeParameters such as IsChecksumEnabled and ChecksumAlwaysShow. Developers often need to suppress checksum digits for compact human‑readable output or when the checksum is managed externally.
// Prompt: Configure BarcodeParameters to hide the checksum digit for Code 39 and verify the data excludes it.
// Tags: code39, checksum, hide-checksum, barcode-generation, barcode-recognition, aspose.barcode, csharp

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates hiding the checksum digit for a Code 39 barcode and verifying the decoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a barcode, saves it, reads it back, and validates the text.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample data without checksum
        string codeText = "ABC123";

        // Define output image path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "code39.png");

        // Generate Code 39 barcode, hide checksum and disable its calculation
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, codeText))
        {
            // Disable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
            // Hide checksum in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // Read back the barcode and verify the decoded text matches the original (no checksum)
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code39))
        {
            var result = reader.ReadBarCodes().FirstOrDefault();
            if (result != null)
            {
                Console.WriteLine($"Decoded text: {result.CodeText}");
                if (result.CodeText == codeText)
                {
                    Console.WriteLine("Verification succeeded: decoded text matches original (checksum excluded).");
                }
                else
                {
                    Console.WriteLine("Verification failed: decoded text does not match the original.");
                }
            }
            else
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }
    }
}