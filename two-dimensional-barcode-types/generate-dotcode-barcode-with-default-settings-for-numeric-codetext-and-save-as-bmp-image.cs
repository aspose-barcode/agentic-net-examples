// Title: Generate DotCode barcode and save as BMP image
// Description: Demonstrates creating a DotCode barcode with numeric data using Aspose.BarCode and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DotCode to produce barcodes. Typical use cases include encoding numeric identifiers for inventory, tracking, or labeling systems, where developers need to generate and export barcode images in various formats such as BMP, PNG, or JPEG.
// Prompt: Generate a DotCode barcode with default settings for numeric CodeText and save as BMP image.
// Tags: dotcode, barcode, generation, bmp, aspose.barcode, encode, imageformat

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a DotCode barcode from numeric text and saves it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output BMP file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DotCodeNumeric.bmp");

        // Set the numeric data to be encoded in the barcode.
        string codeText = "123456";

        // Initialize the barcode generator with DotCode symbology and the specified code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Save the generated barcode image in BMP format to the output path.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DotCode barcode saved to: {outputPath}");
    }
}