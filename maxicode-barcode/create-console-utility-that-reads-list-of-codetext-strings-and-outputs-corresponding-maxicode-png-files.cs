// Title: Generate MaxiCode PNG files from a list of codetext strings
// Description: Demonstrates how to create MaxiCode barcodes using Aspose.BarCode and save each as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on the MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce high‑resolution PNG files. Developers often need batch generation of barcodes for inventory, shipping, or tracking systems, and this snippet provides a straightforward template for such tasks.
// Prompt: Create a console utility that reads a list of codetext strings and outputs corresponding MaxiCode PNG files.
// Tags: maxicode, barcode generation, png output, aspose.barcode, console utility

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console utility that generates MaxiCode barcodes from a predefined list of codetext strings
/// and saves each barcode as a PNG file in a temporary output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over the codetext list, creates a MaxiCode
    /// barcode for each entry, and writes the resulting PNG image to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a sample list of codetext strings to be encoded as MaxiCode barcodes.
        List<string> codetexts = new List<string>
        {
            "SampleCodeText1",
            "SampleCodeText2",
            "SampleCodeText3"
        };

        // Create a unique temporary output folder for the generated PNG files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Output folder: " + outputFolder);

        // Iterate through each codetext, generate a barcode, and save it as a PNG file.
        int index = 0;
        foreach (string text in codetexts)
        {
            // Build the full file path for the current barcode image.
            string filePath = Path.Combine(outputFolder, $"MaxiCode_{index}.png");

            // Initialize the barcode generator for MaxiCode with the current codetext.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, text))
            {
                // Optional: adjust the module size (pixel dimension) for better image clarity.
                generator.Parameters.Barcode.XDimension.Pixels = 15f;

                // Save the generated barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated: {filePath}");
            index++;
        }

        Console.WriteLine("All barcodes generated.");
    }
}