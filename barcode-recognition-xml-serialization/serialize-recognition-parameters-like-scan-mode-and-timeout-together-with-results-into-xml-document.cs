using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "barcode.png";

        // Generate a simple EAN13 barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            generator.Save(imagePath);
        }

        // Read the barcode with custom recognition parameters
        using (var reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            // Set recognition timeout (milliseconds)
            reader.Timeout = 3000;

            // Enable checksum validation during recognition
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Allow recognition of barcodes with incorrect checksums
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Export the current recognition settings to an XML file
            string settingsXmlPath = "recognitionSettings.xml";
            reader.ExportToXml(settingsXmlPath);

            // Perform barcode recognition
            var results = reader.ReadBarCodes();

            // Build an XML document that includes both settings reference and results
            var doc = new XDocument(
                new XElement("RecognitionResult",
                    new XElement("SettingsFile", settingsXmlPath),
                    new XElement("BarCodes",
                        from result in results
                        select new XElement("BarCode",
                            new XElement("Type", result.CodeTypeName),
                            new XElement("CodeText", result.CodeText),
                            new XElement("Confidence", result.Confidence),
                            new XElement("ReadingQuality", result.ReadingQuality),
                            // Include 1D extended parameters if available
                            result.Extended?.OneD != null
                                ? new XElement("OneDExtended",
                                    new XElement("Value", result.Extended.OneD.Value),
                                    new XElement("CheckSum", result.Extended.OneD.CheckSum))
                                : null
                        )
                    )
                )
            );

            // Save the combined XML document
            doc.Save("recognitionResult.xml");
        }

        // Optional cleanup of the generated image
        // File.Delete(imagePath);
    }
}