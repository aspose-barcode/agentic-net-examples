// Title: Create GS1 DataMatrix barcode with grayscale filter saved as TIFF
// Description: Demonstrates generating a GS1 DataMatrix barcode, applying black‑on‑white grayscale colors, and exporting it to a TIFF image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology configuration and image output. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create GS1 DataMatrix barcodes, set visual properties such as bar and background colors, and save the result in a raster format. Developers working with product identification, inventory, or logistics often need to generate GS1 DataMatrix codes and customize their appearance for printing or digital use.
// Prompt: Create a GS1 DataMatrix barcode, apply a grayscale filter, and save the result as a TIFF image.
// Tags: gs1datamatrix, barcode, generation, grayscale, tiff, aspose.barcode, encode types, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a GS1 DataMatrix barcode with grayscale colors and saving it as a TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name
        string outputPath = "gs1_datamatrix.tiff";

        // GS1 DataMatrix requires an Application Identifier (AI) (01) followed by a 14‑digit GTIN
        string gs1CodeText = "(01)00123456789012";

        // Initialize the barcode generator for GS1 DataMatrix symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, gs1CodeText))
        {
            // Configure the barcode to use black bars on a white background (grayscale)
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Output the full path of the saved image for verification
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}