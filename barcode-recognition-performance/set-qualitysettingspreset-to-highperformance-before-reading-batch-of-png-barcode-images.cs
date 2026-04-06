using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define the folder that will contain barcode images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Create a few sample barcode images so the example can run end‑to‑end
            CreateSampleBarcode(Path.Combine(inputFolder, "code1.png"), "ABC123");
            CreateSampleBarcode(Path.Combine(inputFolder, "code2.png"), "XYZ789");
        }

        // Get all PNG files in the folder
        string[] pngFiles = Directory.GetFiles(inputFolder, "*.png");
        if (pngFiles.Length == 0)
        {
            Console.WriteLine("No PNG barcode images found in the folder.");
            return;
        }

        // Process each image with HighPerformance quality preset
        foreach (string filePath in pngFiles)
        {
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128, DecodeType.Code39, DecodeType.QR))
            {
                // Set the recognition quality to HighPerformance
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Read and output all detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }

    // Helper method to generate a simple barcode image
    private static void CreateSampleBarcode(string filePath, string codeText)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Save the generated barcode as PNG
            generator.Save(filePath);
        }
    }
}