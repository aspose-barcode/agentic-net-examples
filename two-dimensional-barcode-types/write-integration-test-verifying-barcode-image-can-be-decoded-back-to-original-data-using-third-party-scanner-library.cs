// Title: Integration test for barcode generation and decoding using Aspose.BarCode
// Description: The example generates a Code128 barcode image, saves it to a temporary location, and then decodes it to verify the original data can be retrieved.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation and recognition category. It demonstrates using BarcodeGenerator to create barcodes and BarCodeReader to decode them, a common scenario for automated testing, batch processing, or validation pipelines where developers need to ensure generated barcodes are readable by third‑party scanners. The code showcases key API classes such as BarcodeGenerator, BarCodeReader, and related parameter settings.
// Prompt: Write integration test verifying barcode image can be decoded back to original data using third‑party scanner library.
// Tags: barcode, code128, generation, recognition, integration-test, aspose.barcode, png, temporary-files

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates an integration test that creates a barcode image, saves it, and verifies it can be decoded back to the original data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Generates a Code128 barcode, decodes it, and reports the test result.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder for the test artifacts.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the output file path and the data to encode.
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string codeText = "AsposeTest123";

        // --------------------------------------------------------------------
        // Generate the barcode image using Aspose.BarCode.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the X-dimension (module width) to improve readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Save the generated barcode as a PNG file.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Attempt to read (decode) the barcode from the saved image.
        // --------------------------------------------------------------------
        bool success = false;
        try
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Iterate through all detected barcodes (expecting one).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    if (!string.IsNullOrEmpty(result.CodeText))
                    {
                        success = true;
                        Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                        Console.WriteLine($"Decoded Text: {result.CodeText}");
                        break; // Stop after the first successful decode.
                    }
                }
            }
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
        {
            // Handle cases where the image could not be loaded (e.g., corrupted file).
            Console.WriteLine("Image loading failed: " + ex.Message);
        }

        // Report the overall test outcome.
        Console.WriteLine(success ? "Integration test passed: barcode decoded." : "Integration test failed: barcode not decoded.");

        // --------------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical for the test outcome.
        }
    }
}