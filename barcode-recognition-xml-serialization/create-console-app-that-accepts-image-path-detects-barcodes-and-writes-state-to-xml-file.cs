// Title: Detect barcodes in an image and export recognition state to XML
// Description: Demonstrates how to load an image, recognize barcodes using Aspose.BarCode, and save the recognition state as an XML file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the BarCodeReader class for detecting barcodes and the ExportToXml method for persisting recognition details. Typical scenarios include batch processing of scanned documents, inventory verification, and automated data extraction where developers need to programmatically read barcodes and log results.
// Prompt: Create a console app that accepts an image path, detects barcodes, and writes state to an XML file.
// Tags: barcode detection, xml export, aspose.barcode, barcodereader, barcodegenerator, console app

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that reads an image, detects any barcodes it contains,
/// and exports the recognition state to an XML file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be an existing image path.</param>
    static void Main(string[] args)
    {
        string imagePath;

        // Determine the source image: use the provided path if valid, otherwise generate a sample barcode.
        if (args.Length > 0 && File.Exists(args[0]))
        {
            imagePath = args[0];
        }
        else
        {
            // Create a temporary folder for the sample image.
            string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);
            imagePath = Path.Combine(tempFolder, "sample.png");

            // Generate a Code128 barcode and save it as PNG.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated sample barcode image at: {imagePath}");
        }

        // Verify that the image file exists before attempting recognition.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for the image.
        using (var reader = new BarCodeReader(imagePath))
        {
            // Perform barcode detection.
            var results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Export the full recognition state to an XML file alongside the image.
            string xmlPath = Path.ChangeExtension(imagePath, ".xml");
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Recognition state exported to XML: {xmlPath}");
        }
    }
}