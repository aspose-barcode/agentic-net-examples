// Title: Generate RM4SCC 2‑State Postal Barcode with Unfilled Bars
// Description: Demonstrates how to create an RM4SCC postal barcode and disable bar filling so the bars are only outlined.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of EncodeTypes, BarcodeGenerator, and barcode appearance settings. Typical scenarios include generating postal barcodes for mail sorting and customizing visual styles such as unfilled (outline‑only) bars. Developers often need to adjust rendering options to meet printing or design requirements.
// Prompt: Generate an RM4SCC 2‑state postal barcode and disable bar filling using FilledBars false.
// Tags: rm4scc, postal, barcode, generation, filledbars, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates an RM4SCC 2‑state postal barcode with unfilled bars
/// and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample RM4SCC code text (13 characters: 2 letters, 9 digits, 2 letters)
        string codeText = "AB123456789CD";

        // Initialize the barcode generator for the RM4SCC symbology with the provided code text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.RM4SCC, codeText))
        {
            // Set the FilledBars property to false so bars are drawn as outlines only
            generator.Parameters.Barcode.FilledBars = false;

            // Specify the output file name and format (PNG)
            string outputFile = "rm4scc.png";

            // Render and save the barcode image to the file system
            generator.Save(outputFile, BarCodeImageFormat.Png);

            // Inform the user where the barcode image has been saved
            Console.WriteLine($"RM4SCC barcode generated and saved to: {outputFile}");
        }
    }
}