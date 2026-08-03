// Title: Load XML state, set image, and read barcodes
// Description: Demonstrates loading a BarCodeReader configuration from an XML state file, assigning an image, and printing detected barcode values.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to import a BarCodeReader configuration from XML using BarCodeReader.ImportFromXml, set a barcode image with SetBarCodeImage, and retrieve results via ReadBarCodes. Typical use cases include batch processing of images with predefined settings, automated scanning workflows, and integration into CI pipelines where configuration is stored externally. Developers often need to load saved state, apply it to new images, and extract barcode data programmatically.
// Prompt: Develop a utility that loads an XML state file, sets the corresponding image, and outputs detected barcode values.
// Tags: barcode, xml, import, read, aspose.barcode, barcodereader, detection

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Utility that loads a BarCodeReader configuration from an XML state file,
/// assigns a barcode image, and outputs detected barcode values to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the file paths for the XML state file and the barcode image.
        const string xmlPath = "state.xml";
        const string imagePath = "barcode.png";

        // Ensure the XML state file exists before proceeding.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML state file not found: {xmlPath}");
            return;
        }

        // Ensure the barcode image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Barcode image file not found: {imagePath}");
            return;
        }

        // Load a configured BarCodeReader instance from the XML state file.
        // ImportFromXml parses the XML and returns a ready‑to‑use reader.
        using (BarCodeReader reader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Assign the image that will be processed by the reader.
            reader.SetBarCodeImage(imagePath);

            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }
    }
}