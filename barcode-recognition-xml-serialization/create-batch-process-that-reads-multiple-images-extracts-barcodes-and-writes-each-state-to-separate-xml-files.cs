using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define input and output directories
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputXml");

        // Ensure directories exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If no images are present, create a sample barcode image
        string[] existingImages = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        if (existingImages.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(samplePath);
            }
            existingImages = new[] { samplePath };
        }

        // Process each image file
        foreach (string imagePath in existingImages)
        {
            // Initialize reader for the current image
            using (var reader = new BarCodeReader(imagePath))
            {
                // Perform barcode detection
                reader.ReadBarCodes();

                // Prepare XML output path (one XML per image)
                string xmlFileName = Path.GetFileNameWithoutExtension(imagePath) + ".xml";
                string xmlPath = Path.Combine(outputFolder, xmlFileName);

                // Export detection results and reader state to XML
                reader.ExportToXml(xmlPath);
            }
        }

        Console.WriteLine("Barcode extraction completed. XML files are located in: " + outputFolder);
    }
}