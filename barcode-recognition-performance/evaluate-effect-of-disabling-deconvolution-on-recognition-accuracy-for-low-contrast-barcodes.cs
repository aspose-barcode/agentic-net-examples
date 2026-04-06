using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Create a low‑contrast Code128 barcode (bars and background colors are similar)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "LOWCONTRAST"))
        {
            // Set bar color to a medium gray
            generator.Parameters.Barcode.BarColor = Color.FromArgb(150, 150, 150);
            // Set background color to a slightly lighter gray
            generator.Parameters.BackColor = Color.FromArgb(200, 200, 200);
            // Keep image size reasonable
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 100f;
            generator.Save("lowcontrast.png");
        }

        // Recognize the barcode with default deconvolution settings
        using (var reader = new BarCodeReader("lowcontrast.png", DecodeType.Code128))
        {
            Console.WriteLine("=== Recognition with default deconvolution (Normal) ===");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"CodeText : {result.CodeText}");
            }

            // Disable (minimize) deconvolution by setting it to Fast mode
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            Console.WriteLine("\n=== Recognition with deconvolution set to Fast (minimal) ===");
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"CodeText : {result.CodeText}");
            }
        }
    }
}