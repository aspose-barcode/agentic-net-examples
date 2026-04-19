using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string originalText = "Unicode test: こんにちは世界 🌍";
        const string barcodeFile = "hanxin.png";

        if (File.Exists(barcodeFile))
        {
            File.Delete(barcodeFile);
        }

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.HanXin, originalText))
        {
            generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.ECI;
            generator.Parameters.Barcode.HanXin.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(barcodeFile);
        }

        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        using (BarCodeReader reader = new BarCodeReader(barcodeFile, DecodeType.HanXin))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            string decodedText = results[0].CodeText;

            if (decodedText == originalText)
            {
                Console.WriteLine("Success: Decoded text matches original.");
            }
            else
            {
                Console.WriteLine("Failure: Decoded text does not match.");
                Console.WriteLine("Original: " + originalText);
                Console.WriteLine("Decoded : " + decodedText);
            }
        }
    }
}