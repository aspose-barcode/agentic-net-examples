// Title: Export and Import Barcode Generator Settings via XML
// Description: Demonstrates creating a barcode generator, exporting its configuration to an XML file, and reloading those settings. Useful for persisting UI‑based barcode options.
// Category-Description: This example belongs to the Aspose.BarCode generation settings management category. It showcases the BarcodeGenerator class, its Parameters property, and the ExportToXml / ImportFromXml methods. Developers often need to save user‑defined barcode options to XML for later reuse in UI components or configuration files.
// Prompt: Design a UI component that loads barcode settings from an XML file and populates property editors.
// Tags: barcode symbology, export, import, xml, settings, generation, aspose.barcode, ui

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting and importing barcode generator settings using XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a sample configuration if missing, then loads and displays settings.
    /// </summary>
    static void Main()
    {
        // Determine path for the XML configuration file in the current directory
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "generatorSettings.xml");

        // If the configuration file does not exist, create a sample generator and export its settings
        if (!File.Exists(xmlPath))
        {
            // Initialize a QR code generator with sample text
            using (var gen = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
            {
                // Configure visual appearance and code text parameters
                gen.Parameters.Barcode.BarColor = Color.Blue;
                gen.Parameters.Barcode.FilledBars = false;
                gen.Parameters.Barcode.XDimension.Point = 2f;
                gen.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                gen.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                gen.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Export the configured parameters to an XML file
                gen.ExportToXml(xmlPath);
                Console.WriteLine($"Sample XML configuration created at: {xmlPath}");
            }
        }

        // Load barcode generation settings from the previously created (or existing) XML file
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Output loaded settings to the console for verification
            Console.WriteLine("Loaded barcode generation settings from XML:");
            Console.WriteLine($"Encode Type: {generator.BarcodeType}");
            Console.WriteLine($"Code Text: {generator.CodeText}");
            Console.WriteLine($"Bar Color: {generator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"Filled Bars: {generator.Parameters.Barcode.FilledBars}");
            Console.WriteLine($"X Dimension (points): {generator.Parameters.Barcode.XDimension.Point}");
            Console.WriteLine($"Code Text Location: {generator.Parameters.Barcode.CodeTextParameters.Location}");
            Console.WriteLine($"Code Text Font Family: {generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName}");
            Console.WriteLine($"Code Text Font Size (points): {generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point}");
        }
    }
}