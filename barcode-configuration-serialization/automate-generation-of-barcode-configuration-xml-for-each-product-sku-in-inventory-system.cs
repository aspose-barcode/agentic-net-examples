using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating barcode configuration XML files for a list of product SKUs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates XML configuration files for each SKU in the sample inventory.
    /// </summary>
    static void Main()
    {
        // Define a sample inventory containing product SKUs.
        var products = new List<string>
        {
            "SKU00123",
            "SKU00456",
            "SKU00789",
            "SKU01011",
            "SKU01314"
        };

        // Determine the output directory for the generated XML configuration files.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeConfigs");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Iterate over each SKU and generate its barcode configuration.
        foreach (var sku in products)
        {
            // Initialize a barcode generator for Code128 using the current SKU as the code text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, sku))
            {
                // Configure barcode appearance and settings.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // Enable checksum.
                generator.Parameters.Barcode.BarHeight.Point = 40f;                  // Set bar height.
                generator.Parameters.Barcode.XDimension.Point = 2f;                 // Set X dimension (module width).
                generator.Parameters.Resolution = 300f;                              // Set image resolution.

                // Build the full path for the XML file corresponding to the current SKU.
                string xmlPath = Path.Combine(outputDir, $"{sku}.xml");
                // Export the generator's configuration to an XML file.
                generator.ExportToXml(xmlPath);

                // Inform the user that the XML file has been created.
                Console.WriteLine($"Generated XML configuration for SKU '{sku}' at: {xmlPath}");
            }
        }

        // Indicate that the process has completed.
        Console.WriteLine("Barcode configuration XML generation completed.");
    }
}