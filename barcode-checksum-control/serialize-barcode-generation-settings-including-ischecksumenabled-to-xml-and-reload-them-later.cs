// Title: Serialize and reuse Aspose.BarCode generation settings via XML
// Description: Demonstrates exporting barcode generator settings, including checksum control, to an XML file and reloading them to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode settings serialization category, illustrating how to persist and restore barcode generation parameters using the BarcodeGenerator class. Typical use cases include saving configuration for later reuse, sharing settings across applications, or maintaining consistent barcode output. Developers often need to export settings to XML, modify them, and import them back to ensure repeatable results.
// Prompt: Serialize barcode generation settings, including IsChecksumEnabled, to XML and reload them later.
// Tags: barcode, serialization, xml, checksum, code39, aspnet, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting and importing barcode generation settings using XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode generator, saves its settings to XML, reloads them, and generates a PNG image.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for the demo files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the XML settings and the resulting barcode image
        string xmlPath = Path.Combine(workFolder, "barcodeSettings.xml");
        string imagePath = Path.Combine(workFolder, "barcode.png");

        // -----------------------------------------------------------------
        // Step 1: Create a barcode generator and configure its settings
        // -----------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Disable checksum for Code39 (demonstrates IsChecksumEnabled)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Set a foreground color to illustrate that other settings are also persisted
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Export the current generator configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Load the saved settings from the XML file into a new generator
        // -----------------------------------------------------------------
        using (BarcodeGenerator loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Output the restored checksum setting (optional verification)
            Console.WriteLine("IsChecksumEnabled after import: " + loadedGenerator.Parameters.Barcode.IsChecksumEnabled);

            // Generate and save the barcode image using the imported settings
            loadedGenerator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -----------------------------------------------------------------
        // Step 3: Report the locations of the generated files
        // -----------------------------------------------------------------
        Console.WriteLine("Barcode image saved to: " + imagePath);
        Console.WriteLine("XML settings saved to: " + xmlPath);
    }
}