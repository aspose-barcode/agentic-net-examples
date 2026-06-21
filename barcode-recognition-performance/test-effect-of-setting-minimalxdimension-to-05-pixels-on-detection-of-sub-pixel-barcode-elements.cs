using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a sub‑pixel Code128 barcode and reading it with
/// default and custom quality settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it with different settings, and cleans up.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary file path for the barcode image.
        // ------------------------------------------------------------
        string tempDir = Path.GetTempPath();
        string barcodePath = Path.Combine(tempDir, "subpixel_barcode.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode with a very small XDimension
        // (sub‑pixel size) and save it to the temporary file.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set XDimension to 0.5 points (≈0.5 pixel at 72 DPI).
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Save the generated barcode image to the specified path.
            generator.Save(barcodePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using the default quality settings.
        // ------------------------------------------------------------
        Console.WriteLine("Reading with default QualitySettings:");
        using (var readerDefault = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in readerDefault.ReadBarCodes())
            {
                Console.WriteLine($"  Detected: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Read the barcode with MinimalXDimension set to 0.5 pixels
        // to allow recognition of sub‑pixel elements.
        // ------------------------------------------------------------
        Console.WriteLine("Reading with MinimalXDimension = 0.5 pixels:");
        using (var readerCustom = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Configure recognition to accept sub‑pixel elements.
            readerCustom.QualitySettings.MinimalXDimension = 0.5f;

            foreach (var result in readerCustom.ReadBarCodes())
            {
                Console.WriteLine($"  Detected: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary image file.
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}