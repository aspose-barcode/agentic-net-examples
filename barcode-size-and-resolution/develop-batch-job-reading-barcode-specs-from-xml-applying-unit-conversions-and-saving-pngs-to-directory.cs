// Title: Batch barcode generation from XML specifications
// Description: Demonstrates reading barcode definitions from an XML file, converting dimensions, and generating PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to parse external data (XML), map symbology names to EncodeTypes, apply unit conversions, and save barcodes as PNG files. It highlights key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use for automated batch barcode creation in reporting, inventory, or labeling workflows.
// Prompt: Develop batch job reading barcode specs from XML, applying unit conversions, and saving PNGs to directory.
// Tags: barcode, batch, xml, png, aspose.barcode, generation, encode-types, xdimension

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that reads barcode specifications from an XML file,
/// applies unit conversions, and generates PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the batch processing of barcode specifications.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder for the batch job
        string workFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Prepare a sample XML file containing barcode specifications
        string xmlPath = Path.Combine(workFolder, "specs.xml");
        string sampleXml = @"
<Barcodes>
    <Barcode>
        <Symbology>Code128</Symbology>
        <CodeText>ABC123</CodeText>
        <XDimensionMm>0.5</XDimensionMm>
    </Barcode>
    <Barcode>
        <Symbology>QR</Symbology>
        <CodeText>https://example.com</CodeText>
        <XDimensionMm>0.3</XDimensionMm>
    </Barcode>
    <Barcode>
        <Symbology>DataMatrix</Symbology>
        <CodeText>DM001</CodeText>
        <XDimensionMm>0.4</XDimensionMm>
    </Barcode>
</Barcodes>";
        File.WriteAllText(xmlPath, sampleXml);

        // Load and parse the XML document
        XDocument doc = XDocument.Load(xmlPath);
        var barcodeElements = doc.Root?.Elements("Barcode") ?? Enumerable.Empty<XElement>();

        // Create an output folder for the generated PNG images
        string outputFolder = Path.Combine(workFolder, "Output");
        Directory.CreateDirectory(outputFolder);

        int index = 1;
        foreach (var elem in barcodeElements)
        {
            // Extract required fields from the XML element
            string symbologyName = elem.Element("Symbology")?.Value?.Trim() ?? "";
            string codeText = elem.Element("CodeText")?.Value?.Trim() ?? "";
            string xDimStr = elem.Element("XDimensionMm")?.Value?.Trim() ?? "";

            // Validate that all necessary data is present
            if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText) || string.IsNullOrEmpty(xDimStr))
            {
                Console.WriteLine($"Skipping incomplete specification at index {index}.");
                index++;
                continue;
            }

            // Resolve the symbology name to a BaseEncodeType using reflection
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{symbologyName}' at index {index}. Skipping.");
                index++;
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Parse the XDimension value (in millimeters) and ensure it is positive
            if (!float.TryParse(xDimStr, System.Globalization.CultureInfo.InvariantCulture, out float xDimMm) || xDimMm <= 0f)
            {
                Console.WriteLine($"Invalid XDimension '{xDimStr}' at index {index}. Skipping.");
                index++;
                continue;
            }

            // Generate the barcode image with the specified parameters
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Parameters.Barcode.XDimension.Millimeters = xDimMm;

                string fileName = $"barcode_{index}.png";
                string filePath = Path.Combine(outputFolder, fileName);
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved {filePath}");
            }

            index++;
        }

        Console.WriteLine("Batch processing completed.");
    }
}