// Title: Right-align human‑readable text for ITF‑14 barcode
// Description: Demonstrates how to generate an ITF‑14 barcode and align its human‑readable text to the right.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to customize barcode appearance. Typical scenarios include creating product packaging barcodes where text positioning matters. Developers often need to control text location, alignment, and output format when generating barcodes programmatically.
// Prompt: Right-align barcode text for ITF‑14 barcodes by setting CodetextParameters.Alignment = TextAlignment.Right.
// Tags: itf-14, barcode, text alignment, right align, aspose.barcode, generation, png, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates right‑aligning the human‑readable text of an ITF‑14 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an ITF‑14 barcode, sets text location and alignment, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define a sample ITF‑14 barcode value (14 digits)
        const string codeText = "12345678901231";

        // Initialize the barcode generator for the ITF‑14 symbology with the provided code text
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // Position the human‑readable text below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Align the human‑readable text to the right side
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Specify the output file path and save the barcode as a PNG image
            const string outputPath = "itf14_right_aligned.png";
            generator.Save(outputPath);

            // Inform the user where the barcode image was saved
            Console.WriteLine($"Barcode saved to {outputPath}");
        }
    }
}