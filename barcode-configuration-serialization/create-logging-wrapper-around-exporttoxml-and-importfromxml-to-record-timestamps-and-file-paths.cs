// Title: Logging Wrapper for Barcode XML Export/Import
// Description: Demonstrates how to wrap Aspose.BarCode ExportToXml and ImportFromXml methods with simple file logging to capture timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode XML serialization category, showing how to persist and restore BarcodeGenerator and BarCodeReader configurations using ExportToXml/ImportFromXml. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and common use cases like saving settings for later reuse. Developers often need to log these operations for debugging or audit trails.
// Prompt: Create a logging wrapper around ExportToXml and ImportFromXml to record timestamps and file paths.
// Tags: barcode, xml, export, import, logging, aspose.barcode, generator, reader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates logging of XML export/import operations for Aspose.BarCode objects.
/// </summary>
class Program
{
    // Path to the log file; set during initialization.
    static string logFilePath;

    /// <summary>
    /// Appends a timestamped message to the log file.
    /// </summary>
    /// <param name="message">The message to log.</param>
    static void Log(string message)
    {
        File.AppendAllText(logFilePath, $"{DateTime.Now:O} {message}{Environment.NewLine}");
    }

    /// <summary>
    /// Exports a BarcodeGenerator configuration to an XML file and logs the operation.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to export.</param>
    /// <param name="xmlPath">Destination XML file path.</param>
    static void ExportGeneratorToXml(BarcodeGenerator generator, string xmlPath)
    {
        Log($"Exporting BarcodeGenerator to '{xmlPath}'");
        generator.ExportToXml(xmlPath);
    }

    /// <summary>
    /// Imports a BarcodeGenerator configuration from an XML file and logs the operation.
    /// </summary>
    /// <param name="xmlPath">Source XML file path.</param>
    /// <returns>A new BarcodeGenerator instance populated from XML.</returns>
    static BarcodeGenerator ImportGeneratorFromXml(string xmlPath)
    {
        Log($"Importing BarcodeGenerator from '{xmlPath}'");
        return BarcodeGenerator.ImportFromXml(xmlPath);
    }

    /// <summary>
    /// Exports a BarCodeReader configuration to an XML file and logs the operation.
    /// </summary>
    /// <param name="reader">The BarCodeReader instance to export.</param>
    /// <param name="xmlPath">Destination XML file path.</param>
    static void ExportReaderToXml(BarCodeReader reader, string xmlPath)
    {
        Log($"Exporting BarCodeReader to '{xmlPath}'");
        reader.ExportToXml(xmlPath);
    }

    /// <summary>
    /// Imports a BarCodeReader configuration from an XML file and logs the operation.
    /// </summary>
    /// <param name="xmlPath">Source XML file path.</param>
    /// <returns>A new BarCodeReader instance populated from XML.</returns>
    static BarCodeReader ImportReaderFromXml(string xmlPath)
    {
        Log($"Importing BarCodeReader from '{xmlPath}'");
        return BarCodeReader.ImportFromXml(xmlPath);
    }

    /// <summary>
    /// Entry point of the demo. Creates, exports, imports, and uses barcode generator and reader objects while logging each XML operation.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for all demo files.
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);
        logFilePath = Path.Combine(workFolder, "log.txt");

        // Define file paths for XML configurations and generated images.
        string generatorXml = Path.Combine(workFolder, "generator.xml");
        string generatorImg = Path.Combine(workFolder, "generated.png");
        string generatorImgImported = Path.Combine(workFolder, "generated_imported.png");
        string readerXml = Path.Combine(workFolder, "reader.xml");

        // ---------- BarcodeGenerator: create, export, import ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Adjust visual settings.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Export configuration to XML and log the action.
            ExportGeneratorToXml(generator, generatorXml);

            // Save the generated barcode image.
            generator.Save(generatorImg, BarCodeImageFormat.Png);
        }

        // Import the previously exported generator configuration and save a new image.
        using (var importedGenerator = ImportGeneratorFromXml(generatorXml))
        {
            importedGenerator.Save(generatorImgImported, BarCodeImageFormat.Png);
        }

        // ---------- BarCodeReader: read image, export, import ----------
        using (var reader = new BarCodeReader(generatorImg, DecodeType.Code128))
        {
            // Configure reader settings.
            reader.BarcodeSettings.StripFNC = true;

            // Export reader configuration to XML and log the action.
            ExportReaderToXml(reader, readerXml);
        }

        // Import the reader configuration, set the image and decode type, then read barcodes.
        using (var importedReader = ImportReaderFromXml(readerXml))
        {
            importedReader.SetBarCodeImage(generatorImg);
            importedReader.SetBarCodeReadType(DecodeType.Code128);
            var results = importedReader.ReadBarCodes();

            // Output each decoded barcode to the console.
            foreach (var result in results)
            {
                Console.WriteLine($"Read barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
            }
        }

        // Inform the user where the demo files are located.
        Console.WriteLine($"Demo completed. Files are in: {workFolder}");
    }
}