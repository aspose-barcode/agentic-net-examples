// Title: Export and Import Postal Barcode Configuration via XML
// Description: Demonstrates how to create an XML configuration file that defines default XDimension, BarHeight, and FilledBars for postal barcodes, then reuse it to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category. It shows how to use BarcodeGenerator to set default barcode parameters, export them to an XML configuration file, and import the configuration for later barcode creation. Developers working with postal symbologies often need to standardize visual settings across multiple barcodes; this pattern provides a reusable, file‑based approach using the Parameters, ExportToXml, and ImportFromXml APIs.
// Prompt: Create a configuration file that defines default XDimension, BarHeight, and FilledBars for all postal barcode operations.
// Tags: postal, barcode, configuration, xml, xdimension, barheight, filledbars, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports default postal barcode settings to an XML file
/// and then imports them to generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a configuration file with default XDimension, BarHeight,
    /// and FilledBars for postal barcodes, then generates a barcode using that configuration.
    /// </summary>
    static void Main()
    {
        // Define file paths for the configuration XML and the output image
        string configPath = Path.Combine(Directory.GetCurrentDirectory(), "postalConfig.xml");
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "PostalFromConfig.png");

        // Create a sample generator to define default settings and export them to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set default parameters for postal barcodes
            generator.Parameters.Barcode.XDimension.Pixels = 3f;   // Width of the smallest bar element
            generator.Parameters.Barcode.BarHeight.Pixels = 50f; // Height of the barcode bars
            generator.Parameters.Barcode.FilledBars = false;    // Use unfilled (hollow) bars

            // Export the configuration to an XML file for later reuse
            generator.ExportToXml(configPath);
        }

        // Import the configuration from XML and generate a barcode using the defaults
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(configPath))
        {
            // Apply the desired symbology and code text
            importedGenerator.BarcodeType = EncodeTypes.Planet;
            importedGenerator.CodeText = "123456";

            // Save the generated barcode image as PNG
            importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the files were created
        Console.WriteLine("Configuration exported to: " + configPath);
        Console.WriteLine("Barcode generated using configuration: " + outputPath);
    }
}