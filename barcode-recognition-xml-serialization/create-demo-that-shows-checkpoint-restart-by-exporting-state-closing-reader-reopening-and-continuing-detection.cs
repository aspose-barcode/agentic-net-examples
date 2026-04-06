using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        string imagePath = Path.Combine(Path.GetTempPath(), "demo_barcode.png");
        string statePath = Path.Combine(Path.GetTempPath(), "reader_state.xml");

        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            generator.Save(imagePath);
        }

        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"First read - Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            reader.ExportToXml(statePath);
        }

        using (BarCodeReader reader = BarCodeReader.ImportFromXml(statePath))
        {
            reader.SetBarCodeImage(imagePath);
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Second read - Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        try { File.Delete(imagePath); } catch { }
        try { File.Delete(statePath); } catch { }
    }
}