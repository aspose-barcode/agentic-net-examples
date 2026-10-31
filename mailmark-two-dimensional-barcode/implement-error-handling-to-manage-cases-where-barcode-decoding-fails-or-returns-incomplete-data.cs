// Title: Generate and Decode a Code128 Barcode with Error Handling
// Description: This example demonstrates generating a Code128 barcode image, saving it to a temporary folder, and then decoding it while handling possible errors such as missing files or incomplete data.
// Category-Description: This sample belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers commonly use these APIs to embed barcodes in documents, validate scanned data, and integrate barcode workflows into applications. The example highlights typical error‑handling patterns needed when working with image files and decoding results.
// Prompt: Implement error handling to manage cases where barcode decoding fails or returns incomplete data.
// Tags: code128, barcode generation, barcode recognition, error handling, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and decoding using Aspose.BarCode with comprehensive error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a Code128 barcode, attempts to decode it, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // -------------------- Barcode Generation --------------------
        try
        {
            // Initialize the generator with Code128 symbology and sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set visual parameters (e.g., X-dimension in pixels)
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Save the generated barcode as a PNG file
                generator.Save(barcodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
            return;
        }

        // -------------------- Verify Image Existence --------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file not found.");
            return;
        }

        // -------------------- Barcode Decoding --------------------
        try
        {
            // Initialize the reader for Code128 decoding
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Enable default checksum validation for added reliability
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                // Perform the decoding operation
                BarCodeResult[] results = reader.ReadBarCodes();

                // Evaluate decoding results
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the image.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        // Check for incomplete or empty decoded data
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            Console.WriteLine("Decoded barcode data is incomplete or empty.");
                        }
                        else
                        {
                            // Output detailed decoding information
                            Console.WriteLine($"Decoded Type: {result.CodeTypeName}");
                            Console.WriteLine($"Decoded Text: {result.CodeText}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        }
                    }
                }
            }
        }
        catch (ArgumentException argEx)
        {
            // Handle cases where the image is invalid or unsupported
            Console.WriteLine($"Invalid image or format: {argEx.Message}");
        }
        catch (Exception ex)
        {
            // General error handling for decoding failures
            Console.WriteLine($"Error reading barcode: {ex.Message}");
        }

        // -------------------- Cleanup Temporary Files --------------------
        try
        {
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup
        }
    }
}