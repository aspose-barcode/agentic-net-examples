// Title: Generate RM4SCC Postal Barcode with Empty Bars
// Description: Demonstrates how to create an RM4SCC 2‑state postal barcode, disable bar filling, and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of EncodeTypes, BarcodeGenerator, and related parameter settings. Developers often need to produce postal barcodes (e.g., RM4SCC) for mailing applications, customizing visual aspects such as bar filling and module size. The snippet illustrates typical steps: preparing output paths, configuring barcode properties, and exporting the image.
// Prompt: Generate an RM4SCC 2‑state postal barcode and disable bar filling using FilledBars false.
// Tags: rm4scc, postal, barcode, emptybars, filledbars, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating an RM4SCC barcode with empty (unfilled) bars using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "RM4SCC_EmptyBars.png");

        // The data to encode in the RM4SCC barcode.
        string codeText = "123456ASPOSE";

        // Initialize the barcode generator with the RM4SCC symbology and the provided text.
        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Set the module (X) dimension to 4 pixels for better visibility.
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Disable filling of the bars to produce an empty‑bars (2‑state) barcode.
            generator.Parameters.Barcode.FilledBars = false;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}