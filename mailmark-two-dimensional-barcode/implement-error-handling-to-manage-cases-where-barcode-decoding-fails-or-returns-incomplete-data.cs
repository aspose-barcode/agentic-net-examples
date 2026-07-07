// Title: Barcode Generation and Decoding with Error Handling
// Description: Demonstrates generating a Code128 barcode, saving it to a temporary file, and decoding it while handling possible failures.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to generate barcodes for labeling and later read them from images or scanned documents; this snippet illustrates typical API usage, quality settings, and robust error handling for such scenarios.
// Prompt: Implement error handling to manage cases where barcode decoding fails or returns incomplete data.
// Tags: barcode symbology, generation, recognition, error handling, code128, png, aspose.barcode, qualitysettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode, saves it to a temporary PNG file, and then decodes it
/// while handling possible errors such as missing files, unreadable barcodes, or incomplete data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation, decoding, and cleanup with comprehensive error handling.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // ------------------------------------------------------------
        // Generate a sample barcode (Code128) and save it to the file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Optional: configure generator parameters here if needed
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file not found at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Attempt to read the barcode with error handling
        // ------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Use a high‑performance quality preset for faster processing
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Perform the recognition
                var results = reader.ReadBarCodes();

                // No barcodes detected
                if (results == null || results.Length == 0)
                {
                    Console.WriteLine("No barcode detected in the image.");
                    return;
                }

                // Process each detected barcode
                foreach (var result in results)
                {
                    // Check for missing or empty CodeText (incomplete data)
                    if (string.IsNullOrEmpty(result.CodeText))
                    {
                        Console.WriteLine($"Detected barcode of type '{result.CodeTypeName}' but CodeText is missing or empty.");
                        continue;
                    }

                    // Output basic information about the decoded barcode
                    Console.WriteLine($"Barcode Type   : {result.CodeTypeName}");
                    Console.WriteLine($"Code Text      : {result.CodeText}");
                    Console.WriteLine($"Confidence     : {result.Confidence}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    Console.WriteLine($"Region Angle   : {result.Region.Angle}");
                    Console.WriteLine($"Region Bounds  : {result.Region.Rectangle}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
        catch (Exception ex)
        {
            // General exception handling for unexpected errors during decoding
            Console.WriteLine($"An error occurred while decoding the barcode: {ex.Message}");
        }
        finally
        {
            // ------------------------------------------------------------
            // Clean up the temporary image file
            // ------------------------------------------------------------
            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
            catch
            {
                // Ignored – cleanup failure should not affect program flow
            }
        }
    }
}