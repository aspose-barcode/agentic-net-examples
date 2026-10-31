// Title: Generate Barcode Configuration XML for Product SKUs
// Description: Demonstrates how to create barcode generator instances for each SKU and export their configuration to XML files.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and the ExportToXml method to produce barcode configuration files. Developers often need to automate barcode setup for inventory systems, batch processing, or integration pipelines, and this snippet shows typical steps such as setting dimensions and saving XML definitions.
// Prompt: Automate generation of barcode configuration XML for each product SKU in an inventory system.
// Tags: barcode, code128, xml, export, aspose.barcode, generation, inventory, sku, automation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates barcode configuration XML files for a list of product SKUs.
/// </summary>
class Program
{
    /// <summary>
    /// Creates a BarcodeGenerator for each SKU, configures optional parameters, and exports the configuration to an XML file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a sample list of product SKUs to process.
        List<string> skus = new List<string>
        {
            "SKU001",
            "SKU002",
            "SKU003"
        };

        // Determine a temporary directory to store the generated XML configuration files.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeConfigs");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Iterate over each SKU, generate a barcode, and export its configuration.
        foreach (string sku in skus)
        {
            // Initialize a barcode generator for Code128 using the SKU as the encoded text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sku))
            {
                // Optional: set the module (X) dimension to control barcode size.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Build the full path for the XML file named after the SKU.
                string xmlPath = Path.Combine(outputDir, $"{sku}.xml");

                // Export the generator's current state (including parameters) to an XML file.
                generator.ExportToXml(xmlPath);

                // Inform the user that the export succeeded.
                Console.WriteLine($"Exported barcode configuration for SKU '{sku}' to '{xmlPath}'.");
            }
        }

        // Final status message after processing all SKUs.
        Console.WriteLine("All barcode configuration XML files have been generated.");
    }
}