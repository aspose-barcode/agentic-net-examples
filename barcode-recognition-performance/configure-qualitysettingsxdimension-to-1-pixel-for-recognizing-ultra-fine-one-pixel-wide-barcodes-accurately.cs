// Title: Ultra‑fine 1‑pixel barcode generation and recognition using Aspose.BarCode
// Description: Demonstrates how to generate a Code128 barcode with a 1‑pixel XDimension and configure the reader to recognize such ultra‑fine barcodes accurately.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator, BarCodeReader, and QualitySettings to control module size (XDimension) for both creating and decoding barcodes. Developers working with high‑density or low‑resolution barcodes often need to adjust XDimension to ensure reliable detection, especially when dealing with one‑pixel wide modules.
// Prompt: Configure QualitySettings.XDimension to 1 pixel for recognizing ultra‑fine one‑pixel wide barcodes accurately.
// Tags: code128, xdimension, barcode generation, barcode recognition, qualitysettings, ultra‑fine, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a Code128 barcode with a 1‑pixel XDimension
/// and then reads it back using minimal XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a Code128 barcode with XDimension set to 1 pixel (ultra‑fine modules)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 1f; // ultra‑fine 1‑pixel modules
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using minimal XDimension settings
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Configure recognition to treat minimal element size as 1 pixel
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes detected: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Quality: {result.ReadingQuality}");
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}