using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define output file
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode.png");

        // Create a Code128 barcode with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set bar width reduction to 5 points (approx. 5%)
            generator.Parameters.Barcode.BarWidthReduction.Point = 5f;

            // Simulate low‑resolution mobile scanner by lowering image resolution
            generator.Parameters.Resolution = 72; // DPI

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Verify the file was created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using low‑quality settings to mimic a low‑resolution scanner
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            // Set recognition quality to low
            reader.QualitySettings.BarcodeQuality = BarcodeQualityMode.Low;

            bool found = false;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected at low quality.");
            }
        }
    }
}