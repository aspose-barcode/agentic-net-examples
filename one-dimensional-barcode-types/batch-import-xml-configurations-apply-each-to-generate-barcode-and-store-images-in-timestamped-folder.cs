using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output folders
        string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BarcodesXml");
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BarcodesOutput",
            DateTime.Now.ToString("yyyyMMdd_HHmmss"));

        // Ensure input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample XML configuration if the folder is empty
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputFolder, "Sample1.xml");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export the configuration to XML
                generator.ExportToXml(sampleXmlPath);
            }
            xmlFiles = new[] { sampleXmlPath };
        }

        // Process each XML configuration
        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"File not found: {xmlPath}");
                continue;
            }

            // Import barcode settings from XML and generate the image
            using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Determine output image file name
                string imageFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                string imagePath = Path.Combine(outputFolder, imageFileName);

                // Save the generated barcode image
                generator.Save(imagePath);
                Console.WriteLine($"Generated barcode saved to: {imagePath}");
            }
        }
    }
}