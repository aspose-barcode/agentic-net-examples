using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Paths for temporary image and final XML
        const string imagePath = "sample.png";
        const string xmlPath = "recognition_result.xml";

        // Ensure a sample barcode image exists
        if (!File.Exists(imagePath))
        {
            // Create a simple Code128 barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
            {
                generator.Save(imagePath);
            }
        }

        // Verify the image file before processing
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file '{imagePath}' not found.");
            return;
        }

        // Perform recognition and serialize parameters + results
        using (var reader = new BarCodeReader(imagePath))
        {
            // Set recognition parameters
            reader.Timeout = 5000; // milliseconds
            reader.QualitySettings = QualitySettings.NormalQuality;
            reader.BarcodeSettings.DetectEncoding = true;

            // Read barcodes
            BarCodeResult[] results = reader.ReadBarCodes();

            // Build XML document containing parameters and results
            var doc = new XDocument(
                new XElement("BarCodeRecognition",
                    new XElement("Parameters",
                        new XElement("Timeout", reader.Timeout),
                        new XElement("Quality", "NormalQuality"),
                        new XElement("DetectEncoding", reader.BarcodeSettings.DetectEncoding)
                    ),
                    new XElement("Results",
                        from r in results
                        select new XElement("Result",
                            new XElement("CodeType", r.CodeTypeName),
                            new XElement("CodeText", r.CodeText),
                            new XElement("Confidence", r.Confidence),
                            new XElement("ReadingQuality", r.ReadingQuality),
                            new XElement("Angle", r.Region.Angle)
                        )
                    )
                )
            );

            // Save XML to file
            doc.Save(xmlPath);
        }

        Console.WriteLine($"Recognition data saved to '{xmlPath}'.");
    }
}