// Title: Logging wrapper for Aspose.BarCode XML export/import operations
// Description: Demonstrates how to wrap ExportToXml and ImportFromXml with logging that records timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to persist and restore barcode generator settings using XML. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and the ExportToXml/ImportFromXml methods. Developers often need to serialize settings for reuse, versioning, or deployment, and logging these actions aids troubleshooting and audit trails.
// Prompt: Create a logging wrapper around ExportToXml and ImportFromXml to record timestamps and file paths.
// Tags: barcode, code128, export, import, xml, logging, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides static methods that wrap Aspose.BarCode XML export and import operations
/// with simple file-based logging of timestamps and file paths.
/// </summary>
class BarcodeXmlLogger
{
    // Log file name used for all logging entries.
    private const string LogFile = "barcode_log.txt";

    /// <summary>
    /// Exports the specified <see cref="BarcodeGenerator"/> settings to an XML file
    /// while writing start and end timestamps to the log.
    /// </summary>
    /// <param name="generator">The barcode generator whose settings are to be exported.</param>
    /// <param name="xmlPath">The destination XML file path.</param>
    /// <returns>True if the export succeeded; otherwise, false.</returns>
    public static bool ExportToXmlWithLog(BarcodeGenerator generator, string xmlPath)
    {
        // Log the start of the export operation.
        string startMessage = $"{DateTime.Now:o} - ExportToXml started. Path: {xmlPath}{Environment.NewLine}";
        File.AppendAllText(LogFile, startMessage);

        // Perform the actual export.
        bool result = generator.ExportToXml(xmlPath);

        // Log the completion status of the export operation.
        string endMessage = $"{DateTime.Now:o} - ExportToXml completed. Success: {result}{Environment.NewLine}";
        File.AppendAllText(LogFile, endMessage);

        return result;
    }

    /// <summary>
    /// Imports a <see cref="BarcodeGenerator"/> from an XML file while logging timestamps.
    /// </summary>
    /// <param name="xmlPath">The source XML file path.</param>
    /// <returns>A new <see cref="BarcodeGenerator"/> instance created from the XML.</returns>
    public static BarcodeGenerator ImportFromXmlWithLog(string xmlPath)
    {
        // Log the start of the import operation.
        string startMessage = $"{DateTime.Now:o} - ImportFromXml started. Path: {xmlPath}{Environment.NewLine}";
        File.AppendAllText(LogFile, startMessage);

        // Perform the actual import.
        BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath);

        // Log the successful creation of the generator.
        string endMessage = $"{DateTime.Now:o} - ImportFromXml completed. Generator created.{Environment.NewLine}";
        File.AppendAllText(LogFile, endMessage);

        return generator;
    }
}

/// <summary>
/// Entry point of the example that demonstrates exporting, importing, and saving a barcode
/// while using the logging wrapper defined in <see cref="BarcodeXmlLogger"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates the barcode generation, XML persistence, and image saving.
    /// </summary>
    static void Main()
    {
        // Ensure a clean log file for each run.
        if (File.Exists("barcode_log.txt"))
        {
            File.Delete("barcode_log.txt");
        }

        // Create a barcode generator for Code128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Export generator settings to XML with logging.
            string xmlPath = "barcode_settings.xml";
            BarcodeXmlLogger.ExportToXmlWithLog(generator, xmlPath);
        }

        // Import generator settings from XML with logging.
        BarcodeGenerator importedGenerator = BarcodeXmlLogger.ImportFromXmlWithLog("barcode_settings.xml");

        // Save the imported barcode as an image file.
        using (importedGenerator)
        {
            string imagePath = "imported_barcode.png";
            importedGenerator.Save(imagePath);
            Console.WriteLine($"Barcode image saved to {Path.GetFullPath(imagePath)}");
        }

        // Indicate that processing has completed.
        Console.WriteLine("Processing completed.");
    }
}