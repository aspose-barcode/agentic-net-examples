// Title: Detect Large Barcodes in High‑Resolution Scans Using XDimension Settings
// Description: Demonstrates generating a high‑resolution barcode image and configuring QualitySettings to detect large barcodes by setting XDimension mode to Large and MinimalXDimension to 6 pixels.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with QualitySettings for accurate detection in high‑resolution scans. Developers working with barcode imaging often need to adjust XDimension and resolution settings to reliably read large or dense barcodes; this snippet provides a concise reference for those scenarios.
// Prompt: Set QualitySettings.XDimension to 6 pixels for detecting large barcodes in high‑resolution scans.
// Tags: barcode symbology, generation, recognition, qualitysettings, xdimension, highresolution, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a high‑resolution Code128 barcode,
/// then reads it using QualitySettings configured for large barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode,
    /// reads it with adjusted XDimension settings, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the demo files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // --------------------------------------------------------------------
        // Generate a high‑resolution barcode image (300 DPI) and save as PNG
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Resolution = 300; // high DPI for better scan quality
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode with QualitySettings tuned for large barcodes
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure XDimension mode to Large and set minimal element size to 6 pixels
            reader.QualitySettings.XDimension = XDimensionMode.Large;
            reader.QualitySettings.MinimalXDimension = 6f;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results to the console
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Optional cleanup of temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors (e.g., file still in use)
        }
    }
}