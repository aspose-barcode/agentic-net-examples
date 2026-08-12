// Title: Generate DotCode barcode and save as BMP image
// Description: Demonstrates creating a DotCode barcode with numeric data using Aspose.BarCode and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the EncodeTypes enumeration and the BarcodeGenerator class to produce a DotCode symbology image. Typical use cases include embedding compact, high‑density barcodes in documents or packaging. Developers often need to generate barcodes programmatically and export them to common image formats such as BMP, PNG, or JPEG.
// Prompt: Generate a DotCode barcode with default settings for numeric CodeText and save as BMP image.
// Tags: dotcode, barcode, generation, bmp, aspose.barcode, encode, image

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DotCode barcode with numeric content
/// and saves it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name and format.
        string outputPath = "dotcode.bmp";

        // Initialize the barcode generator for DotCode symbology with numeric CodeText.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Save the generated barcode image as BMP using the generator's default settings.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"DotCode barcode saved to {outputPath}");
    }
}