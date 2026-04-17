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

        if (File.Exists(imagePath)) File.Delete(imagePath);
        if (File.Exists(statePath)) File.Delete(statePath);

        // 1. Generate a simple Code128 barcode and save it to a file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            generator.Save(imagePath);
        }

        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // 2. Create a reader, configure it, and export its state (checkpoint)
        using (BarCodeReader reader = new BarCodeReader())
        {
            reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128);
            reader.SetBarCodeImage(imagePath);
            reader.ExportToXml(statePath);
        }

        if (!File.Exists(statePath))
        {
            Console.WriteLine("Failed to export reader state.");
            return;
        }

        // 3. Reopen the reader from the saved state and continue detection
        using (BarCodeReader resumedReader = BarCodeReader.ImportFromXml(statePath))
        {
            resumedReader.SetBarCodeImage(imagePath);

            foreach (BarCodeResult result in resumedReader.ReadBarCodes())
            {
                Console.WriteLine("Detected Type : " + result.CodeTypeName);
                Console.WriteLine("Detected Text : " + result.CodeText);
            }
        }

        try { File.Delete(imagePath); } catch { }
        try { File.Delete(statePath); } catch { }
    }
}