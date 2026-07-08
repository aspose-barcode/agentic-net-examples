// Title: Generate DataBar Stacked Barcode with 2D Composite Component and PDF Output
// Description: Demonstrates setting DataBar stacked aspect ratio to 12, enabling the 2D composite component, and saving the result as a PDF file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure DataBar stacked symbology using the BarcodeGenerator class. Typical use cases include creating retail product barcodes with composite components for additional data. Developers often need to adjust aspect ratios, enable 2D components, and export barcodes to various formats such as PDF.
// Prompt: Set DataBar stacked parameters aspect ratio twelve, enable 2D component, generate PDF output.
// Tags: databar stacked, aspect ratio, 2d component, pdf output, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a DataBar Stacked barcode, configures its aspect ratio,
/// enables the 2D composite component, and saves the result as a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to "databar_stacked.pdf".
    /// </summary>
    static void Main()
    {
        // Sample GTIN code text suitable for DataBar stacked symbology
        const string codeText = "(01)12345678901231";

        // Initialize a BarcodeGenerator for DataBar stacked symbology with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
        {
            // Set the aspect ratio of the DataBar stacked module to 12 (wide modules)
            generator.Parameters.Barcode.DataBar.AspectRatio = 12f;

            // Enable the 2D composite component for the DataBar barcode
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = true;

            // Save the generated barcode as a PDF file in the current directory
            generator.Save("databar_stacked.pdf");
        }
    }
}