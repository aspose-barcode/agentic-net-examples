// Title: Demonstrate toggling UseMinimalXDimension in barcode reading
// Description: Shows how to enable UseMinimalXDimension for barcode reading, then reset to normal to restore default element size handling.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using BarcodeGenerator to create a barcode image and BarCodeReader with QualitySettings to control X-dimension handling. Developers often need to adjust X-dimension for precise scanning or to revert to default behavior after custom processing. The key API classes shown are BarcodeGenerator, BarCodeReader, and QualitySettings, useful for creating, reading, and fine‑tuning barcode images.
// Prompt: Deactivate UseMinimalXDimension after processing to restore default element size handling.
// Tags: barcode, code128, generation, recognition, useminimalxdimension, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it with
/// UseMinimalXDimension enabled, then deactivates the setting to
/// restore normal X‑dimension handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation,
    /// reading with custom X‑dimension settings, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode and save it as a PNG file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode with UseMinimalXDimension enabled
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable minimal X‑dimension mode and set a custom minimal value
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform the read operation under the custom setting
            var resultsWithMinimal = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read with UseMinimalXDimension: {resultsWithMinimal.Length}");
            foreach (var result in resultsWithMinimal)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Deactivate UseMinimalXDimension by resetting to Normal mode
            reader.QualitySettings.XDimension = XDimensionMode.Normal;

            // Perform the read operation again with default X‑dimension handling
            var resultsAfterReset = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read after deactivating UseMinimalXDimension: {resultsAfterReset.Length}");
            foreach (var result in resultsAfterReset)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
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