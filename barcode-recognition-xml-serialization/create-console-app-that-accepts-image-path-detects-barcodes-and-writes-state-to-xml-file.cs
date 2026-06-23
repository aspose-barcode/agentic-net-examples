using System;
using System.IO;
using System.Xml;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Reads barcodes from an image file and writes the detection results to an XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may specify the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the image path: use the first command‑line argument if supplied,
        // otherwise fall back to a default sample image.
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the specified image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Define the output XML file path where barcode information will be saved.
        string xmlPath = "Barcodes.xml";

        // Create a BarCodeReader that attempts to decode all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Perform the barcode recognition and obtain an array of results.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Write the detection results to an XML file with indentation for readability.
            using (var writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
            {
                writer.WriteStartDocument();               // Begin the XML document.
                writer.WriteStartElement("Barcodes");      // Root element.

                // Iterate over each detected barcode and write its details as an element.
                foreach (var result in results)
                {
                    writer.WriteStartElement("BarCode");
                    writer.WriteAttributeString("Type", result.CodeTypeName ?? string.Empty);
                    writer.WriteAttributeString("CodeText", result.CodeText ?? string.Empty);
                    writer.WriteEndElement(); // BarCode
                }

                writer.WriteEndElement(); // Barcodes
                writer.WriteEndDocument(); // End of XML document.
            }

            // Inform the user about the number of barcodes detected and the output location.
            Console.WriteLine($"Detected {results.Length} barcode(s). Results written to {xmlPath}.");
        }
    }
}