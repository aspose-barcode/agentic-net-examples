// Title: Load barcode settings from XML and display them
// Description: Demonstrates exporting barcode generator settings to an XML file, then importing them back and printing the configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use BarcodeGenerator, ExportToXml, and ImportFromXml to persist and restore barcode settings. Typical use cases include saving user‑defined barcode options, sharing configurations across applications, and initializing UI property editors with saved values. Developers often need to serialize settings for reuse or to populate UI controls in design‑time or runtime scenarios.
// Prompt: Design a UI component that loads barcode settings from an XML file and populates property editors.
// Tags: barcode, xml, configuration, export, import, aspose.barcode, settings, ui, property editors

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting barcode settings to XML, importing them back,
/// and outputting the loaded configuration to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary environment, saves barcode settings,
    /// reloads them from XML, and displays the values.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the XML settings file and a sample barcode image
        string xmlPath = Path.Combine(tempFolder, "settings.xml");
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // ------------------------------------------------------------
        // Step 1: Configure a BarcodeGenerator and export its settings to XML
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Apply custom visual and layout settings
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;
            generator.Parameters.Resolution = 150f;
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Persist the current configuration to an XML file
            generator.ExportToXml(xmlPath);

            // Optionally save a sample barcode image for visual verification
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the XML file was created successfully
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create the XML settings file.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Load the barcode settings from the XML file
        // ------------------------------------------------------------
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Simulate populating UI property editors by writing values to the console
            Console.WriteLine("Loaded Barcode Settings:");
            Console.WriteLine($"  Symbology      : {loadedGenerator.BarcodeType}");
            Console.WriteLine($"  Code Text      : {loadedGenerator.CodeText}");
            Console.WriteLine($"  Bar Color      : {loadedGenerator.Parameters.Barcode.BarColor}");
            Console.WriteLine($"  X Dimension    : {loadedGenerator.Parameters.Barcode.XDimension.Point} pt");
            Console.WriteLine($"  Padding Left   : {loadedGenerator.Parameters.Barcode.Padding.Left.Point} pt");
            Console.WriteLine($"  Padding Top    : {loadedGenerator.Parameters.Barcode.Padding.Top.Point} pt");
            Console.WriteLine($"  Padding Right  : {loadedGenerator.Parameters.Barcode.Padding.Right.Point} pt");
            Console.WriteLine($"  Padding Bottom : {loadedGenerator.Parameters.Barcode.Padding.Bottom.Point} pt");
            Console.WriteLine($"  Resolution     : {loadedGenerator.Parameters.Resolution} DPI");
            Console.WriteLine($"  AutoSizeMode   : {loadedGenerator.Parameters.AutoSizeMode}");
            Console.WriteLine($"  Image Width    : {loadedGenerator.Parameters.ImageWidth.Pixels} px");
            Console.WriteLine($"  Image Height   : {loadedGenerator.Parameters.ImageHeight.Pixels} px");
        }

        // ------------------------------------------------------------
        // Cleanup: delete temporary files and folder
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
            if (File.Exists(imagePath)) File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for the demo
        }
    }
}