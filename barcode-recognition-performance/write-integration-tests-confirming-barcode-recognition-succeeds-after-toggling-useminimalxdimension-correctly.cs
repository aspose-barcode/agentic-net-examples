// Title: Barcode generation and recognition with XDimensionMode toggle
// Description: Demonstrates creating a Code128 barcode image, then reading it using normal and UseMinimalXDimension XDimension modes to verify successful detection.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to use BarcodeGenerator and BarCodeReader with XDimensionMode settings. Developers often need to adjust XDimension for varying barcode densities; this snippet illustrates typical use cases such as toggling UseMinimalXDimension and setting MinimalXDimension for reliable scanning.
/// Prompt: Write integration tests confirming barcode recognition succeeds after toggling UseMinimalXDimension correctly.
/// Tags: barcode, code128, generation, recognition, xdimension, minimalxdimension, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode and reading it with different XDimension modes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, runs recognition tests, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a Code128 barcode image and save it as PNG
        string barcodePath = Path.Combine(tempFolder, "code128.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeTest123"))
        {
            // Default settings are sufficient; no need to set XDimension here
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Test barcode reading with normal XDimension mode
        ReadAndValidate("Normal", XDimensionMode.Normal, null, barcodePath);

        // Test barcode reading with UseMinimalXDimension mode and a minimal X dimension value
        ReadAndValidate("UseMinimalXDimension", XDimensionMode.UseMinimalXDimension, 1f, barcodePath);

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Reads a barcode image using the specified XDimension mode and optional minimal X dimension,
    /// then outputs the detection results.
    /// </summary>
    /// <param name="modeDescription">Human‑readable description of the XDimension mode being tested.</param>
    /// <param name="xDimMode">The XDimensionMode to apply to the reader's quality settings.</param>
    /// <param name="minimalX">Optional minimal X dimension value; null if not applicable.</param>
    /// <param name="imagePath">Path to the barcode image file.</param>
    static void ReadAndValidate(string modeDescription, XDimensionMode xDimMode, float? minimalX, string imagePath)
    {
        Console.WriteLine($"Reading with XDimension mode: {modeDescription}");
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the selected XDimension mode
            reader.QualitySettings.XDimension = xDimMode;

            // Apply MinimalXDimension if a value is provided
            if (minimalX.HasValue)
            {
                reader.QualitySettings.MinimalXDimension = minimalX.Value;
            }

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output detection outcome
            if (results.Length > 0)
            {
                Console.WriteLine($"Success: {results.Length} barcode(s) detected.");
                foreach (var result in results)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            else
            {
                Console.WriteLine("Failure: No barcodes detected.");
            }
        }
    }
}