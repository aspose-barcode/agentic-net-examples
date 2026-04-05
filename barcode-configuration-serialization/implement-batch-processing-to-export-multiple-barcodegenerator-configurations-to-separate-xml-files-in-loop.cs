using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define output folder for XML files
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodesXml");
        Directory.CreateDirectory(outputFolder);

        // Sample barcode configurations: Encode type, code text, output file name
        var configurations = new (BaseEncodeType type, string codeText, string fileName)[]
        {
            (EncodeTypes.Code128, "ABC123", "code128.xml"),
            (EncodeTypes.QR, "https://example.com", "qr.xml"),
            (EncodeTypes.EAN13, "123456789012", "ean13.xml")
        };

        foreach (var config in configurations)
        {
            // Create a barcode generator with the specified type and code text
            using (var generator = new BarcodeGenerator(config.type, config.codeText))
            {
                // Example of setting a size-related property using unit members
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;

                // Build full path for the XML file
                string xmlPath = Path.Combine(outputFolder, config.fileName);

                // Export the barcode configuration to XML
                bool success = generator.ExportToXml(xmlPath);
                Console.WriteLine($"Exported {config.fileName}: {(success ? "Success" : "Failed")}");
            }
        }

        Console.WriteLine("Batch export completed.");
    }
}