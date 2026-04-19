using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output folders (fallback to default locations)
        string inputFolder = args.Length > 0 ? args[0] : Path.Combine(Directory.GetCurrentDirectory(), "InputBarcodes");
        string outputFolder = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "OutputBarcodes");

        // Ensure the folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the input folder has no XML files, create a small sample XML using ExportToXml
        string[] existingXml = Directory.GetFiles(inputFolder, "*.xml");
        if (existingXml.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputFolder, "SampleBarcode.xml");
            using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export the generator settings to XML so ImportFromXml can read it later
                sampleGenerator.ExportToXml(sampleXmlPath);
            }
        }

        // Process each XML file in the input folder
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Import barcode settings from the XML file
                using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Build output image path (same name, .png extension)
                    string outputImagePath = Path.Combine(
                        outputFolder,
                        Path.GetFileNameWithoutExtension(xmlPath) + ".png");

                    // Save the generated barcode image
                    generator.Save(outputImagePath);
                    Console.WriteLine($"Generated barcode image: {outputImagePath}");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors gracefully and continue with the next file
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }
    }
}