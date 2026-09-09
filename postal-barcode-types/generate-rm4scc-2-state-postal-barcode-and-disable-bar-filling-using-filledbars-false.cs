// Title: Generate RM4SCC barcode with unfilled bars
// Description: Demonstrates creating an RM4SCC 2‑state postal barcode and disabling bar filling.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to configure barcode parameters such as X‑dimension and FilledBars for postal symbologies. It uses BarcodeGenerator and EncodeTypes to produce image files, a common task for developers needing to create printable barcodes for mailing and logistics applications. The snippet illustrates typical steps: setting output folder, configuring parameters, and saving the barcode image.
// Prompt: Generate an RM4SCC 2‑state postal barcode and disable bar filling using FilledBars false.
// Tags: rm4scc, postal barcode, filledbars, generation, image, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating an RM4SCC barcode with unfilled bars and saving it as PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures parameters, and saves the image.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory relative to the current working folder
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir); // Ensure the directory exists

        // Build the full file path for the resulting PNG image
        string filePath = Path.Combine(outputDir, "RM4SCC_FilledBarsFalse.png");

        // Initialize the barcode generator for RM4SCC symbology with the data "123456"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.RM4SCC, "123456"))
        {
            // Set the X‑dimension (module width) to 4 pixels for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Disable filling of the bars (2‑state mode)
            generator.Parameters.Barcode.FilledBars = false;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {filePath}");
    }
}