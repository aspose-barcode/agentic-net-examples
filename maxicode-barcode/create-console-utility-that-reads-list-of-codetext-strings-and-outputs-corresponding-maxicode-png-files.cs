// Title: Generate MaxiCode PNG files from a list of strings
// Description: Demonstrates how to create MaxiCode barcodes and save them as PNG images using Aspose.BarCode. The utility writes each barcode to a temporary folder.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It shows how to configure barcode parameters, such as X‑dimension, and how to save the generated images in PNG format. Developers working with shipping or logistics solutions often need to produce MaxiCode symbols for package tracking, and this snippet illustrates the typical workflow using the BarcodeGenerator class and related parameter objects.
// Prompt: Create a console utility that reads a list of codetext strings and outputs corresponding MaxiCode PNG files.
// Tags: maxicode, barcode generation, png output, aspnet.barcode, encode types, console utility

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console utility that generates MaxiCode barcodes from predefined text strings
/// and saves each barcode as a PNG file in a temporary directory.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a list of codetext values,
    /// creates a MaxiCode barcode for each, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output folder for the generated PNG files
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // List of sample codetext strings to be encoded as MaxiCode barcodes
        List<string> codetexts = new List<string>
        {
            "Sample1",
            "Åspóse.Barcóde©",
            "123456789"
        };

        int index = 1;
        // Generate a barcode image for each codetext entry
        foreach (string ct in codetexts)
        {
            // Build the full file path for the current PNG output
            string filePath = Path.Combine(outputDir, $"MaxiCode_{index}.png");

            // Initialize the barcode generator with MaxiCode symbology and the current text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, ct))
            {
                // Set the X-dimension (module size) in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 15f;

                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Inform the user about the generated file location
            Console.WriteLine($"Generated: {filePath}");
            index++;
        }
    }
}