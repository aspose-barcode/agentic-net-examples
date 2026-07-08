// Title: Export barcode configuration to XML and reuse it
// Description: Demonstrates exporting the current barcode generator settings to an XML file and then importing those settings in another application to create a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to persist and reuse barcode generation settings via XML. It highlights the use of BarcodeGenerator, ExportToXml, and ImportFromXml APIs, which are essential for developers needing consistent barcode appearance across multiple applications or sessions. Typical use cases include configuration sharing, version control of barcode settings, and automated deployment pipelines.
// Prompt: Export current barcode configuration to an XML file, then import it in another application.
// Tags: barcode symbology, export, import, xml, configuration, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to an XML file
/// and importing that configuration to generate a barcode image in a separate step.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Exports barcode settings to XML, then imports them to create an image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the XML configuration and the resulting barcode image.
        string xmlPath = "barcodeConfig.xml";
        string imagePath = "barcode.png";

        // --------------------------------------------------------------------
        // Create a barcode generator, configure it, and export its settings.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Optional: customize a visual property (e.g., barcode color).
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;

            // Export the current configuration to an XML file.
            bool exportSuccess = generator.ExportToXml(xmlPath);
            Console.WriteLine(exportSuccess
                ? $"Configuration exported to '{xmlPath}'."
                : $"Failed to export configuration to '{xmlPath}'.");
        }

        // ---------------------------------------------------------------
        // Verify that the XML file was created before attempting import.
        // ---------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file '{xmlPath}' does not exist. Exiting.");
            return;
        }

        // ---------------------------------------------------------------
        // Import the barcode configuration from the XML file and save image.
        // ---------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Generate and save the barcode image using the imported settings.
            importedGenerator.Save(imagePath);
            Console.WriteLine($"Barcode image saved to '{imagePath}'.");
        }
    }
}