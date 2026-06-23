using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcodes from images and exporting the results to XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes a set of sample images,
    /// extracts barcode information, and writes the data to XML files.
    /// </summary>
    static void Main()
    {
        // Define sample image file paths (replace with actual paths as needed)
        string[] imagePaths = new string[]
        {
            "sample1.png",
            "sample2.png",
            "sample3.png"
        };

        // Ensure the output directory exists; create it if it does not
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each image file individually
        foreach (string imagePath in imagePaths)
        {
            // Skip processing if the file cannot be found
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Initialize a barcode reader for the current image, supporting all barcode types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes present in the image
                var results = reader.ReadBarCodes();

                // Determine the XML output file name based on the image name
                string xmlFileName = Path.GetFileNameWithoutExtension(imagePath) + ".xml";
                string xmlPath = Path.Combine(outputDir, xmlFileName);

                // Write the barcode data to an XML file with indentation for readability
                using (var writer = XmlWriter.Create(xmlPath, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();                                 // XML declaration
                    writer.WriteStartElement("Barcodes");                        // Root element
                    writer.WriteAttributeString("SourceImage", Path.GetFileName(imagePath));

                    // Iterate over each detected barcode and write its details
                    foreach (var result in results)
                    {
                        writer.WriteStartElement("BarCode");

                        writer.WriteElementString("Type", result.CodeTypeName ?? string.Empty);
                        writer.WriteElementString("CodeText", result.CodeText ?? string.Empty);
                        writer.WriteElementString("Confidence", result.Confidence.ToString());
                        writer.WriteElementString(
                            "ReadingQuality",
                            result.ReadingQuality.ToString(CultureInfo.InvariantCulture));

                        // Write the region (bounding rectangle) of the barcode
                        var rect = result.Region.Rectangle;
                        writer.WriteStartElement("Region");
                        writer.WriteElementString("X", rect.X.ToString(CultureInfo.InvariantCulture));
                        writer.WriteElementString("Y", rect.Y.ToString(CultureInfo.InvariantCulture));
                        writer.WriteElementString("Width", rect.Width.ToString(CultureInfo.InvariantCulture));
                        writer.WriteElementString("Height", rect.Height.ToString(CultureInfo.InvariantCulture));
                        writer.WriteEndElement(); // </Region>

                        writer.WriteEndElement(); // </BarCode>
                    }

                    writer.WriteEndElement(); // </Barcodes>
                    writer.WriteEndDocument(); // End of XML document
                }

                // Inform the user that processing for the current image is complete
                Console.WriteLine($"Processed '{imagePath}' -> '{xmlPath}'");
            }
        }
    }
}