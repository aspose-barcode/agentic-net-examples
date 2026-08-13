// Title: QR Code Reading Quality under Different Lighting Conditions
// Description: Demonstrates generating QR codes with varying background colors to simulate lighting and compares their recognition quality using ReadingQuality values.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes and BarCodeReader with QualitySettings for evaluating detection performance. Developers often need to assess how environmental factors like lighting affect barcode readability, making ReadingQuality a valuable metric for quality assurance and image preprocessing pipelines.
// Prompt: Compare recognition quality of QR codes captured under different lighting conditions by analyzing ReadingQuality values.
// Tags: qr, lighting, readingquality, barcode, generation, recognition, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates QR code images with different simulated lighting conditions and evaluates their
/// recognition quality using the ReadingQuality property provided by Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates QR codes with bright and dim backgrounds,
    /// saves them, and then reads each image to output the ReadingQuality metric.
    /// </summary>
    static void Main()
    {
        // Define the directory where generated images will be stored.
        string outputDir = "output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Text to encode in the QR code.
        const string qrText = "Lighting Test QR";

        // Define lighting scenarios: a name and the background color that simulates the lighting.
        var conditions = new (string Name, Color Background)[]
        {
            ("Bright", Color.White),      // Simulates well‑lit environment.
            ("Dim", Color.LightGray)      // Simulates low‑light environment.
        };

        // --------------------------------------------------------------------
        // Generate QR code images for each lighting condition.
        // --------------------------------------------------------------------
        foreach (var condition in conditions)
        {
            string filePath = Path.Combine(outputDir, $"{condition.Name}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Apply the background color to mimic the lighting condition.
                generator.Parameters.BackColor = condition.Background;

                // Save the generated QR code as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Recognize each generated image and output its ReadingQuality value.
        // --------------------------------------------------------------------
        foreach (var condition in conditions)
        {
            string filePath = Path.Combine(outputDir, $"{condition.Name}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                // Use high‑quality settings to improve detection accuracy.
                reader.QualitySettings = QualitySettings.HighQuality;

                // Iterate through all detected barcodes (should be one per image).
                foreach (var result in reader.ReadBarCodes())
                {
                    double quality = result.ReadingQuality;
                    Console.WriteLine($"{condition.Name} lighting - ReadingQuality: {quality}");
                }
            }
        }
    }
}