// Title: Serialize barcode recognition parameters and results to XML
// Description: Demonstrates generating a barcode, recognizing it, and saving both recognition parameters and results into an XML file.
// Prompt: Serialize recognition parameters like scan mode and timeout together with results into an XML document.
// Tags: barcode, symbology, code128, recognition, xml, serialization, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode, reads it, and serializes
/// the recognition parameters and results into an XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, reads it, and writes recognition data to XML.
    /// </summary>
    static void Main()
    {
        // Paths for the generated barcode image and the output XML
        string imagePath = "barcode.png";
        string xmlPath = "barcode_results.xml";

        // ------------------------------------------------------------
        // Generate a sample barcode image using Code128 symbology
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Create a BarCodeReader to recognize the barcode from the image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Set recognition parameters
            reader.Timeout = 5000; // timeout in milliseconds
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Perform recognition and obtain all detected barcodes
            BarCodeResult[] results = reader.ReadBarCodes();

            // --------------------------------------------------------
            // Build XML document containing both parameters and results
            // --------------------------------------------------------
            var doc = new XDocument(
                new XElement("BarCodeRecognition",
                    new XElement("Parameters",
                        new XElement("Timeout", reader.Timeout),
                        new XElement("Deconvolution", reader.QualitySettings.Deconvolution.ToString())
                    ),
                    new XElement("Results",
                        from r in results
                        select new XElement("Result",
                            new XElement("CodeText", r.CodeText ?? string.Empty),
                            new XElement("CodeType", r.CodeTypeName ?? string.Empty),
                            new XElement("ReadingQuality", r.ReadingQuality),
                            new XElement("Angle", r.Region.Angle),
                            new XElement("Region",
                                new XElement("X", r.Region.Rectangle.X),
                                new XElement("Y", r.Region.Rectangle.Y),
                                new XElement("Width", r.Region.Rectangle.Width),
                                new XElement("Height", r.Region.Rectangle.Height)
                            )
                        )
                    )
                )
            );

            // Save the XML document to the specified file
            doc.Save(xmlPath);
        }

        Console.WriteLine($"Recognition data saved to '{xmlPath}'.");
    }
}