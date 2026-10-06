// Title: Generate GS1 DataMatrix Barcode and Save as High‑Resolution TIFF
// Description: Demonstrates creating a GS1 DataMatrix barcode, configuring a square ECC200 version, and exporting it as a 300 dpi TIFF image suitable for print.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.GS1DataMatrix, set resolution, select DataMatrix version and ECC type, and save the result in a TIFF format. Developers working on printing or packaging solutions often need to generate GS1‑compliant DataMatrix symbols with precise dimensions and high‑resolution output.
// Prompt: Create a GS1 DataMatrix barcode, specify square shape, and export as high‑resolution TIFF for printing.
// Tags: gs1, datamatrix, barcode, generation, tiff, resolution, printing, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
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
        // Define the full output file path (GS1DataMatrix.tiff in the current directory)
        string outputPath = Path.Combine(Environment.CurrentDirectory, "GS1DataMatrix.tiff");

        // Ensure the target directory exists; create it if necessary
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // GS1 DataMatrix code text (GTIN‑14 example)
        string codeText = "(01)12345678901231";

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, codeText))
        {
            // Set a high resolution (300 dpi) for print‑ready output
            generator.Parameters.Resolution = 300f;

            // Choose a square ECC200 version (32 × 32 modules) to enforce a square shape
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Optional: disable filled bars for DataMatrix (default is false)
            generator.Parameters.Barcode.FilledBars = false;

            // Save the generated barcode as a high‑resolution TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}