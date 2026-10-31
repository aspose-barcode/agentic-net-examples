// Title: Generate RM4SCC Postal Barcode with Empty Bars
// Description: Demonstrates creating an RM4SCC 2‑state postal barcode and disabling bar filling.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as X‑Dimension and FilledBars using the BarcodeGenerator class. Typical use cases include generating postal barcodes for mail sorting where empty bars are required. Developers often need to customize visual appearance and export formats for integration into document workflows.
// Prompt: Generate an RM4SCC 2‑state postal barcode and disable bar filling using FilledBars false.
// Tags: rm4scc, barcode generation, png, filledbars, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates an RM4SCC barcode with empty (unfilled) bars
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates the barcode, configures visual parameters, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working folder.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Build the full file path for the resulting PNG image.
        string outputPath = Path.Combine(outputDir, "RM4SCC_EmptyBars.png");
        // The data to encode in the barcode.
        string codeText = "123456ASPOSE";

        // Initialize the barcode generator with the RM4SCC symbology and the text to encode.
        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Set the X-dimension (module width) to 4 pixels for better visibility.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Disable filling of the bars; the barcode will be rendered with empty bars.
            generator.Parameters.Barcode.FilledBars = false;
            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}