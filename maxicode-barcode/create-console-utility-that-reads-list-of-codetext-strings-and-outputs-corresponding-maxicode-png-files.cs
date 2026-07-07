// Title: Generate MaxiCode PNG files from a list of codetext strings
// Description: Demonstrates how to create MaxiCode barcodes and save them as PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of EncodeTypes.MaxiCode with the BarcodeGenerator class. Developers often need to produce MaxiCode symbols for shipping, logistics, and tracking applications; this snippet illustrates typical setup, resolution configuration, and image export to PNG format, serving as a reference for similar console utilities.
// Prompt: Create a console utility that reads a list of codetext strings and outputs corresponding MaxiCode PNG files.
// Tags: maxicode, barcode generation, png output, aspose.barcode, console utility, encode types

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Console application that generates MaxiCode barcodes from predefined codetext strings
/// and saves each barcode as a PNG image file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a collection of codetext strings,
    /// creates a MaxiCode barcode for each, and writes the resulting PNG file to disk.
    /// </summary>
    static void Main()
    {
        // Define a sample list of MaxiCode codetext strings.
        // In a real scenario these could be read from a file, database, or user input.
        string[] codetexts = new[]
        {
            "524032140056999Test message", // Mode 2 example
            "B1050 056999Another message", // Mode 3 example (space separates postal code)
            "Sample MaxiCode Text 1",
            "Sample MaxiCode Text 2",
            "Sample MaxiCode Text 3"
        };

        // Iterate through each codetext, generate a barcode, and save it as a PNG file.
        for (int i = 0; i < codetexts.Length; i++)
        {
            string text = codetexts[i];
            string fileName = $"maxicode_{i + 1}.png";

            // Create a BarcodeGenerator for MaxiCode using the current codetext.
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, text))
            {
                // Optional: set the image resolution (dots per inch) if higher quality is required.
                generator.Parameters.Resolution = 300;

                // Save the generated barcode image in PNG format.
                generator.Save(fileName, BarCodeImageFormat.Png);
            }

            // Output a confirmation message to the console.
            Console.WriteLine($"Generated {fileName} for codetext: {text}");
        }
    }
}