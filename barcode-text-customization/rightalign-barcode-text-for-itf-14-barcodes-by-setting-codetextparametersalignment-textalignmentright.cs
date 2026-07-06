// Title: Right-align text for ITF‑14 barcode
// Description: Demonstrates how to right‑align the human‑readable text of an ITF‑14 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on text formatting options. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters.Alignment to control human‑readable text placement. Developers often need to adjust text alignment for compliance with labeling standards or aesthetic requirements.
// Prompt: Right-align barcode text for ITF‑14 barcodes by setting CodetextParameters.Alignment = TextAlignment.Right.
// Tags: itf-14, text alignment, barcode generation, png output, aspose.barcode, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates an ITF‑14 barcode with right‑aligned human‑readable text and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies right alignment to the text, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for ITF‑14 symbology with sample numeric data.
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, "1234567890123"))
        {
            // Set the text alignment to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode image to a PNG file.
            generator.Save("itf14_right_aligned.png");
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine("ITF‑14 barcode with right‑aligned text saved as 'itf14_right_aligned.png'.");
    }
}