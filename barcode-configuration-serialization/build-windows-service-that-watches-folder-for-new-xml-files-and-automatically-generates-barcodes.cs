using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define folders
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputXml");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure input folder exists
        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Create a sample XML file if none exist
        string sampleXmlPath = Path.Combine(inputFolder, "sample.xml");
        if (!File.Exists(sampleXmlPath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "12345";
                // Export the barcode settings to XML
                generator.ExportToXml(sampleXmlPath);
            }
        }

        // Process each XML file in the input folder
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            // Import barcode settings from XML
            using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
            {
                // Determine output image path
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlFile);
                string outputImagePath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

                // Save the generated barcode image
                generator.Save(outputImagePath);
                Console.WriteLine($"Generated barcode saved to: {outputImagePath}");
            }
        }
    }
}