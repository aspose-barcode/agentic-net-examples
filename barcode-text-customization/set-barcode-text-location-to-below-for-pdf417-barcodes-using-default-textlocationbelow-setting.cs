// Title: Set PDF417 barcode text location to below
// Description: Demonstrates setting the human‑readable text position beneath a PDF417 barcode and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as rows, X‑dimension, and text location using the BarcodeGenerator class. Typical use cases include creating printable barcodes with readable text positioned according to layout requirements. Developers often need to adjust CodeTextParameters.Location to control where the text appears relative to the barcode.
// Prompt: Set barcode text location to below for PDF417 barcodes, using the default TextLocation.Below setting.
// Tags: pdf417, textlocation, below, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a PDF417 barcode with the text displayed below the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program.
    /// </summary>
    static void Main()
    {
        // Build the output directory path relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Create the directory if it does not already exist.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full file path for the generated PNG image.
        string outputPath = Path.Combine(outputDir, "Pdf417_Below.png");

        // Initialize the barcode generator for PDF417 symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleCodeText"))
        {
            // Set specific PDF417 parameters: number of rows and X‑dimension in pixels.
            generator.Parameters.Barcode.Pdf417.Rows = 12;
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Position the human‑readable text below the barcode (default setting).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Save the generated barcode image to the specified path in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}