// Title: Generate GS1 DataMatrix Barcode with ECC200 and Save as PNG
// Description: Demonstrates how to create a GS1 DataMatrix barcode using Aspose.BarCode, set the error correction level to ECC200, and export the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on DataMatrix symbology with GS1 formatting. It showcases the use of BarcodeGenerator, EncodeTypes, and DataMatrixEccType to configure error correction and image dimensions. Developers commonly use these APIs to produce machine‑readable GS1 DataMatrix codes for product identification, inventory, and logistics, and to export them in common image formats.
// Prompt: Generate a GS1 DataMatrix barcode with error correction level 200 and export as PNG.
// Tags: datamatrix, gs1, barcode, generation, png, ecc200, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode with ECC200 error correction
/// and saves it as a PNG image using the Aspose.BarCode library.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare the output directory where the PNG file will be saved.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "gs1_datamatrix.png");

        // --------------------------------------------------------------------
        // Resolve the GS1 DataMatrix symbology from the EncodeTypes enumeration.
        // --------------------------------------------------------------------
        var field = typeof(EncodeTypes).GetField("GS1DataMatrix");
        if (field == null)
        {
            Console.WriteLine("EncodeTypes does not contain GS1DataMatrix symbology.");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // --------------------------------------------------------------------
        // Create a BarcodeGenerator for the GS1 DataMatrix with the sample data.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(encodeType, "(01)12345678901231"))
        {
            // Set the error correction level to ECC200 for DataMatrix.
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Optional: adjust the module (pixel) size of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}