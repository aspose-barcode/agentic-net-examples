// Title: Deactivate UseMinimalXDimension after barcode processing
// Description: Demonstrates generating a Code128 barcode, reading it with minimal X-dimension mode, then disabling that mode to restore default element size handling.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to configure XDimension settings using the QualitySettings API, a common requirement when precise barcode sizing is needed for scanning devices. Developers often generate barcodes, adjust XDimension for minimal width, and later revert to normal handling for subsequent reads.
// Prompt: Deactivate UseMinimalXDimension after processing to restore default element size handling.
// Tags: barcode, code128, generation, recognition, xdimension, minimalxdimension, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with minimal X-dimension handling,
/// then deactivates that mode to use the default X-dimension processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, two-stage reading, and cleanup.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode with a specific X-dimension
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeDemo"))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // First read: enable UseMinimalXDimension to allow smaller element sizes
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Read with UseMinimalXDimension: {results.Length} barcode(s) found.");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Deactivate UseMinimalXDimension to restore default X-dimension handling
            reader.QualitySettings.XDimension = XDimensionMode.Normal;

            // Second read: use default X-dimension settings
            BarCodeResult[] defaultResults = reader.ReadBarCodes();
            Console.WriteLine($"Read after deactivating UseMinimalXDimension: {defaultResults.Length} barcode(s) found.");
            foreach (var result in defaultResults)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
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
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}