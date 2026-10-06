// Title: Barcode generation, recognition, and state import/export demo
// Description: Demonstrates generating a Code128 barcode, exporting its recognition state to XML, and re-importing that state to read the barcode again. Useful for background services that process barcode images based on saved XML states.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing how to use BarcodeGenerator, BarCodeReader, and the XML state import/export APIs. Developers building automated barcode workflows—such as background services monitoring folders for new images and associated state files—can learn how to persist and restore reader settings, handle image generation, and perform recognition without manual intervention. Typical use cases include batch processing, archival, and integration with external systems.
// Prompt: Develop a background service that monitors a folder, imports XML states, and processes pending barcode images automatically.
// Tags: barcode generation, barcode recognition, xml state import, xml export, code128, background service, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, state export to XML, and re-import for recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates a barcode image, exports its
    /// recognition state to XML, then imports the state to read the barcode again.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeService_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated barcode image and its recognition state XML
        string imagePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "state.xml");

        // Generate a simple Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and export the reader state to an XML file
        using (var reader = new BarCodeReader(imagePath, (SingleDecodeType)DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var res in results)
            {
                Console.WriteLine($"Initial read: {res.CodeTypeName} - {res.CodeText}");
            }

            // Export the current recognition state (including settings) to XML
            reader.ExportToXml(xmlPath);
        }

        // Verify that the XML state file was successfully created
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to export recognition state to XML.");
            return;
        }

        // Simulate processing of pending XML state files in the temporary folder
        string[] xmlFiles = Directory.GetFiles(tempFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            Console.WriteLine($"Processing XML state: {Path.GetFileName(xmlFile)}");

            // Import reader settings from the XML state file
            using (var importedReader = BarCodeReader.ImportFromXml(xmlFile))
            {
                // Associate the previously generated barcode image with the imported reader
                importedReader.SetBarCodeImage(imagePath);

                // Perform barcode recognition using the imported settings
                var importedResults = importedReader.ReadBarCodes();
                foreach (var res in importedResults)
                {
                    Console.WriteLine($"Imported read: {res.CodeTypeName} - {res.CodeText}");
                }
            }
        }

        // Optional cleanup: delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors in CI environments
        }
    }
}