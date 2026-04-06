using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string inputFolder = "BarcodesXml";
        string outputFolder = "BarcodesImages";

        // Ensure the input folder exists; create it and a sample XML if missing.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            string sampleXmlPath = Path.Combine(inputFolder, "sample.xml");
            using (var gen = new BarcodeGenerator(EncodeTypes.Code128))
            {
                gen.CodeText = "Demo123";
                gen.Parameters.Barcode.XDimension.Point = 2f;
                gen.Parameters.Barcode.BarHeight.Point = 40f;
                gen.ExportToXml(sampleXmlPath);
            }
        }

        // Ensure the output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each XML file in the input folder.
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            // Import barcode settings from the XML file.
            using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
            {
                if (generator == null)
                {
                    Console.WriteLine($"Failed to import XML: {xmlFile}");
                    continue;
                }

                // Determine the output image path.
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlFile);
                string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

                // Generate and save the barcode image.
                generator.Save(outputPath);
                Console.WriteLine($"Generated barcode saved to: {outputPath}");

                // Optional: recognize the generated barcode to verify.
                using (var reader = new BarCodeReader(outputPath))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Recognized: Type={result.CodeTypeName}, Text={result.CodeText}");
                    }
                }
            }
        }

        Console.WriteLine("Processing complete.");
    }
}