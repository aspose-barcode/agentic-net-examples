// Title: Generate Barcode Configuration XML for Product SKUs
// Description: Demonstrates how to create barcode generator settings for a list of product SKUs and export each configuration to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters (e.g., symbology, dimensions, colors) and serialize those settings to XML using the BarcodeGenerator class. Typical use cases include batch preparation of barcode configurations for inventory systems, printing workflows, or integration with external services that consume barcode definition files. Developers often need to automate such exports to streamline deployment and maintain consistency across multiple products.
// Prompt: Automate generation of barcode configuration XML for each product SKU in an inventory system.
// Tags: barcode symbology, configuration, xml export, code128, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates barcode configuration XML files for a set of product SKUs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates barcode generators for each SKU,
    /// configures basic parameters, and exports the settings to individual XML files.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of product SKUs to process.
        List<string> skus = new List<string>
        {
            "SKU001",
            "SKU002",
            "SKU003",
            "SKU004",
            "SKU005"
        };

        // Create a unique temporary folder to store the generated XML configuration files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodesConfig_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"XML configuration files will be saved to: {outputFolder}");

        // Iterate over each SKU, generate a barcode, configure its appearance, and export to XML.
        foreach (string sku in skus)
        {
            // Use Code128 symbology for the SKU barcode.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sku))
            {
                // Set module size (X dimension) and bar color as part of the barcode configuration.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Build the full path for the XML file named after the current SKU.
                string xmlPath = Path.Combine(outputFolder, $"{sku}.xml");

                // Export the current generator's settings to the XML file.
                generator.ExportToXml(xmlPath);
                Console.WriteLine($"Generated XML for SKU '{sku}' at: {xmlPath}");
            }
        }

        Console.WriteLine("Barcode configuration XML generation completed.");
    }
}