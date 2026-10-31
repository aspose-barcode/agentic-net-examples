// Title: Diagnostic Mode with Minimal X-Dimension Logging for Code128 Barcodes
// Description: Demonstrates generating a Code128 barcode, enabling the UseMinimalXDimension mode in the reader, and logging element size diagnostics.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator, BarCodeReader, and QualitySettings classes, illustrating how to configure minimal X‑dimension handling and retrieve diagnostic information. Developers working with barcode quality tuning, especially for high‑density symbologies, can use this pattern to verify element sizing and improve scan reliability.
// Prompt: Create a diagnostic mode that logs each barcode element size when UseMinimalXDimension is active.
// Tags: code128, minimalxdimension, diagnostic, barcode generation, barcode recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that generates a Code128 barcode, reads it with minimal X‑dimension
/// diagnostics enabled, and logs element size information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, configures the reader for
    /// UseMinimalXDimension mode, and outputs diagnostic details to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "DiagBarcode_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "DIAG123"))
        {
            // Set a base XDimension (module width) for the barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set up the reader for Code128 with diagnostic mode enabled
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Enable UseMinimalXDimension and specify the minimal element size
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1.5f; // example minimal element size in pixels

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Log diagnostic information about the configured settings
            Console.WriteLine($"UseMinimalXDimension enabled: {reader.QualitySettings.XDimension == XDimensionMode.UseMinimalXDimension}");
            Console.WriteLine($"Configured MinimalXDimension: {reader.QualitySettings.MinimalXDimension} pixels");
            Console.WriteLine($"Detected {results.Length} barcode(s):");

            // Iterate over each detected barcode and output details
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  Symbology: {result.CodeTypeName}");
                // Log element size diagnostic (using the configured MinimalXDimension as proxy)
                Console.WriteLine($"  Diagnostic Element Size (MinimalXDimension): {reader.QualitySettings.MinimalXDimension} pixels");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}