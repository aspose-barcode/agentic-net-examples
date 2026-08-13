// Title: Export BarcodeGenerator configuration to XML
// Description: Demonstrates exporting a configured BarcodeGenerator's state to an XML file using the ExportToXml(string) overload.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category. It showcases how to set up barcode parameters with the BarcodeGenerator class, adjust visual properties, and persist the configuration to an XML file via ExportToXml. Developers often need to save and reuse barcode settings across applications or environments, making XML export a common practice for configuration management.
// Prompt: Export a configured BarcodeGenerator state to an XML file using ExportToXml(string) overload.
// Tags: code128, export, xml, aspose.barcode, bargenerator, configuration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that configures a BarcodeGenerator and exports its state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Sets up barcode parameters and saves them to an XML configuration file.
    /// </summary>
    static void Main()
    {
        // Define the output path for the exported XML configuration.
        string xmlPath = "barcode_config.xml";

        // Initialize a BarcodeGenerator for Code128 symbology with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // ----- Configure barcode visual and functional parameters -----
            generator.Parameters.Barcode.XDimension.Point = 2f;                     // Module size (width of the smallest bar)
            generator.Parameters.Barcode.BarHeight.Point = 50f;                    // Height of the barcode bars
            generator.Parameters.Barcode.FilledBars = true;                        // Use filled bars instead of outlines
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false; // Suppress exceptions for invalid text
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f; // Font size for human‑readable text
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below; // Position of the code text
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;    // Color of the bars
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;           // Background color of the image

            // ----- Export the configured generator state to an XML file -----
            bool success = generator.ExportToXml(xmlPath);

            // Output the result of the export operation.
            Console.WriteLine($"Export to XML {(success ? "succeeded" : "failed")}. File: {Path.GetFullPath(xmlPath)}");
        }
    }
}