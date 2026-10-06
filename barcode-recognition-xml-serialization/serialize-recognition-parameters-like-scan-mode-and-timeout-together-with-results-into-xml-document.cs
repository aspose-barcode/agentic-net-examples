// Title: Serialize barcode recognition parameters and results to XML
// Description: Demonstrates generating a QR barcode, reading it with custom recognition settings, exporting those settings to XML, and appending the read results into the same XML document.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and BarCodeResult, illustrating typical workflows like barcode creation, customized scanning (e.g., strip FNC, XDimension, timeout), exporting reader state, and combining parameters with results for downstream processing or auditing. Developers working with barcode automation often need to persist both configuration and outcomes, making this pattern valuable for logging, reporting, or integration scenarios.
/// Prompt: Serialize recognition parameters like scan mode and timeout together with results into an XML document.
/// Tags: qr, barcode, serialization, xml, recognition, parameters, aspose.barcode, generation, reading

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation, customized recognition, and exporting both settings and results to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it with specific parameters, and saves an XML document containing both the parameters and the read results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the barcode image and XML outputs
        string imagePath = Path.Combine(tempDir, "barcode.png");
        string xmlPath = Path.Combine(tempDir, "readerState.xml");
        string finalXmlPath = Path.Combine(tempDir, "readerStateWithResults.xml");

        // -------------------------------------------------
        // Generate a simple QR barcode and save it as PNG
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Adjust the X-dimension (module size) for better readability
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Read the barcode with custom recognition settings
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Configure recognition parameters
            reader.BarcodeSettings.StripFNC = true;                 // Remove Function Code characters
            reader.QualitySettings.XDimension = XDimensionMode.Small; // Expect small modules
            reader.Timeout = 2000;                                 // Set timeout to 2000 ms

            BarCodeResult[] results;
            try
            {
                // Attempt to read barcodes using the configured settings
                results = reader.ReadBarCodes();
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle timeout or abort scenarios gracefully
                Console.WriteLine($"Recognition aborted after {ex.ExecutionTime} ms");
                results = new BarCodeResult[0];
            }

            // -------------------------------------------------
            // Export the current reader state (parameters) to XML
            // -------------------------------------------------
            reader.ExportToXml(xmlPath);

            // -------------------------------------------------
            // Load the exported XML and append recognition results
            // -------------------------------------------------
            var doc = XDocument.Load(xmlPath);
            var root = doc.Root ?? new XElement("BarCodeReaderState");
            var resultsElem = new XElement("Results");

            foreach (var res in results)
            {
                var resultElem = new XElement("Result",
                    new XElement("CodeText", res.CodeText),
                    new XElement("CodeTypeName", res.CodeTypeName),
                    new XElement("ReadingQuality", res.ReadingQuality),
                    new XElement("Region",
                        new XElement("X", res.Region.Rectangle.X),
                        new XElement("Y", res.Region.Rectangle.Y),
                        new XElement("Width", res.Region.Rectangle.Width),
                        new XElement("Height", res.Region.Rectangle.Height),
                        new XElement("Angle", res.Region.Angle)
                    )
                );
                resultsElem.Add(resultElem);
            }

            // Append the results element to the root and save the combined XML
            root.Add(resultsElem);
            doc.Save(finalXmlPath);
            Console.WriteLine($"XML with parameters and results saved to: {finalXmlPath}");
        }
    }
}