using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input and output folders relative to the current directory
        string inputFolder = Path.Combine(Environment.CurrentDirectory, "InputBarcodes");
        string outputFolder = Path.Combine(Environment.CurrentDirectory, "OutputBarcodes");
        string logFile = Path.Combine(outputFolder, "error.log");

        // Ensure the directories exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Clear previous log file
        using (var logClear = new StreamWriter(logFile, false)) { }

        // If there are no XML files, create a sample configuration
        string[] existingXml = Directory.GetFiles(inputFolder, "*.xml");
        if (existingXml.Length == 0)
        {
            string sampleXml = Path.Combine(inputFolder, "Sample1.xml");
            using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export the configuration to XML
                sampleGenerator.ExportToXml(sampleXml);
            }
        }

        // Process each XML configuration file
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import generator settings from the XML file
                using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Determine output image path (same name, PNG format)
                    string outputImage = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(xmlPath) + ".png");

                    // Save the generated barcode image
                    generator.Save(outputImage);
                    Console.WriteLine($"Generated barcode: {outputImage}");
                }
            }
            catch (BarCodeException ex)
            {
                // Log Aspose.BarCode specific errors
                using (var log = new StreamWriter(logFile, true))
                {
                    log.WriteLine($"{DateTime.Now}: BarCodeException processing '{xmlPath}': {ex.Message}");
                }
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors
                using (var log = new StreamWriter(logFile, true))
                {
                    log.WriteLine($"{DateTime.Now}: Exception processing '{xmlPath}': {ex.Message}");
                }
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }
    }
}