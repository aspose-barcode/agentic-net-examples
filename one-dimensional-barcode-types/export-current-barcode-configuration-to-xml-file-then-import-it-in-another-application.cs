// Title: Export and Import Barcode Configuration via XML
// Description: Demonstrates exporting a configured barcode generator to an XML file and importing it later to recreate the same barcode.
// Category-Description: Shows how to use Aspose.BarCode's configuration export/import APIs. This example belongs to the configuration management category, illustrating the use of BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml to persist and reuse barcode settings across applications. Developers often need to share barcode configurations, automate deployment, or maintain consistency, and these APIs provide a straightforward XML-based approach.
// Prompt: Export current barcode configuration to an XML file, then import it in another application.
// Tags: barcode, export, import, xml, configuration, aspose.barcode, code128, image, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a barcode generator's configuration to XML,
/// then imports the configuration to generate the same barcode in a new context.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, exports its settings,
    /// and demonstrates importing those settings to generate an identical barcode.
    /// </summary>
    static void Main()
    {
        // ------------------- Create and configure a barcode -------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Set human‑readable text styling (font family and size)
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Pixels = 14f;

            // Export the current configuration to an XML file
            string xmlPath = "barcodeConfig.xml";
            bool exportSuccess = generator.ExportToXml(xmlPath);
            Console.WriteLine($"Export succeeded: {exportSuccess}");

            // Optionally save the barcode image generated with the current settings
            generator.Save("barcode.png");
        }

        // ------------------- Import configuration in another context -------------------
        string importXmlPath = "barcodeConfig.xml";
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(importXmlPath))
        {
            // Generate and save the barcode using the imported settings
            importedGenerator.Save("importedBarcode.png", BarCodeImageFormat.Png);
        }
    }
}