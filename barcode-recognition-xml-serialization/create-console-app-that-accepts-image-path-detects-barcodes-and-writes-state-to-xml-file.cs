// Title: Detect barcodes in an image and export results to XML
// Description: Loads an image, detects any barcodes using Aspose.BarCode, and writes detection details to an XML file.
// Category-Description: Demonstrates barcode recognition with Aspose.BarCode in a console application. The example shows how to use BarcodeGenerator for fallback image creation, BarCodeReader for detecting all supported symbologies, and XmlWriter for persisting results. Ideal for developers needing quick barcode extraction and reporting in automation pipelines.
// Prompt: Create a console app that accepts an image path, detects barcodes, and writes state to an XML file.
// Tags: barcode detection, code128, xml output, aspose.barcode, console app

using System;
using System.IO;
using System.Xml;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Console application that reads an image, detects barcodes, and writes detection results to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, generates a sample barcode if the file is missing,
    /// reads all supported barcodes, and saves the results to an XML document.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is treated as the image file path.</param>
    static void Main(string[] args)
    {
        // Determine image path (first argument or default)
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // If the image does not exist, generate a simple sample barcode
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as a PNG file
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Prepare XML output path (same folder as the image)
        string xmlPath = Path.Combine(Path.GetDirectoryName(imagePath) ?? "", "barcode_results.xml");

        // Read barcodes from the image using all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();

            // Create an XML writer with indentation for readability
            using (var writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Barcodes");

                // Iterate over each detected barcode and write its details
                foreach (var result in results)
                {
                    writer.WriteStartElement("BarCode");

                    writer.WriteElementString("Type", result.CodeTypeName ?? string.Empty);
                    writer.WriteElementString("CodeText", result.CodeText ?? string.Empty);
                    writer.WriteElementString("Confidence", result.Confidence.ToString());
                    writer.WriteElementString("ReadingQuality", result.ReadingQuality.ToString());

                    // Write the region (bounding rectangle) of the barcode
                    var rect = result.Region.Rectangle;
                    writer.WriteStartElement("Region");
                    writer.WriteElementString("X", rect.X.ToString());
                    writer.WriteElementString("Y", rect.Y.ToString());
                    writer.WriteElementString("Width", rect.Width.ToString());
                    writer.WriteElementString("Height", rect.Height.ToString());
                    writer.WriteEndElement(); // Region

                    writer.WriteEndElement(); // BarCode
                }

                writer.WriteEndElement(); // Barcodes
                writer.WriteEndDocument();
            }
        }

        // Inform the user where the XML file was written
        Console.WriteLine($"Barcode detection completed. Results saved to: {Path.GetFullPath(xmlPath)}");
    }
}