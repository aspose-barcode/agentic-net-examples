using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates importing barcode settings from an XML configuration file and generating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Handles XML configuration loading, barcode generation, and error fallback.
    /// </summary>
    static void Main()
    {
        // Paths for configuration and output
        string xmlPath = "barcodeConfig.xml";
        string outputPath = "output.png";

        // Verify that the XML configuration file exists; create a sample if missing
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML configuration file not found: {xmlPath}");

            // Sample XML content with basic barcode settings
            string sampleXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <EncodeType>Code128</EncodeType>
  <CodeText>123456</CodeText>
</BarcodeGenerator>";

            // Write the sample XML to disk
            File.WriteAllText(xmlPath, sampleXml);
            Console.WriteLine($"Sample XML created at {xmlPath}");
        }

        try
        {
            // Import barcode settings from the XML file
            using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Save the generated barcode image to the specified path
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Specific handling when the XML expects an image set via SetBarCodeImage
            if (ex.Message.Contains("SetBarCodeImage", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Error: The XML configuration expects a barcode image but none was provided via SetBarCodeImage.");

                // Attempt to generate a simple fallback barcode
                try
                {
                    using (var fallbackGen = new BarcodeGenerator(EncodeTypes.Code128, "Fallback123"))
                    {
                        fallbackGen.Save(outputPath);
                        Console.WriteLine($"Fallback barcode saved to {outputPath}");
                    }
                }
                catch (Exception fallbackEx)
                {
                    Console.WriteLine($"Fallback generation failed: {fallbackEx.Message}");
                }
            }
            else
            {
                // General import failure
                Console.WriteLine($"ImportFromXml failed: {ex.Message}");
            }
        }
    }
}