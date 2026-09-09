// Title: Demonstrate toggling UseMinimalXDimension in barcode reading
// Description: Generates a Code128 barcode, reads it with UseMinimalXDimension enabled, then disables it to show default element size handling.
// Category-Description: This example belongs to the Aspose.BarCode reading operations category. It showcases the use of BarCodeReader, QualitySettings, and XDimensionMode to control barcode element sizing during recognition. Developers often need to adjust XDimension for minimal size detection or revert to normal handling when processing multiple scans.
// Prompt: Deactivate UseMinimalXDimension after processing to restore default element size handling.
// Tags: barcode symbology, reading, xdimension, minimalxdimension, aspose.barcode, png, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that demonstrates how to enable and then deactivate the
/// UseMinimalXDimension mode when reading a barcode with Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it twice
    /// (first with UseMinimalXDimension enabled, then with normal settings),
    /// and outputs the results to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "tempBarcode.png");

        // Generate a simple Code128 barcode and save it as a PNG file.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully.
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Open the barcode image for reading.
        using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            // Activate UseMinimalXDimension mode to allow detection of very small bars.
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform the first read with UseMinimalXDimension enabled.
            BarCodeResult[] firstResults = reader.ReadBarCodes();
            Console.WriteLine($"First read (UseMinimalXDimension) count: {firstResults.Length}");
            foreach (BarCodeResult result in firstResults)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Deactivate UseMinimalXDimension by restoring the default (Normal) mode.
            reader.QualitySettings.XDimension = XDimensionMode.Normal;

            // Perform the second read with normal XDimension handling.
            BarCodeResult[] secondResults = reader.ReadBarCodes();
            Console.WriteLine($"Second read (Normal) count: {secondResults.Length}");
            foreach (BarCodeResult result in secondResults)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up the temporary barcode image file.
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}