// Title: Minimal XDimension Filtering Demo
// Description: Demonstrates how setting MinimalXDimension higher than the barcode's XDimension can prevent detection of otherwise valid barcodes.
// Prompt: Validate that setting MinimalXDimension higher than XDimension filters out valid barcodes unintentionally.
// Tags: barcode, code128, xdimension, minimalxdimension, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode and shows how MinimalXDimension
/// influences barcode recognition. It first sets MinimalXDimension higher than the
/// actual XDimension (causing the barcode to be missed) and then lower (allowing detection).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, then attempts to read it
    /// with different MinimalXDimension settings to illustrate their effect.
    /// </summary>
    static void Main()
    {
        const string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode with a small XDimension (module size)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set module size to 1 point (XDimension)
            generator.Parameters.Barcode.XDimension.Point = 1f;

            // Disable automatic sizing so the XDimension value is respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the generated barcode image to disk
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Attempt to read the barcode with MinimalXDimension set higher than the actual XDimension
        // Expected outcome: barcode is not detected because the minimal requirement is not met
        // ------------------------------------------------------------
        Console.WriteLine("Reading with MinimalXDimension = 2 (higher than XDimension 1):");
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Instruct the reader to respect MinimalXDimension during detection
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Set MinimalXDimension to a value larger than the barcode's XDimension
            reader.QualitySettings.MinimalXDimension = 2f;

            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected: {result.CodeText}");
                found = true;
            }

            if (!found)
                Console.WriteLine("No barcode detected (as expected).");
        }

        // ------------------------------------------------------------
        // Now read the same image with a lower MinimalXDimension
        // Expected outcome: barcode is detected because the requirement is satisfied
        // ------------------------------------------------------------
        Console.WriteLine("\nReading with MinimalXDimension = 0.5 (lower than XDimension 1):");
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 0.5f;

            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected: {result.CodeText}");
                found = true;
            }

            if (!found)
                Console.WriteLine("No barcode detected (unexpected).");
        }
    }
}