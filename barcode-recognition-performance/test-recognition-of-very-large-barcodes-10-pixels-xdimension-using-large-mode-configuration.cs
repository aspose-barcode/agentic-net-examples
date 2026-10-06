// Title: Large XDimension Barcode Generation and Recognition Example
// Description: Demonstrates generating a Code128 barcode with an XDimension larger than 10 pixels using Large mode, then recognizing it.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating high‑resolution barcodes and BarCodeReader for decoding them, highlighting the XDimensionMode.Large setting. Developers working with large‑format barcodes, such as those required for printing on large surfaces or high‑density scanning, will find this pattern useful.
// Prompt: Test recognition of very large barcodes (>10 pixels XDimension) using Large mode configuration.
// Tags: barcode symbology, generation, recognition, large xdimension, code128, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a Code128 barcode with a large XDimension and then reads it back using Large mode settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode image,
    /// reads the barcode using large XDimension mode, outputs the results, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "LargeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode content and output file path
        string barcodeText = "LargeBarcodeTest1234567890";
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a barcode with XDimension set to 12 points (>10 pixels) in Large mode
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Parameters.Barcode.XDimension.Point = 12f; // XDimension > 10 pixels
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Recognize the generated barcode using Large XDimension mode
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.QualitySettings.XDimension = XDimensionMode.Large;
            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}