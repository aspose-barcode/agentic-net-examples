using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides helper methods to export and import barcode generator settings with console logging.
/// </summary>
class BarcodeXmlLogger
{
    /// <summary>
    /// Exports the settings of the specified <see cref="BarcodeGenerator"/> to an XML file and logs the operation.
    /// </summary>
    /// <param name="generator">The barcode generator whose settings will be exported.</param>
    /// <param name="filePath">The file path where the XML will be saved.</param>
    public static void ExportToXmlWithLog(BarcodeGenerator generator, string filePath)
    {
        // Log the export action with a timestamp.
        Console.WriteLine($"{DateTime.Now:O}: Exporting barcode settings to \"{filePath}\"");
        // Perform the actual export.
        generator.ExportToXml(filePath);
    }

    /// <summary>
    /// Imports barcode generator settings from an XML file and logs the operation.
    /// </summary>
    /// <param name="filePath">The XML file containing the barcode settings.</param>
    /// <returns>A new <see cref="BarcodeGenerator"/> instance initialized with the imported settings.</returns>
    public static BarcodeGenerator ImportFromXmlWithLog(string filePath)
    {
        // Log the import action with a timestamp.
        Console.WriteLine($"{DateTime.Now:O}: Importing barcode settings from \"{filePath}\"");
        // Perform the actual import.
        return BarcodeGenerator.ImportFromXml(filePath);
    }
}

/// <summary>
/// Demonstrates exporting barcode settings to XML, importing them back, and generating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the XML settings and the resulting barcode image.
        string xmlPath = "barcodeSettings.xml";
        string imagePath = "importedBarcode.png";

        // Create a barcode generator, configure it, and export its settings to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Example configuration (optional): set barcode height and enable checksum.
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Log the creation and export process.
            Console.WriteLine($"{DateTime.Now:O}: Creating barcode and exporting settings.");
            BarcodeXmlLogger.ExportToXmlWithLog(generator, xmlPath);
        }

        // Import the barcode settings from XML and generate an image.
        using (var importedGenerator = BarcodeXmlLogger.ImportFromXmlWithLog(xmlPath))
        {
            // Save the generated barcode image to the specified path.
            importedGenerator.Save(imagePath);
            // Log the successful save operation.
            Console.WriteLine($"{DateTime.Now:O}: Saved imported barcode image to \"{imagePath}\"");
        }
    }
}