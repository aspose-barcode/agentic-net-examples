// Title: Barcode Generation and Recognition with Minimal XDimension Diagnostic Logging
// Description: Demonstrates generating a QR barcode, saving it as PNG, and recognizing it while logging element size information when UseMinimalXDimension is enabled.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to configure AutoSizeMode, XDimension, and QualitySettings for minimal XDimension mode. It shows typical use cases such as creating a barcode image, reading it, and extracting diagnostic information about element sizes—common tasks for developers needing precise control over barcode dimensions.
// Prompt: Create a diagnostic mode that logs each barcode element size when UseMinimalXDimension is active.
// Tags: qr,barcode,generation,recognition,xdimension,diagnostic,aspose.barcode,imagemanipulation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, saving, and recognition with diagnostic logging of element sizes when UseMinimalXDimension is active.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, reads it with minimal XDimension mode, and logs size information.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file path for the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDiag_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Ensure default sizing (AutoSizeMode.None) so XDimension influences size
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = 2f; // example module size
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the generated image to log its pixel dimensions
        using (var bitmap = new Bitmap(barcodePath))
        {
            Console.WriteLine($"Generated barcode image size: {bitmap.Width}x{bitmap.Height} pixels");
        }

        // Read the barcode with UseMinimalXDimension enabled for diagnostic logging
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Enable UseMinimalXDimension mode and set a minimal element size (in pixels)
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f;

            // Log the current recognition settings
            Console.WriteLine($"Recognition XDimension mode: {reader.QualitySettings.XDimension}");
            Console.WriteLine($"Recognition MinimalXDimension: {reader.QualitySettings.MinimalXDimension} pixels");

            // Perform barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected (expected if unsupported or unreadable).");
            }
            else
            {
                // Iterate through detected barcodes and log detailed information
                foreach (var result in results)
                {
                    Console.WriteLine("=== Detected Barcode ===");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    // Log element size information (XDimension and MinimalXDimension) used during recognition
                    Console.WriteLine($"Used XDimension mode: {reader.QualitySettings.XDimension}");
                    Console.WriteLine($"Used MinimalXDimension: {reader.QualitySettings.MinimalXDimension} pixels");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}