using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "low_contrast.png";
        const string codeText = "LOWCONTRAST";

        // Generate a low‑contrast barcode (dark gray bars on light gray background)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Color.DarkGray;
            generator.Parameters.BackColor = Color.LightGray;
            generator.Save(imagePath);
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Read with minimal deconvolution (Fast)
        using (var readerFast = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            readerFast.QualitySettings = QualitySettings.NormalQuality;
            readerFast.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            foreach (var result in readerFast.ReadBarCodes())
            {
                Console.WriteLine("Fast Deconvolution:");
                Console.WriteLine($"  CodeText        : {result.CodeText}");
                Console.WriteLine($"  Confidence      : {result.Confidence}");
                Console.WriteLine($"  ReadingQuality  : {result.ReadingQuality}");
            }
        }

        // Read with stronger deconvolution (Slow)
        using (var readerSlow = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            readerSlow.QualitySettings = QualitySettings.NormalQuality;
            readerSlow.QualitySettings.Deconvolution = DeconvolutionMode.Slow;

            foreach (var result in readerSlow.ReadBarCodes())
            {
                Console.WriteLine("Slow Deconvolution:");
                Console.WriteLine($"  CodeText        : {result.CodeText}");
                Console.WriteLine($"  Confidence      : {result.Confidence}");
                Console.WriteLine($"  ReadingQuality  : {result.ReadingQuality}");
            }
        }
    }
}