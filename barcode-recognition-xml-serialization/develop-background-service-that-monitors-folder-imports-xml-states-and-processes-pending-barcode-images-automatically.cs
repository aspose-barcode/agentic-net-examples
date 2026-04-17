using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define input and output folders relative to the current directory
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // -----------------------------------------------------------------
        // Seed a sample barcode, export its image and XML settings if not present
        // -----------------------------------------------------------------
        string sampleXmlPath = Path.Combine(inputFolder, "Sample1.xml");
        string sampleImagePath = Path.Combine(inputFolder, "Sample1.png");

        if (!File.Exists(sampleXmlPath) || !File.Exists(sampleImagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample1"))
            {
                // Save the barcode image
                generator.Save(sampleImagePath);
                // Export the generator settings to XML
                generator.ExportToXml(sampleXmlPath);
            }
        }

        // -----------------------------------------------------------------
        // Process each XML file: import settings, generate barcode image, save it
        // -----------------------------------------------------------------
        foreach (string xmlFile in Directory.GetFiles(inputFolder, "*.xml"))
        {
            using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
            {
                string baseName = Path.GetFileNameWithoutExtension(xmlFile);
                string outImagePath = Path.Combine(outputFolder, baseName + "_Generated.png");
                generator.Save(outImagePath);
            }
        }

        // -----------------------------------------------------------------
        // Read generated barcode images and output detection results
        // -----------------------------------------------------------------
        foreach (string imageFile in Directory.GetFiles(outputFolder, "*.png"))
        {
            using (var reader = new BarCodeReader(imageFile, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Image: {Path.GetFileName(imageFile)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}