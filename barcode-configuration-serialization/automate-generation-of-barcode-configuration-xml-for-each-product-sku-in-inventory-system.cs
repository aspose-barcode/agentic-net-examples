// Title: Generate Barcode Configuration XML for Product SKUs
// Description: Demonstrates how to create Code128 barcode generators for a list of product SKUs and export each generator's configuration to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing the use of BarcodeGenerator, EncodeTypes, and generator Parameters to produce XML representations of barcode settings. Typical use cases include batch creation of barcode configurations for inventory systems, automated deployment pipelines, and integration with external services that consume barcode definition files. Developers often need to generate, store, and later apply these XML configurations to ensure consistent barcode rendering across applications.
// Prompt: Automate generation of barcode configuration XML for each product SKU in an inventory system.
// Tags: code128, xml-generation, barcodegenerator, encode-types, parameters, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates XML configuration files for Code128 barcodes,
/// one file per product SKU in a sample inventory list.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a list of SKUs,
    /// creates a barcode generator for each, configures appearance settings,
    /// and exports the generator state to an XML file.
    /// </summary>
    static void Main()
    {
        // Sample list of product SKUs to process
        List<string> skus = new List<string>
        {
            "SKU00123",
            "SKU00456",
            "SKU00789",
            "SKU01012",
            "SKU01345"
        };

        // Create a unique temporary output folder for the generated XML files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeConfig_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Generating barcode XML configurations in: " + outputFolder);

        // Process each SKU individually
        foreach (string sku in skus)
        {
            // Initialize a barcode generator for Code128 using the SKU as the encoded text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sku))
            {
                // Configure visual appearance of the barcode
                generator.Parameters.Barcode.XDimension.Pixels = 2f;                     // Set module width
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;   // Set bar color
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;          // Set background color

                // Determine the file path for the XML configuration of the current SKU
                string xmlPath = Path.Combine(outputFolder, sku + ".xml");

                // Export the generator's configuration to an XML file
                generator.ExportToXml(xmlPath);
                Console.WriteLine($"Exported XML for SKU '{sku}' to '{xmlPath}'");
            }
        }

        Console.WriteLine("Barcode configuration XML generation completed.");
    }
}