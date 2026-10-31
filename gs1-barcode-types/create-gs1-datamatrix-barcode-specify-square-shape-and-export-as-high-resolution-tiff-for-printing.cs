// Title: Generate a GS1 DataMatrix barcode and save as high‑resolution TIFF
// Description: Demonstrates creating a GS1 DataMatrix barcode with a square shape, setting printing‑ready resolution, and exporting it to a TIFF image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as symbology, version, module size, colors, and image resolution using the BarcodeGenerator class. Typical use cases include generating machine‑readable GS1 DataMatrix codes for product labeling and printing high‑quality images for packaging. Developers often need to customize barcode appearance and output format for integration into automated printing workflows.
// Prompt: Create a GS1 DataMatrix barcode, specify square shape, and export as high‑resolution TIFF for printing.
// Tags: gs1datamatrix, datamatrix, barcode, generation, tiff, highresolution, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode and saving it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, configures its appearance and resolution, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1_datamatrix.tiff");
        // GS1 DataMatrix payload (Application Identifier 01 with GTIN)
        string codeText = "(01)01234567890128";

        // Initialize the barcode generator with GS1 DataMatrix symbology and the payload
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set image resolution to 300 DPI for high‑quality printing
            generator.Parameters.Resolution = 300f;
            // Choose a square DataMatrix version (32x32 modules)
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            // Define the size of a single module (2 points)
            generator.Parameters.Barcode.XDimension.Point = 2f;
            // Set barcode foreground and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}