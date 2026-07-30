// Title: Generate barcode from saved XML configuration
// Description: Demonstrates loading barcode settings from an XML file to produce a reproducible barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to export generator settings to XML and later import them for consistent barcode generation. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, which are commonly employed when developers need repeatable barcode outputs across environments or deployments. Ideal for scenarios like automated testing, batch processing, or configuration‑driven applications.
// Prompt: Convert an existing barcode image generation workflow to use saved XML configurations for reproducible output.
// Tags: barcode, xml, configuration, generation, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode generator, exporting its configuration to XML,
/// importing the configuration, and generating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Handles configuration file creation, import, and barcode image generation.
    /// </summary>
    static void Main()
    {
        const string configFile = "barcodeConfig.xml";
        const string outputFile = "barcodeFromConfig.png";

        // Ensure a configuration file exists. If not, create one with sample settings.
        if (!File.Exists(configFile))
        {
            // Create a barcode generator with sample settings.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Sample visual settings.
                generator.Parameters.Barcode.BarColor = Color.Blue;
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 50f;
                generator.Parameters.Barcode.FilledBars = true;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
                generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

                // Export the configuration to XML for later reuse.
                generator.ExportToXml(configFile);
                Console.WriteLine($"Configuration file created: {configFile}");
            }
        }

        // Load the barcode generator from the saved XML configuration.
        BarcodeGenerator loadedGenerator;
        try
        {
            loadedGenerator = BarcodeGenerator.ImportFromXml(configFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to import configuration: {ex.Message}");
            return;
        }

        // Use the loaded generator to produce the barcode image.
        using (loadedGenerator)
        {
            // The CodeText may be defined in the XML; if not, set a default.
            if (string.IsNullOrEmpty(loadedGenerator.CodeText))
            {
                loadedGenerator.CodeText = "Default123";
            }

            // Save the generated barcode image.
            loadedGenerator.Save(outputFile, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image generated from XML configuration: {outputFile}");
        }
    }
}