using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Input and output XML file paths
        const string inputXml = "barcodeConfig.xml";
        const string outputXml = "barcodeConfig_modified.xml";

        // Verify that the input file exists
        if (!File.Exists(inputXml))
        {
            Console.WriteLine($"Input file not found: {inputXml}");
            return;
        }

        // Load the barcode configuration from XML, modify the foreground color, and export it back
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(inputXml))
        {
            // Change the barcode foreground (bars) color
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;

            // Export the modified configuration to a new XML file
            bool success = generator.ExportToXml(outputXml);
            Console.WriteLine(success
                ? $"Successfully exported modified configuration to {outputXml}"
                : $"Failed to export modified configuration to {outputXml}");
        }
    }
}