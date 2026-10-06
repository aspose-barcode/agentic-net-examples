// Title: Change DataMatrix barcode text color to orange
// Description: Demonstrates how to set the code text color of a DataMatrix barcode to orange while preserving the default background.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating customization of barcode appearance using the BarcodeGenerator class. It shows how to modify visual properties such as code text color for DataMatrix symbology, a common requirement when integrating barcodes into branded materials or UI designs. Developers often need to adjust colors without affecting other barcode elements, and this snippet provides a concise reference.
// Prompt: Change only the text color of a DataMatrix barcode to orange while keeping default background.
// Tags: datamatrix, barcode, colortext, orange, aspnet, aspnetcore, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode with the code text colored orange and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies the orange text color, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "datamatrix.png");

        // Initialize the barcode generator for DataMatrix symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleText"))
        {
            // Set only the barcode code text (the human‑readable part) color to orange.
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }
}