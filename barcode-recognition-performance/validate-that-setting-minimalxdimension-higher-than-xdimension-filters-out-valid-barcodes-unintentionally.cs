// Title: Minimal XDimension Filtering Demo
// Description: Demonstrates how setting MinimalXDimension higher than the barcode's XDimension can unintentionally filter out a valid Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for decoding. Developers often need to adjust XDimension settings to handle low‑resolution scans, and this snippet illustrates the impact of MinimalXDimension on detection results.
// Prompt: Validate that setting MinimalXDimension higher than XDimension filters out valid barcodes unintentionally.
// Tags: code128, generation, recognition, png, minimalxdimension, xdimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with default settings,
/// then attempts to read it with a higher MinimalXDimension to demonstrate filtering behavior.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, performs two reads, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image.
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // -------------------------------------------------
        // Generate a Code128 barcode with a specific XDimension.
        // -------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the module size (XDimension) to 2 points.
            // AutoSizeMode defaults to None, so this value is applied directly.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            // Save the barcode image as PNG.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // First read: use default reader settings (should detect the barcode).
        // -------------------------------------------------
        using (BarCodeReader defaultReader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            bool found = false;
            foreach (BarCodeResult result in defaultReader.ReadBarCodes())
            {
                Console.WriteLine($"[Default] Detected barcode: {result.CodeText}");
                found = true;
            }
            if (!found)
            {
                Console.WriteLine("[Default] No barcode detected.");
            }
        }

        // -------------------------------------------------
        // Second read: configure MinimalXDimension higher than the generated XDimension.
        // -------------------------------------------------
        using (BarCodeReader minimalReader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable MinimalXDimension mode.
            minimalReader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Set a threshold of 5 points, which exceeds the generated 2‑point module size.
            minimalReader.QualitySettings.MinimalXDimension = 5f;

            bool found = false;
            foreach (BarCodeResult result in minimalReader.ReadBarCodes())
            {
                Console.WriteLine($"[MinimalXDimension] Detected barcode: {result.CodeText}");
                found = true;
            }
            if (!found)
            {
                Console.WriteLine("[MinimalXDimension] No barcode detected (filtering out valid barcode).");
            }
        }

        // -------------------------------------------------
        // Clean up: delete the temporary barcode image file.
        // -------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Suppress any exceptions during cleanup.
        }
    }
}