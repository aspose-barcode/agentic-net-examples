// Title: Export barcode recognition state to XML after processing a single image
// Description: Demonstrates generating a QR barcode, reading it, and exporting the reader's internal state to an XML file. Useful for debugging or persisting recognition results.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image, BarCodeReader to detect barcodes, and the ExportToXml method to save the recognition state. Developers working with barcode scanning, logging, or integration testing often need to persist reader state for analysis, and this snippet illustrates the typical workflow with key classes like BarcodeGenerator, BarCodeReader, and related settings.
// Prompt: Export the recognition state to an XML file after processing a single barcode image.
// Tags: qr, barcode, generation, recognition, export, xml, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates a QR barcode, reads it, and exports the recognition state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder to store generated files.
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the barcode image and the exported XML.
        string imagePath = Path.Combine(workFolder, "sample.png");
        string xmlPath = Path.Combine(workFolder, "readerState.xml");

        // Generate a QR barcode image with custom X-dimension.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the barcode reader and configure settings.
        using (var reader = new BarCodeReader())
        {
            // Example setting: strip Function Code (FNC) characters from the result.
            reader.BarcodeSettings.StripFNC = true;

            // Load the generated image into the reader.
            reader.SetBarCodeImage(imagePath);

            // Perform barcode recognition.
            var results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes detected: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // Export the internal recognition state to an XML file.
            reader.ExportToXml(xmlPath);
        }

        // Confirm that the XML file was created and report its location.
        if (File.Exists(xmlPath))
        {
            Console.WriteLine($"Recognition state exported to: {xmlPath}");
        }
        else
        {
            Console.WriteLine("Failed to export recognition state.");
        }
    }
}