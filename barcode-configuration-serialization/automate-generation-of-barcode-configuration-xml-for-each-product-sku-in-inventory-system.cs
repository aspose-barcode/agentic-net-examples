// Title: Generate barcode XML configuration and PNG images for product SKUs
// Description: Demonstrates how to create Code128 barcodes for a list of SKUs, export their configuration to XML, and save PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode configuration and export category, showcasing the BarcodeGenerator class for encoding, the ExportToXml method for persisting settings, and image saving via Save. Developers often need to automate barcode creation for inventory systems, export settings for later reuse, and generate visual assets for reporting or labeling.
// Prompt: Automate generation of barcode configuration XML for each product SKU in an inventory system.
// Tags: barcode symbology, barcode generation, xml export, png output, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that generates barcode XML configurations and PNG images for a set of product SKUs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary output folder, generates Code128 barcodes for each SKU,
    /// exports the generator settings to XML, saves PNG images, and demonstrates importing from XML.
    /// </summary>
    static void Main(string[] args)
    {
        // Define a list of product SKUs to process
        List<string> skus = new List<string> { "SKU001", "SKU002", "SKU003", "SKU004", "SKU005" };

        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeConfig_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Iterate over each SKU, generate barcode, export XML, and save PNG image
        foreach (string sku in skus)
        {
            // Build file paths for XML configuration and PNG image
            string xmlPath = Path.Combine(outputDir, sku + ".xml");
            string imgPath = Path.Combine(outputDir, sku + ".png");

            // Initialize the barcode generator with Code128 symbology and the SKU value
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sku))
            {
                // Set visual parameters: module size and bar color
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Export the generator configuration to an XML file
                generator.ExportToXml(xmlPath);

                // Save the rendered barcode as a PNG image
                generator.Save(imgPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated XML and image for {sku} in {outputDir}");
        }

        // Demonstrate importing a previously saved XML configuration and generating an image
        if (skus.Count > 0)
        {
            string firstXml = Path.Combine(outputDir, skus[0] + ".xml");
            string importedImg = Path.Combine(outputDir, skus[0] + "_imported.png");

            // Load generator settings from XML and save a new PNG image
            using (BarcodeGenerator importedGen = BarcodeGenerator.ImportFromXml(firstXml))
            {
                importedGen.Save(importedImg, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Imported XML and generated image for {skus[0]}");
        }
    }
}