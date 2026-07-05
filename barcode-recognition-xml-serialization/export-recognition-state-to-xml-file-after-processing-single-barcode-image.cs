// Title: Export Barcode Recognition State to XML
// Description: Demonstrates how to read a barcode image, display detected information, and export the full recognition state to an XML file.
// Prompt: Export the recognition state to an XML file after processing a single barcode image.
// Tags: barcode, recognition, xml, export, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads a barcode image, prints detection results,
/// and saves the complete recognition state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the barcode image to be processed.
        const string imagePath = "barcode.png";

        // Path where the recognition state XML will be saved.
        const string xmlOutputPath = "recognition_state.xml";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file \"{imagePath}\" not found.");
            return;
        }

        // Create a BarCodeReader for the image, detecting all supported symbologies.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic information about each detected barcode.
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }

            // Export the full recognition state (detected barcodes, settings, metadata) to an XML file.
            reader.ExportToXml(xmlOutputPath);
            Console.WriteLine($"Recognition state exported to \"{xmlOutputPath}\".");
        }
    }
}