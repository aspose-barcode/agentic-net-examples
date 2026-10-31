// Title: Change DataMatrix barcode text color to orange
// Description: Demonstrates how to generate a DataMatrix barcode with orange text while retaining the default white background.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on visual customization of barcode appearance. It shows how to use the BarcodeGenerator class together with CodeTextParameters to modify text color. Developers often need to match branding colors or improve readability, and this snippet illustrates the typical steps for setting text color without affecting other barcode properties.
// Prompt: Change only the text color of a DataMatrix barcode to orange while keeping default background.
// Tags: datamatrix, textcolor, orange, barcode, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a DataMatrix barcode with orange text color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Define the output directory and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated barcode image
        string outPath = Path.Combine(outputDir, "DataMatrix_OrangeText.png");

        // Initialize the barcode generator for DataMatrix with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "ABC123"))
        {
            // Set only the text (code) color to orange; background remains default
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Orange;

            // Save the barcode as a PNG image
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outPath}");
    }
}