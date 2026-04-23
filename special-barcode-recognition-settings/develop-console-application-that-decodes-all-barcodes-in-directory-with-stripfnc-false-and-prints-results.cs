using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define the folder that contains barcode images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");

        // Ensure the folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Get image files (common formats) from the folder
        string[] imageFiles = Directory.GetFiles(inputFolder)
            .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        // If the folder is empty, create a sample barcode image so the example runs end‑to‑end
        if (imageFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(samplePath);
            }
            imageFiles = new[] { samplePath };
        }

        // Process each image file
        foreach (string filePath in imageFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize the reader for all supported barcode types
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Ensure FNC characters are not stripped
                reader.BarcodeSettings.StripFNC = false;

                // Read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcodes detected in file: {Path.GetFileName(filePath)}");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}