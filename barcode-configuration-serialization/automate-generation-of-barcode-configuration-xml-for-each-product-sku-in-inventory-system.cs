// Title: Generate Code128 Barcodes and Export Configuration XML for Product SKUs
// Description: This example creates Code128 barcode images for a list of product SKUs and saves the generation settings as XML files.
// Category-Description: Demonstrates Aspose.BarCode generation and configuration export. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce barcode images and ExportToXml to persist settings. Ideal for inventory systems needing automated barcode creation and reusable configuration files. Suitable for developers working with barcode symbologies, image output, and XML configuration management.
// Prompt: Automate generation of barcode configuration XML for each product SKU in an inventory system.
// Tags: barcode symbology, generation, png, xml, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Code128 barcode images for a set of product SKUs
/// and exports the corresponding generation settings to XML configuration files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over sample SKUs, creates barcode images,
    /// and writes XML configuration files for each.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of product SKUs to process
        string[] skus = { "SKU001", "SKU002", "SKU003", "SKU004", "SKU005" };

        // Specify the output directory for both barcode images and XML configuration files
        string outputDir = "Barcodes";
        Directory.CreateDirectory(outputDir); // Ensure the directory exists

        // Process each SKU individually
        foreach (string sku in skus)
        {
            // Build full file paths for the image and XML files
            string imagePath = Path.Combine(outputDir, $"{sku}.png");
            string xmlPath   = Path.Combine(outputDir, $"{sku}.xml");

            // Initialize the barcode generator with Code128 symbology and the current SKU value
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, sku))
            {
                // Example of customizing a barcode parameter: set X-dimension (module width) to 2 points
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG image
                generator.Save(imagePath, BarCodeImageFormat.Png);

                // Export the current generation settings to an XML configuration file
                generator.ExportToXml(xmlPath);
            }

            // Inform the user about the generated files
            Console.WriteLine($"Generated barcode for {sku}: image={imagePath}, config={xmlPath}");
        }
    }
}