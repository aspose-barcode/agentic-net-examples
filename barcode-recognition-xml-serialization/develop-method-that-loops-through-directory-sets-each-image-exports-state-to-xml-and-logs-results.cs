using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputXml");

        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Seed a sample barcode image if the input folder is empty
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "Sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = "Sample123";
                generator.Save(samplePath);
            }
        }

        string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            string xmlPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(imagePath) + ".xml");
            bool exportSuccess = false;
            int foundCount = 0;

            using (var bitmap = new Bitmap(imagePath))
            using (var reader = new BarCodeReader())
            {
                reader.SetBarCodeImage(bitmap);
                reader.ReadBarCodes();
                foundCount = reader.FoundCount;
                exportSuccess = reader.ExportToXml(xmlPath);
            }

            Console.WriteLine($"Processed '{Path.GetFileName(imagePath)}' - Barcodes found: {foundCount}, Exported XML: {exportSuccess}");
        }

        Console.WriteLine("Processing completed.");
    }
}