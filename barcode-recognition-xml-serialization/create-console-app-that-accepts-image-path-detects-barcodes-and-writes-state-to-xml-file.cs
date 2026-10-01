// Title: Detect Barcodes in an Image and Export Results to XML
// Description: Demonstrates how to read an image file, detect all barcodes using Aspose.BarCode, and save the detection results to an XML document.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class for scanning images, extracting CodeText and Symbology, and persisting results in a structured XML format. Typical use cases include batch processing of scanned documents, inventory verification, and automated data extraction where developers need to programmatically capture barcode information and store it for downstream processing.
// Prompt: Create a console app that accepts an image path, detects barcodes, and writes state to an XML file.
// Tags: barcode detection, xml output, console application, aspose.barcode, barcodereader, barcode recognition

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that detects barcodes in an image file and writes the results to an XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Parses command‑line arguments, validates the input image, and initiates barcode detection.
    /// </summary>
    /// <param name="args">Optional arguments: [0] = image path, [1] = XML output path.</param>
    static void Main(string[] args)
    {
        // Default temporary file locations used when no arguments are supplied.
        string sampleImagePath = Path.Combine(Path.GetTempPath(), "sample.png");
        string sampleXmlPath = Path.Combine(Path.GetTempPath(), "detected_barcodes.xml");

        // Use provided arguments if present; otherwise fall back to defaults.
        string imagePath = args.Length > 0 ? args[0] : sampleImagePath;
        string xmlOutputPath = args.Length > 1 ? args[1] : sampleXmlPath;

        // Verify that the image file exists before attempting detection.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Perform barcode detection and write the results to the specified XML file.
            DetectBarcodesAndWriteXml(imagePath, xmlOutputPath);
            Console.WriteLine($"Barcode detection completed. Results saved to: {xmlOutputPath}");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors to the console.
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Detects all barcodes in the given image and creates an XML file containing each barcode's text and symbology.
    /// </summary>
    /// <param name="imagePath">Path to the image file to be scanned.</param>
    /// <param name="xmlOutputPath">Path where the resulting XML document will be saved.</param>
    private static void DetectBarcodesAndWriteXml(string imagePath, string xmlOutputPath)
    {
        // Initialize the barcode reader for the specified image.
        using (var reader = new BarCodeReader(imagePath))
        {
            // Root XML element that will contain individual barcode entries.
            var barcodesElement = new XElement("Barcodes");

            // Iterate through all detected barcodes.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Create an XML element for each barcode with attributes for code text and symbology.
                var barcodeElement = new XElement("Barcode",
                    new XAttribute("CodeText", result.CodeText ?? string.Empty),
                    new XAttribute("Symbology", result.CodeTypeName ?? string.Empty));

                // Add the barcode element to the root container.
                barcodesElement.Add(barcodeElement);
            }

            // Build the final XML document with a declaration and the root element.
            var document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), barcodesElement);
            // Save the XML document to the specified path.
            document.Save(xmlOutputPath);
        }
    }
}