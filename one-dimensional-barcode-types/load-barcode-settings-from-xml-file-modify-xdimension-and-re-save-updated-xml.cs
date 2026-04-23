using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output XML file paths
        string inputXml = "barcodeSettings.xml";
        string outputXml = "barcodeSettings_updated.xml";

        // Verify that the input file exists
        if (!File.Exists(inputXml))
        {
            Console.WriteLine($"Input XML file not found: {inputXml}");
            return;
        }

        // Load barcode settings from the XML file
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(inputXml))
        {
            if (generator == null)
            {
                Console.WriteLine("Failed to import barcode settings from XML.");
                return;
            }

            // Modify the XDimension (using Point units as required)
            generator.Parameters.Barcode.XDimension.Point = 2f; // Set XDimension to 2 points

            // Save the updated settings back to a new XML file
            bool success = generator.ExportToXml(outputXml);
            Console.WriteLine(success
                ? $"Updated XML saved to: {outputXml}"
                : "Failed to export updated XML.");
        }
    }
}