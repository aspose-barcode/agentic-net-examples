using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare sample data
        string[] oneDTexts = { "1234567890", "ABCDEFGHIJ", "9876543210", "CODE128TEST", "0011223344" };
        string[] twoDTexts = { "https://example.com/1", "https://example.com/2", "https://example.com/3", "https://example.com/4", "https://example.com/5" };

        // Folder for temporary images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // Generate 1D barcodes (Code128)
        for (int i = 0; i < oneDTexts.Length; i++)
        {
            string filePath = Path.Combine(outputFolder, $"code128_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, oneDTexts[i]))
            {
                // Ensure consistent image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;
                generator.Save(filePath);
            }
        }

        // Generate 2D barcodes (QR)
        for (int i = 0; i < twoDTexts.Length; i++)
        {
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, twoDTexts[i]))
            {
                // Ensure consistent image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;
                generator.Save(filePath);
            }
        }

        // Measure recognition speed for 1D barcodes
        Stopwatch sw1D = Stopwatch.StartNew();
        for (int i = 0; i < oneDTexts.Length; i++)
        {
            string filePath = Path.Combine(outputFolder, $"code128_{i}.png");
            using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
            {
                reader.QualitySettings = QualitySettings.HighPerformance;
                foreach (var result in reader.ReadBarCodes())
                {
                    // No output needed; just force recognition
                }
            }
        }
        sw1D.Stop();

        // Measure recognition speed for 2D barcodes
        Stopwatch sw2D = Stopwatch.StartNew();
        for (int i = 0; i < twoDTexts.Length; i++)
        {
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");
            using (var reader = new BarCodeReader(filePath, DecodeType.QR))
            {
                reader.QualitySettings = QualitySettings.HighPerformance;
                foreach (var result in reader.ReadBarCodes())
                {
                    // No output needed; just force recognition
                }
            }
        }
        sw2D.Stop();

        // Output comparison
        Console.WriteLine($"1D (Code128) total recognition time for {oneDTexts.Length} items: {sw1D.ElapsedMilliseconds} ms");
        Console.WriteLine($"2D (QR) total recognition time for {twoDTexts.Length} items: {sw2D.ElapsedMilliseconds} ms");
        Console.WriteLine($"Average per barcode - 1D: {sw1D.ElapsedMilliseconds / (double)oneDTexts.Length:F2} ms, 2D: {sw2D.ElapsedMilliseconds / (double)twoDTexts.Length:F2} ms");
    }
}