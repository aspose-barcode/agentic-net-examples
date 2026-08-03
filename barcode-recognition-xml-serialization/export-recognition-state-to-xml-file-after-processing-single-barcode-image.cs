// Title: Export barcode recognition state to XML
// Description: Demonstrates how to read a barcode from an image and export the full recognition state to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader to detect barcodes, retrieve their properties, and serialize the recognition session via ExportToXml. Developers often need to log or audit barcode scans, and this pattern shows how to generate a detailed XML report using key classes like BarCodeReader, DecodeType, and BarcodeGenerator.
// Prompt: Export the recognition state to an XML file after processing a single barcode image.
// Tags: barcode, recognition, xml, export, aspose.barcode, code128, generation, reading

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a sample barcode (if missing), reads it,
/// displays detected information, and exports the complete recognition state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation (optional), recognition,
    /// console output, and XML export of the recognition state.
    /// </summary>
    static void Main()
    {
        // Define file names in the current directory
        string imagePath = "sample_barcode.png";
        string xmlPath = "recognition_state.xml";

        // Ensure a barcode image exists; generate one if missing
        if (!File.Exists(imagePath))
        {
            // Create a barcode generator for Code128 with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated sample barcode image: {imagePath}");
            }
        }

        // Verify the image file exists before processing
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Create a reader for the image and detect barcodes
        using (var reader = new BarCodeReader())
        {
            // Assign the image to the reader
            reader.SetBarCodeImage(imagePath);

            // Use all supported symbologies for detection
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Perform recognition and retrieve results
            var results = reader.ReadBarCodes();

            // Output detected barcodes to console
            foreach (var result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }

            // Export the full recognition state to an XML file
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Recognition state exported to: {xmlPath}");
        }
    }
}