// Title: Barcode Generation with XML Configuration Export/Import
// Description: Demonstrates creating a barcode, exporting its settings to XML, and reproducing the same barcode by importing the configuration.
// Prompt: Convert an existing barcode image generation workflow to use saved XML configurations for reproducible output.
// Tags: barcode, code128, xml, export, import, image, aspose

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to generate a barcode, export its configuration to XML,
/// and then recreate the same barcode by importing the saved XML configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the generated images and the XML configuration
        string imagePath1 = "barcode1.png";
        string configPath = "barcode_config.xml";
        string imagePath2 = "barcode2.png";

        // -----------------------------------------------------------------
        // Step 1: Create a barcode generator, configure visual settings, and save the image
        // -----------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set visual appearance of the barcode
            generator.Parameters.Barcode.BarColor = Color.Blue;          // Barcode bars color
            generator.Parameters.BackColor = Color.White;               // Background color
            generator.Parameters.Barcode.XDimension.Point = 2f;        // Width of the smallest bar
            generator.Parameters.Barcode.BarHeight.Point = 40f;        // Height of the barcode
            generator.Parameters.Barcode.Padding.Left.Point = 5f;      // Left padding
            generator.Parameters.Barcode.Padding.Top.Point = 5f;       // Top padding
            generator.Parameters.Barcode.Padding.Right.Point = 5f;     // Right padding
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;    // Bottom padding

            // Save the generated barcode image to a PNG file
            generator.Save(imagePath1, BarCodeImageFormat.Png);

            // Export the current generator configuration to an XML file for later reuse
            bool exportSuccess = generator.ExportToXml(configPath);
            Console.WriteLine(exportSuccess
                ? $"Configuration exported to '{configPath}'."
                : $"Failed to export configuration to '{configPath}'.");
        }

        // -----------------------------------------------------------------
        // Step 2: Load the saved configuration from XML and generate the same barcode
        // -----------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            Console.WriteLine($"Configuration file '{configPath}' not found. Skipping import step.");
            return;
        }

        try
        {
            // Import a new generator instance using the previously saved XML settings
            using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(configPath))
            {
                // Save the barcode image generated from the imported configuration
                importedGenerator.Save(imagePath2, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode generated from XML configuration saved to '{imagePath2}'.");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the import process
            Console.WriteLine($"Error importing configuration: {ex.Message}");
        }
    }
}