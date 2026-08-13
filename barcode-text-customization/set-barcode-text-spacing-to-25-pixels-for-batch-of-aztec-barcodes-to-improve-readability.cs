// Title: Set text spacing for batch Aztec barcodes
// Description: Demonstrates how to generate multiple Aztec barcodes with a custom human‑readable text spacing of 2.5 pixels, improving readability of the printed codes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Aztec symbology and visual customization. It showcases the use of BarcodeGenerator, EncodeTypes, and CodeTextParameters to adjust text layout. Developers often need to tweak text spacing, location, and other rendering options when creating batches of barcodes for labeling or packaging applications.
// Prompt: Set barcode text spacing to 2.5 pixels for a batch of Aztec barcodes to improve readability.
// Tags: aztec, barcode, text-spacing, batch, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of Aztec barcodes with custom text spacing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output folder, defines sample texts, and saves each barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare output folder for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "AztecBarcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample code texts for the batch of barcodes
        string[] codeTexts = new string[]
        {
            "ABC123",
            "HelloWorld",
            "1234567890",
            "AztecTest",
            "Sample5"
        };

        // Iterate through each text value and generate an Aztec barcode
        for (int i = 0; i < codeTexts.Length; i++)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Aztec, codeTexts[i]))
            {
                // Set human‑readable text spacing to 2.5 pixels
                generator.Parameters.Barcode.CodeTextParameters.Space.Point = 2.5f;

                // Place the code text below the barcode for better visual separation
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

                // Build the file path and save the barcode as a PNG image
                string filePath = Path.Combine(outputFolder, $"Aztec_{i + 1}.png");
                generator.Save(filePath);
                Console.WriteLine($"Saved: {filePath}");
            }
        }
    }
}