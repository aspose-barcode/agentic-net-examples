// Title: Logging wrapper for BarcodeGenerator and BarCodeReader XML export/import
// Description: Demonstrates how to wrap Aspose.BarCode ExportToXml and ImportFromXml calls with logging that records timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode XML serialization category, showcasing the use of BarcodeGenerator, BarCodeReader, and their ExportToXml/ImportFromXml methods. Developers often need to persist barcode configurations to XML for later reuse, and logging these operations aids debugging and audit trails. The snippet illustrates typical export, import, and image generation workflows.
// Prompt: Create a logging wrapper around ExportToXml and ImportFromXml to record timestamps and file paths.
// Tags: barcode, xml, export, import, logging, aspose.barcode, generator, reader, serialization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates logging of XML export/import operations for Aspose.BarCode generators and readers.
/// </summary>
class Program
{
    // Path to the log file; set at runtime.
    static string LogFilePath;

    /// <summary>
    /// Appends a timestamped message to the log file.
    /// </summary>
    /// <param name="message">The message to log.</param>
    static void Log(string message)
    {
        File.AppendAllText(LogFilePath, $"{DateTime.Now:O} {message}{Environment.NewLine}");
    }

    /// <summary>
    /// Exports a BarcodeGenerator configuration to XML while logging the operation.
    /// </summary>
    /// <param name="generator">The generator to export.</param>
    /// <param name="xmlPath">Destination XML file path.</param>
    static void ExportGeneratorToXml(BarcodeGenerator generator, string xmlPath)
    {
        Log($"ExportGenerator start: {xmlPath}");
        generator.ExportToXml(xmlPath);
        Log($"ExportGenerator completed: {xmlPath}");
    }

    /// <summary>
    /// Imports a BarcodeGenerator configuration from XML while logging the operation.
    /// </summary>
    /// <param name="xmlPath">Source XML file path.</param>
    /// <returns>The imported BarcodeGenerator instance.</returns>
    static BarcodeGenerator ImportGeneratorFromXml(string xmlPath)
    {
        Log($"ImportGenerator start: {xmlPath}");
        var gen = BarcodeGenerator.ImportFromXml(xmlPath);
        Log($"ImportGenerator completed: {xmlPath}");
        return gen;
    }

    /// <summary>
    /// Exports a BarCodeReader configuration to XML while logging the operation.
    /// </summary>
    /// <param name="reader">The reader to export.</param>
    /// <param name="xmlPath">Destination XML file path.</param>
    static void ExportReaderToXml(BarCodeReader reader, string xmlPath)
    {
        Log($"ExportReader start: {xmlPath}");
        reader.ExportToXml(xmlPath);
        Log($"ExportReader completed: {xmlPath}");
    }

    /// <summary>
    /// Imports a BarCodeReader configuration from XML while logging the operation.
    /// </summary>
    /// <param name="xmlPath">Source XML file path.</param>
    /// <returns>The imported BarCodeReader instance.</returns>
    static BarCodeReader ImportReaderFromXml(string xmlPath)
    {
        Log($"ImportReader start: {xmlPath}");
        var reader = BarCodeReader.ImportFromXml(xmlPath);
        Log($"ImportReader completed: {xmlPath}");
        return reader;
    }

    /// <summary>
    /// Executes the demonstration: creates temporary files, performs export/import with logging, and writes results.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for all demo artifacts.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeLogDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        LogFilePath = Path.Combine(tempDir, "log.txt");

        // ---------- BarcodeGenerator Export/Import ----------
        string generatorXml = Path.Combine(tempDir, "generator.xml");
        string generatorImageOriginal = Path.Combine(tempDir, "barcode_original.png");
        string generatorImageImported = Path.Combine(tempDir, "barcode_imported.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Configure barcode appearance.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export configuration to XML with logging.
            ExportGeneratorToXml(generator, generatorXml);

            // Save the generated barcode image.
            generator.Save(generatorImageOriginal, BarCodeImageFormat.Png);
        }

        using (var importedGenerator = ImportGeneratorFromXml(generatorXml))
        {
            // Save image generated from imported configuration.
            importedGenerator.Save(generatorImageImported, BarCodeImageFormat.Png);
        }

        // ---------- BarCodeReader Export/Import ----------
        string readerXml = Path.Combine(tempDir, "reader.xml");
        string readerImagePath = generatorImageOriginal; // Reuse the previously generated image.
        string readerResultLog = Path.Combine(tempDir, "reader_results.txt");

        using (var reader = new BarCodeReader(readerImagePath, DecodeType.QR))
        {
            // Adjust reader settings.
            reader.BarcodeSettings.StripFNC = true;

            // Export reader configuration to XML with logging.
            ExportReaderToXml(reader, readerXml);

            // Perform initial read and log result count.
            var results = reader.ReadBarCodes();
            File.AppendAllText(readerResultLog, $"Initial read count: {results.Length}{Environment.NewLine}");
        }

        using (var importedReader = ImportReaderFromXml(readerXml))
        {
            // Apply the same image and decode type to the imported reader.
            importedReader.SetBarCodeImage(readerImagePath);
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Perform read with imported configuration and log result count.
            var results = importedReader.ReadBarCodes();
            File.AppendAllText(readerResultLog, $"Imported read count: {results.Length}{Environment.NewLine}");
        }

        // Output locations for user reference.
        Console.WriteLine($"Temporary folder: {tempDir}");
        Console.WriteLine($"Log file: {LogFilePath}");
        Console.WriteLine($"Generator XML: {generatorXml}");
        Console.WriteLine($"Reader XML: {readerXml}");
        Console.WriteLine($"Reader results logged to: {readerResultLog}");
    }
}