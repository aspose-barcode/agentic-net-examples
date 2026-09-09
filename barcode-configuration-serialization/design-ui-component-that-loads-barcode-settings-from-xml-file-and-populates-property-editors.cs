// Title: Load and Export Barcode Generator Settings via XML
// Description: Demonstrates exporting a barcode generator's configuration to an XML file and importing it back, enabling UI components to persist and reload barcode settings.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, its Parameters, and the ExportToXml/ImportFromXml methods. Developers often need to save user‑defined barcode settings, load them later, or share configurations across applications. The pattern shown is common for building property editors or UI components that work with persisted barcode configurations.
// Prompt: Design a UI component that loads barcode settings from an XML file and populates property editors.
// Tags: barcode, symbology, export, import, xml, settings, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting barcode generator settings to XML and importing them back,
/// simulating the loading of settings into UI property editors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, exports settings,
    /// imports them, displays the loaded values, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the XML file that will hold the generator settings
        string xmlPath = Path.Combine(tempFolder, "generatorSettings.xml");

        // Step 1: Create a barcode generator, configure its properties, and export to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Set visual appearance of the barcode
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Green;
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightGray;

            // Adjust module size and padding
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Persist the configured settings to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Import the barcode generator configuration from the XML file
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);

        // Simulate displaying loaded settings in a UI property editor
        Console.WriteLine("Loaded Barcode Settings:");
        Console.WriteLine($"BarColor: {importedGenerator.Parameters.Barcode.BarColor}");
        Console.WriteLine($"BackColor: {importedGenerator.Parameters.BackColor}");
        Console.WriteLine($"XDimension (Pixels): {importedGenerator.Parameters.Barcode.XDimension.Pixels}");
        Console.WriteLine($"Padding Left (Point): {importedGenerator.Parameters.Barcode.Padding.Left.Point}");
        Console.WriteLine($"Padding Top (Point): {importedGenerator.Parameters.Barcode.Padding.Top.Point}");
        Console.WriteLine($"Padding Right (Point): {importedGenerator.Parameters.Barcode.Padding.Right.Point}");
        Console.WriteLine($"Padding Bottom (Point): {importedGenerator.Parameters.Barcode.Padding.Bottom.Point}");

        // Clean up resources
        importedGenerator.Dispose();

        // Attempt to delete the temporary folder; ignore any errors during cleanup
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }
}