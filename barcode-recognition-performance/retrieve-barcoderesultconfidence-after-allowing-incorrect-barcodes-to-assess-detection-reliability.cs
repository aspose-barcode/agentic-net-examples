// Title: Retrieve Barcode Confidence with AllowIncorrectBarcodes Setting
// Description: Demonstrates how to read a Code128 barcode and obtain the Confidence value while toggling the AllowIncorrectBarcodes quality setting.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader, QualitySettings, and BarCodeResult. Developers often need to assess detection reliability, especially when allowing imperfect barcodes, by examining the Confidence metric returned after decoding.
// Prompt: Retrieve BarCodeResult.Confidence after allowing incorrect barcodes to assess detection reliability.
// Tags: barcode symbology, reading, confidence, allowincorrectbarcodes, aspose.barcode, code128, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode, reads it twice with different
/// AllowIncorrectBarcodes settings, and outputs the detection confidence for each read.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it with AllowIncorrectBarcodes set to false and true,
    /// prints the type, text, and confidence of each detected barcode, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode with AllowIncorrectBarcodes set to false
        // ------------------------------------------------------------
        Console.WriteLine("Reading with AllowIncorrectBarcodes = false");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Disallow detection of barcodes that do not meet strict quality criteria
            reader.QualitySettings.AllowIncorrectBarcodes = false;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output each result's type, text, and confidence
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
            }
        }

        // ------------------------------------------------------------
        // Read the same barcode with AllowIncorrectBarcodes set to true
        // ------------------------------------------------------------
        Console.WriteLine("Reading with AllowIncorrectBarcodes = true");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Permit detection of barcodes that may have minor quality issues
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output each result's type, text, and confidence
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Confidence: {result.Confidence}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}