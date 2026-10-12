// Title: Generate QR barcode, export reader state to XML, and log file paths
// Description: This example creates a QR code image, reads it using Aspose.BarCode, exports the reader's state to XML, and logs the used file paths.
// Category-Description: Demonstrates core Aspose.BarCode operations such as barcode generation, image loading, state export, and custom logging. It showcases the BarcodeGenerator, BarCodeReader, and related classes, useful for developers needing to persist barcode processing details or audit file usage. Ideal for tutorials on QR code handling, XML state management, and simple logging in .NET applications.
// Prompt: Design a logging mechanism that records the file path used in SetBarCodeImage alongside the exported XML state.
// Tags: qr, barcode generation, barcode reading, xml export, logging, aspose.barcode, csharp, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, reading, XML export, and logging of file paths using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, exports the reader state, and logs the file locations.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory for all generated files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define paths for the barcode image, exported XML, and log file
        string imagePath = Path.Combine(workDir, "barcode.png");
        string xmlPath = Path.Combine(workDir, "readerState.xml");
        string logPath = Path.Combine(workDir, "log.txt");

        // Generate a QR code image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Initialize a barcode reader, load the generated image, and export its internal state to XML
        using (BarCodeReader reader = new BarCodeReader())
        {
            reader.SetBarCodeImage(imagePath);
            reader.ExportToXml(xmlPath);
        }

        // Build a log entry containing the image path used in SetBarCodeImage and the XML export location
        string logEntry = $"SetBarCodeImage Path: {imagePath}{Environment.NewLine}" +
                          $"Exported XML Path: {xmlPath}{Environment.NewLine}";

        // Append the log entry to the log file
        File.AppendAllText(logPath, logEntry);

        // Write log details to the console for immediate feedback
        Console.WriteLine("Logging completed. Details:");
        Console.WriteLine(logEntry);
        Console.WriteLine($"All files are located in: {workDir}");
    }
}