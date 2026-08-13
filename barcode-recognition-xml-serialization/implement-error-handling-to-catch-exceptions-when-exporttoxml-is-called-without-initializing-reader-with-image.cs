// Title: ExportToXml error handling demonstration
// Description: Demonstrates catching exceptions when calling ExportToXml on a BarCodeReader that hasn't been initialized with an image.
// Category-Description: This example belongs to the Aspose.BarCode reading and exporting category, illustrating the use of BarCodeReader and BarcodeGenerator classes to read barcodes and export results to XML. Developers often need to handle missing image scenarios, export data for downstream processing, and ensure robust error handling in barcode automation workflows.
// Prompt: Implement error handling to catch exceptions when ExportToXml is called without initializing the reader with an image.
// Tags: barcode symbology, error handling, export, xml, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates error handling for ExportToXml when the BarCodeReader is not initialized with an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Shows both failing and successful ExportToXml scenarios.
    /// </summary>
    static void Main()
    {
        // Path for the XML file that will be created by ExportToXml.
        string xmlPath = "reader_export.xml";

        // -----------------------------------------------------------------
        // Attempt to export without initializing the reader with an image.
        // This should throw an exception which we catch and handle.
        // -----------------------------------------------------------------
        try
        {
            using (var reader = new BarCodeReader())
            {
                // ExportToXml requires an image; without one it throws.
                reader.ExportToXml(xmlPath);
            }
        }
        catch (Exception ex)
        {
            // Expected exception handling.
            Console.WriteLine("Caught exception as expected: " + ex.Message);
        }

        // -----------------------------------------------------------------
        // Optional: demonstrate a successful ExportToXml after setting an image.
        // -----------------------------------------------------------------
        string barcodeImagePath = "sample.png";

        // Generate a sample barcode image to use for the successful case.
        GenerateSampleBarcode(barcodeImagePath);

        try
        {
            using (var reader = new BarCodeReader(barcodeImagePath))
            {
                // Now the reader has an image, so ExportToXml succeeds.
                reader.ExportToXml(xmlPath);
                Console.WriteLine("ExportToXml succeeded after initializing the reader with an image.");
            }
        }
        catch (Exception ex)
        {
            // Unexpected exception handling.
            Console.WriteLine("Unexpected error during ExportToXml: " + ex.Message);
        }
    }

    /// <summary>
    /// Generates a simple Code128 barcode image for demonstration purposes.
    /// </summary>
    /// <param name="filePath">Path where the barcode image will be saved.</param>
    static void GenerateSampleBarcode(string filePath)
    {
        // Create a barcode generator for Code128 with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Save the generated barcode image to the specified file.
            generator.Save(filePath);
        }
    }
}