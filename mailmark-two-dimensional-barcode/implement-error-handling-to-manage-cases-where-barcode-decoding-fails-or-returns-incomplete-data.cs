// Title: Generate and Decode a Code128 Barcode with Error Handling
// Description: This example creates a Code128 barcode image, saves it to a temporary location, and then decodes it while handling possible errors such as missing files or incomplete data.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, covering BarcodeGenerator, BarCodeReader, and related settings. Useful for developers needing to produce barcodes and reliably extract their data, handling common failure scenarios like unreadable images, checksum validation, and missing code text. Part of a collection of barcode processing examples for .NET.
// Prompt: Implement error handling to manage cases where barcode decoding fails or returns incomplete data.
// Tags: code128, barcode generation, barcode decoding, error handling, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it, and decoding it with robust error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // Generate a barcode image and handle any generation errors
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set barcode dimensions
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                // Save the barcode as a PNG file
                generator.Save(barcodePath, BarCodeImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating barcode: {ex.Message}");
            return;
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image file not found.");
            return;
        }

        // Decode the barcode with comprehensive error handling
        try
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Enable checksum validation for higher reliability
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                BarCodeResult[] results = reader.ReadBarCodes();

                // Check if any barcodes were detected
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        // Handle cases where the code text is missing or incomplete
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            Console.WriteLine("Barcode detected but code text is incomplete.");
                        }
                        else
                        {
                            Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                            Console.WriteLine($"Code text: {result.CodeText}");
                            Console.WriteLine($"Reading quality: {result.ReadingQuality}");
                            Console.WriteLine($"Confidence: {result.Confidence}");
                        }
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Invalid image file: {ex.Message}");
        }
        catch (RecognitionAbortedException ex)
        {
            Console.WriteLine($"Recognition aborted: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error during decoding: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files and directories, suppressing any cleanup errors
            try
            {
                if (File.Exists(barcodePath))
                    File.Delete(barcodePath);
                if (Directory.Exists(tempDir))
                    Directory.Delete(tempDir, true);
            }
            catch
            {
                // Ignored
            }
        }
    }
}