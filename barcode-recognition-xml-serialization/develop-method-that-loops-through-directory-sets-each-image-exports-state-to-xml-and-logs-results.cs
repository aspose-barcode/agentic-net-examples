using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define input and output directories
        string inputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Results");

        // Ensure directories exist
        if (!Directory.Exists(inputDir))
            Directory.CreateDirectory(inputDir);
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Seed a sample barcode image if the input folder is empty
        string[] existingFiles = Directory.GetFiles(inputDir);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputDir, "sample_code128.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(samplePath);
            }
            Console.WriteLine($"Sample barcode created at: {samplePath}");
        }

        // Process each image file in the input directory
        string[] imageFiles = Directory.GetFiles(inputDir, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string imagePath in imageFiles)
        {
            // Only process common image extensions
            string ext = Path.GetExtension(imagePath).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp")
                continue;

            Console.WriteLine($"Processing file: {Path.GetFileName(imagePath)}");

            // Initialize BarCodeReader for the current image
            using (var reader = new BarCodeReader(imagePath))
            {
                // Read all barcodes in the image
                var results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine("  No barcodes detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }

                // Export reader settings and state to XML
                string xmlFile = Path.Combine(outputDir, Path.GetFileNameWithoutExtension(imagePath) + ".xml");
                bool exported = reader.ExportToXml(xmlFile);
                Console.WriteLine(exported
                    ? $"  Exported XML to: {xmlFile}"
                    : $"  Failed to export XML for: {Path.GetFileName(imagePath)}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}