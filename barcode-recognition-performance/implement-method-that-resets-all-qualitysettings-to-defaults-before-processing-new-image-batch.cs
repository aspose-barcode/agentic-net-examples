using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace ResetQualitySettingsDemo
{
    class Program
    {
        // Resets the QualitySettings of a BarCodeReader to the default preset.
        static void ResetQualitySettings(BarCodeReader reader)
        {
            // The default preset is NormalQuality.
            reader.QualitySettings = QualitySettings.NormalQuality;
        }

        static void Main(string[] args)
        {
            // Prepare input and output folders.
            string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputImages");
            if (!Directory.Exists(inputFolder))
                Directory.CreateDirectory(inputFolder);

            // Generate sample barcode images if none exist.
            string[] sampleFiles = Directory.GetFiles(inputFolder, "*.png");
            if (sampleFiles.Length == 0)
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
                {
                    string filePath = Path.Combine(inputFolder, "sample1.png");
                    generator.Save(filePath);
                }

                using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC-987"))
                {
                    string filePath = Path.Combine(inputFolder, "sample2.png");
                    generator.Save(filePath);
                }
            }

            // Process each image in the batch.
            foreach (string imagePath in Directory.GetFiles(inputFolder, "*.png"))
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.Code128, DecodeType.Code39))
                {
                    // Reset quality settings before processing this image.
                    ResetQualitySettings(reader);

                    // Read barcodes.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
        }
    }
}