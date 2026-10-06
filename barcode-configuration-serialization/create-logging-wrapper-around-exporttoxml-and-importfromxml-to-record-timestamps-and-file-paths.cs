// Title: Logging Wrapper for Barcode XML Export/Import
// Description: Demonstrates how to wrap Aspose.BarCode ExportToXml and ImportFromXml calls with logging that records timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode XML serialization category, showing how to persist and restore barcode generator and reader configurations using ExportToXml/ImportFromXml. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and the Export/Import methods, which developers commonly use for saving settings, sharing configurations, or automating batch processing.
// Prompt: Create a logging wrapper around ExportToXml and ImportFromXml to record timestamps and file paths.
// Tags: barcode, xml, export, import, logging, qrcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates logging of barcode generator and reader XML export/import operations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, exports/imports configurations to XML with logging, and reads the barcode.
    /// </summary>
    static void Main()
    {
        // Define a temporary working directory for all demo files
        string basePath = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(basePath);

        // Paths for generator XML, generated image, reader XML, and log file
        string generatorXmlPath = Path.Combine(basePath, "generator.xml");
        string generatorImagePath = Path.Combine(basePath, "generated.png");
        string readerXmlPath = Path.Combine(basePath, "reader.xml");
        string logPath = Path.Combine(basePath, "log.txt");

        // Clean up any leftover files from previous runs
        foreach (var file in new[] { generatorXmlPath, generatorImagePath, readerXmlPath, logPath })
        {
            if (File.Exists(file))
                File.Delete(file);
        }

        // -------------------------------------------------
        // Create and configure a QR code generator
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Pixels = 12f;

            // Export generator state to XML with logging
            ExportGeneratorToXml(generator, generatorXmlPath, logPath);

            // Save the generated barcode image
            generator.Save(generatorImagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Import generator state from XML and generate a second image
        // -------------------------------------------------
        using (var importedGenerator = ImportGeneratorFromXml(generatorXmlPath, logPath))
        {
            string importedImagePath = Path.Combine(basePath, "imported.png");
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Create a barcode reader for the generated image
        // -------------------------------------------------
        using (var reader = new BarCodeReader(generatorImagePath, DecodeType.QR))
        {
            // Export reader state to XML with logging
            ExportReaderToXml(reader, readerXmlPath, logPath);
        }

        // -------------------------------------------------
        // Import reader state from XML and read the barcode again
        // -------------------------------------------------
        using (var importedReader = ImportReaderFromXml(readerXmlPath, logPath))
        {
            importedReader.SetBarCodeImage(generatorImagePath);
            importedReader.SetBarCodeReadType(DecodeType.QR);
            var results = importedReader.ReadBarCodes();

            // Output read results to console
            foreach (var result in results)
            {
                Console.WriteLine($"Read Code: {result.CodeText} Type: {result.CodeTypeName}");
            }
        }

        Console.WriteLine("Demo completed. Logs written to: " + logPath);
    }

    /// <summary>
    /// Exports a BarcodeGenerator configuration to XML and logs the operation.
    /// </summary>
    static void ExportGeneratorToXml(BarcodeGenerator generator, string xmlPath, string logPath)
    {
        Log($"Exporting BarcodeGenerator to XML. Path: {xmlPath}", logPath);
        generator.ExportToXml(xmlPath);
        Log($"Export completed. Path: {xmlPath}", logPath);
    }

    /// <summary>
    /// Imports a BarcodeGenerator configuration from XML and logs the operation.
    /// </summary>
    static BarcodeGenerator ImportGeneratorFromXml(string xmlPath, string logPath)
    {
        Log($"Importing BarcodeGenerator from XML. Path: {xmlPath}", logPath);
        if (!File.Exists(xmlPath))
        {
            Log($"File not found: {xmlPath}", logPath);
            throw new FileNotFoundException("XML file not found.", xmlPath);
        }
        var gen = BarcodeGenerator.ImportFromXml(xmlPath);
        Log($"Import completed. Path: {xmlPath}", logPath);
        return gen;
    }

    /// <summary>
    /// Exports a BarCodeReader configuration to XML and logs the operation.
    /// </summary>
    static void ExportReaderToXml(BarCodeReader reader, string xmlPath, string logPath)
    {
        Log($"Exporting BarCodeReader to XML. Path: {xmlPath}", logPath);
        reader.ExportToXml(xmlPath);
        Log($"Export completed. Path: {xmlPath}", logPath);
    }

    /// <summary>
    /// Imports a BarCodeReader configuration from XML and logs the operation.
    /// </summary>
    static BarCodeReader ImportReaderFromXml(string xmlPath, string logPath)
    {
        Log($"Importing BarCodeReader from XML. Path: {xmlPath}", logPath);
        if (!File.Exists(xmlPath))
        {
            Log($"File not found: {xmlPath}", logPath);
            throw new FileNotFoundException("XML file not found.", xmlPath);
        }
        var reader = BarCodeReader.ImportFromXml(xmlPath);
        Log($"Import completed. Path: {xmlPath}", logPath);
        return reader;
    }

    /// <summary>
    /// Appends a timestamped log entry to the specified log file.
    /// </summary>
    static void Log(string message, string logPath)
    {
        string entry = $"[{DateTime.Now:O}] {message}{Environment.NewLine}";
        File.AppendAllText(logPath, entry);
    }
}