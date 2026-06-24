using System;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates barcode images based on an XML configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Converts a string containing a numeric value with an optional unit suffix
    /// (pt, in, mm) to points. 1 inch = 72 points, 1 mm = 72/25.4 points.
    /// </summary>
    /// <param name="valueWithUnit">The value string, e.g., "2in" or "5mm".</param>
    /// <returns>The value expressed in points.</returns>
    /// <exception cref="ArgumentException">Thrown when the input is null, empty, or cannot be parsed.</exception>
    static float ConvertToPoints(string valueWithUnit)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(valueWithUnit))
            throw new ArgumentException("Value cannot be null or empty.", nameof(valueWithUnit));

        // Normalise string for case‑insensitive comparison
        valueWithUnit = valueWithUnit.Trim().ToLowerInvariant();
        float multiplier = 1f; // default multiplier (points)

        // Determine multiplier based on unit suffix
        if (valueWithUnit.EndsWith("pt"))
        {
            multiplier = 1f; // points are the base unit
            valueWithUnit = valueWithUnit.Substring(0, valueWithUnit.Length - 2);
        }
        else if (valueWithUnit.EndsWith("in"))
        {
            multiplier = 72f; // inches to points
            valueWithUnit = valueWithUnit.Substring(0, valueWithUnit.Length - 2);
        }
        else if (valueWithUnit.EndsWith("mm"))
        {
            multiplier = 72f / 25.4f; // millimetres to points
            valueWithUnit = valueWithUnit.Substring(0, valueWithUnit.Length - 2);
        }
        else
        {
            // No recognised suffix – assume the value is already in points
            multiplier = 1f;
        }

        // Parse the numeric part using invariant culture
        if (!float.TryParse(valueWithUnit, NumberStyles.Float, CultureInfo.InvariantCulture, out float numeric))
            throw new ArgumentException($"Unable to parse numeric part of '{valueWithUnit}'.", nameof(valueWithUnit));

        return numeric * multiplier;
    }

    /// <summary>
    /// Entry point of the application. Reads barcode definitions from an XML file,
    /// generates corresponding PNG images, and saves them to an output folder.
    /// </summary>
    static void Main()
    {
        // Path to the input XML file (creates a sample if missing)
        string xmlPath = "barcodes.xml";
        if (!File.Exists(xmlPath))
        {
            // Build a simple sample XML document
            var sampleXml = new XDocument(
                new XElement("Barcodes",
                    new XElement("Barcode",
                        new XElement("Symbology", "Code128"),
                        new XElement("CodeText", "Sample123"),
                        new XElement("ImageWidth", "2in"),
                        new XElement("ImageHeight", "1in")
                    ),
                    new XElement("Barcode",
                        new XElement("Symbology", "QR"),
                        new XElement("CodeText", "https://example.com"),
                        new XElement("XDimension", "0.5mm")
                    )
                )
            );
            sampleXml.Save(xmlPath);
            Console.WriteLine($"Sample XML created at '{xmlPath}'.");
        }

        // Ensure the output directory exists
        string outputDir = "BarcodesOutput";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load the XML document
        XDocument doc;
        try
        {
            doc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load XML file: {ex.Message}");
            return;
        }

        // Retrieve all <Barcode> elements
        var barcodeElements = doc.Root?.Elements("Barcode");
        if (barcodeElements == null)
        {
            Console.WriteLine("No <Barcode> elements found in the XML.");
            return;
        }

        int index = 0;
        foreach (var elem in barcodeElements)
        {
            index++;

            // Extract required fields
            string symbologyName = elem.Element("Symbology")?.Value?.Trim();
            string codeText = elem.Element("CodeText")?.Value?.Trim();

            // Validate required data
            if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Skipping barcode #{index}: missing Symbology or CodeText.");
                continue;
            }

            // Resolve the symbology name to an EncodeTypes value via reflection
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{symbologyName}' for barcode #{index}.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            try
            {
                // Initialise the barcode generator with the resolved type and text
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Optional: set image width if specified
                    var imgWidthElem = elem.Element("ImageWidth");
                    if (imgWidthElem != null)
                    {
                        float widthPt = ConvertToPoints(imgWidthElem.Value);
                        generator.Parameters.ImageWidth.Point = widthPt;
                    }

                    // Optional: set image height if specified
                    var imgHeightElem = elem.Element("ImageHeight");
                    if (imgHeightElem != null)
                    {
                        float heightPt = ConvertToPoints(imgHeightElem.Value);
                        generator.Parameters.ImageHeight.Point = heightPt;
                    }

                    // Optional: set XDimension (module size) if specified
                    var xDimElem = elem.Element("XDimension");
                    if (xDimElem != null)
                    {
                        float xDimPt = ConvertToPoints(xDimElem.Value);
                        generator.Parameters.Barcode.XDimension.Point = xDimPt;
                    }

                    // Optional: set BarHeight if specified (requires AutoSizeMode.None)
                    var barHeightElem = elem.Element("BarHeight");
                    if (barHeightElem != null)
                    {
                        float barHeightPt = ConvertToPoints(barHeightElem.Value);
                        generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                        generator.Parameters.Barcode.BarHeight.Point = barHeightPt;
                    }

                    // Save the generated barcode as a PNG file
                    string fileName = $"{symbologyName}_{index}.png";
                    string outputPath = Path.Combine(outputDir, fileName);
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode #{index}: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode #{index}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}