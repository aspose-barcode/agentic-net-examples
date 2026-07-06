// Title: Generate GS1 Code 128 barcode with custom human‑readable text
// Description: Demonstrates creating a GS1 Code 128 barcode, adding centered human‑readable text below, and saving it as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1Code128, configure CodeTextParameters, and apply AutoSizeMode. Developers often need to produce GS1‑compliant barcodes for product identification, customize human‑readable text appearance, and export to common image formats like JPEG.
// Prompt: Generate a GS1 Code 128 barcode, embed custom human‑readable text below, and save as JPEG.
// Tags: gs1, code128, barcode, generation, human‑readable, jpeg, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program demonstrating GS1 Code 128 barcode generation with custom human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures text, and saves as JPEG.
    /// </summary>
    static void Main()
    {
        // Define GS1 Code 128 data: (01) – GTIN, (21) – Serial Number
        const string gs1Data = "(01)12345678901231(21)ABC123";

        // Initialize the barcode generator for GS1 Code 128 with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1Data))
        {
            // Position human‑readable text below the barcode and center it horizontally
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Set the font family and size for the human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Enable automatic sizing using interpolation (no explicit bar height required)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define output file path and save the barcode as a JPEG image
            const string outputPath = "gs1_code128.jpg";
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine("GS1 Code 128 barcode saved successfully.");
    }
}