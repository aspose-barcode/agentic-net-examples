using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output folders
        string inputFolder = Path.Combine(Environment.CurrentDirectory, "BarcodesXml");
        string outputFolder = Path.Combine(Environment.CurrentDirectory, "BarcodesTiff");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample XML configuration if the input folder is empty
        string[] existingXml = Directory.GetFiles(inputFolder, "*.xml");
        if (existingXml.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputFolder, "Sample1.xml");
            using (var gen = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export the generator settings to an XML file
                gen.ExportToXml(sampleXmlPath);
            }
        }

        // Process each XML configuration file
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                // Import generator settings from XML
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    // Determine output TIFF file path
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlFile) + ".tif";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the barcode image as TIFF
                    generator.Save(outputPath, BarCodeImageFormat.Tiff);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{xmlFile}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch barcode generation completed.");
    }
}