// Title: Demonstrate using Minimal X Dimension to filter sub‑pixel noise in barcode recognition
// Description: Shows how to generate a Code128 barcode, then read it with QualitySettings configured to use a minimal X dimension of 1 pixel, which helps eliminate sub‑pixel noise during detection.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition settings category. It illustrates configuring the QualitySettings of BarCodeReader, specifically XDimensionMode.UseMinimalXDimension and MinimalXDimension, which are commonly used to improve detection accuracy for low‑resolution images. Developers working with barcode scanning, image preprocessing, or noise reduction can refer to this pattern when optimizing recognition performance.
// Prompt: Activate QualitySettings.UseMinimalXDimension and set MinimalXDimension to 1 pixel to filter sub‑pixel noise.
// Tags: barcode, code128, minimalxdimension, noise-filter, qualitysettings, generation, recognition, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode and reading it with minimal X dimension settings to filter sub‑pixel noise.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a sample Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize the barcode reader with the generated image and specify the expected symbology
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Activate minimal X dimension mode and set the minimal dimension to 1 pixel
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}