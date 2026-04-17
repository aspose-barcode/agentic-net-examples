using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the XML configuration file
        const string xmlPath = "barcodeConfig.xml";

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Configuration file not found: {xmlPath}");
            return;
        }

        // Import the barcode generator settings from the XML file
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the generated barcode image to a file
            const string outputPath = "generatedBarcode.png";
            generator.Save(outputPath);
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}