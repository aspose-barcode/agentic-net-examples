// Title: Strip FNC Symbols in Code128 Barcode Decoding
// Description: Demonstrates how to configure BarCodeReader to retain FNC symbols when decoding a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It shows the use of BarcodeGenerator to create a barcode image and BarCodeReader with its BarcodeSettings.StripFNC property to control whether FNC symbols are stripped from the decoded text. Developers working with barcode scanning, especially those needing to preserve special function characters, commonly use these APIs for image generation, format conversion, and accurate data extraction.
// Prompt: Set BarCodeReader.StripFNC to true to retain FNC symbols in decoded results.
// Tags: barcode symbology, code128, stripfnc, decoding, aspose.barcode, generation, reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with StripFNC enabled,
/// and outputs the decoded information to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary barcode image, reads it while preserving FNC symbols,
    /// and cleans up all temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the example files
        string tempDir = Path.Combine(Path.GetTempPath(), "StripFNCExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the full path for the barcode image file
        string barcodePath = Path.Combine(tempDir, "code128.png");

        // -------------------------------------------------
        // Generate a simple Code128 barcode and save as PNG
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode image to the temporary path
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Read the barcode with StripFNC set to true
        // -------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Configure the reader to retain FNC symbols in the decoded text
            reader.BarcodeSettings.StripFNC = true;

            // Iterate through all detected barcodes (only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Clean up temporary files and directory
        // -------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}