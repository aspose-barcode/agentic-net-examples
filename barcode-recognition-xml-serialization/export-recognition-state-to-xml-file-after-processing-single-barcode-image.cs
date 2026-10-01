// Title: Export Barcode Recognition State to XML
// Description: Generates a Code128 barcode, reads it using Aspose.BarCode, and exports the recognition results to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and System.Xml.Linq to serialize the recognition state. Typical use cases include automated testing, audit logging, and integration scenarios where barcode data must be persisted in a structured format. Developers often need to combine generation, decoding, and custom output handling in batch or CI pipelines.
// Prompt: Export the recognition state to an XML file after processing a single barcode image.
// Tags: barcode, code128, recognition, xml, export, aspose.barcode, generation, reading

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, recognizing it, and exporting the recognition state to an XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary barcode image, reads it, and writes the recognition results to XML.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the sample files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Paths for the generated barcode image and the output XML
        string barcodePath = Path.Combine(tempDir, "sample.png");
        string xmlPath = Path.Combine(tempDir, "recognition_state.xml");

        // Generate a sample barcode image (Code128)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode and collect recognition results
        BarCodeResult[] results;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            results = reader.ReadBarCodes();
        }

        // Build XML document representing the recognition state
        var doc = new XDocument(
            new XElement("RecognitionResult",
                new XAttribute("SourceFile", barcodePath),
                new XElement("Results",
                    // Create an element for each detected barcode
                    new Func<XElement[]>(() =>
                    {
                        var elems = new XElement[results.Length];
                        for (int i = 0; i < results.Length; i++)
                        {
                            var res = results[i];
                            elems[i] = new XElement("BarCode",
                                new XElement("CodeText", res.CodeText ?? string.Empty),
                                new XElement("CodeTypeName", res.CodeTypeName ?? string.Empty),
                                new XElement("ReadingQuality", res.ReadingQuality.ToString()),
                                new XElement("Extended",
                                    // Example of extended parameters (if any)
                                    new XElement("IsGS1", res.Extended?.GS1CompositeBar?.ToString() ?? "false")
                                )
                            );
                        }
                        return elems;
                    })()
                )
            )
        );

        // Save the XML to file
        doc.Save(xmlPath);

        Console.WriteLine($"Barcode image saved to: {barcodePath}");
        Console.WriteLine($"Recognition state exported to XML: {xmlPath}");
    }
}