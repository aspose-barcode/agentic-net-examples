// Title: Generate GS1 DataMatrix barcode and save as high‑resolution TIFF
// Description: Demonstrates creating a GS1 DataMatrix barcode with a 14‑digit GTIN, forcing a square shape, and exporting it as a 300 dpi TIFF image suitable for printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as resolution, shape, and colors using the BarcodeGenerator class. Typical use cases include producing machine‑readable GS1 DataMatrix symbols for packaging and printing high‑quality images with Aspose.Drawing.Imaging. Developers often need to customize DataMatrix versions, set DPI, and export to formats like TIFF for print workflows.
// Prompt: Create a GS1 DataMatrix barcode, specify square shape, and export as high‑resolution TIFF for printing.
// Tags: gs1 datamatrix, barcode generation, tiff, high resolution, aspose.barcodes, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode and saves it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated TIFF image.
        string outputPath = "gs1_datamatrix.tif";

        // GS1 DataMatrix requires a 14‑digit GTIN wrapped in the (01) Application Identifier.
        string codeText = "(01)00123456789012";

        // Initialize the barcode generator for the GS1 DataMatrix symbology with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set a high resolution (e.g., 300 dpi) for print‑quality output.
            generator.Parameters.Resolution = 300f;

            // Force a square shape by selecting a specific DataMatrix version (32 × 32 modules).
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

            // Optional: define foreground (barcode) and background colors.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a TIFF image using the specified resolution.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user that the barcode image has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to {outputPath}");
    }
}