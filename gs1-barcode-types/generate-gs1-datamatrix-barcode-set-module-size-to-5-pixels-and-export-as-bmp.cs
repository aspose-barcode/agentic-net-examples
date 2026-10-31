// Title: Generate GS1 DataMatrix Barcode and Save as BMP
// Description: This example creates a GS1 DataMatrix barcode with a specified module size and saves it as a BMP image file.
// Category-Description: Demonstrates Aspose.BarCode generation of GS1 DataMatrix symbology, configuring barcode parameters such as XDimension (module size), and exporting the result to a bitmap image. Typical for developers needing to embed GS1-compliant DataMatrix codes in packaging, labeling, or inventory systems, using classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat.
// Prompt: Generate a GS1 DataMatrix barcode, set module size to 5 pixels, and export as BMP.
// Tags: gs1, datamatrix, barcode, generation, bmp, module-size, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 DataMatrix barcode and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the barcode, configures its module size, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1_datamatrix.bmp");

        // Initialize the barcode generator with GS1 DataMatrix symbology and the required data string.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1DataMatrix, "(01)12345678901231"))
        {
            // Set the module (X) dimension to 5 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;

            // Save the generated barcode as a BMP image.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 DataMatrix barcode saved to: {outputPath}");
    }
}