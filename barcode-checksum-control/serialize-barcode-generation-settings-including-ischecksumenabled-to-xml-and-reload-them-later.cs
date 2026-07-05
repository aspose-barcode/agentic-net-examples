// Title: Serialize and Reload Barcode Generation Settings
// Description: Demonstrates how to serialize barcode generator settings, including checksum enablement, to an XML file and later import them to generate a barcode image.
// Prompt: Serialize barcode generation settings, including IsChecksumEnabled, to XML and reload them later.
// Tags: barcode, serialization, xml, checksum, aspose.barcode, code128, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to export barcode generation settings to XML,
/// import them back, and generate a barcode image using the restored settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Serializes barcode settings (including checksum) to XML, reloads them,
    /// and creates a barcode image file.
    /// </summary>
    static void Main()
    {
        // Define file paths in the current directory
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcodeSettings.xml");
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a barcode generator, configure checksum and visual options, then export settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Optionally set other visual parameters
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Export the current settings to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Import the settings from the XML file into a new generator instance
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Verify that the checksum setting was restored (optional console output)
            Console.WriteLine("IsChecksumEnabled after import: " + importedGenerator.Parameters.Barcode.IsChecksumEnabled);

            // Save a barcode image using the imported settings
            importedGenerator.Save(imagePath);
        }

        // Indicate completion
        Console.WriteLine("Barcode generated and settings serialized successfully.");
    }
}