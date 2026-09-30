// Title: Logging wrapper for Aspose.BarCode XML export and import
// Description: Demonstrates how to record timestamps and file paths when exporting a barcode generator's configuration to XML and importing it back, using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of ExportToXml and ImportFromXml methods together with a simple file‑based logging mechanism. Developers often need to persist barcode settings, share them across applications, or audit configuration changes; the key API classes are BarcodeGenerator, BarcodeXmlLogger, and BarCodeReader. Typical use cases include automated testing, configuration versioning, and troubleshooting.
// Prompt: Create a logging wrapper around ExportToXml and ImportFromXml to record timestamps and file paths.
// Tags: barcode, xml, export, import, logging, aspose.barcode, configuration, generator, reader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides static methods that wrap Aspose.BarCode XML export/import operations
/// with simple file‑based logging of timestamps and file paths.
/// </summary>
class BarcodeXmlLogger
{
    // Path to the log file stored in the system temporary folder.
    private static readonly string LogFile = Path.Combine(Path.GetTempPath(), "BarcodeXmlLog.txt");

    /// <summary>
    /// Exports the configuration of a <see cref="BarcodeGenerator"/> to an XML file
    /// and writes a log entry containing the UTC timestamp and target file path.
    /// </summary>
    /// <param name="generator">The barcode generator whose configuration will be exported.</param>
    /// <param name="xmlFilePath">The full path of the XML file to create.</param>
    public static void ExportToXmlWithLog(BarcodeGenerator generator, string xmlFilePath)
    {
        // Export configuration to XML string via a memory stream.
        using (var ms = new MemoryStream())
        {
            generator.ExportToXml(ms);
            ms.Position = 0;
            using (var sr = new StreamReader(ms, leaveOpen: true))
            {
                string xmlContent = sr.ReadToEnd();

                // Ensure the target directory exists.
                string dir = Path.GetDirectoryName(xmlFilePath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // Write the XML content to the specified file.
                File.WriteAllText(xmlFilePath, xmlContent);
            }
        }

        // Log the successful export operation.
        Log($"{DateTime.UtcNow:u} - Exported XML to \"{xmlFilePath}\"");
    }

    /// <summary>
    /// Imports a barcode generator configuration from an XML file and logs the operation.
    /// </summary>
    /// <param name="xmlFilePath">The full path of the XML file to read.</param>
    /// <returns>A new <see cref="BarcodeGenerator"/> instance configured from the XML.</returns>
    public static BarcodeGenerator ImportFromXmlWithLog(string xmlFilePath)
    {
        if (!File.Exists(xmlFilePath))
        {
            // Log the failure and throw an exception if the file does not exist.
            Log($"{DateTime.UtcNow:u} - Import failed: file not found \"{xmlFilePath}\"");
            throw new FileNotFoundException("XML configuration file not found.", xmlFilePath);
        }

        // Read XML content into a memory stream.
        byte[] bytes = File.ReadAllBytes(xmlFilePath);
        using (var ms = new MemoryStream(bytes))
        {
            // Import configuration from the memory stream.
            BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(ms);

            // Log the successful import operation.
            Log($"{DateTime.UtcNow:u} - Imported XML from \"{xmlFilePath}\"");

            return generator;
        }
    }

    // Writes a log entry to the log file and echoes it to the console.
    private static void Log(string message)
    {
        File.AppendAllText(LogFile, message + Environment.NewLine);
        Console.WriteLine(message);
    }
}

class Program
{
    /// <summary>
    /// Demonstrates exporting a barcode generator to XML, importing it back,
    /// and logging each step. Also shows barcode image creation and reading.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for all demo files.
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the XML configuration and barcode images.
        string xmlPath = Path.Combine(workFolder, "generator_config.xml");
        string imagePath = Path.Combine(workFolder, "barcode.png");

        // Create a barcode generator, configure it, and export its XML with logging.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Example visual configuration.
            generator.Parameters.Barcode.BarColor = Color.DarkBlue;
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Export configuration to XML and log the operation.
            BarcodeXmlLogger.ExportToXmlWithLog(generator, xmlPath);

            // Save the barcode image for later reading.
            generator.Save(imagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to \"{imagePath}\"");
        }

        // Import the configuration from XML with logging and generate a new image.
        using (var importedGenerator = BarcodeXmlLogger.ImportFromXmlWithLog(xmlPath))
        {
            string importedImagePath = Path.Combine(workFolder, "barcode_imported.png");
            importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Imported barcode image saved to \"{importedImagePath}\"");
        }

        // Demonstrate reading the original barcode image using BarCodeReader.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read CodeText: {result.CodeText}, Symbology: {result.CodeTypeName}");
            }
        }

        // Indicate where the log file is located for the user.
        Console.WriteLine($"Log file located at \"{Path.Combine(Path.GetTempPath(), "BarcodeXmlLog.txt")}\"");
    }
}