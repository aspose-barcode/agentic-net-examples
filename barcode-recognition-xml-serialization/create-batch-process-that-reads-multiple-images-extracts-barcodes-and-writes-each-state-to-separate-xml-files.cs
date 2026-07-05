// Title: Batch barcode extraction to XML
// Description: Demonstrates reading multiple image files, extracting any barcodes found, and writing each barcode's details to a separate XML file.
// Prompt: Create a batch process that reads multiple images, extracts barcodes, and writes each state to separate XML files.
// Tags: barcode, batch, xml, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Xml;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that processes a collection of image files,
/// extracts all detected barcodes, and writes each barcode's
/// type and text to an individual XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over a predefined list of image paths,
    /// reads barcodes using Aspose.BarCode, and generates XML files for each barcode found.
    /// </summary>
    static void Main()
    {
        // Define the list of image files to be processed.
        // Adjust the file paths as needed for your environment.
        string[] imageFiles = new string[]
        {
            "image1.png",
            "image2.png",
            "image3.png",
            "image4.png",
            "image5.png"
        };

        // Process each image file in the list.
        foreach (string imagePath in imageFiles)
        {
            // Verify that the file exists before attempting to read it.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Initialize a barcode reader for the current image,
            // configured to detect all supported barcode types.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                int barcodeIndex = 0; // Counter for naming XML files uniquely per image.

                // Iterate over all detected barcodes in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Construct the XML file name using the image name and barcode index.
                    string xmlFileName = $"{Path.GetFileNameWithoutExtension(imagePath)}_{barcodeIndex}.xml";

                    // Create an XML writer with indentation for readability.
                    using (var writer = XmlWriter.Create(xmlFileName, new XmlWriterSettings { Indent = true }))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("BarCode");

                        // Write barcode type and text elements, handling possible null values.
                        writer.WriteElementString("Type", result.CodeTypeName ?? string.Empty);
                        writer.WriteElementString("CodeText", result.CodeText ?? string.Empty);

                        writer.WriteEndElement(); // </BarCode>
                        writer.WriteEndDocument();
                    }

                    Console.WriteLine($"Processed barcode {barcodeIndex} from '{imagePath}' -> '{xmlFileName}'");
                    barcodeIndex++;
                }

                // If no barcodes were detected, inform the user.
                if (barcodeIndex == 0)
                {
                    Console.WriteLine($"No barcodes detected in '{imagePath}'.");
                }
            }
        }
    }
}