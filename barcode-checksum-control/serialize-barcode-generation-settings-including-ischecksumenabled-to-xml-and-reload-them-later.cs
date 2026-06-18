using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a barcode, export its settings to XML,
/// import those settings, and save the resulting images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves its image, exports settings to XML,
    /// imports the settings, and saves the imported barcode image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the generated barcode images and the XML settings file.
        string xmlPath = "barcodeSettings.xml";
        string originalImagePath = "barcode_original.png";
        string importedImagePath = "barcode_imported.png";

        // --------------------------------------------------------------------
        // Create a barcode generator, enable checksum, save the image,
        // and export the generator's configuration to an XML file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "12345"))
        {
            // Enable checksum for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image to the specified path.
            generator.Save(originalImagePath);

            // Export the current generator settings (including checksum) to XML.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the previously saved XML settings into a new generator instance
        // and save the barcode image generated from those imported settings.
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image created from the imported settings.
            importedGenerator.Save(importedImagePath);
        }

        // Output the full paths of the generated files for verification.
        Console.WriteLine($"Original barcode saved to: {Path.GetFullPath(originalImagePath)}");
        Console.WriteLine($"XML settings saved to: {Path.GetFullPath(xmlPath)}");
        Console.WriteLine($"Imported barcode saved to: {Path.GetFullPath(importedImagePath)}");
    }
}