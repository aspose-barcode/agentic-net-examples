// Title: Generate Code128 Barcode with Embedded FNC Symbols and Demonstrate StripFNC Behavior
// Description: The example creates a Code128 barcode containing FNC1‑FNC4 characters, saves it as PNG, and then reads it twice—once preserving the FNC symbols and once stripping them—to illustrate the StripFNC setting.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator for creating barcodes, the BarCodeReader for decoding, and the StripFNC property of BarcodeSettings to control whether function characters (FNC) are retained. Developers working with Code128 or other symbologies that support FNC symbols use this pattern to test encoding and decoding scenarios, especially when validating data preprocessing pipelines.
// Prompt: Write a script that generates synthetic barcode images containing embedded FNC symbols for testing StripFNC behavior.
// Tags: code128, fnc symbols, stripfnc, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with embedded FNC symbols and reading it with different StripFNC settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates the barcode, saves it, and reads it with StripFNC false and true.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "FncBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "Code128FNC.png");

        // Define FNC characters (ASCII 200‑203) used by Code128
        const char FNC1 = (char)200;
        const char FNC2 = (char)201;
        const char FNC3 = (char)202;
        const char FNC4 = (char)203;

        // Build the barcode text with embedded FNC symbols
        string codeText = "Aspose" + FNC1 + FNC2 + FNC3 + FNC4;

        // Generate the barcode image and save it as PNG
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2f; // Set module width for better readability
            gen.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated at: " + barcodePath);
        Console.WriteLine();

        // -----------------------------------------------------------------
        // Read the barcode with StripFNC = false (retain FNC symbols)
        // -----------------------------------------------------------------
        Console.WriteLine("Read with StripFNC = false:");
        BaseDecodeType decodeType = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.StripFNC = false; // Preserve function characters
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        Console.WriteLine();

        // -----------------------------------------------------------------
        // Read the barcode with StripFNC = true (remove FNC symbols)
        // -----------------------------------------------------------------
        Console.WriteLine("Read with StripFNC = true:");
        using (BarCodeReader reader = new BarCodeReader(barcodePath, decodeType))
        {
            reader.BarcodeSettings.StripFNC = true; // Strip function characters from the result
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}