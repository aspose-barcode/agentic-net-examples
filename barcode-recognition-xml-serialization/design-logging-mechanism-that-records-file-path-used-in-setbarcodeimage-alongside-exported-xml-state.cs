// Title: Barcode generation, recognition, and state logging example
// Description: Demonstrates generating a QR barcode, reading it, exporting both generation and recognition states to XML, and logging the image path with the XML data.
// Category-Description: This example belongs to the Aspose.BarCode operations category covering barcode generation, recognition, and state export. It showcases the use of BarcodeGenerator, BarCodeReader, and their ExportToXml methods, common tasks for developers who need to persist barcode processing state or integrate with external systems. Typical use cases include audit logging, debugging, and data exchange where the barcode image path and its processing details must be recorded.
// Prompt: Design a logging mechanism that records the file path used in SetBarCodeImage alongside the exported XML state.
// Tags: qr,barcode,generation,recognition,xml,logging,aspose.barcode,aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, XML export, and logging of processing details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR barcode, reads it, exports states to XML, and logs the image path with XML content.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeLogDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the barcode image, XML exports, and log file
        string barcodeImagePath = Path.Combine(workFolder, "barcode.png");
        string generatorXmlPath = Path.Combine(workFolder, "generator.xml");
        string readerXmlPath = Path.Combine(workFolder, "reader.xml");
        string logPath = Path.Combine(workFolder, "log.txt");

        // Generate a QR barcode and export its generation state to XML
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(generatorXmlPath);
        }

        // Initialize a BarCodeReader, set the barcode image source, and export its recognition state to XML
        using (BarCodeReader reader = new BarCodeReader())
        {
            reader.SetBarCodeImage(barcodeImagePath);
            reader.ExportToXml(readerXmlPath);
        }

        // Read the exported XML contents into strings
        string generatorXml = File.ReadAllText(generatorXmlPath);
        string readerXml = File.ReadAllText(readerXmlPath);

        // Log the image path used in SetBarCodeImage together with the exported XML states
        using (StreamWriter logWriter = new StreamWriter(logPath, true))
        {
            logWriter.WriteLine($"SetBarCodeImage Path: {barcodeImagePath}");
            logWriter.WriteLine("Generator XML State:");
            logWriter.WriteLine(generatorXml);
            logWriter.WriteLine("Reader XML State:");
            logWriter.WriteLine(readerXml);
            logWriter.WriteLine("----");
        }

        // Inform the user where the log file is located
        Console.WriteLine("Logging completed. Log file located at:");
        Console.WriteLine(logPath);
    }
}