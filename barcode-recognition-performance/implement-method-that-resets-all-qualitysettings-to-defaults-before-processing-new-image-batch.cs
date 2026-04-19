using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a folder with sample barcode images
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // Generate a few sample barcode images if the folder is empty
        if (Directory.GetFiles(inputFolder, "*.png").Length == 0)
        {
            GenerateSampleBarcodes(inputFolder);
        }

        // Process each image in the folder
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Create a reader for the current image
            using (BarCodeReader reader = new BarCodeReader())
            {
                // Set the decode types you want to recognize
                reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.QR);

                // Load the image
                reader.SetBarCodeImage(imagePath);

                // Reset quality settings to defaults before processing this image
                ResetQualitySettings(reader);

                // Read barcodes
                BarCodeResult[] results = reader.ReadBarCodes();
                Console.WriteLine($"Processing '{Path.GetFileName(imagePath)}' - found {results.Length} barcode(s):");
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }

    // Resets all QualitySettings of the reader to the default preset (NormalQuality)
    static void ResetQualitySettings(BarCodeReader reader)
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));
        reader.QualitySettings = QualitySettings.NormalQuality;
    }

    // Generates a few sample barcode images for demonstration purposes
    static void GenerateSampleBarcodes(string folder)
    {
        var samples = new[]
        {
            new { Type = EncodeTypes.Code128, Text = "ABC123", File = "code128.png" },
            new { Type = EncodeTypes.QR, Text = "https://example.com", File = "qr.png" },
            new { Type = EncodeTypes.EAN13, Text = "1234567890128", File = "ean13.png" }
        };

        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folder, sample.File);
            using (var generator = new BarcodeGenerator(sample.Type, sample.Text))
            {
                generator.Save(filePath);
            }
        }
    }
}