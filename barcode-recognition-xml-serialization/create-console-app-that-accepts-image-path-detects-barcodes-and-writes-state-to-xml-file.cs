// Title: Detect barcodes in an image and export reader state to XML
// Description: This example loads an image, uses Aspose.BarCode to detect all barcodes, prints them, and saves the reader's internal state as an XML file.
// Category-Description: Demonstrates Aspose.BarCode barcode recognition workflow, covering BarCodeReader initialization, image loading, barcode extraction, and state export. Typical use cases include batch processing of scanned documents, inventory verification, and integration with downstream systems that consume XML metadata. Developers working with barcode scanning often need to log detection results and preserve reader configuration for auditing or debugging.
// Prompt: Create a console app that accepts an image path, detects barcodes, and writes state to an XML file.
// Tags: barcode detection, xml export, aspose.barcode, console app, c#

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that reads an image, detects barcodes, and saves the reader state to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, processes the image, and writes results.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is the image file path.</param>
    static void Main(string[] args)
    {
        // Determine image path (first argument or default sample)
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Build the XML output path in the same folder as the image
        string xmlPath = Path.Combine(
            Path.GetDirectoryName(imagePath) ?? Directory.GetCurrentDirectory(),
            Path.GetFileNameWithoutExtension(imagePath) + "_state.xml");

        // Create BarCodeReader, set image, read barcodes, and export state
        using (BarCodeReader reader = new BarCodeReader())
        {
            // Load the image to be processed
            reader.SetBarCodeImage(imagePath);

            // Read all barcodes present in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes found
            Console.WriteLine($"Barcodes found: {results.Length}");

            // List each detected barcode with its type and text
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Export the reader's internal state to an XML file
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Reader state saved to: {xmlPath}");
        }
    }
}