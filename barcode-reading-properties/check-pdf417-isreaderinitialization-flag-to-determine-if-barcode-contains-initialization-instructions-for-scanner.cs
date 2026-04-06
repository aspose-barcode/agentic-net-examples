using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "pdf417.png";

        // Create a PDF417 barcode with initialization flag set to true
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "INIT"))
        {
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = true;
            generator.Save(filePath);
        }

        // Read the barcode and check the IsReaderInitialization flag
        using (var reader = new BarCodeReader(filePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                bool isInit = result.Extended.Pdf417.IsReaderInitialization;
                Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                Console.WriteLine("Code Text: " + result.CodeText);
                Console.WriteLine("IsReaderInitialization flag: " + isInit);
            }
        }
    }
}