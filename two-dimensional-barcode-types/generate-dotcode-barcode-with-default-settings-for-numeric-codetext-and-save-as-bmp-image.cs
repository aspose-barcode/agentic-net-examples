// Title: Generate DotCode Barcode and Save as BMP
// Description: Demonstrates creating a DotCode barcode with numeric data using Aspose.BarCode and saving it as a BMP image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DotCode to produce 2‑D barcodes. Typical use cases include encoding numeric identifiers for inventory, tracking, or authentication, where developers need to generate and export barcode images in various formats such as BMP, PNG, or JPEG.
// Prompt: Generate a DotCode barcode with default settings for numeric CodeText and save as BMP image.
// Tags: dotcode, barcode, generation, bmp, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DotCode barcode with numeric text and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file name (saved in the program's working directory).
        string outputPath = "dotcode.bmp";

        // Initialize a BarcodeGenerator for DotCode symbology with numeric CodeText.
        // The constructor takes the symbology type and the text to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, "1234567890"))
        {
            // Save the generated barcode as a BMP image using default settings.
            generator.Save(outputPath, BarCodeImageFormat.Bmp);
        }

        // Notify the user that the image has been created.
        Console.WriteLine($"DotCode barcode saved to '{outputPath}'.");
    }
}