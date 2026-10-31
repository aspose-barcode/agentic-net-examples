// Title: Large XDimension Barcode Generation and Recognition Example
// Description: Demonstrates generating a Code128 barcode with an XDimension larger than 10 pixels and recognizing it using the Large XDimension mode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to work with high‑resolution barcodes. It uses key API classes such as BarcodeGenerator, BarCodeReader, QualitySettings, and XDimensionMode to create and decode barcodes that require a large XDimension. Developers often need this pattern for printing barcodes on large labels or for scenarios where increased module size improves scan reliability.
/// Prompt: Test recognition of very large barcodes (>10 pixels XDimension) using Large mode configuration.
/// Tags: barcode, code128, large-xdimension, generation, recognition, qualitysettings, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Code128 barcode with a large XDimension and then reads it using
/// the Large XDimension mode to demonstrate handling of very large barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it back, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "largeBarcode.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode with an XDimension greater than 10 pixels.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LargeXDimTest"))
        {
            // Set the XDimension to 12 pixels (large module size).
            generator.Parameters.Barcode.XDimension.Pixels = 12f;
            // Save the barcode as a PNG image to the temporary location.
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the generated barcode using Large XDimension mode.
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(tempPath, decodeType))
        {
            // Configure the reader to treat the barcode as having a large XDimension.
            reader.QualitySettings.XDimension = XDimensionMode.Large;

            // Perform the recognition.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected and their details.
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete temporary file: {ex.Message}");
        }
    }
}