// Title: Generate DotCode barcode and save as BMP
// Description: Demonstrates creating a DotCode barcode with numeric data using Aspose.BarCode and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DotCode. Typical use cases include generating machine‑readable DotCode symbols for inventory, tracking, or authentication purposes. Developers often need to set the code text, choose a symbology, and export the result to common image formats such as BMP, PNG, or JPEG.
// Prompt: Generate a DotCode barcode with default settings for numeric CodeText and save as BMP image.
// Tags: dotcode, barcode generation, bmp, aspose.barcode, encode types, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DotCode barcode with numeric text and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator for DotCode, saves the image, and writes the output path.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "DotCodeNumeric.bmp");

        // Initialize the barcode generator with DotCode symbology and numeric code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Save the generated barcode as a BMP image
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"DotCode barcode saved to: {outputPath}");
    }
}