// Title: Generate DotCode barcode with numeric data and save as BMP
// Description: Demonstrates creating a DotCode barcode using Aspose.BarCode with a numeric CodeText and saving the result as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DotCode to produce 2‑D barcodes. Typical use cases include encoding numeric identifiers for inventory, tracking, or authentication purposes, where developers need to generate and export barcode images in common formats such as BMP. The snippet shows the essential steps—initializing the generator, specifying the symbology and data, and saving the image—useful for quick integration in .NET applications.
// Prompt: Generate a DotCode barcode with default settings for numeric CodeText and save as BMP image.
// Tags: dotcode, barcode, generation, bmp, aspose.barcode, csharp, encode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DotCode barcode with numeric content
/// and saves it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "DotCodeNumeric.bmp");
        // Numeric data to encode in the barcode.
        string codeText = "1234567890";

        // Create a BarcodeGenerator for DotCode symbology with the specified code text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Save the generated barcode image as BMP.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"DotCode barcode saved to: {outputPath}");
    }
}