using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define symbologies and sample texts
        var symbologies = new (BaseEncodeType encode, string text)[]
        {
            (EncodeTypes.Code128, "CODE128TEST"),
            (EncodeTypes.QR, "QRTEST123"),
            (EncodeTypes.DataMatrix, "DMTEST"),
            (EncodeTypes.EAN13, "1234567890128")
        };

        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeSamples");
        Directory.CreateDirectory(outputDir);

        foreach (var (encode, text) in symbologies)
        {
            string baseFileName = $"{encode}_{text}";
            string colorPath = Path.Combine(outputDir, baseFileName + "_color.png");
            string grayPath = Path.Combine(outputDir, baseFileName + "_gray.png");

            // Generate and save color barcode image
            using (var generator = new BarcodeGenerator(encode, text))
            {
                generator.Save(colorPath);
            }

            // Load color image and create a grayscale version
            using (var colorBmp = new Bitmap(colorPath))
            using (var grayBmp = new Bitmap(colorBmp.Width, colorBmp.Height))
            {
                for (int y = 0; y < colorBmp.Height; y++)
                {
                    for (int x = 0; x < colorBmp.Width; x++)
                    {
                        var pixel = colorBmp.GetPixel(x, y);
                        int gray = (pixel.R + pixel.G + pixel.B) / 3;
                        var grayColor = Color.FromArgb(gray, gray, gray);
                        grayBmp.SetPixel(x, y, grayColor);
                    }
                }
                grayBmp.Save(grayPath);
            }

            // Recognize barcode from color image
            BarCodeResult colorResult = null;
            using (var reader = new BarCodeReader(colorPath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    colorResult = result;
                    break; // Expect single result
                }
            }

            // Recognize barcode from grayscale image
            BarCodeResult grayResult = null;
            using (var reader = new BarCodeReader(grayPath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    grayResult = result;
                    break; // Expect single result
                }
            }

            // Output comparison
            Console.WriteLine($"Symbology: {encode}");
            Console.WriteLine($"  Expected Text: {text}");
            if (colorResult != null)
            {
                Console.WriteLine($"  Color Image - Detected Text: {colorResult.CodeText}, Confidence: {colorResult.Confidence}");
            }
            else
            {
                Console.WriteLine("  Color Image - No barcode detected.");
            }

            if (grayResult != null)
            {
                Console.WriteLine($"  Grayscale Image - Detected Text: {grayResult.CodeText}, Confidence: {grayResult.Confidence}");
            }
            else
            {
                Console.WriteLine("  Grayscale Image - No barcode detected.");
            }

            Console.WriteLine();
        }
    }
}