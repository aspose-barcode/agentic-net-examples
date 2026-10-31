// Title: Generate DataBar Expanded Stacked barcode with three columns and aspect ratio 8, saved as BMP
// Description: This example creates a DataBar Expanded Stacked barcode with three columns and an aspect ratio of eight, then saves it as a BMP image file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.DatabarExpandedStacked. Typical scenarios include encoding GS1-128 data for retail or logistics applications where DataBar symbols are required. Developers often need to customize dimensions, column count, and aspect ratio before exporting to common image formats such as BMP, PNG, or JPEG.
// Prompt: Create DataBar Expanded Stacked barcode with three columns, aspect ratio eight, save BMP image.
// Tags: databar, expanded stacked, barcode, generation, bmp, aspose.barcode, encode types, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataBar Expanded Stacked barcode and saves it as a BMP image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "DatabarExpandedStacked_3Columns_Aspect8.bmp");

        // The data to encode; includes an Application Identifier (01) followed by a 14‑digit GTIN.
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with the desired symbology and data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DatabarExpandedStacked, codeText))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Configure the DataBar symbol to use three columns.
            generator.Parameters.Barcode.DataBar.Columns = 3;

            // Set the aspect ratio of the DataBar symbol to eight.
            generator.Parameters.Barcode.DataBar.AspectRatio = 8;

            // Save the generated barcode as a BMP image to the specified file.
            generator.Save(outputFile, BarCodeImageFormat.Bmp);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputFile}");
    }
}