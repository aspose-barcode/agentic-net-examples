// Title: Generate Code128 barcode with embedded FNC symbols and demonstrate StripFNC option
// Description: Creates a Code128 barcode image containing FNC1, FNC2, and FNC3 characters, then reads it twice to show the effect of the StripFNC setting on decoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on the StripFNC property used to control whether Function (FNC) characters are retained or removed during recognition. Developers testing barcode parsing, data validation, or custom symbology handling often need such examples.
// Prompt: Write a script that generates synthetic barcode images containing embedded FNC symbols for testing StripFNC behavior.
// Tags: barcode symbology, generation, recognition, fnc, stripfnc, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode with embedded FNC symbols
/// and how the StripFNC setting influences the decoded result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with
    /// StripFNC set to false and true, and outputs the decoded values.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary output directory for the generated barcode image.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "FncTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "Code128FNC.png");

        // ---------------------------------------------------------------
        // Build a string that contains FNC1, FNC2, and FNC3 characters.
        // These are represented by Unicode code points 241‑243.
        // ---------------------------------------------------------------
        string fnc1 = ((char)241).ToString(); // FNC1
        string fnc2 = ((char)242).ToString(); // FNC2
        string fnc3 = ((char)243).ToString(); // FNC3

        // ---------------------------------------------------------------
        // Generate a Code128 barcode that embeds the FNC symbols.
        // ---------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose" + fnc1 + fnc2 + fnc3))
        {
            // Set a modest X-dimension for better visibility.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ---------------------------------------------------------------
        // Read the barcode with StripFNC disabled (false) – FNC symbols are retained.
        // ---------------------------------------------------------------
        Console.WriteLine("Read with StripFNC: false");
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // ---------------------------------------------------------------
        // Read the same barcode with StripFNC enabled (true) – FNC symbols are removed.
        // ---------------------------------------------------------------
        Console.WriteLine("Read with StripFNC: true");
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // ---------------------------------------------------------------
        // Optional cleanup of temporary files and directory.
        // ---------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(outputDir);
        }
        catch
        {
            // Suppress any exceptions during cleanup.
        }
    }
}