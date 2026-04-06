using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
class Program
{
    static void Main()
    {
        // Generate a QR code image.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "LightingTest"))
        {
            generator.Save("qr.png");
        }

        // Analyze reading quality with different recognition presets.
        AnalyzeReadingQuality("NormalQuality", QualitySettings.NormalQuality);
        AnalyzeReadingQuality("HighQuality", QualitySettings.HighQuality);
        AnalyzeReadingQuality("HighPerformance", QualitySettings.HighPerformance);
    }

    static void AnalyzeReadingQuality(string presetName, QualitySettings preset)
    {
        using (var reader = new BarCodeReader("qr.png", DecodeType.QR))
        {
            // Apply the specified quality preset.
            reader.QualitySettings = preset;

            foreach (var result in reader.ReadBarCodes())
            {
                // ReadingQuality is a double representing the percentage of quality.
                Console.WriteLine($"{presetName} ReadingQuality: {result.ReadingQuality}");
            }
        }
    }
}