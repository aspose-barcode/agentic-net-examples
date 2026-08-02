// Title: Barcode generation and recognition with UseMinimalXDimension
// Description: Demonstrates generating a Code128 barcode with a small XDimension and recognizing it using default settings and the UseMinimalXDimension mode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the BarcodeGenerator, BarCodeReader, and QualitySettings classes. It illustrates typical scenarios where developers need to create barcodes with fine module sizes and reliably decode them, especially when minimal XDimension detection is required. Useful for integration testing of barcode pipelines.
// Prompt: Write integration tests confirming barcode recognition succeeds after toggling UseMinimalXDimension correctly.
// Tags: code128, barcode, generation, recognition, minimalxdimension, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode, focusing on the UseMinimalXDimension setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it, and reads it back with default and minimal XDimension settings.
    /// </summary>
    static void Main()
    {
        const string barcodeText = "Test123";
        const string filePath = "barcode.png";

        // Clean up any previous file to ensure a fresh start.
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // ---------- Barcode Generation ----------
        // Create a Code128 barcode with a very small XDimension (module size).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Set the XDimension to 1 point to make minimal XDimension detection relevant.
            generator.Parameters.Barcode.XDimension.Point = 1f;
            // Save the generated barcode as a PNG image.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ---------- Default Recognition (no XDimension mode set) ----------
        using (var readerDefault = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Read all barcodes from the image using default settings.
            var results = readerDefault.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Default recognition: CodeText = '{result.CodeText}'");
                if (result.CodeText != barcodeText)
                {
                    Console.WriteLine("Default recognition failed: decoded text does not match original.");
                }
            }
        }

        // ---------- Recognition with UseMinimalXDimension ----------
        using (var readerMinimal = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Enable the UseMinimalXDimension mode to improve detection of small modules.
            readerMinimal.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Optionally specify the minimal XDimension size (in pixels) expected.
            readerMinimal.QualitySettings.MinimalXDimension = 1f;

            // Read all barcodes from the image using the minimal XDimension settings.
            var results = readerMinimal.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"UseMinimalXDimension recognition: CodeText = '{result.CodeText}'");
                if (result.CodeText != barcodeText)
                {
                    Console.WriteLine("UseMinimalXDimension recognition failed: decoded text does not match original.");
                }
            }
        }

        // Optional clean-up: delete the generated barcode image.
        // File.Delete(filePath);
    }
}