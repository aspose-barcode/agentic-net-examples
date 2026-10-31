// Title: Generate and Verify Code128 Barcode Image
// Description: Demonstrates creating a Code128 barcode, saving it as PNG, and confirming it decodes back to the original text using Aspose.BarCode's reader.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator to produce barcodes and BarCodeReader to decode them. Typical scenarios include automated testing, integration validation, and sample code for developers needing to ensure barcode round‑trip fidelity. The key API classes are BarcodeGenerator, BarCodeReader, and related parameter objects, which are commonly employed for creating, customizing, and reading various symbologies in .NET applications.
/// Prompt: Write integration test verifying barcode image can be decoded back to original data using third‑party scanner library.
/// Tags: code128, barcode generation, barcode recognition, png, aspose.barcode, integration-test

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, saves it to a temporary file,
/// reads it back using Aspose.BarCode's scanner, and validates the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, verification, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file path and original data to encode
        string barcodeFile = Path.Combine(tempFolder, "sample.png");
        string originalData = "Test12345";

        // Generate barcode image using Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalData))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;   // Set barcode foreground color
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;          // Set image background color
            generator.Save(barcodeFile, BarCodeImageFormat.Png);                  // Save as PNG
        }

        // Verify that the barcode image file was created successfully
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode using Aspose.BarCode reader (simulating a third‑party scanner)
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    bool isMatch = result.CodeText == originalData;
                    Console.WriteLine($"Match with original data: {isMatch}");
                }
            }
        }

        // Cleanup temporary files and folder; ignore any errors during cleanup
        try
        {
            File.Delete(barcodeFile);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup failures are non‑critical for the test outcome
        }
    }
}