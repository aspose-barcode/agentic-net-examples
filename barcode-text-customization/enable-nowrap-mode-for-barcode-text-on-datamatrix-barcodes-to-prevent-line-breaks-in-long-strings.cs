// Title: Enable NoWrap Mode for DataMatrix Barcode Text
// Description: Demonstrates how to generate a DataMatrix barcode with long code text and prevent line‑breaks by enabling the NoWrap option.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to control human‑readable text rendering for 2‑D symbologies. It uses the BarcodeGenerator class and CodeTextParameters to adjust text location, wrapping, and font. Developers often need to customize barcode captions for readability and layout, especially when dealing with long strings in DataMatrix or QR codes.
/// Prompt: Enable NoWrap mode for barcode text on DataMatrix barcodes to prevent line breaks in long strings.
// Tags: datamatrix, no-wrap, text, png, barcodegenerator, codetextparameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates enabling NoWrap mode for DataMatrix barcode text to keep long strings on a single line.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a DataMatrix barcode with a long code text, disables text wrapping, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "datamatrix.png";

        // Create a long string (200 characters) that would normally cause text wrapping.
        string codeText = new string('A', 200);

        // Initialize a DataMatrix barcode generator with the long code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Position the human‑readable text below the barcode symbol.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Disable automatic line wrapping so the text stays on a single line.
            generator.Parameters.Barcode.CodeTextParameters.NoWrap = true;

            // Set a readable font size for the caption.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 8f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the full path of the saved barcode image.
        Console.WriteLine($"DataMatrix barcode saved to {Path.GetFullPath(outputPath)}");
    }
}