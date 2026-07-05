// Title: Import barcode reader settings from XML and decode an image
// Description: Demonstrates loading a saved BarCodeReader configuration from an XML file, then applying it to a new reader instance to decode a barcode image.
// Prompt: Import a saved XML state file into a new reader instance before setting the image.
// Tags: barcode, import, xml, reader, decode, aspose, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that imports a saved BarCodeReader state from XML and decodes a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads XML state, configures the reader, and reads barcodes from an image.
    /// </summary>
    static void Main()
    {
        // Paths to the XML state file and the barcode image.
        string xmlPath = "readerState.xml";
        string imagePath = "barcode.png";

        // Verify that the XML file exists.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML state file not found: {xmlPath}");
            return;
        }

        // Verify that the image file exists.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Barcode image file not found: {imagePath}");
            return;
        }

        // Create a new BarCodeReader instance.
        using (var reader = new BarCodeReader())
        {
            // Import the saved settings from the XML file.
            // This static method applies the imported settings to the current reader instance.
            BarCodeReader.ImportFromXml(xmlPath);

            // Optionally set the decode type to all supported types.
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Assign the image to be processed.
            reader.SetBarCodeImage(imagePath);

            // Perform recognition and output results.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Type: {result.CodeTypeName}");
                Console.WriteLine($"Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Region: {result.Region.Rectangle}");
                Console.WriteLine();
            }
        }
    }
}