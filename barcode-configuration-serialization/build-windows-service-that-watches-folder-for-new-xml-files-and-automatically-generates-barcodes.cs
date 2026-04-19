using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output folders
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputBarcodes");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputBarcodes");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample XML file if none exist
        string[] existingXml = Directory.GetFiles(inputFolder, "*.xml");
        if (existingXml.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputFolder, "SampleBarcode.xml");
            using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export generator settings to XML
                sampleGenerator.ExportToXml(sampleXmlPath);
            }
        }

        // Process each XML file in the input folder
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode settings from XML
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    if (generator == null)
                    {
                        Console.WriteLine($"Failed to import barcode from '{xmlPath}'.");
                        continue;
                    }

                    // Determine output image path
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the generated barcode image
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode saved to '{outputPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }
    }
}