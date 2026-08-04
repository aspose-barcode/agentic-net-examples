// Title: Batch Barcode Generation from XML
// Description: Demonstrates reading barcode specifications from an XML file, applying unit conversions, and saving PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and related parameter classes to create barcodes in bulk. Typical use cases include automated creation of product labels, inventory tags, or any scenario where barcode data is defined in external files. Developers often need to parse specifications, apply measurements, and export images in common formats.
// Prompt: Develop batch job reading barcode specs from XML, applying unit conversions, and saving PNGs to directory.
// Tags: barcode symbology, batch processing, png output, aspose.barcode, xml parsing

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Reads barcode specifications from an XML file, converts dimensions, and generates PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the batch barcode generation example.
    /// </summary>
    static void Main()
    {
        // Path to the XML file containing barcode specifications.
        const string xmlPath = "barcodespecs.xml";

        // Verify that the specification file exists before proceeding.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Specification file not found: {xmlPath}");
            return;
        }

        // Directory where generated PNG images will be saved.
        const string outputFolder = "OutputBarcodes";

        // Ensure the output directory exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Load the XML document containing barcode definitions.
        XDocument doc;
        using (FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
        {
            doc = XDocument.Load(fs);
        }

        // Conversion factor: 1 millimeter = 2.83465 points (Aspose uses points for dimensions).
        const float mmToPoints = 2.83465f;

        int index = 0;

        // Iterate over each <Barcode> element in the XML.
        foreach (XElement barcodeElem in doc.Root.Elements("Barcode"))
        {
            index++;

            // Extract required and optional values from the XML.
            string symbologyName = barcodeElem.Element("Symbology")?.Value?.Trim();
            string codeText = barcodeElem.Element("CodeText")?.Value?.Trim() ?? string.Empty;
            string xDimMmStr = barcodeElem.Element("XDimensionMm")?.Value?.Trim();

            // Validate that a symbology name is provided.
            if (string.IsNullOrEmpty(symbologyName))
            {
                Console.WriteLine($"Barcode #{index}: Symbology name missing, skipping.");
                continue;
            }

            // Resolve the symbology name to an EncodeTypes field using reflection.
            var fieldInfo = typeof(EncodeTypes).GetField(symbologyName);
            if (fieldInfo == null)
            {
                Console.WriteLine($"Barcode #{index}: Unknown symbology '{symbologyName}', skipping.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

            // Create a BarcodeGenerator instance with the resolved type and code text.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // If an XDimension value (in mm) is provided, convert it to points and apply.
                if (!string.IsNullOrEmpty(xDimMmStr) && float.TryParse(xDimMmStr, out float xDimMm))
                {
                    float xDimPoints = xDimMm * mmToPoints;
                    generator.Parameters.Barcode.XDimension.Point = xDimPoints;
                }

                // Optional: set the barcode foreground color to black.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Build the output file name and path.
                string fileName = $"{symbologyName}_{index}.png";
                string outPath = Path.Combine(outputFolder, fileName);

                // Save the generated barcode as a PNG image.
                generator.Save(outPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode #{index} saved to: {outPath}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}