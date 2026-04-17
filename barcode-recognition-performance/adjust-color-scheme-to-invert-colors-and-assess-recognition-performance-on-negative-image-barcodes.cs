using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string normalPath = "barcode_normal.png";
        const string invertedPath = "barcode_inverted.png";
        const string codeText = "Test12345";

        // Generate normal barcode (black on white)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(normalPath);
        }

        // Generate inverted barcode (white on black)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.White;
            generator.Parameters.BackColor = Aspose.Drawing.Color.Black;
            generator.Save(invertedPath);
        }

        // Verify files exist before recognition
        if (!File.Exists(normalPath) || !File.Exists(invertedPath))
        {
            Console.WriteLine("One or more barcode images were not created.");
            return;
        }

        // Recognize normal barcode
        Console.WriteLine("Recognizing normal barcode:");
        using (var reader = new BarCodeReader(normalPath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  Confidence: {result.Confidence}");
            }
        }

        // Recognize inverted barcode with InverseImage mode enabled
        Console.WriteLine("\nRecognizing inverted barcode with InverseImage mode:");
        using (var reader = new BarCodeReader(invertedPath, DecodeType.Code128))
        {
            reader.QualitySettings.InverseImage = InverseImageMode.Enabled;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
                Console.WriteLine($"  Confidence: {result.Confidence}");
            }
        }
    }
}