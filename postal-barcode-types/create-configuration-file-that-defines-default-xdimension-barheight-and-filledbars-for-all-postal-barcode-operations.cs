// Title: Generate default configuration XML for postal barcode parameters
// Description: Demonstrates how to create an XML file that defines default XDimension, BarHeight, and FilledBars settings for postal barcode generation using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of BarcodeGenerator and its Parameters to set common properties for postal symbologies such as Postnet. Developers often need to define default barcode appearance settings in a reusable XML configuration file for consistent rendering across applications.
// Prompt: Create a configuration file that defines default XDimension, BarHeight, and FilledBars for all postal barcode operations.
// Tags: postal barcode, configuration, xml, generation, aspnet.barcode, xdimension, barheight, filledbars

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates an XML configuration file containing default
/// barcode parameters for postal symbologies (e.g., Postnet). The generated
/// file can be reused across projects to ensure consistent barcode appearance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the XML configuration file
    /// with predefined XDimension, BarHeight, and FilledBars values.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output XML configuration file.
        string configPath = Path.Combine(Directory.GetCurrentDirectory(), "PostalBarcodeDefaults.xml");

        // Initialize a BarcodeGenerator for the Postnet postal symbology.
        // This instance is used only to set default parameters; no barcode is generated.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet))
        {
            // Set the default module width (XDimension) in points.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Set the default height of the bars for 1D barcodes in points.
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Specify that bars should be rendered as filled shapes.
            generator.Parameters.Barcode.FilledBars = true;

            // Export the configured parameters to an XML file at the specified path.
            generator.ExportToXml(configPath);
        }

        // Inform the user where the configuration file has been created.
        Console.WriteLine($"Configuration file created at: {configPath}");
    }
}