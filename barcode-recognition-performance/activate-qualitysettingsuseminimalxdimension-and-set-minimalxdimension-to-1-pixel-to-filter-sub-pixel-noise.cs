// Title: Filter Sub‑Pixel Noise Using Minimal X Dimension in Barcode Recognition
// Description: Demonstrates how to enable the MinimalXDimension quality setting to filter out sub‑pixel noise when reading a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings to improve read accuracy. Developers often need to fine‑tune recognition parameters such as X‑dimension to handle low‑resolution or noisy images; this snippet illustrates a common approach for filtering sub‑pixel noise.
// Prompt: Activate QualitySettings.UseMinimalXDimension and set MinimalXDimension to 1 pixel to filter sub‑pixel noise.
// Tags: barcode symbology, generation, recognition, minimalxdimension, qualitysettings, png, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, reads it using minimal X dimension settings,
/// and cleans up temporary files. Demonstrates filtering sub‑pixel noise with QualitySettings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with minimal X dimension
    /// filtering, outputs the results, and removes temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "code128.png");

        // Generate a Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeDemo"))
        {
            // Set the X dimension (module width) for the generated barcode
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode using minimal X dimension settings to filter sub‑pixel noise
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Enable the minimal X dimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Define the minimal X dimension (in points) to be considered during recognition
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}