using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define the folder that contains images to be processed.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");

        // Ensure the folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // If the folder is empty, create a sample barcode image so the example can run end‑to‑end.
        string[] existingFiles = Directory.GetFiles(inputFolder);
        if (existingFiles.Length == 0)
        {
            string samplePath = Path.Combine(inputFolder, "SampleCode39.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "Sample123"))
            {
                // Save the generated barcode as PNG.
                generator.Save(samplePath, BarCodeImageFormat.Png);
            }
        }

        // Get all image files in the folder (common bitmap extensions).
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in imageFiles)
        {
            // Simple filter to process only supported image extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".tif" && ext != ".tiff")
                continue;

            // Verify the file still exists before processing.
            if (!File.Exists(filePath))
                continue;

            // Create a BarCodeReader for the current file, requesting all supported decode types.
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes found in the image.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }
    }
}