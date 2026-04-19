using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string xmlFile = "barcodeConfig.xml";
        const string outputFile = "generated.png";

        // Verify that the XML configuration file exists
        if (!File.Exists(xmlFile))
        {
            Console.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        try
        {
            // Import barcode settings from the XML file
            using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlFile))
            {
                // Basic validation of required properties
                if (generator == null)
                {
                    throw new ArgumentException("Failed to import barcode configuration.");
                }

                // Barcode type must be specified
                if (generator.BarcodeType == null)
                {
                    throw new ArgumentException("Barcode type is missing in the XML configuration.");
                }

                // CodeText must be provided
                if (string.IsNullOrWhiteSpace(generator.CodeText))
                {
                    throw new ArgumentException("CodeText is missing or empty in the XML configuration.");
                }

                // Save the generated barcode image
                generator.Save(outputFile);
                Console.WriteLine($"Barcode generated and saved to {outputFile}");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Configuration error: {ex.Message}");
        }
        catch (BarCodeException ex)
        {
            Console.WriteLine($"Barcode generation error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}