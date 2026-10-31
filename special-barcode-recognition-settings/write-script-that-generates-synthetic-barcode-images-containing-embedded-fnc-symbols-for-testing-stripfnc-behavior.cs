// Title: Generate Code128 barcode with embedded FNC symbols and demonstrate StripFNC behavior
// Description: The example creates a Code128 barcode containing FNC1, FNC2, and FNC3 symbols, saves it as PNG, and reads it twice—once preserving the FNC symbols and once stripping them—to illustrate the StripFNC setting.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader with BarcodeSettings.StripFNC for controlling whether functional characters (FNC) are retained during decoding. Developers working with Code128 or other symbologies that support FNC symbols use this pattern to test and validate FNC handling in scanning scenarios.
// Prompt: Write a script that generates synthetic barcode images containing embedded FNC symbols for testing StripFNC behavior.
// Tags: code128, fnc symbols, stripfnc, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with embedded FNC symbols,
/// save it as an image, and read it with different StripFNC settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, reads it with StripFNC false and true,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "FncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(outputFolder, "Code128FNC.png");

        // Prepare FNC symbols (Unicode private use area) and compose the barcode text
        string fnc1 = "\u00F1";
        string fnc2 = "\u00F2";
        string fnc3 = "\u00F3";
        string codeText = "Aspose" + fnc1 + fnc2 + fnc3;

        // Generate a Code128 barcode containing the FNC symbols and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2; // Set module width for better readability
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode while preserving FNC symbols (StripFNC = false)
        // --------------------------------------------------------------------
        Console.WriteLine("Read with StripFNC = false:");
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.StripFNC = false; // Keep FNC characters in the result
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Read the same barcode while stripping FNC symbols (StripFNC = true)
        // --------------------------------------------------------------------
        Console.WriteLine("Read with StripFNC = true:");
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.StripFNC = true; // Remove FNC characters from the result
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Optional cleanup of temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(outputFolder))
                Directory.Delete(outputFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup
        }
    }
}