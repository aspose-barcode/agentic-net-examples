using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output folders relative to the executable location
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputXml");
        string outputFolder = Path.Combine(baseDir, "OutputImages");

        // Ensure input folder exists; create if missing
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If there are no XML files, create a sample XML configuration
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputFolder, "sample.xml");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Optional: customize some parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 40f;
                // Export configuration to XML
                generator.ExportToXml(sampleXmlPath);
            }
            // Refresh file list after creating the sample
            xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        }

        // Process each XML file
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode settings from XML
                using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Determine output image path (same name, .png extension)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

                    // Save the generated barcode image
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode image: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }
    }
}