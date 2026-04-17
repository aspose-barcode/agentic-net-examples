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
        // Define input and output folders
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputXml");

        // Ensure folders exist
        if (!Directory.Exists(inputFolder))
            Directory.CreateDirectory(inputFolder);
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Seed sample barcode images if the input folder is empty (up to 5 samples)
        string[] existingImages = Directory.GetFiles(inputFolder, "*.png");
        if (existingImages.Length == 0)
        {
            for (int i = 1; i <= 5; i++)
            {
                string samplePath = Path.Combine(inputFolder, $"Sample{i}.png");
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
                {
                    generator.Save(samplePath);
                }
            }
        }

        // Process each image file
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Create a BarCodeReader for the image with common decode types
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128, DecodeType.QR, DecodeType.Code39, DecodeType.EAN13))
            {
                // Perform barcode detection
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output detected barcodes to console
                Console.WriteLine($"Processing '{Path.GetFileName(imagePath)}' - Found {results.Length} barcode(s):");
                foreach (var result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }

                // Export the reader state (including settings) to an XML file
                string xmlFileName = Path.GetFileNameWithoutExtension(imagePath) + ".xml";
                string xmlPath = Path.Combine(outputFolder, xmlFileName);
                bool exported = reader.ExportToXml(xmlPath);
                if (exported)
                {
                    Console.WriteLine($"  Exported state to XML: {xmlPath}");
                }
                else
                {
                    Console.WriteLine($"  Failed to export XML for {imagePath}");
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}