// Title: Generate Barcode Configuration XML for Inventory SKUs
// Description: Creates Code128 barcode configuration XML files for each product SKU in an inventory, demonstrating how to automate barcode setup using Aspose.BarCode.
// Prompt: Automate generation of barcode configuration XML for each product SKU in an inventory system.
// Tags: barcode symbology, generation, xml, aspose.barcode, inventory

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate barcode configuration XML files for a set of product SKUs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates through a sample inventory and creates
    /// an XML configuration file for each SKU using the Aspose.BarCode library.
    /// </summary>
    static void Main()
    {
        // Sample inventory: SKU -> barcode value (Code128)
        var inventory = new Dictionary<string, string>
        {
            { "SKU001", "1234567890" },
            { "SKU002", "ABCDEF1234" },
            { "SKU003", "9876543210" },
            { "SKU004", "XYZ7890123" },
            { "SKU005", "0011223344" }
        };

        // Determine output folder for XML files
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeConfigs");
        if (!Directory.Exists(outputFolder))
        {
            // Create the folder if it does not exist
            Directory.CreateDirectory(outputFolder);
        }

        // Process each inventory entry
        foreach (var kvp in inventory)
        {
            string sku = kvp.Key;
            string codeText = kvp.Value;

            // Validate SKU input
            if (string.IsNullOrWhiteSpace(sku))
            {
                Console.WriteLine("Warning: SKU is empty. Skipping entry.");
                continue;
            }

            // Create a Code128 barcode generator for the current SKU
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: customize appearance
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Barcode.XDimension.Point = 2f;   // Module size
                generator.Parameters.Barcode.BarHeight.Point = 40f; // Bar height

                // Export configuration to XML file named after the SKU
                string xmlPath = Path.Combine(outputFolder, $"{sku}.xml");
                generator.ExportToXml(xmlPath);
                Console.WriteLine($"Exported barcode configuration for {sku} to {xmlPath}");
            }
        }

        // Indicate successful completion
        Console.WriteLine("All barcode configurations have been generated.");
    }
}