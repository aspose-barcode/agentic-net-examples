// Title: Generate RM4SCC 2‑state postal barcode with outlined bars
// Description: Demonstrates creating an RM4SCC barcode and disabling bar filling so only bar outlines are rendered.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator and its Parameters. It shows usage of EncodeTypes, BarcodeGenerator, and BarcodeParameters to produce postal barcodes, a common requirement for mailing applications where visual style customization is needed. Developers often search for examples on disabling filled bars, setting symbology, and exporting to image formats.
// Prompt: Generate an RM4SCC 2‑state postal barcode and disable bar filling using FilledBars false.
// Tags: rm4scc, barcode, generation, filledbars, png, aspose.barcode

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates generating an RM4SCC 2‑state postal barcode with bar outlines only.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a BarcodeGenerator, disables bar filling, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize the generator for RM4SCC symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.RM4SCC, "AB12345"))
        {
            // Set FilledBars to false so bars are drawn as outlines rather than solid.
            generator.Parameters.Barcode.FilledBars = false;

            // Export the barcode to a PNG file.
            generator.Save("rm4scc.png");
        }
    }
}