// Title: Serialize barcode recognition parameters and results to XML
// Description: Demonstrates generating a QR barcode, recognizing it, and saving both recognition settings and results into an XML file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related classes to create barcodes, configure recognition parameters such as timeout and quality, and serialize the output. Developers often need to log or exchange barcode data with metadata, and this pattern provides a reusable approach for XML reporting.
// Prompt: Serialize recognition parameters like scan mode and timeout together with results into an XML document.
// Tags: qr, barcode, generation, recognition, xml, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, reads it back, and writes
/// both the recognition parameters and the results to an XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, and serializes
    /// the recognition data to XML.
    /// </summary>
    static void Main()
    {
        // Define file paths for the barcode image and the XML output.
        string barcodeImagePath = "barcode.png";
        string xmlOutputPath = "barcode_info.xml";

        // ------------------------------------------------------------
        // Generate a QR barcode and save it as a PNG image.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(barcodeImagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Set up a BarCodeReader to recognize the barcode from the image.
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(barcodeImagePath, DecodeType.AllSupportedTypes))
        {
            // Configure recognition parameters: timeout (milliseconds) and quality preset.
            reader.Timeout = 5000; // 5 seconds
            reader.QualitySettings = QualitySettings.HighQuality;

            // Perform the recognition and obtain all detected results.
            BarCodeResult[] results = reader.ReadBarCodes();

            // ------------------------------------------------------------
            // Build an XML document that includes both the parameters used
            // for recognition and the details of each recognized barcode.
            // ------------------------------------------------------------
            XDocument doc = new XDocument(
                new XElement("BarCodeInfo",
                    new XElement("RecognitionParameters",
                        new XElement("Timeout", reader.Timeout),
                        new XElement("QualityPreset", "HighQuality")
                    ),
                    new XElement("Results",
                        from result in results
                        select new XElement("Result",
                            new XElement("CodeText", result.CodeText ?? string.Empty),
                            new XElement("CodeType", result.CodeTypeName ?? string.Empty),
                            new XElement("ReadingQuality", result.ReadingQuality),
                            new XElement("Angle", result.Region.Angle),
                            new XElement("Region",
                                new XElement("X", result.Region.Rectangle.X),
                                new XElement("Y", result.Region.Rectangle.Y),
                                new XElement("Width", result.Region.Rectangle.Width),
                                new XElement("Height", result.Region.Rectangle.Height)
                            )
                        )
                    )
                )
            );

            // Save the constructed XML document to the specified file.
            doc.Save(xmlOutputPath);
            Console.WriteLine("Recognition data saved to: " + xmlOutputPath);
        }
    }
}