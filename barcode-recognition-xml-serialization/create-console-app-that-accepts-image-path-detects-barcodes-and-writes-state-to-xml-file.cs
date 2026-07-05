// Title: Barcode Detection and XML Export
// Description: Detects all barcodes in an image file and writes their type and text to an XML document.
// Prompt: Create a console app that accepts an image path, detects barcodes, and writes state to an XML file.
// Tags: barcode, detection, xml, console, aspose.barcoderecognition

using System;
using System.IO;
using System.Xml;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to read barcodes from an image and export the results to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Accepts an optional image path argument, detects barcodes, and writes the results to an XML file.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be the image file path.</param>
    static void Main(string[] args)
    {
        // Determine image path from command‑line or use a default sample.
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Prepare the output XML file path by changing the image extension to .xml.
        string xmlPath = Path.ChangeExtension(imagePath, ".xml");

        // Configure the XML writer to produce indented, human‑readable output.
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  "
        };

        // Open the XML writer within a using block to ensure proper disposal.
        using (XmlWriter writer = XmlWriter.Create(xmlPath, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("Barcodes");

            // Initialize the barcode reader to detect all supported barcode types.
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Iterate through each detected barcode in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    writer.WriteStartElement("BarCode");

                    // Write the barcode type name (e.g., QR, Code128).
                    writer.WriteElementString("Type", result.CodeTypeName ?? string.Empty);

                    // Write the decoded text/value of the barcode.
                    writer.WriteElementString("CodeText", result.CodeText ?? string.Empty);

                    // Optional: write the region bounds of the barcode if needed.
                    // var rect = result.Region.Rectangle;
                    // writer.WriteStartElement("Region");
                    // writer.WriteElementString("X", rect.X.ToString());
                    // writer.WriteElementString("Y", rect.Y.ToString());
                    // writer.WriteElementString("Width", rect.Width.ToString());
                    // writer.WriteElementString("Height", rect.Height.ToString());
                    // writer.WriteEndElement(); // Region

                    writer.WriteEndElement(); // BarCode
                }
            }

            writer.WriteEndElement(); // Barcodes
            writer.WriteEndDocument();
        }

        // Inform the user that processing is complete and provide the XML file location.
        Console.WriteLine($"Barcode detection completed. Results saved to: {xmlPath}");
    }
}