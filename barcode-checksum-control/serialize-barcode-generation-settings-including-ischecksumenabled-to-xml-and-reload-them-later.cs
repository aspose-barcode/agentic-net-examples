// Title: Serialize and reload barcode generation settings with checksum
// Description: Demonstrates exporting barcode generator settings, including checksum enablement, to XML and re-importing them to generate identical barcodes.
// Category-Description: This example belongs to the Aspose.BarCode settings serialization category, illustrating how to use BarcodeGenerator, its Parameters, and ExportToXml/ImportFromXml methods. Developers often need to persist barcode configurations for reuse across sessions or environments, such as storing checksum options, dimensions, and symbology settings. The snippet serves as a reference for creating, saving, and reloading barcode generation settings in .NET applications.
// Prompt: Serialize barcode generation settings, including IsChecksumEnabled, to XML and reload them later.
// Tags: barcode, serialization, checksum, code128, png, aspose.barcode, settings, xml

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to serialize barcode generation settings (including checksum) to XML,
/// then reload those settings to produce the same barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, exports its settings to XML,
    /// reloads the settings, and saves both original and reloaded barcode images.
    /// </summary>
    static void Main()
    {
        // Define file paths for the XML settings and the generated barcode images.
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcodeSettings.xml");
        string originalImagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_original.png");
        string loadedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_loaded.png");

        // --------------------------------------------------------------------
        // Create a barcode generator, configure its parameters, export to XML,
        // and save the barcode image using the current settings.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum generation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set a visible property (X dimension) to demonstrate that it is also serialized.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Export the current generator settings to an XML file.
            generator.ExportToXml(xmlPath);

            // Save the barcode image using the configured settings.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Load the barcode generator from the previously saved XML settings
        // and generate a new image to verify that the settings were restored.
        // --------------------------------------------------------------------
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save a barcode image using the loaded settings.
            loadedGenerator.Save(loadedImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for user reference.
        Console.WriteLine($"Settings exported to: {xmlPath}");
        Console.WriteLine($"Original barcode image saved to: {originalImagePath}");
        Console.WriteLine($"Barcode image from loaded settings saved to: {loadedImagePath}");
    }
}