// Title: Detect barcodes in an image and export recognition state to XML
// Description: Loads an image, detects any barcodes using Aspose.BarCode, and writes the reader's internal state to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It demonstrates how to use the BarCodeReader class to scan an image for supported symbologies, retrieve decoded values, and export the full recognition state via ExportToXml. Typical scenarios include batch processing of scanned documents, automated inventory checks, and integration with downstream systems that consume XML reports of barcode data.
// Prompt: Create a console app that accepts an image path, detects barcodes, and writes state to an XML file.
// Tags: barcode detection, barcode recognition, xml output, aspose.barcode, console app

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode detection from an image file and exports the recognition state to an XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Accepts an optional image path argument, generates a sample barcode if the file is missing,
    /// reads barcodes from the image, and writes the reader state to an XML file.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may contain the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image path: use the first argument if supplied, otherwise fall back to a default name.
        string imagePath = args.Length > 0 ? args[0] : "sample_barcode.png";

        // If the specified image does not exist, create a temporary sample barcode image.
        if (!File.Exists(imagePath))
        {
            // Build a temporary file path for the sample barcode.
            string samplePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

            // Generate a Code128 barcode with sample data and save it as PNG.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                generator.Save(samplePath, BarCodeImageFormat.Png);
            }

            // Update the imagePath to point to the newly created sample.
            imagePath = samplePath;
            Console.WriteLine($"Sample barcode generated at: {imagePath}");
        }

        // Initialize the barcode reader for the image and perform recognition.
        using (BarCodeReader reader = new BarCodeReader(imagePath))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected.
            Console.WriteLine($"Barcodes found: {results.Length}");

            // Iterate through each result and display its type and decoded text.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Determine the output XML file path in the same directory as the image.
            string xmlPath = Path.Combine(
                Path.GetDirectoryName(imagePath) ?? Directory.GetCurrentDirectory(),
                "readerState.xml");

            // Export the full recognition state (including metadata) to the XML file.
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Recognition state exported to: {xmlPath}");
        }
    }
}