// Title: Set PDF417 Barcode Text Location Below
// Description: Demonstrates how to generate a PDF417 barcode with the human‑readable text placed below the symbol using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as EncodeTypes, CodeLocation, rows, and XDimension. Developers creating barcodes for documents, labels, or packaging often need to control the placement of the readable text. The example uses BarcodeGenerator, EncodeTypes, and CodeLocation classes, which are common in barcode creation scenarios.
// Prompt: Set barcode text location to below for PDF417 barcodes, using the default TextLocation.Below setting.
// Tags: pdf417, textlocation, below, barcode, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a PDF417 barcode with the text displayed below the symbol.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a PDF417 barcode, sets the text location to below, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build the full path for the resulting PNG file.
        string outputPath = Path.Combine(outputDir, "Pdf417_Below.png");
        string codeText = "Sample PDF417 Text";

        // Create a barcode generator for PDF417 with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Explicitly set the human‑readable text location to below the barcode (default value).
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Optional: adjust the number of rows and the X‑dimension for visual refinement.
            generator.Parameters.Barcode.Pdf417.Rows = 12;
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}