// Title: Export and Import Barcode Settings via XML
// Description: Demonstrates generating a QR barcode, exporting its configuration to an XML file, and re‑importing those settings for reuse.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to persist barcode parameters using ExportToXml and ImportFromXml. Developers working with dynamic barcode creation often need to save and reload settings for consistency across sessions or to share configurations between applications. Key API classes include BarcodeGenerator, EncodeTypes, and QRErrorLevel.
// Prompt: Design a UI component that loads barcode settings from an XML file and populates property editors.
// Tags: barcode, symbology, export, import, xml, settings, aspose.barcode, generation, qr, color

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that exports barcode generation settings to XML,
/// then imports them back to demonstrate persistence of configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, saves its settings,
    /// reloads them, and displays the imported values.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the demo files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path to the XML settings file that will store the barcode configuration
        string xmlPath = Path.Combine(tempFolder, "settings.xml");

        // --------------------------------------------------------------------
        // Generate a sample QR barcode and export its settings to XML
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Configure sample properties for demonstration purposes
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Persist the configured parameters to an XML file
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Verify that the XML file was created and load the settings back
        // --------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML settings file not found: " + xmlPath);
            return;
        }

        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Output the imported barcode settings to the console
            Console.WriteLine("Loaded Barcode Settings:");
            Console.WriteLine($"Encode Type   : {loadedGenerator.BarcodeType}");
            Console.WriteLine($"Code Text     : {loadedGenerator.CodeText}");
            Console.WriteLine($"XDimension (px): {loadedGenerator.Parameters.Barcode.XDimension.Pixels}");
            Console.WriteLine($"Bar Color     : {loadedGenerator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"Back Color    : {loadedGenerator.Parameters.BackColor}");
            Console.WriteLine($"QR Error Level: {loadedGenerator.Parameters.Barcode.QR.ErrorLevel}");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(xmlPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any cleanup errors to avoid interrupting the demo flow
        }
    }
}