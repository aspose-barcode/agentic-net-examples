// Title: Barcode Generation with XML Configuration Fallback
// Description: Demonstrates loading barcode settings from an XML file and falling back to a default Code128 configuration when the import fails.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to use BarcodeGenerator.ImportFromXml, handle errors, and apply default generation settings. Typical use cases include dynamic barcode creation based on external configuration files, with a safety net for missing or corrupt XML. Developers often need to ensure reliable barcode output by providing fallback parameters using the Aspose.BarCode.Generation API.
// Prompt: Implement a fallback mechanism that creates a default barcode configuration if ImportFromXml fails.
// Tags: barcode, xml, fallback, code128, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with an XML configuration and a fallback to default settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Loads barcode configuration from XML, falls back to defaults on failure,
    /// and saves the generated barcode as a PNG image.
    /// </summary>
    static void Main()
    {
        // Path to the XML configuration file
        string xmlPath = "barcodeConfig.xml";

        // Attempt to load barcode configuration from XML.
        // If it fails, fall back to a default configuration.
        BarcodeGenerator generator;
        try
        {
            // Verify that the configuration file exists before attempting import
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException("Configuration file not found.", xmlPath);

            // Import settings from the XML file
            generator = BarcodeGenerator.ImportFromXml(xmlPath);
        }
        catch (Exception ex)
        {
            // Log the import failure and create a default barcode generator
            Console.WriteLine($"ImportFromXml failed: {ex.Message}");
            Console.WriteLine("Creating default barcode configuration.");

            // Default barcode: Code128 with sample text
            generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890");

            // Apply common default visual settings
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.Barcode.XDimension.Point = 2f; // module size
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
        }

        // Ensure the generator is disposed after use
        using (generator)
        {
            // Output file path for the generated barcode image
            string outputPath = "outputBarcode.png";

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode saved to: {Path.GetFullPath(outputPath)}");
        }
    }
}