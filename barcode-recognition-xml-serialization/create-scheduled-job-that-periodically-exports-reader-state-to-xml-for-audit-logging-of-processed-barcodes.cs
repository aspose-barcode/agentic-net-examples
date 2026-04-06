using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file paths
        string barcodePath = "sample_barcode.png";
        string auditXmlPath = "reader_audit.xml";

        // Ensure any existing files are removed for a clean run
        if (File.Exists(barcodePath)) File.Delete(barcodePath);
        if (File.Exists(auditXmlPath)) File.Delete(auditXmlPath);

        // Generate a simple Code128 barcode and save it to a file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "ABC123456";
            generator.Save(barcodePath);
        }

        // Read the generated barcode, output details, and export reader state to XML
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Region Angle: {result.Region.Angle}");
            }

            // Export the reader's internal state to an XML file for audit logging
            bool exportSuccess = reader.ExportToXml(auditXmlPath);
            Console.WriteLine(exportSuccess
                ? $"Reader state exported successfully to '{auditXmlPath}'."
                : $"Failed to export reader state to '{auditXmlPath}'.");
        }
    }
}