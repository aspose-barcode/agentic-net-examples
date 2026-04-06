using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the network share (replace with actual UNC path).
        // For a self‑contained demo we fall back to a local folder.
        string inputFolder = @"\\NetworkShare\Barcodes";

        // Ensure the folder exists; if not, create a local folder and seed a sample image.
        if (!Directory.Exists(inputFolder))
        {
            inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
            Directory.CreateDirectory(inputFolder);

            // Create a sample barcode image so the example can run end‑to‑end.
            string samplePath = Path.Combine(inputFolder, "sample.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(samplePath);
            }
        }

        // Retrieve all common image files from the folder.
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg" && ext != ".bmp" && ext != ".gif")
                continue; // Skip non‑image files.

            // Read barcodes from the image.
            using (var reader = new BarCodeReader(file))
            {
                // Use the default quality preset; can be changed if needed.
                reader.QualitySettings = QualitySettings.NormalQuality;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText: {result.CodeText}");
                    Console.WriteLine($"  Confidence: {result.Confidence}");
                    Console.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                }
            }
        }
    }
}