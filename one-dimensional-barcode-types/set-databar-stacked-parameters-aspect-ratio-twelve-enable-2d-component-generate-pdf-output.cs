// Title: Generate DataBar Stacked barcode with aspect ratio 12, enable 2D component, and save as PDF
// Description: Demonstrates creating a DataBar Stacked barcode, configuring its aspect ratio to 12, turning on the 2D composite component, and exporting the result to a PDF file using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to work with DataBar symbologies. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Typical use cases include retail product labeling and inventory systems where stacked DataBar symbols with 2D components are required. Developers often need to adjust visual parameters like aspect ratio and module size before saving the barcode in various formats.
// Prompt: Set DataBar stacked parameters aspect ratio twelve, enable 2D component, generate PDF output.
// Tags: databar, stacked, aspectratio, 2dcomponent, pdf, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataBar Stacked barcode,
/// configures specific visual parameters, and saves it as a PDF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "DataBarStacked.pdf");

        // Sample code text to encode (GTIN-14 format in this case).
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with the DataBar Stacked symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarStacked, codeText))
        {
            // Optional: set the module (X) size in pixels for finer control over barcode dimensions.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set the aspect ratio of the DataBar symbol to 12 (wide format).
            generator.Parameters.Barcode.DataBar.AspectRatio = 12f;

            // Enable the 2D composite component flag to embed additional data.
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = true;

            try
            {
                // Save the generated barcode as a PDF document.
                generator.Save(outputPath, BarCodeImageFormat.Pdf);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
            catch (BarCodeException ex)
            {
                // Handle any errors that occur during the save operation.
                Console.WriteLine("Failed to save as PDF: " + ex.Message);
            }
        }
    }
}