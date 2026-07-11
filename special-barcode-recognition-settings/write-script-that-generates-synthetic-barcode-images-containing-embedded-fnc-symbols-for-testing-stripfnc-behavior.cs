// Title: Generate GS1 Code128 barcode with embedded FNC symbols for StripFNC testing
// Description: This example creates a PNG barcode image containing GS1 FNC symbols and demonstrates reading it with and without stripping those symbols.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition for GS1 Code128 symbology, focusing on the StripFNC setting. It uses BarcodeGenerator, BarCodeReader, and related parameter classes to control appearance and decoding behavior, a common need for developers testing barcode data preprocessing.
// Prompt: Write a script that generates synthetic barcode images containing embedded FNC symbols for testing StripFNC behavior.
// Tags: gs1code128, fnc, stripfnc, barcode generation, barcode recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a GS1 Code128 barcode containing FNC symbols
/// and how to read it with and without stripping those symbols using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, then reads it twice:
    /// once with the default StripFNC behavior and once with StripFNC enabled.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // --------------------------------------------------------------------
        // Define barcode parameters
        // --------------------------------------------------------------------
        string filePath = Path.Combine(outputDir, "gs1code128.png");
        // Sample GS1 Code128 data containing FNC1 (parentheses) delimiters
        string codeText = "(02)04006664241007(37)1(400)7019590754";

        // --------------------------------------------------------------------
        // Generate barcode with embedded FNC characters (GS1 Code128)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Basic appearance settings
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Size settings
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.BarHeight.Point = 50f;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Disable filled bars to keep thin lines
            generator.Parameters.Barcode.FilledBars = false;

            // Prevent exceptions on automatic correction of the code text
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Human‑readable text styling (optional)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Save the generated barcode image as PNG
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {filePath}");

        // --------------------------------------------------------------------
        // Read barcode without stripping FNC characters (default behavior)
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            Console.WriteLine("\nReading without StripFNC (default):");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Read barcode with StripFNC enabled
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            // Enable stripping of FNC symbols during decoding
            reader.BarcodeSettings.StripFNC = true;
            Console.WriteLine("\nReading with StripFNC = true:");
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}