using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define symbologies and sample texts
        var tests = new List<(BaseEncodeType encode, BaseDecodeType decode, string text)>
        {
            (EncodeTypes.Code128, DecodeType.Code128, "ABC123"),
            (EncodeTypes.QR, DecodeType.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, DecodeType.DataMatrix, "DM12345")
        };

        foreach (var (encode, decode, text) in tests)
        {
            // Grayscale barcode (black bars)
            using (var generatorGray = new BarcodeGenerator(encode, text))
            {
                generatorGray.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generatorGray.Parameters.BackColor = Aspose.Drawing.Color.White;

                using (var msGray = new MemoryStream())
                {
                    generatorGray.Save(msGray, BarCodeImageFormat.Png);
                    msGray.Position = 0;

                    using (var readerGray = new BarCodeReader(msGray, decode))
                    {
                        var resultGray = GetFirstResult(readerGray);
                        Console.WriteLine($"Symbology: {encode.GetType().Name} | Text: {text} | Mode: Grayscale | Confidence: {resultGray?.Confidence}");
                    }
                }
            }

            // Color barcode (blue bars)
            using (var generatorColor = new BarcodeGenerator(encode, text))
            {
                generatorColor.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generatorColor.Parameters.BackColor = Aspose.Drawing.Color.White;

                using (var msColor = new MemoryStream())
                {
                    generatorColor.Save(msColor, BarCodeImageFormat.Png);
                    msColor.Position = 0;

                    using (var readerColor = new BarCodeReader(msColor, decode))
                    {
                        var resultColor = GetFirstResult(readerColor);
                        Console.WriteLine($"Symbology: {encode.GetType().Name} | Text: {text} | Mode: Color | Confidence: {resultColor?.Confidence}");
                    }
                }
            }

            Console.WriteLine(new string('-', 80));
        }
    }

    // Helper to read the first barcode result, returns null if none found
    private static BarCodeResult GetFirstResult(BarCodeReader reader)
    {
        foreach (var result in reader.ReadBarCodes())
        {
            return result;
        }
        return null;
    }
}