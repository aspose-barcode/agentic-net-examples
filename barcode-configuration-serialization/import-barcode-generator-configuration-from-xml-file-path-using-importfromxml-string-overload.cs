using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define temporary XML configuration file path
        string xmlConfigPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.xml");
        // Define output image file path
        string outputImagePath = Path.Combine(Path.GetTempPath(), "importedBarcode.png");

        // Create a barcode generator, configure it, and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";
            // Export configuration to XML file
            generator.ExportToXml(xmlConfigPath);
        }

        // Import barcode generator configuration from the XML file
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlConfigPath))
        {
            // Save the barcode image using the imported settings
            importedGenerator.Save(outputImagePath);
        }

        // Inform the user where files are located
        Console.WriteLine("Configuration exported to: " + xmlConfigPath);
        Console.WriteLine("Barcode image generated at: " + outputImagePath);
    }
}