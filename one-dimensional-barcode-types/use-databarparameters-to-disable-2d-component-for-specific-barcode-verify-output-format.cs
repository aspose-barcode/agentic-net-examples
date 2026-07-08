// Title: Disable 2D Composite Component in DataBar OmniDirectional Barcode
// Description: Demonstrates how to generate a DataBar OmniDirectional barcode with the 2‑D composite component turned off and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on configuring DataBarParameters for barcode customization. It showcases the use of BarcodeGenerator, EncodeTypes, and DataBar settings to control 2‑D components, a common requirement when generating compact linear barcodes for retail or logistics applications. Developers often need to toggle composite components and verify output formats such as PNG, JPEG, or SVG.
// Prompt: Use DataBarParameters to disable 2D component for specific barcode, verify output format.
// Tags: databar, disable 2d component, png, aspose.barcode, generation, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a DataBar OmniDirectional barcode with the 2‑D composite component disabled
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures parameters,
    /// saves the image, and validates the output file.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be saved
        string outputPath = "databar.png";

        // Sample GTIN code for DataBar OmniDirectional (Application Identifier 01)
        string codeText = "(01)12345678901231";

        // Initialize the barcode generator with the desired symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, codeText))
        {
            // Disable the optional 2‑D composite component of the DataBar barcode
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Persist the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the file exists and has the expected PNG extension
        if (File.Exists(outputPath) && Path.GetExtension(outputPath).Equals(".png", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"Barcode image successfully saved as PNG: {outputPath}");
        }
        else
        {
            Console.WriteLine("Failed to create the barcode image or incorrect format.");
        }
    }
}