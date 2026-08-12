// Title: Integration test for barcode generation and verification using Aspose.BarCode
// Description: Generates a Code128 barcode image, saves it to a temporary folder, then decodes it to confirm the extracted data matches the original string.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator to create barcodes and BarCodeReader to decode them. Typical use cases include automated testing of barcode workflows, validating data integrity, and ensuring compatibility with third‑party scanners. Developers often need to generate temporary barcode images, read them back, and verify correctness using these core API classes.
// Prompt: Write integration test verifying barcode image can be decoded back to original data using third‑party scanner library.
// Tags: barcode, code128, generation, recognition, integration-test, aspose.barcode, png, temporary-files

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates an integration test that generates a Code128 barcode,
/// saves it as PNG, and verifies decoding returns the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test application.
    /// Generates a barcode, decodes it, and reports success or failure.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the barcode image path
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // The data to encode
        const string originalData = "Test123";

        // Generate the barcode image and save it
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, originalData))
        {
            // Save as PNG (default format inferred from extension)
            generator.Save(barcodePath);
        }

        // Verify that the image file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the barcode image
        bool decodedSuccessfully = false;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                if (result.CodeText == originalData)
                {
                    decodedSuccessfully = true;
                }
            }
        }

        // Output the test result
        if (decodedSuccessfully)
        {
            Console.WriteLine("Integration test passed: decoded data matches original.");
        }
        else
        {
            Console.WriteLine("Integration test failed: decoded data does not match original.");
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect test outcome
        }
    }
}